using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewStock
    {
        public int IdStock { get; set; }
        public int IdWarehouse { get; set; }
        public string WarehouseName { get; set; }
        public string BondType { get; set; }
        public int BondNumber { get; set; }
        public DateOnly BondDate { get; set; }
        public int IdProduct { get; set; }
        public string Photo { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public int InputQuantity { get; set; }
        public int OutputQuantity { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
