using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewInvoseHeder
	{
		public int IdInvoseHeder { get; set; }
		public int InvoiceNumber { get; set; }
	
		public DateTime DateInvos { get; set; }
		public string IdUser { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool OutstandingBill { get; set; }
		public bool CurrentState { get; set; }
   
        public string PaymentMethodAr { get; set; }
        public string PaymentMethodEn { get; set; }
    }
}
