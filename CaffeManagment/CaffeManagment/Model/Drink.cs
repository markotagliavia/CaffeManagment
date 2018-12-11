using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Model
{
    public class Drink : ValidationBase
    {
        private Guid id;
        private TipPica tipPica;
        private Poreklo poreklo;
        private string sifraPica;
        private string nazivPica;

        public Drink()
        {
            id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public TipPica TipPica
        {
            get { return tipPica; }
            set { tipPica = value; }
        }
        public Poreklo Poreklo
        {
            get { return poreklo; }
            set { poreklo = value; }
        }
        public string SifraPica
        {
            get { return sifraPica; }
            set { sifraPica = value; }
        }
        public string NazivPica
        {
            get { return nazivPica; }
            set { nazivPica = value; }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.NazivPica))
            {
                this.ValidationErrors[nameof(NazivPica)] = "Naziv pića je obavezno polje.";
            }
            if (string.IsNullOrWhiteSpace(this.SifraPica))
            {
                this.ValidationErrors[nameof(SifraPica)] = "Šifra pića je obavezno polje.";
            }
            //to do da je unique
        }
    }

    public class DrinkWithPriceAndQuantity
    {
        private string naziv;
        private int kolicina;
        private float cena;

        public DrinkWithPriceAndQuantity(string naziv, int kolicina, float cena)
        {
            naziv = this.naziv;
            kolicina = this.kolicina;
            cena = this.cena;
        }

        public int Kolicina { get => kolicina; set => kolicina = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public float Cena { get => cena; set => cena = value; }
    }
}
