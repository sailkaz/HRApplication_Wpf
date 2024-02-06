using HRApplication_Wpf.Models;
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
    /// Logika interakcji dla klasy ConnectionPathView.xaml
    /// </summary>
    public partial class ConnectionPathView : MetroWindow
    {
        public ConnectionPathView(bool canCloseWindow)
        {
            InitializeComponent();
            DataContext = new ConnectionPathViewModel(canCloseWindow);
        }
    }
}
