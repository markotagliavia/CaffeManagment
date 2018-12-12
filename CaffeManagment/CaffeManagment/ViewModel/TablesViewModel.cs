using CaffeManagment.Common;
using CaffeManagment.Model;
using CaffeManagment.SecurityManager;
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
            Test();
        }

        private void Test()
        {
            Tables = new Dictionary<string, Table>();
            for (int i = 0; i < 22; i++)
            {
                Table t = new Table();
                t.OznakaStola = $"sto{i}";
                int moduo = i % 3;
                if (moduo == 0)
                {
                    t.StanjeStola = Enumerations.State.EMPTY;
                }
                else if (moduo == 1)
                {
                    t.StanjeStola = Enumerations.State.BUSY;
                }
                else
                {
                    t.StanjeStola = Enumerations.State.RESERVED;
                }
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

        public string NazivStola
        {
            get
            {
                return SelectedTable.OznakaStola;
            }
            private set
            {
                SelectedTable.OznakaStola = value;
                OnPropertyChanged(NazivStola);
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
            }
        }



        #endregion

    }
}
