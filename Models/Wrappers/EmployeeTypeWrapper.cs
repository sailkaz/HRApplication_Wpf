using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication_Wpf.Models.Wrappers
{
    public class EmployeeTypeWrapper : IDataErrorInfo
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Id):
                        if (Id == 0)
                            return Error = "Określ stanowisko pracownika";

                        else 
                            Error = string.Empty;

                        break;

                    default:

                        break;
                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsEmployeeTypeValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(Error);
            }
        }

    }
}
