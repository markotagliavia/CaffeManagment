using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Common
{
    public class Enumerations
    {
        [Serializable]
        public enum TipPica
        {
            ALKOHOLNO,
            BEZALKOHOLNO
        }
        [Serializable]
        public enum Poreklo
        {
            DOMACE,
            STRANO
        }
        [Serializable]
        public enum State
        {
            EMPTY,
            RESERVED,
            BUSY
        }
        [Serializable]
        public enum Operation
        {
            ADD,
            EDIT,
            DELETE
        }
        [Serializable]
        public enum Navigation
        {
            TABLES,
            PRICELIST,
            ADDPRICELISTITEM,
            EDITPRICELISTITEM,
            CHECKS
        }

        [Serializable]
        public enum NacinPlacanja : int
        {
            KES = 0,
            KARTICA = 1,
            CEK = 2,
            VIRMAN = 4
        }

        [Serializable]
        public enum PoreskaGrupa : int
        {
            A = 0,
            G = 1,
            D = 2,
            DJ = 4,
            E = 5,
            ZH = 6,
            I = 7,
            J = 8,
            K = 9
        }
    }
}
