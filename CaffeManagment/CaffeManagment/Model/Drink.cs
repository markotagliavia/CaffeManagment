using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Model
{
    [Serializable]
    public class Drink : ValidationBase
    {
        private Guid id;
        private TipPica tipPica;
        private Poreklo poreklo;
        private string sifraPica;
        private string nazivPica;
        private float acutelPrice;
        private float popust;
        private int stand;
        private int grupaArtikla;
        private PoreskaGrupa poreskaGrupa;

        public Drink()
        {
            id = Guid.NewGuid();
            Stand = 1;
            GrupaArtikla = 1;
            Popust = 0;
            PoreskaGrupa = Enumerations.PoreskaGrupa.DJ;
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


        public float AcutelPrice { get => acutelPrice; set => acutelPrice = value; }
        public float Popust { get => popust; set => popust = value; }
        public int Stand { get => stand; set => stand = value; }
        public int GrupaArtikla { get => grupaArtikla; set => grupaArtikla = value; }
        public PoreskaGrupa PoreskaGrupa { get => poreskaGrupa; set => poreskaGrupa = value; }

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
            
            var priceList = DataSourceUtil.Instance.ReadPriceList();
            if (priceList.Items.Values.Any(x => x.SifraPica.Equals(this.SifraPica) && x.Id != this.Id))
            {
                this.ValidationErrors[nameof(SifraPica)] = "Šifra pića je jedinstveno polje.";
            }

            try
            {
                int p = Int32.Parse(this.sifraPica);
                if (p < 1 || p > 11956)
                {
                    this.ValidationErrors[nameof(SifraPica)] = "Šifra pića mora biti broj 1-11956.";
                }
            }
            catch (Exception exe)
            {
                this.ValidationErrors[nameof(SifraPica)] = "Šifra pića mora biti broj 1-11956.";
            }
        }
    }

    [Serializable]
    public class DrinkWithPriceAndQuantity
    {
        private string naziv;
        private string sifra;
        private int kolicina;
        private float cena;
        private PoreskaGrupa poreskaGrupa;

        public DrinkWithPriceAndQuantity(string naziv, string sifra, int kolicina, float cena, PoreskaGrupa pg)
        {
            this.naziv = naziv;
            this.kolicina = kolicina;
            this.cena = cena;
            this.sifra = sifra;
            this.poreskaGrupa = pg;
        }

        public int Kolicina { get => kolicina; set => kolicina = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public float Cena { get => cena; set => cena = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public PoreskaGrupa PoreskaGrupa { get => poreskaGrupa; set => poreskaGrupa = value; }
    }
}
