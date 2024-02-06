using HRApplication_Wpf.Commands;
using HRApplication_Wpf.Models.Domains;
using HRApplication_Wpf.Models.Wrappers;
using HRApplication_Wpf.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRApplication_Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public MainViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee);
            DismissEmployeeCommand = new AsyncRelayCommand(DismissEmployee, CanDismissEmployee);
            EmployeeTypeSelectionCommand = new RelayCommand(ChangeEmployeeTypeSelection);
            SetConnectionPathCommand = new RelayCommand(SetConnectionPath);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            var loginWindow = new LoginView();
            loginWindow.ShowDialog();

            LoadedWindow(null);
          
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DismissEmployeeCommand { get; set; }
        public ICommand EmployeeTypeSelectionCommand { get; set; }
        public ICommand SetConnectionPathCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        private ObservableCollection<EmployeeWrapper> _employees;

        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        private EmployeeWrapper _selectedEmployee;

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EmployeeType> _employeeTypes;

        public ObservableCollection<EmployeeType> EmployeeTypes
        {
            get { return _employeeTypes; }
            set 
            { 
                _employeeTypes = value; 
                OnPropertyChanged();
            }
        }

        private int _selectedEmployeeTypeId;

        public int SelectedEmployeeTypeId
        {
            get { return _selectedEmployeeTypeId; }
            set 
            { 
                _selectedEmployeeTypeId = value;
                OnPropertyChanged();
            }
        }

        private DateTime defaultDismissDate = DateTime.MinValue;

        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditEmployeeWindow.Closed += AddEditEmployeeWindow_Closed;
            addEditEmployeeWindow.ShowDialog();
        }

        private void AddEditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            RefreshApplication();
        }

        private void RefreshApplication()
        {
            Employees = new ObservableCollection<EmployeeWrapper>
                (_repository.GetEmployees(SelectedEmployeeTypeId));
        }
        private void ChangeEmployeeTypeSelection(object obj)
        {
            RefreshApplication();
        }

        private void InitEmployeeTypes()
        {
            var employeeTypes = _repository.GetEmployeeTypes();
            employeeTypes.Insert(0, new EmployeeType { Id = 0, Name = "Wszyscy"});

            EmployeeTypes = new ObservableCollection<EmployeeType>(employeeTypes);

            SelectedEmployeeTypeId = 0;
        }

        private async Task DismissEmployee(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Zwalnianie pracownika",
                $"Czy napewno chcesz zwolnić pracownika " +
                $"{SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            AddEditEmployee(SelectedEmployee);
        }

        private bool CanDismissEmployee(object arg)
        {
            if (SelectedEmployee != null && SelectedEmployee.DismissDate == defaultDismissDate)
                return true;

            return false;
        }

        private void SetConnectionPath(object obj)
        {
            var setConnectionPathWindow = new ConnectionPathView(true);
            setConnectionPathWindow.ShowDialog();
        }

        private async void LoadedWindow(object obj)
        {

            if (!IsValidConnectionPath())
            {
                var metrowindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metrowindow.ShowMessageAsync("Błąd połączenia.",
                    $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }

                else
                {
                    var connectionPathWinodow = new ConnectionPathView(false);
                    connectionPathWinodow.ShowDialog();
                }
            }

            else
            {
                RefreshApplication();
                InitEmployeeTypes();
            }

        }

        private bool IsValidConnectionPath()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }

            catch (Exception)

            {
                return false;
            }
        }
    }
}
