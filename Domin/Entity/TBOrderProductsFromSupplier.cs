using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBOrderProductsFromSupplier
    {
        [Key]
        public int IdOrderProductsFromSupplier { get; set; }
        public int IdSupplier { get; set; }
        public int NumberOrderProducts { get; set; }
        public DateOnly DateOrderProducts { get; set; }
        public int IdProduct { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlQuantity")]
        public int Quantity { get; set; }
        public int IdUnit { get; set; }
        public int TotalQuantity { get; set; }  
        [MaxLength(2000, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength2000")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string? Nouts { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
