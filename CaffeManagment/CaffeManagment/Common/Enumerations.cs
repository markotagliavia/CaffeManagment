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
    }
}
