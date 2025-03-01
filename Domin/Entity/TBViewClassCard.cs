using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewClassCard
    {
        public int IdClassCard { get; set; }
        public int IdUnit { get; set; }
        public string Unit { get; set; }
        public string ItemName { get; set; }
        public string Photo { get; set; }
        public string CodeItem { get; set; }
        public DateOnly? ProductionDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
