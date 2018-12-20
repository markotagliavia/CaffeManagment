using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

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
        private float uplaceno;
        private float kusur;
        private NacinPlacanja nacinPlacanja;

        public Check()
        {
            NacinPlacanja = NacinPlacanja.KES;
            Uplaceno = 0;
            Kusur = 0;
        }

        public Check(Table table)
        {
            NacinPlacanja = NacinPlacanja.KES;
            Uplaceno = 0;
            Kusur = 0;
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
        public NacinPlacanja NacinPlacanja { get => nacinPlacanja; set => nacinPlacanja = value; }
        public float Kusur { get => kusur; set => kusur = value; }
        public float Uplaceno { get => uplaceno; set => uplaceno = value; }
    }
}
