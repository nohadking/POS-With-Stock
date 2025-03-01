using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewPurchase
	{
        public int IdPurchase { get; set; }
        public int IdSupplier { get; set; }
        public string PhotoSupplier { get; set; }
        public string SupplierName { get; set; }
        public int IdPaymentMethod { get; set; }
        public string PaymentMethodAr { get; set; }
        public string Statement { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public int PurchaseNumber { get; set; }
        public int? PurchaseSubNumber { get; set; }
        public int IdProduct { get; set; }
        public string ItemName { get; set; }
        public string PhotoItem { get; set; }
        public string CodeItem { get; set; }
        public int IdUnit { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public int? FreeQuantity { get; set; }
        public int AllQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
		public decimal Total { get; set; }
		public decimal? SingleDiscount { get; set; }
		public decimal? shipping { get; set; }
		public string? Nouts { get; set; }
		public int TotalQuantity { get; set; }
		public decimal TotalDiscount { get; set; }
		public decimal TotalAll { get; set; }
		public string DataEntry { get; set; }
		public DateTime DateTimeEntry { get; set; }
		public bool CurrentState { get; set; }
	}
}
