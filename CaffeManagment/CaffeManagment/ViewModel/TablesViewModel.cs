using CaffeManagment.Common;
using CaffeManagment.Model;
using CaffeManagment.SecurityManager;
using CaffeManagment.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaffeManagment.ViewModel
{
    public class TablesViewModel : BindableBase
    {
        private Visibility isAdmin;
        private User userOnSession;
        private string username;
        private string nazivStola;
        private bool selektovanSto;
        private ObservableCollection<DrinkWithPriceAndQuantity> picaDataSource;
        private int ukupnoZaSto;
        private int ukupnoZaSve;
        private bool stampajSto;
        private bool stampajSve;
        private Dictionary<string, Table> tables;
        private Table selectedTable;

        #region Commands
        public MyICommand OtvoriStoCommand { get; private set; }
        public MyICommand MojNalogCommand { get; private set; }
        public MyICommand SviNaloziCommand { get; private set; }
        public MyICommand StampajStoCommand { get; private set; }
        public MyICommand StampajSveCommand { get; private set; }
        #endregion

        public TablesViewModel()
        {
            this.userOnSession = MainWindowViewModel.Instance.UserOnSession;
            isAdmin = userOnSession.Role.Equals(Role.Admin) ? Visibility.Visible : Visibility.Hidden;
            nazivStola = "";
            selektovanSto = false;
            picaDataSource = new ObservableCollection<DrinkWithPriceAndQuantity>();
            ukupnoZaSto = 0;
            ukupnoZaSve = 0;
            stampajSto = false;
            stampajSve = false;
            username = userOnSession.Username;
            OtvoriStoCommand = new MyICommand(OtvoriStoExecute);
            MojNalogCommand = new MyICommand(MojNalogExecute);
            SviNaloziCommand = new MyICommand(SviNaloziExecute);
            StampajStoCommand = new MyICommand(StampajStoExecute);
            StampajSveCommand = new MyICommand(StampajSveExecute);
            Test();
            MainWindowViewModel.Instance.TableChanged += HandleTableChanged;
        }

        #region Commands Implementation
        private void StampajSveExecute()
        {
            //TO DO
        }

        private void StampajStoExecute()
        {
           //TO DO
        }

        private void SviNaloziExecute()
        {
            MessageBox.Show("U procesu razvoja...");
            //TO DO
        }

        private void MojNalogExecute()
        {
            MessageBox.Show("U procesu razvoja...");
            //TO DO
        }

        private void OtvoriStoExecute()
        {
            if (selectedTable != null)
            {
                AddRemoveDrinkForTableView a = new AddRemoveDrinkForTableView(userOnSession, selectedTable);
                a.ShowDialog();
            }
        }

        #endregion

        private void Test()
        {
            Tables = new Dictionary<string, Table>();
            for (int i = 0; i < 25; i++)
            {
                Table t = new Table();
                t.OznakaStola = $"sto{i}";
                t.StanjeStola = Enumerations.State.EMPTY;
                Tables.Add(t.OznakaStola,t);
            }
        }

        #region Properties

        public Visibility IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        public User UserOnSession
        {
            get
            {
                return userOnSession;
            }
            set
            {
                userOnSession = value;
                OnPropertyChanged("UserOnSession");
            }
        }

        private void HandleTableChanged(Table t)
        {
            SelectedTable = t;
            OnPropertyChanged(nameof(NazivStola));
        }

        public string NazivStola
        {
            get
            {
                return SelectedTable?.OznakaStola ?? "";
            }
            set
            {
                SelectedTable.OznakaStola = value;
                OnPropertyChanged(NazivStola);
                MainWindowViewModel.Instance.NotifySelectionChanged(SelectedTable);
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public bool SelektovanSto
        {
            get
            {
                return selektovanSto;
            }
            set
            {
                selektovanSto = value;
                OnPropertyChanged("SelektovanSto");
            }
        }

        public bool StampajSto
        {
            get
            {
                return stampajSto;
            }
            set
            {
                stampajSto = value;
                OnPropertyChanged("StampajSto");
            }
        }

        public bool StampajSve
        {
            get
            {
                return stampajSve;
            }
            set
            {
                stampajSve = value;
                OnPropertyChanged("StampajSve");
            }
        }

        public int UkupnoZaSto
        {
            get
            {
                return ukupnoZaSto;
            }
            set
            {
                ukupnoZaSto = value;
                OnPropertyChanged("UkupnoZaSto");
            }
        }

        public int UkupnoZaSve
        {
            get
            {
                return ukupnoZaSve;
            }
            set
            {
                ukupnoZaSve = value;
                OnPropertyChanged("UkupnoZaSve");
            }
        }

        public ObservableCollection<DrinkWithPriceAndQuantity> PicaDataSource
        {
            get
            {
                return picaDataSource;
            }
            set
            {
                picaDataSource = value;
                OnPropertyChanged("PicaDataSource");
            }
        }

        public Dictionary<string, Table> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }

        public Table SelectedTable
        {
            get
            { return selectedTable; }
            set
            {
                selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                OnPropertyChanged(NazivStola);
                if (selectedTable != null && selectedTable.OznakaStola.Length > 0)
                {
                    SelektovanSto = true;
                }
            }
        }



        #endregion

    }
}
