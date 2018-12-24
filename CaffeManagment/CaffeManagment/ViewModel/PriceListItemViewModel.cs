using CaffeManagment.Common;
using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    public class PriceListItemViewModel : BindableBase
    {
        private Drink priceListItem;
        public Action CloseAction { get; set; }
        public MyICommand AddNewPriceItem { get; set; }
        private Operation operation;
        public bool Editable { get; set; }
        public List<PoreskaGrupa> Enums => Enum.GetValues(typeof(PoreskaGrupa))
                        .Cast<PoreskaGrupa>()
                        .ToList();

        public PriceListItemViewModel(Operation o,Drink d = null)
        {
            operation = o;
            if (o == Operation.ADD)
            {
                priceListItem = new Drink();
                priceListItem.AcutelPrice = 0;
                PriceListItem.PoreskaGrupa = PoreskaGrupa.DJ;
                priceListItem.Stanje = 0;
                Editable = true;
                
            }
            else if (o == Operation.EDIT)
            {
                priceListItem = d;
                Editable = false;
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
                    SavePrice();
                }
                else
                {
                    PriceList priceList = DataSourceUtil.Instance.ReadPriceList();
                    if (priceList == null)
                    {
                        priceList = new PriceList();
                    }
                    if (priceList.Items.Any(x => x.Value.Id == PriceListItem.Id))
                    {
                        priceList.Items.Remove(PriceListItem.Id);
                        priceList.Items.Add(PriceListItem.Id, PriceListItem);
                        DataSourceUtil.Instance.WritePriceList(priceList);
                    }
                    EditPrice();
                }

                MainWindowViewModel.Instance.NotifyReload();
                if (CloseAction != null)
                {
                    CloseAction.Invoke();
                }
            }

            

        }

        private void SavePrice()
        {
            var csv = new StringBuilder();
            csv.AppendLine($"U,1,______,_,__;{PriceListItem.NazivPica};{PriceListItem.AcutelPrice};99999;1;{PriceListItem.GrupaArtikla};{PriceListItem.PoreskaGrupa}; 0;<Kod {PriceListItem.SifraPica}>;");
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cena.inv");

            File.WriteAllText(filePath, csv.ToString());
            MessageBox.Show("Nova cena je uspešno kreirana.");
        }

        private void EditPrice()
        {
            var csv = new StringBuilder();
            csv.AppendLine($"107, 1, ______, _, __; 4;{PriceListItem.SifraPica};{PriceListItem.AcutelPrice};");
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Edit.inv");

            File.WriteAllText(filePath, csv.ToString());
            MessageBox.Show("Cena je uspešno izmenjena.");
        }



        private bool CanAdd()
        {
            //to do

            return true;
        }
    }
}
