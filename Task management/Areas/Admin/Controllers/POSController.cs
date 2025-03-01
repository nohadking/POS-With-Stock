using Grpc.Core;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cashier")]
    public class POSController : Controller
    {
        IICategory iCategory;
        IIProduct iProduct;
		IIInvoseHeder iInvoseHeder;
		IIUserInformation iUserInformation;
		IIPaymentMethod iPaymentMethod;
        IIInvose iInvose;
        IICompanyInformation iCompanyInformation;
        public POSController(IICategory iCategory1,IIProduct iProduct1,IIInvoseHeder iInvoseHeder1, IIUserInformation iUserInformation1,IIPaymentMethod iPaymentMethod1,IIInvose iInvose1,IICompanyInformation iCompanyInformation1)
        {
            iCategory = iCategory1;
            iProduct = iProduct1;
			iInvoseHeder = iInvoseHeder1;
			iUserInformation = iUserInformation1;
			iPaymentMethod = iPaymentMethod1;
            iInvose = iInvose1;
            iCompanyInformation = iCompanyInformation1;


        }
        public IActionResult MyPOS()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCategory = iCategory.GetAll();
            vmodel.ListViewProduct = iProduct.GetAll();
			var numberinvose = vmodel.ListViewInvoseHede = iInvoseHeder.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.InvoiceNumber) + 1
        : 1;
            ViewBag.user = iUserInformation.GetAllByRole("Customer");
			vmodel.ListPaymentMethod = iPaymentMethod.GetAllActive();

            var payMeth = vmodel.ListPaymentMethod.FirstOrDefault(p => p.PaymentMethodAr.Contains("نقد"));
            TempData["idForPay"] = payMeth.IdPaymentMethod;
			TempData["ArDesForPay"] = payMeth.PaymentMethodAr;

            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel); 
        }

		public IActionResult GetProductsByCategory(int IdCategory)
		{
			if (IdCategory <= 0)
			{
				return BadRequest("Invalid category ID");
			}

			var products = iProduct.GetAllv(IdCategory);
			if (products == null || !products.Any())
			{
				return NotFound("No products found for this category");
			}

			// عرض المنتجات باستخدام PartialView
			return PartialView("_ProductsPartial", products);
		}



        //[HttpPost]
        //public async Task<IActionResult> PrintReceipt([FromBody] string receiptContent)
        //{
        //    if (string.IsNullOrEmpty(receiptContent))
        //    {
        //        return RedirectToAction("MyPOS");
        //    }

        //    // تسجيل محتوى الفاتورة للتأكد من وصول البيانات بشكل صحيح
        //    Console.WriteLine(receiptContent); // أو استخدام أي أداة تصحيح أخرى

        //    // تابع العمليات الأخرى
        //    PrintDocument pd = new PrintDocument();
        //    pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";

        //    pd.PrintPage += (sender, e) =>
        //    {
        //        Font headerFont = new Font("Arial", 14, FontStyle.Bold);
        //        Font bodyFont = new Font("Arial", 12);
        //        e.Graphics.DrawString("Receipt", headerFont, Brushes.Black, 100, 50);
        //        e.Graphics.DrawString(receiptContent, bodyFont, Brushes.Black, 100, 100);
        //        e.Graphics.DrawString("VAT Payable through central registration", bodyFont, Brushes.Black, 100, 200);
        //        e.Graphics.DrawString("Thank you for shopping with us!", bodyFont, Brushes.Black, 100, 230);
        //    };

        //    await Task.Run(() => pd.Print());

        //    return RedirectToAction("MyPOS");
        //}.


        [HttpPost]
        public async Task<IActionResult> PrintReceipt([FromBody] ReceiptData receiptData)
        {
            if (string.IsNullOrEmpty(receiptData.ReceiptContent) || string.IsNullOrEmpty(receiptData.InvoiceNo))
            {
                return RedirectToAction("MyPOS");
            }
            // تسجيل محتوى الفاتورة للتأكد من وصول البيانات بشكل صحيح
            Console.WriteLine("Invoice No: " + receiptData.InvoiceNo); // رقم الفاتورة
            Console.WriteLine("Receipt Content: " + receiptData.ReceiptContent); // محتوى الفاتورة
            // جلب بيانات الفاتورة
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewInvose = iInvose.GetByInvoiceNumber(Convert.ToInt32(receiptData.InvoiceNo));
            // التأكد من وجود بيانات الفاتورة
            if (vmodel.ListViewInvose == null || !vmodel.ListViewInvose.Any())
            {
                return RedirectToAction("MyPOS");
            }
            // إعداد معلومات الشركة
            string companyName = "My Company Name";
            string companyAddress = "123 Main Street, City, Country";
            string companyPhone = "Phone: +123456789";
            string companyTaxInfo = "VAT No: 123456789";
            decimal totalAmount = 0;
            // إعداد مستند الطباعة
            //PrintDocument pd = new PrintDocument
            //{
            //    PrinterSettings = { PrinterName = "Microsoft Print to PDF" }
            //};
            //PrintDocument pd = new PrintDocument
            //{
            //    PrinterSettings = { PrinterName = null } // استخدام الطابعة الافتراضية
            //};

            // إعداد مستند الطباعة
            PrintDocument pd = new PrintDocument
            {
                PrinterSettings = { PrinterName = new PrinterSettings().PrinterName } // تعيين الطابعة الافتراضية
            };
            // تعيين حجم الورق الحراري (عرض 8 سم - 80 مم)
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 320, 1180); // 80mm x 118mm تقريبا كحجم ورقة حرارية
            pd.PrintPage += (sender, e) =>
            {
                Font headerFont = new Font("Arial", 10, FontStyle.Bold);
                Font bodyFont = new Font("Arial", 8);
                Font totalFont = new Font("Arial", 8, FontStyle.Bold);
                int yPosition = 10; // الموضع الأولي للطباعة
                int leftMargin = 10; // الهامش الأيسر
                // إضافة هيدر الفاتورة بمعلومات الشركة
                e.Graphics.DrawString(companyName, headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                e.Graphics.DrawString(companyAddress, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
                e.Graphics.DrawString(companyPhone, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
                e.Graphics.DrawString(companyTaxInfo, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;
               // إضافة عنوان الفاتورة
                e.Graphics.DrawString("Invoice Receipt", headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                e.Graphics.DrawString($"Invoice No: {receiptData.InvoiceNo}", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
                e.Graphics.DrawString($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                // طباعة تفاصيل المنتجات كجدول
                e.Graphics.DrawString("Product Details:", headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
                // رأس الجدول
                e.Graphics.DrawString("الصنف", bodyFont, Brushes.Black, leftMargin, yPosition);
                e.Graphics.DrawString("الكمية", bodyFont, Brushes.Black, leftMargin + 65, yPosition); // المسافة بين الأعمدة
                e.Graphics.DrawString("السعر", bodyFont, Brushes.Black, leftMargin + 90, yPosition);
                e.Graphics.DrawString("الأجمالي", bodyFont, Brushes.Black, leftMargin + 140, yPosition);
                yPosition += 15;

                e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;

                foreach (var product in vmodel.ListViewInvose)
                {
                    // طباعة تفاصيل كل منتج
                    e.Graphics.DrawString(product.ProductNameAr, bodyFont, Brushes.Black, leftMargin, yPosition);
                    e.Graphics.DrawString(product.Quantity.ToString(), bodyFont, Brushes.Black, leftMargin + 65, yPosition);
                    e.Graphics.DrawString($"{product.price:F3}", bodyFont, Brushes.Black, leftMargin + 90, yPosition);
                    e.Graphics.DrawString($"{product.total:F3}", bodyFont, Brushes.Black, leftMargin + 140, yPosition);

                    yPosition += 15;

                    totalAmount += product.total; // حساب المجموع الإجمالي
                }
                e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;

                // طباعة المجاميع
                //e.Graphics.DrawString($"Total Amount: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                //e.Graphics.DrawString($"VAT (15%): {totalAmount * 0.15M:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                //e.Graphics.DrawString($"Grand Total: {totalAmount * 1.15M:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;

                e.Graphics.DrawString($"Total Amount: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                //e.Graphics.DrawString($"VAT (15%): {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
                e.Graphics.DrawString($"Grand Total: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;

                // إضافة رسالة شكر
                e.Graphics.DrawString("Thank you for shopping with us!", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
            };

            // تنفيذ عملية الطباعة
            await Task.Run(() => pd.Print());
            // إعادة التوجيه إلى صفحة MyPOS بعد الطباعة
            return RedirectToAction("MyPOS");
        }
        // تعريف الكلاس لاستقبال البيانات
        public class ReceiptData
        {
            public string ReceiptContent { get; set; }
            public string InvoiceNo { get; set; }
        }

    }
}
