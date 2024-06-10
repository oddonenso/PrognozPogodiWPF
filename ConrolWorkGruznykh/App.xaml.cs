using ConrolWorkGruznykh.View;
using System.Windows;

namespace ConrolWorkGruznykh
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
