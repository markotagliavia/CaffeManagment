using CaffeManagment.Common;
using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    public class PriceListItemViewModel:BindableBase
    {
        private Drink priceListItem;
        public Action CloseAction { get; set; }
        public MyICommand AddNewPriceItem { get; set; }
        private Operation operation;

        public PriceListItemViewModel(Operation o,Drink d = null)
        {
            operation = o;
            if (o == Operation.ADD)
            {
                priceListItem = new Drink();
                priceListItem.AcutelPrice = 0;
               
            }
            else if (o == Operation.EDIT)
            {
                priceListItem = d;
            }
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
            PriceListItem.Validate();

            if (PriceListItem.IsValid)
            {
                if (operation == Operation.ADD)
                {
                    PriceList priceList = DataSourceUtil.Instance.ReadPriceList();

                    if (priceList == null)
                    {
                        priceList = new PriceList();
                    }
                    priceList.Items.Add(PriceListItem.Id, PriceListItem);
                    DataSourceUtil.Instance.WritePriceList(priceList);
                }
                else
                {
                    PriceList priceList = DataSourceUtil.Instance.ReadPriceList();
                    if (priceList.Items.Any(x => x.Value.Id == PriceListItem.Id))
                    {
                        priceList.Items.Remove(PriceListItem.Id);
                        priceList.Items.Add(PriceListItem.Id, PriceListItem);
                        DataSourceUtil.Instance.WritePriceList(priceList);
                    }
                }

                MainWindowViewModel.Instance.NotifyReload();
                if (CloseAction != null)
                {
                    CloseAction.Invoke();
                }
            }

            

        }


        private bool CanAdd()
        {
            //to do

            return true;
        }
    }
}
