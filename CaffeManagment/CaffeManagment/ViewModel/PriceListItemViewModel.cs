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
        private Drink priceListItem;
        public Action CloseAction { get; set; }
        public MyICommand AddNewPriceItem { get; set; }

        public PriceListItemViewModel()
        {
            priceListItem = new Drink();
            priceListItem.AcutelPrice = 0;
            AddNewPriceItem = new MyICommand(OnAdd, CanAdd);
        }
        

        public Drink PriceListItem
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
            PriceList priceList = DataSourceUtil.Instance.ReadPriceList();
            
            if (priceList == null)
            {
                priceList = new PriceList();
            }
            priceList.Items.Add(PriceListItem.Id, PriceListItem);
            DataSourceUtil.Instance.WritePriceList(priceList);
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
