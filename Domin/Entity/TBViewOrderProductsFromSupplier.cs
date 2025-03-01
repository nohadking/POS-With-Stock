using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewOrderProductsFromSupplier
    {
        public int IdOrderProductsFromSupplier { get; set; }
     
        public int IdSupplier { get; set; }
        public int SupplierName { get; set; }
        public string PhotoSupplier { get; set; }
        public int NumberOrderProducts { get; set; }
        public DateOnly DateOrderProducts { get; set; }
        public int IdProduct { get; set; }
        public string ItemName { get; set; }
        public string PhotoItem { get; set; }
        public int Quantity { get; set; }
        public int IdUnit { get; set; }
        public string Unit { get; set; }
        public int TotalQuantity { get; set; }
        public string? Nouts { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
