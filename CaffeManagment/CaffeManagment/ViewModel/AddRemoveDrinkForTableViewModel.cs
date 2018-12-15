using CaffeManagment.Common;
using CaffeManagment.Model;
using CaffeManagment.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CaffeManagment.ViewModel
{
    public class AddRemoveDrinkForTableViewModel : BindableBase
    {
        #region Members
        private User userOnSession;
        private Table tableForEdit;
        private string imeStola;
        private string kolicinaText = "1";
        private string searchText = "";
        private bool addEnabled;
        private bool removeEnabled;
        private int _selectedPiceLevo = -1;
        private int _selectedPiceDesno = -1;
        private ObservableCollection<Drink> piceLevo;
        private ICollectionView defaultViewPiceLevo;
        private ObservableCollection<DrinkWithPriceAndQuantity> piceDesno;
        #endregion

        #region Commands
        public MyICommand<object> SacuvajCommand { get; private set; }
        public MyICommand<string> OtkaziCommand { get; private set; }
        public MyICommand<int> AddCommand { get; private set; }
        public MyICommand<int> RemoveCommand { get; private set; }
        public MyICommand<object> FilterCommand { get; private set; }
        #endregion

        public AddRemoveDrinkForTableViewModel(User user, Table t)
        {
            SacuvajCommand = new MyICommand<object>(SacuvajExecute);
            OtkaziCommand = new MyICommand<string>(OtkaziExecute);
            AddCommand = new MyICommand<int>(AddExecute);
            RemoveCommand = new MyICommand<int>(RemoveExecute);
            FilterCommand = new MyICommand<object>(FilterExecute);
            userOnSession = user;
            tableForEdit = t;
            imeStola = tableForEdit.OznakaStola;
            piceLevo = new ObservableCollection<Drink>();
            piceDesno = new ObservableCollection<DrinkWithPriceAndQuantity>();
            PopulatePiceLevoGrid();
            PopulatePiceDesnoGrid();
            DefaultViewPiceLevo = CollectionViewSource.GetDefaultView(PiceLevo);
        }

        #region CommandsImplementation

        private void OtkaziExecute(string obj)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType().Equals(typeof(AddRemoveDrinkForTableView)))
                {
                    PiceDesno.Clear();
                    w.Close();
                }
            }
        }

        private void SacuvajExecute(object obj)
        {
            try
            {
                tableForEdit.Poruceno = PiceDesno;
                if (PiceDesno.Count > 0)
                {
                    tableForEdit.StanjeStola = Enumerations.State.BUSY;
                }
                else
                {
                    tableForEdit.StanjeStola = Enumerations.State.EMPTY;
                }
                var pom = DataSourceUtil.Instance.ReadTables();
                pom.FirstOrDefault(x => x.Id == tableForEdit.Id).StanjeStola = tableForEdit.StanjeStola;
                pom.FirstOrDefault(x => x.Id == tableForEdit.Id).Poruceno = tableForEdit.Poruceno;
                if (DataSourceUtil.Instance.WriteTables(pom))
                {
                    ((TablesViewModel)MainWindowViewModel.Instance.CurrentViewModel).Tables = DataSourceUtil.Instance.ReadTables();
                    MainWindowViewModel.Instance.NotifySelectionChanged(pom.FirstOrDefault(x => x.Id == tableForEdit.Id));
                    System.Media.SystemSounds.Asterisk.Play();
                    OtkaziExecute(null);
                }
                else
                {
                    MessageBox.Show("Greška, poručena pića nisu sačuvana.");
                }
            }
            catch (Exception exe)
            {
                Console.WriteLine($"Greška, poručena pića nisu sačuvana. Greska je {exe.Message}");
            }
        }

        private void FilterExecute(object obj)
        {
            string pom = SearchText.Trim().ToUpper();
            if (pom.Equals("") || String.IsNullOrEmpty(pom) || String.IsNullOrWhiteSpace(pom))
            {
                DefaultViewPiceLevo = CollectionViewSource.GetDefaultView(PiceLevo);
                DefaultViewPiceLevo.Filter = null;
            }
            else
            {
                DefaultViewPiceLevo = CollectionViewSource.GetDefaultView(DefaultViewPiceLevo);
                DefaultViewPiceLevo.Filter =
                        w => ((Drink)w).SifraPica.ToUpper().Contains(pom) || ((Drink)w).NazivPica.ToUpper().Contains(pom);
            }
        }

        private void RemoveExecute(int obj)
        {
            if (SelectedPiceDesno != -1)
            {
                piceDesno.RemoveAt(SelectedPiceDesno);
            }
            else
            {
                MessageBox.Show("Morate selektovati dodato piće.");
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
                    DrinkWithPriceAndQuantity pk = new DrinkWithPriceAndQuantity(p.NazivPica, p.SifraPica, kolicina, p.AcutelPrice);
                    PiceDesno.Add(pk);
                }
                else
                {
                    MessageBox.Show("Niste odabrali piće iz tabele.");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                MessageBox.Show("Niste uneli validnu vrednost za količinu.");
            }
        }
        #endregion

        #region Properties

        public User UserOnSession { get => userOnSession; set => userOnSession = value; }
        public Table TableForEdit { get => tableForEdit; set { tableForEdit = value; OnPropertyChanged("TableForEdit"); } }
        public ICollectionView DefaultViewPiceLevo { get => defaultViewPiceLevo; set => defaultViewPiceLevo = value; }
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
                OnPropertyChanged("SelectedPiceLevo");
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
                OnPropertyChanged("SelectedPiceDesno");
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
                OnPropertyChanged("SearchText");
                FilterExecute(null);
            }
        }
        #endregion

        #region HelperMethods
        private void PopulatePiceLevoGrid()
        {
            PriceList pom;
            if ((pom = DataSourceUtil.Instance.ReadPriceList()) != null)
            {
                foreach (KeyValuePair<Guid, Drink> pair in pom.Items)
                {
                    
                    PiceLevo.Add(pair.Value);
                }
            }
        }

        private void PopulatePiceDesnoGrid()
        {
            PiceDesno = tableForEdit.Poruceno;
        }
        #endregion
    }
}
