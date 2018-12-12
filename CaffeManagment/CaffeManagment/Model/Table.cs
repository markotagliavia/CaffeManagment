﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Model
{
    public class Table
    {
        private string oznakaStola;

        private string waiter;

        private State stanjeStola;

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

        public State StanjeStola
        {
            get { return stanjeStola; }
            set { stanjeStola = value; }
        }
    }
}
