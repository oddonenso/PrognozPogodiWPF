using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConrolWorkGruznykh.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async _ => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            var data = new Dictionary<string, string>
            {
                { "action", "login" },
                { "login", Login },
                { "password", Password }
            };

            var json = JsonConvert.SerializeObject(data);
            var response = await SendRequestAsync(json);
            // Обработка ответа (например, отображение сообщения пользователю)
        }

        private async Task<string> SendRequestAsync(string request)
        {
            using (var client = new TcpClient("127.0.0.1", 5000))
            {
                var stream = client.GetStream();
                var requestBytes = Encoding.UTF8.GetBytes(request);
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
