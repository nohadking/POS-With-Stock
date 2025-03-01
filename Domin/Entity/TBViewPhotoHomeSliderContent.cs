using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewPhotoHomeSliderContent
	{
		public int IdPhotoHomeSliderContent { get; set; }
		public int IdHomeSliderContent { get; set; }
		public string TitleOneAr { get; set; }
		public string TitleOneEn { get; set; }
		public string TitleTwoAr { get; set; }
		public string TitleTwoEn { get; set; }
		public string TitleButtonAr { get; set; }
		public string TitleButtonEn { get; set; }
		public string UrlButtonAr { get; set; }
		public string UrlButtonEn { get; set; }
		public string Photo { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool CurrentState { get; set; }
	}
}
