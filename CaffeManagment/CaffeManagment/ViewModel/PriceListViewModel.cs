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
using System.Windows.Forms;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    public class PriceListViewModel : BindableBase
    {
        public ObservableCollection<Drink> priceListItems { get; set; }
        private List<string> filterList;
        private string selectedFilter;
        private ICollectionView defaultView;
        public MyICommand AddNewPriceItem { get; set; }
        public MyICommand EditNewPriceItem { get; set; }
        public MyICommand DeleteNewPriceItem { get; set; }
        public MyICommand SearchPriceItem { get; private set; }
        private Drink selectedPriceListItem;
        private Visibility pretraziVisiblity;
        private Visibility textVisiblity;
        private Visibility comboVisiblity;
        private Visibility digitVisibility;
        private string textSearch;
        private string numberSearch;
        private List<string> enums;
        private string selectedEnum;

        public PriceListViewModel()
        {
            priceListItems = new ObservableCollection<Drink>();
            LoadfilterList();
            Load();
            MainWindowViewModel.Instance.Reload += Load;
            AddNewPriceItem = new MyICommand(OnAdd,CanAdd);
            EditNewPriceItem = new MyICommand(OnEdit, CanEdit);
            DeleteNewPriceItem = new MyICommand(OnDelete, CanDelete);
            SearchPriceItem = new MyICommand(OnSearch);
            DefaultView = CollectionViewSource.GetDefaultView(priceListItems);
            PretraziVisiblity = Visibility.Collapsed;
            TextVisiblity = Visibility.Collapsed;
            ComboVisiblity = Visibility.Collapsed;
            DigitVisibility = Visibility.Collapsed;
        }

        private void OnSearch()
        {
            DefaultView = CollectionViewSource.GetDefaultView(DefaultView);
            if (TextVisiblity == Visibility.Visible)
            {
                
                if (SelectedFilter.Equals("Šifri"))
                {
                    DefaultView.Filter =
                    w => ((Drink)w).SifraPica.ToUpper().Contains(TextSearch.ToUpper());
                }
                else if (SelectedFilter.Equals("Nazivu"))
                {
                    DefaultView.Filter =
                    w => ((Drink)w).NazivPica.ToUpper().Contains(TextSearch.ToUpper());
                }

            }
            else if (DigitVisibility == Visibility.Visible)
            {
                try
                {
                    Double kolicina = Double.Parse(NumberSearch);

                    DefaultView.Filter =
                        w => ((Drink)w).AcutelPrice == kolicina;
                }
                catch (Exception ex)
                {

                }

            }
            else if (ComboVisiblity == Visibility.Visible)
            {
                if (SelectedFilter.Equals("Tipu"))
                {
                    DefaultView.Filter =
                    w => ((Drink)w).TipPica.ToString().Contains(SelectedEnum);

                }
                else
                {
                    DefaultView.Filter =
                    w => ((Drink)w).Poreklo.ToString().Contains(SelectedEnum);
                }
            }

                DefaultView.Refresh();
        }

        private void LoadfilterList()
        {
            FilterList = new List<string>(6);
            FilterList.Add("/");
            FilterList.Add("Šifri");
            FilterList.Add("Nazivu");
            FilterList.Add("Ceni");
            FilterList.Add("Tipu");
            FilterList.Add("Poreklu");
            SelectedFilter = FilterList.First();
        }

        private void OnDelete()
        {
            if (SelectedPriceListItem == null)
            {
                System.Windows.MessageBox.Show("Morate selektovati cenu koju želite da obrišete.");
                return;
            }
            DialogResult dr = System.Windows.Forms.MessageBox.Show("Da li ste sigurni?",
                      "Bisanje cene", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    Delete();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private bool CanDelete()
        {
            return true;
        }

        private void Delete()
        {
            var list = DataSourceUtil.Instance.ReadPriceList();
            if (list != null)
            {
                if (list.Items.Any(x => x.Value.Id == SelectedPriceListItem.Id))
                {
                    list.Items.Remove(SelectedPriceListItem.Id);
                }
            }
            DataSourceUtil.Instance.WritePriceList(list);
            Load();
        }

        private void OnEdit()
        {
            PriceListItemView priceList = new PriceListItemView(Enumerations.Operation.EDIT,SelectedPriceListItem);
            priceList.ShowDialog();
        }

        private bool CanEdit()
        {
            return true;
        }

        private void Load()
        {
            var priceList = DataSourceUtil.Instance.ReadPriceList();
            priceListItems.Clear();
            if (priceList != null)
            {
                foreach (var item in priceList.Items)
                {
                    priceListItems.Add(item.Value);
                }
            }
        }

        public Drink SelectedPriceListItem
        {
            get => selectedPriceListItem;
            set
            {
                selectedPriceListItem = value;
                OnPropertyChanged(nameof(SelectedPriceListItem));
            }
        }

        public ICollectionView DefaultView { get => defaultView; set => defaultView = value; }
        public List<string> FilterList
        {
            get => filterList;
            set => filterList = value;
        }
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
                SetFilter();
            }
        }

        public Visibility PretraziVisiblity { get => pretraziVisiblity; set { pretraziVisiblity = value; OnPropertyChanged(nameof(PretraziVisiblity)); } }
        public Visibility TextVisiblity { get => textVisiblity; set { textVisiblity = value; OnPropertyChanged(nameof(TextVisiblity)); } }
        public Visibility ComboVisiblity { get => comboVisiblity; set { comboVisiblity = value; OnPropertyChanged(nameof(ComboVisiblity)); } }
        public Visibility DigitVisibility { get => digitVisibility; set { digitVisibility = value; OnPropertyChanged(nameof(DigitVisibility)); } }

        public string TextSearch { get => textSearch; set { textSearch = value; OnPropertyChanged(nameof(TextSearch)); } }

        public string NumberSearch { get => numberSearch; set { numberSearch = value; OnPropertyChanged(nameof(NumberSearch)); } }

        public List<string> Enums { get => enums; set { enums = value; OnPropertyChanged(nameof(Enums)); } }

        public string SelectedEnum { get => selectedEnum; set { selectedEnum = value; OnPropertyChanged(nameof(SelectedEnum)); } }

        private void SetFilter()
        {
            if (SelectedFilter.Equals("/"))
            {
                PretraziVisiblity = Visibility.Collapsed;
                TextVisiblity = Visibility.Collapsed;
                ComboVisiblity = Visibility.Collapsed;
                DigitVisibility = Visibility.Collapsed;

                DefaultView = CollectionViewSource.GetDefaultView(priceListItems);
                DefaultView.Filter = null;
                DefaultView.Refresh();
            }
            else
            {
                PretraziVisiblity = Visibility.Visible;
                TextVisiblity = (SelectedFilter.Equals("Šifri") || SelectedFilter.Equals("Nazivu")) ? Visibility.Visible : Visibility.Collapsed;
                DigitVisibility = (SelectedFilter.Equals("Ceni")) ? Visibility.Visible : Visibility.Collapsed;
                ComboVisiblity = (SelectedFilter.Equals("Tipu") || SelectedFilter.Equals("Poreklu")) ? Visibility.Visible : Visibility.Collapsed;
                Enums = (SelectedFilter.Equals("Tipu") || SelectedFilter.Equals("Poreklu"))? LoadEnums() : new List<string>(5);

            }
        }

        private List<string> LoadEnums()
        {
            var ret = new List<string>(5);
            if (SelectedFilter.Equals("Tipu"))
            {
                ret = Enum.GetValues(typeof(TipPica))
                        .Cast<TipPica>()
                        .Select(v => v.ToString())
                        .ToList();
                
            }
            else
            {
                ret = Enum.GetValues(typeof(Poreklo))
                        .Cast<Poreklo>()
                        .Select(v => v.ToString())
                        .ToList();
            }

            SelectedEnum = ret.FirstOrDefault();

            return ret;
        }

        public void OnAdd()
        {
            PriceListItemView priceList = new PriceListItemView(Enumerations.Operation.ADD);
            priceList.ShowDialog();

        }


        private bool CanAdd()
        {
            //to do

            return true;
        }

    }
}
