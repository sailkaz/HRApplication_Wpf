using HRApplication_Wpf.Commands;
using HRApplication_Wpf.Models;
using System.Windows;
using System.Windows.Input;

namespace HRApplication_Wpf.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

        }


        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private string _nickname;

        public string Nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
                OnPropertyChanged();
            }
        }

        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }


        private void Confirm(object obj)
        {
            var loginParams = obj as PasswordBoxParams;
            var userPassword = loginParams.PasswordBox.Password;
            if (IsLoginDataValid(userPassword))
                CloseWindow(loginParams.Window);
            else
            {
                MessageBox.Show("Błędne hasło i(lub) nazwa użytkownika.");
                
            }
                
        }

        private bool IsLoginDataValid(string userPassword)
        {
            if (Users.UserName == Nickname && Users.Password == userPassword)
                return true;
            else
                return false;
        }

        private void Cancel(object obj)
        {
            Application.Current.Shutdown();
        }
        private void CloseWindow(Window window)
        {
            window.Close();
        }

    }
}
