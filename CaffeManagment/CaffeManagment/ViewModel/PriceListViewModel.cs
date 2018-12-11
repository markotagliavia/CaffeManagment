using CaffeManagment.Common;
using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CaffeManagment.ViewModel
{
    class PriceListViewModel : BindableBase
    {
        public ObservableCollection<PriceListItem> priceListItems { get; set; }
        private ICollectionView defaultView;
        public MyICommand AddNewPriceItem { get; set; }
        private PriceListItem selectedPriceListItem;

        public PriceListViewModel()
        {
            priceListItems = new ObservableCollection<PriceListItem>();
            AddNewPriceItem = new MyICommand(OnAdd,CanAdd);
            DefaultView = CollectionViewSource.GetDefaultView(priceListItems);
        }

        public PriceListItem SelectedPriceListItem
        {
            get => selectedPriceListItem;
            set
            {
                selectedPriceListItem = value;
                OnPropertyChanged(nameof(SelectedPriceListItem));
            }
        }

        public ICollectionView DefaultView { get => defaultView; set => defaultView = value; }

        public void OnAdd()
        {
            //CurrentNote.Validate();
            //if (CurrentNote.IsValid)
            //{
            //    Notes.Add(new Note()
            //    {
            //        Title = CurrentNote.Title,
            //        Description = CurrentNote.Description
            //    });
            //}

        }


        private bool CanAdd()
        {
            //to do

            return true;
        }

    }
}
