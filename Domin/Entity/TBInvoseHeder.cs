using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBInvoseHeder
    {
        [Key]
        public int IdInvoseHeder { get; set; }
        public int InvoiceNumber{ get; set; }
		
        public int IdPaymentMethod { get; set; }
        public DateTime DateInvos { get; set; }
        public string IdUser { get; set; }	
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool OutstandingBill { get; set; }
		public bool CurrentState { get; set; }


	}
}
