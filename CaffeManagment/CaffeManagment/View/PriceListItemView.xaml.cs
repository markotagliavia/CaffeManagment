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

namespace CaffeManagment.View
{
    /// <summary>
    /// Interaction logic for PriceListItemView.xaml
    /// </summary>
    public partial class PriceListItemView : MetroWindow
    {
        public PriceListItemView()
        {
            InitializeComponent();
            PriceListItemViewModel vm = new PriceListItemViewModel();
            this.DataContext = vm;
            tippica.ItemsSource = Enum.GetValues(typeof(TipPica)).Cast<TipPica>();
            poreklo.ItemsSource = Enum.GetValues(typeof(Poreklo)).Cast<Poreklo>();
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
