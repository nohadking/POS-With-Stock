using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBAccountingRestriction
    {
        [Key]
        public int IdaccountingRestrictions { get; set; }
        public int NumberaccountingRestrictions { get; set; } // max number + 1 
        public string AccountingName { get; set; }  // supllier
        public string BondType { get; set; } // سند شرا
        public int BondNumber { get; set; } // سند شرا
                                             // ء 
        public decimal Debtor { get; set; } // 0
        public decimal creditor { get; set; } // القيمة الاجمالية للسند total price for purcheas
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlStatementPurchase")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Statement { get; set; } // 
        [MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string? Nouts { get; set; } // 
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}

