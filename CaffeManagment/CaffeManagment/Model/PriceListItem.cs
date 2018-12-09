using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    public class PriceListItem
    {
        private DateTime startDateTime;
        private DateTime endDateTime;
        private Drink drink;
        private bool action;

        public DateTime StartDateTime { get => startDateTime; set => startDateTime = value; }
        public DateTime EndDateTime { get => endDateTime; set => endDateTime = value; }
        public Drink Drink { get => drink; set => drink = value; }
        public bool Action { get => action; set => action = value; }
    }
}
