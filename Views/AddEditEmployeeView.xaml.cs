﻿using HRApplication_Wpf.Models.Wrappers;
using HRApplication_Wpf.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRApplication_Wpf.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditEmployeeView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : MetroWindow
    {
        public AddEditEmployeeView(EmployeeWrapper employee = null)
        {
            InitializeComponent();
            DataContext = new AddEditEmployeeViewModel(employee);
        }
    }
}
