using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBOrderOnline
    {
        [Key]
        public int IdOrderOnline { get; set; }
        public int IdInvose { get; set; }
        public string UserId { get; set; }
        public int IdTypeOrder { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Nouts { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }



    }
}
