using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    [Serializable]
    public class PriceListItem
    {
        private DateTime startDateTime;
        private DateTime endDateTime;
        private bool activePrice;
        private Drink drink;
        private float price;
        private bool action;

		
        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value; }
        }
        public DateTime EndDateTime
        {
            get { return endDateTime; }
            set { endDateTime = value; }
        }
        public Drink Drink
        {
            get { return drink; }
            set { drink = value; }
        }
        public bool Action
        {
            get { return action; }
            set { action = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public bool ActivePrice { get => activePrice; set => activePrice = value; }
    }
}
