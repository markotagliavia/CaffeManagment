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

        private User waiter;

        private Dictionary<int, Drink> poruceno;

        public string OznakaStola { get => oznakaStola; set => oznakaStola = value; }
        public Dictionary<int, Drink> Poruceno { get => poruceno; set => poruceno = value; }
        public User Waiter { get => waiter; set => waiter = value; }
    }
}
