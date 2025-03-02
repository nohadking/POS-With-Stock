using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBStock
    {
        [Key] 
        public int IdStock { get; set; }//الرقم التلقائي
        public int IdWarehouse { get; set; }//id المستودع
        public string BondType { get; set; }//نوع السند يدخل كسترينج مع الحفظ أي بالمشتريات سند مشتريات 
        public int BondNumber { get; set; }//رقم السند 
        public DateOnly BondDate { get; set; }//تاريخ السند 
        public int IdProduct { get; set; }//ادي الصنف 
        public int InputQuantity { get; set; }//الكمية المدخلة
        public int OutputQuantity { get; set; }//الكمية المخرجة وتكون في المشتريات 0
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
