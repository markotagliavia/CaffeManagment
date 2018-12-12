using CaffeManagment.Model;
using CaffeManagment.ViewModel;
using MahApps.Metro.Controls;


namespace CaffeManagment.View
{
    /// <summary>
    /// Interaction logic for AddRemoveDrinkForTableView.xaml
    /// </summary>
    public partial class AddRemoveDrinkForTableView : MetroWindow
    {
        public AddRemoveDrinkForTableView(User u, Model.Table t)
        {
            InitializeComponent();
            this.DataContext = new AddRemoveDrinkForTableViewModel(u, t);
        }
    }
}
