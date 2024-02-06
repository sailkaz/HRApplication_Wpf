using ControlzEx.Standard;
using HRApplication_Wpf.Commands;
using HRApplication_Wpf.Models.Domains;
using HRApplication_Wpf.Models.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace HRApplication_Wpf.ViewModels
{
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddEditEmployeeViewModel(EmployeeWrapper employee)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (employee == null)
                Employee = new EmployeeWrapper();

            else
            {
                Employee = employee;
                IsUpdate = true;
            }

            InitEmployeeTypes();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        private EmployeeWrapper _employee;

        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
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

        private ObservableCollection<EmployeeType> _employeeType;

        public ObservableCollection<EmployeeType> EmployeeTypes
        {
            get { return _employeeType; }
            set
            {
                _employeeType = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private void InitEmployeeTypes()
        {
            var employeeTypes = _repository.GetEmployeeTypes();
            employeeTypes.Insert(0, new EmployeeType { Id = 0, Name = "---Brak---" });

            EmployeeTypes = new ObservableCollection<EmployeeType>(employeeTypes);

            SelectedEmployeeTypeId = Employee.EmployeeType.Id;
        }


        private void Confirm(object obj)
        {
            if (!Employee.isValid)
                return;

            if (!IsUpdate)
                AddEditEmployee();

            else
                UpdateEmployee();

            CloseWindow(obj as Window);
        }

        private void AddEditEmployee()
        {
            _repository.AddEmployee(Employee);
        }

        private void UpdateEmployee()
        {
            _repository.UpdateEmployee(Employee);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
