using CaffeManagment.Common;
using CaffeManagment.Login;
using CaffeManagment.Model;
using System.Windows;
using System.Windows.Media;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        public MyICommand<Navigation> NavCommand { get; set; }

        private BindableBase currentViewModel;

        private TablesViewModel tablesViewModel;
        private PriceListViewModel priceListViewModel;

        private string _viewModelTitle = "Stolovi";
        private System.Windows.Media.Color c1;
        private System.Windows.Media.Brush _firmColor;
        private System.Windows.Media.Color c2;
        private System.Windows.Media.Brush _backgroundColor;
        private Visibility buttonOpenMenu;
        private Visibility buttonCloseMenu;
        private User userOnSession;
        private static MainWindowViewModel instance;
        public static MainWindowViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindowViewModel();
                }
                return instance;
            }
        }

        public MainWindowViewModel()
        {
            c1 = System.Windows.Media.Color.FromArgb(255, 53, 128, 191);
            FirmColor = new SolidColorBrush(c1);
            c2 = System.Windows.Media.Color.FromArgb(255, 37, 44, 50);
            BackgroundColor = new SolidColorBrush(c2);
            NavCommand = new MyICommand<Navigation>(OnNav);
            ButtonCloseMenu = Visibility.Collapsed;
            ButtonOpenMenu = Visibility.Visible;
            LoadPriceList();
            //OnNav(Navigation.TABLES);
            
        }

        #region CommandsImplementation

        public void Close(string obj)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            Application.Current.Shutdown();
        }

        #endregion

        public void OnNav(Navigation destination)
        {
            switch (destination)
            {
                case Navigation.TABLES:
                    this.ViewModelTitle = "Stolovi";
                    this.CurrentViewModel = new TablesViewModel(UserOnSession);
                    break;
                case Navigation.PRICELIST:
                    this.ViewModelTitle = "Cenovnik";
                    this.CurrentViewModel = new PriceListViewModel();
                    break;
            }
        }

        private void LoadPriceList()
        {
            //to do
        }

        public string ViewModelTitle
        {
            get
            {
                return _viewModelTitle;
            }
            set
            {
                _viewModelTitle = value;
                OnPropertyChanged(nameof(ViewModelTitle));
            }
        }
        public Brush FirmColor { get => _firmColor; set => _firmColor = value; }
        public Brush BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }
        public Visibility ButtonOpenMenu { get => buttonOpenMenu; set => buttonOpenMenu = value; }
        public Visibility ButtonCloseMenu { get => buttonCloseMenu; set => buttonCloseMenu = value; }
        public User UserOnSession { get => userOnSession; set => userOnSession = value; }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
        public TablesViewModel TablesViewModel { get => tablesViewModel; set => tablesViewModel = value; }
        public PriceListViewModel PriceListViewModel { get => priceListViewModel; set => priceListViewModel = value; }
    }
}
