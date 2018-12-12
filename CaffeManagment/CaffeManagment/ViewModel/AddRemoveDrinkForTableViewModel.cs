using CaffeManagment.Common;
using CaffeManagment.Model;
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
    public class AddRemoveDrinkForTableViewModel : BindableBase
    {
        #region Members
        private User userOnSession;
        private Table tableForEdit;
        private string imeStola;
        private string kolicinaText;
        private string searchText;
        private bool addEnabled;
        private bool removeEnabled;
        private int _selectedPiceLevo = -1;
        private int _selectedPiceDesno = -1;
        private ObservableCollection<Drink> piceLevo;
        private ObservableCollection<DrinkWithPriceAndQuantity> piceDesno;
        #endregion

        #region Commands
        public MyICommand<object> SacuvajCommand { get; private set; }
        public MyICommand<string> OtkaziCommand { get; private set; }
        public MyICommand<int> AddCommand { get; private set; }
        public MyICommand<int> RemoveCommand { get; private set; }
        #endregion

        public AddRemoveDrinkForTableViewModel(User user, Table t)
        {
            SacuvajCommand = new MyICommand<object>(SacuvajExecute);
            OtkaziCommand = new MyICommand<string>(OtkaziExecute);
            AddCommand = new MyICommand<int>(AddExecute);
            RemoveCommand = new MyICommand<int>(RemoveExecute);
            userOnSession = user;
            tableForEdit = t;
            imeStola = tableForEdit.OznakaStola;
            piceLevo = new ObservableCollection<Drink>();
            piceDesno = new ObservableCollection<DrinkWithPriceAndQuantity>();
            PopulateProizvodiGrid();
            KolicinaText = "";
            searchText = "";
        }

        #region CommandsImplementation

        private void OtkaziExecute(string obj)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType().Equals(typeof(AddRemoveDrinkForTableView)))
                {
                    w.Close();
                }
            }
        }

        private void SacuvajExecute(object obj)
        {
            // TO DO
        }

        private void RemoveExecute(int obj)
        {
            if (SelectedPiceDesno != -1)
            {
                piceDesno.RemoveAt(SelectedPiceDesno);
            }
            else
            {
                MessageBox.Show("Morate selektovati odgovarajuću kolonu.");
            }
        }

        private void AddExecute(int index)
        {
            try
            {
                int kolicina = Int32.Parse(KolicinaText);
                if (SelectedPiceLevo != -1)
                {
                    Drink p = PiceLevo.ElementAt(SelectedPiceLevo);
                    //TO DO : nabavi cenu umesto ove nule
                    DrinkWithPriceAndQuantity pk = new DrinkWithPriceAndQuantity(p.NazivPica, p.SifraPica, kolicina, 0);
                    PiceDesno.Add(pk);
                }
                else
                {
                    MessageBox.Show("Niste odabrali pice iz tabele.");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                MessageBox.Show("Niste uneli validnu vrednost za kolicinu.");
            }
        }
        #endregion

        #region Properties

        public User UserOnSession { get => userOnSession; set => userOnSession = value; }
        public Table TableForEdit { get => tableForEdit; set { tableForEdit = value; OnPropertyChanged("TableForEdit"); } }

        public string KolicinaText { get => kolicinaText; set { kolicinaText = value; OnPropertyChanged("KolicinaText"); } }

        public string ImeStola { get => imeStola; set { imeStola = value; OnPropertyChanged("ImeStola"); } }

        public bool RemoveEnabled
        {
            get => removeEnabled;
            set
            {
                removeEnabled = value;
                OnPropertyChanged("RemoveEnabled");
            }
        }

        public bool AddEnabled
        {
            get => addEnabled;
            set
            {
                addEnabled = value;
                OnPropertyChanged("AddEnabled");
            }
        }

        public ObservableCollection<Drink> PiceLevo
        {
            get => piceLevo;
            set
            {
                piceLevo = value;
                OnPropertyChanged("PiceLevo");
            }
        }

        public ObservableCollection<DrinkWithPriceAndQuantity> PiceDesno
        {
            get => piceDesno;
            set
            {
                piceDesno = value;
                OnPropertyChanged("PiceDesno");
            }
        }

        public int SelectedPiceLevo
        {
            get => _selectedPiceLevo;
            set
            {
                _selectedPiceLevo = value;
                if (_selectedPiceLevo> -1)
                {
                    AddEnabled = true;
                }
                else
                {
                    AddEnabled = false;
                }
            }
        }

        public int SelectedPiceDesno
        {
            get => _selectedPiceDesno;
            set
            {
                _selectedPiceDesno = value;
                if (_selectedPiceDesno > -1)
                {
                    RemoveEnabled = true;
                }
                else
                {
                    RemoveEnabled = false;
                }
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                FilterTable(searchText);
            }
        }
        #endregion

        #region HelperMethods
        private void PopulateProizvodiGrid()
        {
            // TO DO
        }

        private void FilterTable(string searchText)
        {
            // TO DO
        }
        #endregion
    }
}
