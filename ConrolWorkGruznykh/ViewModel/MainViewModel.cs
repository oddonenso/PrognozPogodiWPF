using ConrolWorkGruznykh.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConrolWorkGruznykh.ViewModel
{
    public class MainViewModel
    {
        public ICommand OpenLoginCommand { get; }
        public ICommand OpenRegisterCommand { get; }

        public MainViewModel()
        {
            OpenLoginCommand = new Command(OpenLogin);
            OpenRegisterCommand = new Command(OpenRegister);
        }

        private void OpenLogin(object obj)
        {
            var loginView = new LoginView();
            loginView.Show();
        }

        private void OpenRegister(object obj)
        {
            var registerView = new RegisterView();
            registerView.Show();
        }
    }
}
