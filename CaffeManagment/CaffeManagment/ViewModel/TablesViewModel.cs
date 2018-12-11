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

        public TablesViewModel(User userOnSession)
        {
            this.userOnSession = userOnSession;
            isAdmin = userOnSession.Role.Equals(Role.Admin) ? Visibility.Visible : Visibility.Hidden;
            nazivStola = "";
            selektovanSto = false;
            picaDataSource = new ObservableCollection<DrinkWithPriceAndQuantity>();
            ukupnoZaSto = 0;
            ukupnoZaSve = 0;
            stampajSto = false;
            stampajSve = false;
            username = userOnSession.Username;
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
                return nazivStola;
            }
            set
            {
                nazivStola = value;
                OnPropertyChanged("NazivStola");
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

        #endregion

    }
}
