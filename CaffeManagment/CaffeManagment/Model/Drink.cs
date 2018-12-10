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
            Id = Guid.NewGuid();
        }

        public Guid Id { get => id; set => id = value; }
        public TipPica TipPica { get => tipPica; set => tipPica = value; }
        public Poreklo Poreklo { get => poreklo; set => poreklo = value; }
        public string SifraPica { get => sifraPica; set => sifraPica = value; }
        public string NazivPica { get => nazivPica; set => nazivPica = value; }

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
}
