using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    public class Table
    {
        private string oznakaStola;

        private string waiter;

        private Dictionary<int, Drink> poruceno;

        public string OznakaStola
        {
            get { return oznakaStola; }
            set { oznakaStola = value; }
        }
        public Dictionary<int, Drink> Poruceno
        {
            get { return poruceno; }
            set { poruceno = value; }
        }
        public string Waiter
        {
            get { return waiter; }
            set { waiter = value; }
        }
    }
}
