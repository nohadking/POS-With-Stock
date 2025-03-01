using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBDeliveryCompanyPricing
	{
		[Key]
        public int IdDeliveryCompanyPricing { get; set; }
		public int IdDeliveryCompanie { get; set; }
		public int IdArea { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlPricing")]
		public decimal Pricing { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool CurrentState { get; set; }

	}
}
