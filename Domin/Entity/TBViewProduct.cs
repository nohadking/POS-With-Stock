using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewProduct
	{
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public string Photo { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public decimal price { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool Active { get; set; }
		public bool CurrentState { get; set; }
	}
}
