using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewInvose
    {
        public int IdInvose { get; set; }
        public int IdInvoseHeder { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime DateInvos { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string PhoneNumber { get; set; }
        public bool OutstandingBill { get; set; }
        public string PaymentMethodAr { get; set; }
        public string PaymentMethodEn { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public string Photo { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
