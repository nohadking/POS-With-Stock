using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBPhotoHomeSliderContent
	{
		[Key]
        public int IdPhotoHomeSliderContent { get; set; }
		public int IdHomeSliderContent { get; set; }
        public string Photo { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool CurrentState { get; set; }
	}
}
