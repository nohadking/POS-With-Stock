using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewSupplier
    {
        public int IdSupplier { get; set; }
        public int IdPaymentMethod { get; set; }
        public long NumberAccount { get; set; }
        public string PaymentMethodAr { get; set; }
        public string Photo { get; set; }
       
        public string SupplierName { get; set; }
    
        public string Specialization { get; set; }
      
        public string? Phone { get; set; }
      
        public string Mobile { get; set; }
      
        public string NameOner { get; set; }
      
        public string PhoneOner { get; set; }
      
        public string EmailOner { get; set; }
       
        public string EmailCompany { get; set; }
     
        public string Address { get; set; }
      
        public decimal Debtlimit { get; set; }
     
        public int GracePeriod { get; set; }
     
        public string website { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
