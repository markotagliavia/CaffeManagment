using CaffeManagment.Model;
using CaffeManagment.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(User user)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.UserOnSession = user;
        }

        private void TablesCall(object sender, RoutedEventArgs e)
        {

             MainWindowViewModel.Instance.OnNav(Navigation.TABLES);

        }

        private void PriceListCall(object sender, RoutedEventArgs e)
        {

            MainWindowViewModel.Instance.OnNav(Navigation.PRICELIST);

        }
    }
}
