using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBExpense
    {
        [Key]
        public int IdExpense { get; set; }
        public int IdExpenseCategory { get; set; }
        public int IdLevelForeAccount { get; set; }
        public int BondNumber { get; set; }
        public DateOnly DateBond { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlStatement")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Statement { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }  
        public bool CurrentState { get; set; }
        public decimal Amount { get; set; }
    }
}
