using HRApplication_Wpf.Commands;
using HRApplication_Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace HRApplication_Wpf.Models
{
    internal class ConnectionPathViewModel : ViewModelBase
    {
        private ConnectionSettings _connectionSettings;
        private readonly bool _canCloseWindow;

        public ConnectionPathViewModel(bool CanCloseWindow)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            _connectionSettings = new ConnectionSettings();
            _canCloseWindow = CanCloseWindow;
        }


        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        public ConnectionSettings ConnectionSettings
        {
            get { return _connectionSettings; }
            set
            {
                _connectionSettings = value;
                OnPropertyChanged();
            }
        }


        private void Confirm(object obj)
        {
            if (!ConnectionSettings.IsValid)
                return;

            ConnectionSettings.Save();
            RestarApplication();
        }

        private void RestarApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Close(object obj)
        {
            if (_canCloseWindow)
                CloseWindow(obj as Window);

            else
                Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
