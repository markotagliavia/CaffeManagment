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

        public Check()
        {
        }

        public Check(Table table)
        {
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public Dictionary<int, PriceListItem> PriceListItem { get => priceListItem; set => priceListItem = value; }
        public User Waiter { get => waiter; set => waiter = value; }
    }
}
