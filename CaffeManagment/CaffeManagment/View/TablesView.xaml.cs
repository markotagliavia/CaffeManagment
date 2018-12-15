using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CaffeManagment.Model;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CaffeManagment.ViewModel;
using CaffeManagment.Common;

namespace CaffeManagment.View
{
    /// <summary>
    /// Interaction logic for TablesView.xaml
    /// </summary>
    public partial class TablesView : UserControl
    {
        public TablesView()
        {
            InitializeComponent();
            TablesUIConverter t = new TablesUIConverter();
            this.DataContext = new TablesViewModel();
            //Tables = t.Convert((DataContext as TablesViewModel).Tables,null, Tables, null) as Grid;
        }
    }
}
