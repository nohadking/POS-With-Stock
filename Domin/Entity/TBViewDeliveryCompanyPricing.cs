using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewDeliveryCompanyPricing
	{
        public int IdDeliveryCompanyPricing { get; set; }
        public int IdDeliveryCompanie { get; set; }
        public string DeliveryCompanie { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public int IdArea { get; set; }
        public string CitieName { get; set; }
        public string AreaName { get; set; }
		public decimal Pricing { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool CurrentState { get; set; }



	}
}
