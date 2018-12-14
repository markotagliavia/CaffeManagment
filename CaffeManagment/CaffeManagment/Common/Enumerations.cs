using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Common
{
    public class Enumerations
    {
        public enum TipPica
        {
            ALKOHOLNO,
            BEZALKOHOLNO
        }

        public enum Poreklo
        {
            DOMACE,
            STRANO
        }

        public enum State
        {
            EMPTY,
            RESERVED,
            BUSY
        }

        public enum Operation
        {
            ADD,
            EDIT,
            DELETE
        }

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
