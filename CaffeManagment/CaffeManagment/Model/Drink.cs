using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaffeManagment.Common;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.Model
{
    public class Drink
    {
        private int id;
        private TipPica tipPica;
        private Poreklo poreklo;
        private string sifraPica;
        private string nazivPica;

        public int Id
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
    }
}
