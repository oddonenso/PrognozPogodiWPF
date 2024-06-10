using SuperServer;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new Servak();
        await server.StartAsync();
    }
}