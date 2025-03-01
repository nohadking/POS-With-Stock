using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBBrandProduct
	{
		[Key]
        public int IdBrandProduct { get; set; }
		public string Photo { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlTitelOneAr")]
		[MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLengthTitel300")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string TitelOneAr { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlTitelOneEn")]
		[MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLengthTitel300")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string TitelOneEn { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlUrlButtonEn")]
		[MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
		[MinLength(1, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength1")]
		public string URlButtonEn { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlUrlButtonAr")]
		[MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
		[MinLength(1, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength1")]
		public string URlButtonAr { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public string DataEntry { get; set; }
		public bool CurrentState { get; set; }
	}
}
