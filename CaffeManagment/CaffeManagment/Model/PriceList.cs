using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    [Serializable]
    public class PriceList
    {
        private Dictionary<int, PriceListItem> items;

        public Dictionary<int, PriceListItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}
