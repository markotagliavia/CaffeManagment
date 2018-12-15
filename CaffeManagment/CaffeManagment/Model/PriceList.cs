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
        private Dictionary<Guid, Drink> items;

        public PriceList()
        {
            Items = new Dictionary<Guid, Drink>();
        }

        public Dictionary<Guid, Drink> Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}
