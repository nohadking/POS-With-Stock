using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBPurchase 
    {
        [Key] 
        public int IdPurchase { get; set; }//الرقم العام 
        public int IdSupplier { get; set; }//معرف المورد
        public int IdPaymentMethod { get; set; }//معرف طريقة الدفع
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlStatementPurchase")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Statement { get; set; } //البيان
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlPurchaseDate")]
        public DateOnly PurchaseDate { get; set; }//تاريخ السند
        public int PurchaseNumber { get; set; }//رقم السند
        public int? PurchaseSubNumber { get; set; }//الرقم الفرعي للسند
        public int IdProduct { get; set; }//المعرف للمنتج
        public int IdUnit { get; set; }//المعرف لوحدة المنتج
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlQuantity")]
        public int Quantity { get; set; }//الكمية
        public int? FreeQuantity { get; set; }/// <summary>
        /// /الكمية المجانية
        /// </summary>
        public int AllQuantity { get; set; }//مجموع الكمية + الكمية المجانية
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlPurchasePrice")]
        public decimal PurchasePrice { get; set; }//سعر الشراء 
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlTotal")]
        public decimal Total { get; set; }//سعر الشراء *الكمية
        public decimal? SingleDiscount { get; set; }//الاخصم الافرادي علىالمنتج
        public decimal? shipping { get; set; }//مصاريف الشحن والتوصيل
        [MaxLength(500, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength500")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string? Nouts { get; set; }//ملاحظات السند 
        public int TotalQuantity { get; set; }//إجمالي الكمية في كامل السند 
        public decimal TotalDiscount { get; set; }//أجمالي الخصم في كامل السند 
        public decimal TotalAll { get; set; }// الاجمالي العام بعد الخصم لكامل السند 
        public string DataEntry { get; set; }//وقت وتاريخ ادخال السند
        public DateTime DateTimeEntry { get; set; }//مدخل البيانات
        public bool CurrentState { get; set; }//حالة السند 






    }
}
