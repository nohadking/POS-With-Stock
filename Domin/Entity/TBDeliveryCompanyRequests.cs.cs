using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBDeliveryCompanyRequests
	{
		[Key]
        public int IdDeliveryCompanyRequests { get; set; }
		public int IdDeliveryCompanie { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlNameClint")]
		[MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength300")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string NameClint { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlPhonenumber")]
		[MaxLength(20, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength20")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string PhoneNumber { get; set; }
		public int IdDeliveryCompanyPricing { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlFullAddress")]
		[MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string FullAddress { get; set; }
		[MaxLength(2000, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength2000")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string? Nouts { get; set; }

		public int InvoiceNumber { get; set; }
		public DateTime DateTimeInvose { get; set; }
		public int IdPaymentMethod { get; set; }
		public int IdTypeOrder { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }

		public bool CurrentState { get; set; }







	}
}
