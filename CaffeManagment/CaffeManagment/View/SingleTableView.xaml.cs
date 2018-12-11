using CaffeManagment.Model;
using CaffeManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaffeManagment.View
{
    /// <summary>
    /// Interaction logic for SingleTableView.xaml
    /// </summary>
    public partial class SingleTableView : UserControl
    {
        public SingleTableView(Table t)
        {
            InitializeComponent();
            this.DataContext = new SingleTableViewModel(t);
        }
    }
}
