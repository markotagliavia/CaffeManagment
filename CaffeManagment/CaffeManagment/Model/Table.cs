using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Model
{
    [Serializable]
    public class Table
    {
        private Guid id;

        private string oznakaStola;

        private string waiter;

        private State stanjeStola;

        private ObservableCollection<DrinkWithPriceAndQuantity> poruceno;

        public Table()
        {
            Id = Guid.NewGuid();
        }

        public string OznakaStola
        {
            get { return oznakaStola; }
            set { oznakaStola = value; }
        }
        public ObservableCollection<DrinkWithPriceAndQuantity> Poruceno
        {
            get { return poruceno; }
            set { poruceno = value; }
        }
        public string Waiter
        {
            get { return waiter; }
            set { waiter = value; }
        }

        public State StanjeStola
        {
            get { return stanjeStola; }
            set { stanjeStola = value; }
        }

        public Guid Id { get => id; set => id = value; }
    }
}
