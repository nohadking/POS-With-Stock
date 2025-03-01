
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Linq;
using PdfSharp.Fonts;
using System.IO;
using System.Reflection;
using PdfSharpCore.Fonts;
using System.IO;
using PdfSharp.Pdf;
using Infarstuructre.ViewModel;
using MimeKit;
using PdfSharpCore.Pdf;
using Microsoft.Graph.Models.Security;
using static Task_management.Areas.Admin.Controllers.POSController;
using System.Drawing.Printing;
using System.Drawing;
namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoseController : Controller
    {
        IIInvose iInvose;
        IICompanyInformation iCompanyInformation;
        MasterDbcontext dbcontext;
        public InvoseController(IIInvose iInvose1, IICompanyInformation iCompanyInformation1,MasterDbcontext dbcontext1)
        {
            iInvose = iInvose1;
            iCompanyInformation = iCompanyInformation1;
            dbcontext = dbcontext1;
        }
        public IActionResult MyInvose()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewInvose = iInvose.GetAll();
            // جلب جميع البيانات
            var allInvoices = iInvose.GetAll();

            // استخراج أسماء مدخلي البيانات بدون تكرار
            var uniqueCashers = allInvoices.Select(i => i.DataEntry).Distinct().ToList();

            // تمرير البيانات إلى ViewBag
            ViewBag.CasherName = uniqueCashers;
            //طرق الدفع
            var PymentMoted = allInvoices.Select(i => i.PaymentMethodAr).Distinct().ToList();
            ViewBag.PayMoTh = PymentMoted;


            //تاريخ الفاتورة 
            var distinctDates = allInvoices
                .Select(i => i.DateTimeEntry.Date)
                .Distinct()
                .Select(d => d.ToString("yyyy-MM-dd"))
                .ToList();

            ViewBag.dateTai = distinctDates;

            return View(vmodel);
        }
        //علي شوف هي الفنثكشن  ضروري 
        [HttpGet]
        public IActionResult GeneratePdf(string? cacherName, string? payMeth, string? oneDate, string? search, string startDate, string endDate)
        {
            // **1. جلب البيانات من الجداول**
            // هنا يمكنك إضافة منطق جلب البيانات من قاعدة البيانات
            // var products = _context.Products.Select(p => new { p.Name, p.Price }).ToList();
            // var orders = _context.Orders.Select(o => new { o.Id, o.TotalAmount }).ToList();
           
             

             
            

            // **2. إنشاء ملف PDF**
            if (cacherName == null && payMeth == null && oneDate == null && search == null && startDate == null && endDate == null)
            {
               var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                return pdf;
            }
            else
            {
                if (search != null)
                {
                    // حسب كلمة بحث
                    var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                    return pdf;
                }
                else if (cacherName == null && payMeth != null && oneDate == null && search == null && startDate == null && endDate == null)
                {
                    // حسب طريقة دفع
                    var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                    return pdf;
                }
                else if (startDate != null && endDate != null)
                {
                    if (cacherName != null)
                    {
                        if (payMeth != null)
                        {
                            // حسب اسم كاشير وطريقة دفع ومن تاريخ لتاريخ  
                            var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                            return pdf;
                        }
                        else
                        {
                            // حسب اسم كاشير ومن تاريخ لتاريخ  
                            var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                            return pdf;
                        }
                    }
                    else
                    {
                        if (payMeth != null)
                        {
                            // حسب طريقة دفع ومن تاريخ لتاريخ  
                            var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                            return pdf;
                        }
                        else
                        {
                            // من تاريخ لتاريخ
                            var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                            return pdf;
                        }
                    }
                }
                else
                {
                    if (cacherName != null)
                    {
                        if (payMeth != null)
                        {
                            if (oneDate != null)
                            {
                                // حسب اسم كاشير وطريقة دفع وتاريخ يوم محدد
                                var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                                return pdf;
                            }
                            else
                            {
                                // حسب اسم كاشير وطريقة دفع
                                var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                                return pdf;
                            }
                        }
                        else
                        {
                            if (oneDate != null)
                            {
                                // حسب اسم كاشير ويوم محدد
                                var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                                return pdf;
                            }
                            else
                            {
                                // حسب اسم كاشير  
                                var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                                return pdf;
                            }
                        }
                    }
                    else
                    {
                        if (oneDate != null)
                        {
                            // حسب يوم محدد
                            var pdf = CreatePDF(cacherName, payMeth, oneDate, search, startDate, endDate);
                            return pdf;
                        }
                    }
                }
            }
            return Content("Invalid parameters or no data found.", "text/plain");
        }


        public IActionResult CreatePDF(string? cacherName, string? payMeth, string? oneDate, string? search, string startDate, string endDate)
        {
            string cachname = string.Empty;
            if (cacherName != null)
            {
                cachname = dbcontext.VwUsers
                                     .Where(a => a.Email == cacherName)
                                     .Select(a => a.Name)
                                     .FirstOrDefault();
            }
            
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            var compny = dbcontext.TBCompanyInformations.FirstOrDefault();
            var products = new List<TBViewInvose>();

            var pdfDocument = Document.Create(container =>
            {

                // **حساب الإجماليات**
                if (cacherName == null && payMeth == null && oneDate == null && search == null && startDate == null && endDate == null)
                {
                   products = vmodel.ListViewInvose = iInvose.GetAll();
                }
                else
                {
                    if (search != null)
                    {
                        // حسب كلمة بحث
                        products = vmodel.ListViewInvose = iInvose.GetBySearchWord(search);
                    }
                    else if (cacherName == null && payMeth != null && oneDate == null && search == null && startDate == null && endDate == null)
                    {
                        // حسب طريقة دفع
                        products = vmodel.ListViewInvose = iInvose.GetByPayMeth(payMeth);
                    }
                    else if (startDate != null && endDate != null)
                    {
                        var start = Convert.ToDateTime(startDate);
                        var end = Convert.ToDateTime(endDate);
                        if (cacherName != null)
                        {
                            if (payMeth != null)
                            {
                                // حسب اسم كاشير وطريقة دفع ومن تاريخ لتاريخ  
                                products = vmodel.ListViewInvose = iInvose.GetByCasherNameAndPayMethodAndPeriodDate(cacherName, payMeth, start, end);
                            }
                            else
                            {
                                // حسب اسم كاشير ومن تاريخ لتاريخ  
                                products = vmodel.ListViewInvose = iInvose.GetByCasherNameAndPeriodDate(cacherName, start, end);
                            }
                        }
                        else
                        {
                            if (payMeth != null)
                            {
                                // حسب طريقة دفع ومن تاريخ لتاريخ  
                                products = vmodel.ListViewInvose = iInvose.GetByPayMethAndPeriodDate(payMeth, start, end);
                            }
                            else
                            {
                                // من تاريخ لتاريخ
                                products = vmodel.ListViewInvose = iInvose.GetByPeriodDate(start, end);
                            }
                        }
                    }
                    else
                    {
                        if (cacherName != null)
                        {
                            if (payMeth != null)
                            {
                                if (oneDate != null)
                                {
                                    // حسب اسم كاشير وطريقة دفع وتاريخ يوم محدد
                                    var date = Convert.ToDateTime(oneDate);
                                    products = vmodel.ListViewInvose = iInvose.GetByCasherNameAndPayMethAndDateTimeEntry(cacherName, payMeth, date);
                                }
                                else
                                {
                                    // حسب اسم كاشير وطريقة دفع
                                    products = vmodel.ListViewInvose = iInvose.GetByCacherNameAndPay(cacherName, payMeth);
                                }
                            }
                            else
                            {
                                if (oneDate != null)
                                {
                                    // حسب اسم كاشير ويوم محدد
                                    var date = Convert.ToDateTime(oneDate);
                                    products = vmodel.ListViewInvose = iInvose.GetByCacherNameOneDate(cacherName, date);
                                }
                                else
                                {
                                    // حسب اسم كاشير  
                                    products = vmodel.ListViewInvose = iInvose.GetByCacherName(cacherName);
                                }
                            }
                        }
                        else
                        {
                            if (oneDate != null)
                            {
                                // حسب يوم محدد
                                var date = Convert.ToDateTime(oneDate);
                                products = vmodel.ListViewInvose = iInvose.GetByDateTimeEntry(date);
                            }
                        }
                    }
                }

                var totalQuantity = products.Sum(p => p.Quantity);
                var totalAmount = products.Sum(p => p.total);

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                            .Column(header =>
                            {
                                // الترويسة العليا للفاتورة
                                if (cacherName == null && payMeth == null && oneDate == null && search == null && startDate == null && endDate == null)
                                {
                                    header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير ").FontSize(20).Bold();
                                }
                                else
                                {
                                    if (search != null)
                                    {
                                        // حسب كلمة بحث
                                        header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير حسب: {search}").FontSize(20).Bold();
                                    }
                                    else if (cacherName == null && payMeth != null && oneDate == null && search == null && startDate == null && endDate == null)
                                    {
                                        // حسب طريقة دفع
                                        header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير بطريقة الدفع:  {payMeth}").FontSize(20).Bold();
                                    }
                                    else if (startDate != null && endDate != null)
                                    {
                                        var start = Convert.ToDateTime(startDate);
                                        var end = Convert.ToDateTime(endDate);
                                        if (cacherName != null)
                                        {
                                            if (payMeth != null)
                                            {
                                                // حسب اسم كاشير وطريقة دفع ومن تاريخ لتاريخ  
                                                header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف  {cachname} بطريقة الدفع {payMeth} من تاريخ{startDate} لتاريخ {endDate}").FontSize(20).Bold();
                                            }
                                            else
                                            {
                                                // حسب اسم كاشير ومن تاريخ لتاريخ  
                                                header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف  {cachname} من تاريخ{startDate} لتاريخ {endDate}").FontSize(20).Bold();
                                            }
                                        }
                                        else
                                        {
                                            if (payMeth != null)
                                            {
                                                // حسب طريقة دفع ومن تاريخ لتاريخ  
                                                header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير بطريقة الدفع {payMeth} من تاريخ{startDate} لتاريخ {endDate}").FontSize(20).Bold();
                                            }
                                            else
                                            {
                                                // من تاريخ لتاريخ
                                                header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير من تاريخ{startDate} لتاريخ {endDate}").FontSize(20).Bold();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cacherName != null)
                                        {
                                            if (payMeth != null)
                                            {
                                                if (oneDate != null)
                                                {
                                                    // حسب اسم كاشير وطريقة دفع وتاريخ يوم محدد
                                                    header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف {cachname} بطريقة الدفع {payMeth} لتاريخ {oneDate}").FontSize(20).Bold();
                                                }
                                                else
                                                {
                                                    // حسب اسم كاشير وطريقة دفع
                                                    header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف {cachname} بطريقة الدفع {payMeth}").FontSize(20).Bold();
                                                }
                                            }
                                            else
                                            {
                                                if (oneDate != null)
                                                {
                                                    // حسب اسم كاشير ويوم محدد
                                                    header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف {cachname} لتاريخ {oneDate}").FontSize(20).Bold();
                                                }
                                                else
                                                {
                                                    // حسب اسم كاشير محدد
                                                    header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير للموظف {cachname}").FontSize(20).Bold();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (oneDate != null)
                                            {
                                                // حسب يوم محدد
                                                header.Item().Border(1).AlignCenter().Text($"تقرير الفواتير لتاريخ {oneDate}").FontSize(20).Bold();

                                            }
                                        }
                                    }
                                }
          
                                if (compny != null)
                                {
                                    header.Item().Border(1).AlignCenter().Text($" {compny.NameCompanyAr}").FontSize(14);
                                    header.Item().Border(1).AlignCenter().Text($" {compny.AddressAr}").FontSize(12);
                                    header.Item().Border(1).AlignCenter().Text($" {compny.Mobile}").FontSize(12);
                                }
                            });

                    page.Content().Column(content =>
                    {
                        content.Item().AlignCenter().Text($"تقرير الفواتير").FontSize(16).Bold();
                        content.Item().AlignCenter().Text("----------------------------------------------").FontSize(12).Bold();
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {     
                                columns.ConstantColumn(100); // وقت وتاريخ الإدخال
                                columns.ConstantColumn(100); // مدخل البيانات
                                columns.ConstantColumn(50);  // الإجمالي
                                columns.ConstantColumn(50);  // السعر
                                columns.ConstantColumn(50);  // الكمية
                                columns.RelativeColumn(100); // اسم المنتج
                                columns.ConstantColumn(40);  // رقم الفاتورة
                            });

                            table.Header(header =>
                            {               
                                header.Cell().Border(1).AlignCenter().Text("وقت وتاريخ").Bold();
                                header.Cell().Border(1).AlignCenter().Text("مدخل البيانات").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الإجمالي").Bold();
                                header.Cell().Border(1).AlignCenter().Text("السعر").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الكمية").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الصنف").Bold();
                                header.Cell().Border(1).AlignCenter().Text("ر.ف").Bold();
                            });

                            foreach (var product in products)
                            {                     
                                table.Cell().Border(1).AlignCenter().Text(product.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
                                table.Cell().Border(1).AlignCenter().Text(cachname);
                                table.Cell().Border(1).AlignCenter().Text($"${product.total}");
                                table.Cell().Border(1).AlignCenter().Text($"${product.price}");
                                table.Cell().Border(1).AlignCenter().Text(product.Quantity.ToString());
                                table.Cell().Border(1).AlignCenter().Text(product.ProductNameAr);
                                table.Cell().Border(1).AlignCenter().Text(product.InvoiceNumber.ToString());
                            }
                        });

                        content.Item().PaddingTop(10);


                        // **إضافة الفوتر في نهاية التقرير**
                        content.Item().PaddingTop(20).Table(table =>
                        {
                            // تعريف الأعمدة
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // العمود الأول: مجموع الكمية
                                columns.RelativeColumn(); // العمود الثاني: المجموع العام
                            });

                            // المسميات في السطر الأول
                            table.Header(header =>
                            {
                                header.Cell().AlignCenter().Text("مجموع الكمية").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("المجموع العام").FontSize(12).Bold();
                            });

                            // القيم في السطر الثاني
                            table.Cell().Border(1).AlignCenter().Text($"{totalQuantity}").FontSize(12);
                            table.Cell().Border(1).AlignCenter().Text($"${totalAmount}").FontSize(12);
                        });

                        // إضافة تاريخ الطباعة أسفل التقرير
                        content.Item().PaddingTop(10).AlignRight().Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(10).Bold();
                    });


                });
            });
            var pdfData = pdfDocument.GeneratePdf();
            return File(pdfData, "application/pdf", "Report.pdf");
        }



        public async Task<IActionResult> CreateDirectPdf(int num)
        {
            if (num == null)
            {
                return RedirectToAction("MyInvose");
            }

            var invoice = dbcontext.ViewInvose.FirstOrDefault(i => i.InvoiceNumber == num);
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewInvose = iInvose.GetByInvoiceNumber(Convert.ToInt32(num));

            if (vmodel.ListViewInvose == null || !vmodel.ListViewInvose.Any())
            {
                return RedirectToAction("MyInvose");
            }

            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            var company = vmodel.ListCompanyInformation.FirstOrDefault();
            string companyName = company?.NameCompanyAr ?? "N/A";
            string companyAddress = company?.AddressAr ?? "N/A";
            string companyPhone = company?.Phone ?? "N/A";
            string companyTaxInfo = "VAT No: 123456789";

            decimal totalAmount = 0;
            int totalQuantity = 0;

            // إنشاء مستند PDF
            var pdfDocument = Document.Create(container =>
            {
                var products = vmodel.ListViewInvose;

                totalQuantity = products.Sum(p => p.Quantity);
                totalAmount = products.Sum(p => p.total);

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // رأس الصفحة (Header)
                    page.Header()
                        .Column(header =>
                        {
                            header.Item().AlignCenter().Text("تقرير الفاتورة").FontSize(20).Bold();
                            header.Item().AlignCenter().Text(companyName).FontSize(14);
                            header.Item().AlignCenter().Text(companyAddress).FontSize(12);
                            header.Item().AlignCenter().Text(companyPhone).FontSize(12);
                        });

                    // محتوى الفاتورة (Content)
                    page.Content().Column(content =>
                    {
                        content.Item().AlignCenter().Text($"رقم الفاتورة: {num}").FontSize(16).Bold();
                        content.Item().AlignCenter().Text($"تاريخ الفاتورة: {DateTime.Now:yyyy-MM-dd HH:mm:ss}").FontSize(12);

                        // إضافة تفاصيل العميل

                        var invoiceDetails = vmodel.ListViewInvose.FirstOrDefault();
                        if (invoiceDetails != null)
                        {
                            content.Item().AlignLeft().PaddingBottom(5).Text($"اسم العميل: {invoiceDetails.Name}")
                                .FontSize(12);  // إضافة PaddingBottom على Item وليس النص
                            content.Item().AlignLeft().PaddingBottom(5).Text($"رقم الهاتف: {invoiceDetails.PhoneNumber}")
                                .FontSize(12);  // إضافة PaddingBottom على Item وليس النص
                            content.Item().AlignLeft().PaddingBottom(5).Text($"وسيلة الدفع: {invoiceDetails.PaymentMethodAr}")
                                .FontSize(12);  // إضافة PaddingBottom على Item وليس النص
                            content.Item().AlignLeft().PaddingBottom(5).Text($"الفئة: {invoiceDetails.CategoryNameAr}")
                                .FontSize(12);  // إضافة PaddingBottom على Item وليس النص
                            content.Item().AlignLeft().PaddingBottom(10).Text($"حالة الفاتورة: {(invoiceDetails.OutstandingBill ? "معلقة" : "مدفوعة")}")
                                .FontSize(12);  // إضافة PaddingBottom على Item وليس النص
                        }

                        content.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100); // وقت وتاريخ الإدخال
                                columns.ConstantColumn(50);  // الإجمالي
                                columns.ConstantColumn(50);  // السعر
                                columns.ConstantColumn(50);  // الكمية
                                columns.RelativeColumn(100); // اسم المنتج
                                columns.ConstantColumn(40);  // رقم الفاتورة
                            });

                            table.Header(header =>
                            {
                                header.Cell().Border(1).AlignCenter().Text("وقت وتاريخ").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الإجمالي").Bold();
                                header.Cell().Border(1).AlignCenter().Text("السعر").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الكمية").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الصنف").Bold();
                                header.Cell().Border(1).AlignCenter().Text("ر.ف").Bold();
                            });

                            foreach (var product in products)
                            {
                                table.Cell().Border(1).AlignCenter().Text(product.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
                                table.Cell().Border(1).AlignCenter().Text($"${product.total}");
                                table.Cell().Border(1).AlignCenter().Text($"${product.price}");
                                table.Cell().Border(1).AlignCenter().Text(product.Quantity.ToString());
                                table.Cell().Border(1).AlignCenter().Text(product.ProductNameAr);
                                table.Cell().Border(1).AlignCenter().Text(product.InvoiceNumber.ToString());
                            }
                        });

                        content.Item().PaddingTop(10);

                        // **إضافة الفوتر في نهاية التقرير**
                        content.Item().PaddingTop(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // العمود الأول: مجموع الكمية
                                columns.RelativeColumn(); // العمود الثاني: المجموع العام
                            });

                            // المسميات في السطر الأول
                            table.Header(header =>
                            {
                                header.Cell().AlignCenter().Text("مجموع الكمية").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("المجموع العام").FontSize(12).Bold();
                            });

                            // القيم في السطر الثاني
                            table.Cell().Border(1).AlignCenter().Text($"{totalQuantity}").FontSize(12);
                            table.Cell().Border(1).AlignCenter().Text($"${totalAmount}").FontSize(12);
                        });

                        // إضافة تاريخ الطباعة أسفل التقرير
                        content.Item().PaddingTop(10).AlignRight().Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(10).Bold();
                    });
                });
            });

            var pdfData = pdfDocument.GeneratePdf();
            return File(pdfData, "application/pdf", "Invoice_Report.pdf");
        }


    }
}
