using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BD;
using BD.Table;
using Newtonsoft.Json;


namespace SuperServer
{
    public class Servak
    {
        private TcpListener _listener;

        public Servak()
        {
            _listener = new TcpListener(IPAddress.Any, 5000);
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("Сервер запущен...");

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Клиент подключен...");
            var stream = client.GetStream();
            var buffer = new byte[1024];
            var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            var response = HandleRequest(request);
            var responseBytes = Encoding.UTF8.GetBytes(response);

            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            client.Close();
        }

        private string HandleRequest(string request)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var action = data["action"];

            if (action == "register")
            {
                var login = data["login"];
                var password = data["password"];
                var email = data["email"];
                return RegisterUser(login, password, email);
            }
            else if (action == "login")
            {
                var login = data["login"];
                var password = data["password"];
                return LoginUser(login, password);
            }
            return "Invalid action";
        }

        private string RegisterUser(string login, string password, string email)
        {
            using (var context = new Connection())
            {
                if (context.Clients.Any(u => u.Login == login))
                {
                    return "Пользователь уже существует";
                }

                var user = new Client { Login = login, Password = password, Email = email };
                context.Clients.Add(user);
                context.SaveChanges();
            }
            return "Регистрация успешна";
        }

        private string LoginUser(string login, string password)
        {
            using (var context = new Connection())
            {
                var user = context.Clients.SingleOrDefault(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    return "Неверный логин или пароль";
                }
            }
            return "Вход успешен";
        }
    }
}
