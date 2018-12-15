﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    [Serializable]
    public class Check
    {
        private string waiter;
        private DateTime dateTime;
        private ObservableCollection<DrinkWithPriceAndQuantity> pica;
        private int id;
        private bool storniran;
        private float ukupnoPara;
        private string nazivStola;

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
        public ObservableCollection<DrinkWithPriceAndQuantity> Pica
        {
            get { return pica; }
            set { pica = value; }
        }
        public string Waiter
        {
            get { return waiter; }
            set { waiter = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public bool Storniran
        {
            get { return storniran; }
            set { storniran = value; }
        }

        public float UkupnoPara { get => ukupnoPara; set => ukupnoPara = value; }
        public string NazivStola { get => nazivStola; set => nazivStola = value; }
    }
}
