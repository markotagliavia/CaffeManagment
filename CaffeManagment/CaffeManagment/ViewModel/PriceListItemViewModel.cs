using CaffeManagment.Common;
using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.ViewModel
{
    public class PriceListItemViewModel:BindableBase
    {
        private PriceListItem priceListItem;
        public Action CloseAction { get; set; }
        public MyICommand AddNewPriceItem { get; set; }

        public PriceListItemViewModel()
        {
            priceListItem = new PriceListItem();
            priceListItem.ActivePrice = true;
            priceListItem.Drink = new Drink();
            priceListItem.StartDateTime = DateTime.Now;
            AddNewPriceItem = new MyICommand(OnAdd, CanAdd);
        }
        

        public PriceListItem PriceListItem
        {
            get => priceListItem;
            set
            {
                priceListItem = value;
                OnPropertyChanged(nameof(PriceListItem));

            }
        }
        public void OnAdd()
        {
            if (CloseAction != null)
            {
                CloseAction.Invoke();
            }

        }


        private bool CanAdd()
        {
            //to do

            return true;
        }
    }
}
