using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace HRApplication_Wpf.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo

    {
        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        
        public EmployeeWrapper()
        {
            EmployeeType = new EmployeeTypeWrapper();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime DismissDate { get; set; }
        public decimal HourlyRate { get; set; }
        public EmployeeTypeWrapper EmployeeType { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Podaj imię pracownika";
                            _isFirstNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }

                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Podaj nazwisko pracownika";
                            _isLastNameValid = false;
                        }

                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }

                        break;

                    default:

                        break;
                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool isValid
        {
            get
            {
                return _isFirstNameValid && _isLastNameValid && EmployeeType.IsEmployeeTypeValid;
            }
        }
    }
}
