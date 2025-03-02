using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewOrderOnline
    {
        public int IdOrderOnline { get; set; }
        public int IdInvose { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime DateInvos { get; set; }
        public bool OutstandingBill { get; set; }
        public string PaymentMethodAr { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public int IdTypeOrder { get; set; }
        public string TypeOrderEn { get; set; }
        public string TypeOrderAr { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Nouts { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public string ReceivingMethod { get; set; }
    }
}
