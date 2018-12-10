using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    public class Check
    {
        private User waiter;
        private DateTime dateTime;
        private Dictionary<int, PriceListItem> priceListItem;
        private int id;

        public Check()
        {
        }

        public Check(Table table)
        {
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        public Dictionary<int, PriceListItem> PriceListItem
        {
            get { return priceListItem; }
            set { priceListItem = value; }
        }
        public User Waiter
        {
            get { return waiter; }
            set { waiter = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
