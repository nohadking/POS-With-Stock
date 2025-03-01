using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing.Printing;
using System.Drawing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using Infarstuructre.ViewModel;
using Microsoft.Graph.Models;
using Image = System.Drawing.Image;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PurchaseController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IISupplier iSupplier;
        IIPaymentMethod iPaymentMethod;
        IIUnit iUnit;
        IIClassCard iClassCard;
        IIPurchase iPurchase;

        public PurchaseController(MasterDbcontext dbcontext1, IICompanyInformation iCompanyInformation1, IISupplier iSupplier1, IIPaymentMethod iPaymentMethod1, IIUnit iUnit1, IIClassCard iClassCard1, IIPurchase iPurchase1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iSupplier = iSupplier1;
            iPaymentMethod = iPaymentMethod1;
            iUnit = iUnit1;
            iClassCard = iClassCard1;
            iPurchase = iPurchase1;
        }
        public IActionResult MyPurchase()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            //vmodel.ListViewPurchase = iPurchase.GetAll() .GroupBy(i => i.PurchaseNumber) .Select(g => g.First()) .ToList();
            ViewBag.Supplier = vmodel.ListViewSupplier = iSupplier.GetAll().GroupBy(i => i.SupplierName).Select(g => g.First()).ToList();
            ViewBag.PaymentMethod = vmodel.ListPaymentMethod = iPaymentMethod.GetAll();
            ViewBag.Unit = vmodel.ListUnit = iUnit.GetAll();
            ViewBag.ClassCard = vmodel.ListViewClassCard = iClassCard.GetAll();
            ViewBag.Purchase = vmodel.ListViewPurchase = iPurchase.GetAll().GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();

            var numberinvose = vmodel.ListViewPurchase = iPurchase.GetAll()
    .GroupBy(p => p.PurchaseNumber) // تجميع حسب رقم السند
    .Select(g => g.First())        // أخذ السجل الأول من كل مجموعة
    .ToList();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.PurchaseNumber) + 1
        : 1;
            return View(vmodel);
        }
        public IActionResult AddPurchase(int? IdPurchase)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewPurchase = iPurchase.GetAll().GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();
            ViewBag.Supplier = vmodel.ListViewSupplier = iSupplier.GetAll().GroupBy(i => i.SupplierName).Select(g => g.First()).ToList();
            ViewBag.PaymentMethod = vmodel.ListPaymentMethod = iPaymentMethod.GetAll();
            ViewBag.Unit = vmodel.ListUnit = iUnit.GetAll();
            ViewBag.ClassCard = vmodel.ListViewClassCard = iClassCard.GetAll();
            var numberinvose = vmodel.ListViewPurchase = iPurchase.GetAll().Distinct().ToList();

            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.PurchaseNumber) + 1
        : 1;
            ViewBag.Purchase = vmodel.ListViewPurchase = iPurchase.GetAll().GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();
            if (IdPurchase != null)
            {
                vmodel.Purchase = iPurchase.GetById(Convert.ToInt32(IdPurchase));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPurchase slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdPurchase = model.Purchase.IdPurchase;
                slider.IdSupplier = model.Purchase.IdSupplier;
                slider.IdPaymentMethod = model.Purchase.IdPaymentMethod;
                slider.Statement = model.Purchase.Statement;
                slider.PurchaseDate = model.Purchase.PurchaseDate;
                slider.PurchaseNumber = model.Purchase.PurchaseNumber;
                slider.PurchaseSubNumber = model.Purchase.PurchaseSubNumber;
                slider.IdProduct = model.Purchase.IdProduct;
                slider.IdUnit = model.Purchase.IdUnit;
                slider.Quantity = model.Purchase.Quantity;
                slider.FreeQuantity = model.Purchase.FreeQuantity;
                slider.AllQuantity = (model.Purchase.Quantity) + (model.Purchase.FreeQuantity ?? 0);
                slider.PurchasePrice = model.Purchase.PurchasePrice;
                slider.Total = model.Purchase.Total;
                slider.SingleDiscount = model.Purchase.SingleDiscount;
                slider.shipping = model.Purchase.shipping;
                slider.Nouts = model.Purchase.Nouts;
                slider.TotalDiscount = model.Purchase.TotalDiscount;
                slider.TotalQuantity = model.Purchase.TotalQuantity;
                slider.TotalAll = model.Purchase.TotalAll;
                slider.DateTimeEntry = model.Purchase.DateTimeEntry;
                slider.DataEntry = model.Purchase.DataEntry;
                slider.CurrentState = model.Purchase.CurrentState;
                if (slider.IdPurchase == 0 || slider.IdPurchase == null)
                {

                    var reqwest = iPurchase.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyPurchase");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddPurchase");
                    }
                }
                else
                {
                    var reqestUpdate = iPurchase.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyPurchase");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddPurchase");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect(returnUrl);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPurchase)
        {
            var reqwistDelete = iPurchase.deleteData(IdPurchase);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyPurchase");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyPurchase");

            }
        }


        [HttpGet]
        public IActionResult GetSupplierImage(int id)
        {
            var supplier = dbcontext.TBSuppliers.FirstOrDefault(s => s.IdSupplier == id);
            if (supplier != null)
            {
                // المسار الصحيح بناءً على مكان تخزين الصور في wwwroot
                var imageUrl = Url.Content("~/Images/Home/" + supplier.Photo);
                return Json(new { imageUrl });
            }
            return Json(null);
        }

        [HttpGet]
        public IActionResult GetLastPurchasePrice(int id)
        {
            // البحث عن آخر سعر شراء للمنتج بناءً على IdProduct
            var lastPurchase = dbcontext.TBPurchases
                .Where(p => p.IdProduct == id)
                .OrderByDescending(p => p.PurchaseDate) // ترتيب العمليات حسب تاريخ الشراء
                .Select(p => (decimal?)p.PurchasePrice) // تحويل إلى decimal? للتعامل مع القيم الفارغة
                .FirstOrDefault();

            // إذا لم يتم العثور على سعر، قم بإرجاع 0
            return Json(new { lastPrice = lastPurchase.GetValueOrDefault(0) });
        }


        [HttpGet]
        public IActionResult GeneratePurchasePdf(string? suplierName, string? productName, int? purNum, string? oneDate, string startDate, string endDate)
        {
            if (suplierName == null && productName == null && purNum == null && oneDate == null && startDate == null && endDate == null)
            {
                var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                return pdf;
            }
            else
            {
                if (oneDate != null)
                {
                    // حسب تاريخ محدد
                    var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                    return pdf;
                }

                else if (startDate != null && endDate != null)
                {
                    if (suplierName != null)
                    {
                        // حسب اسم مورد بين تاريخين  
                        var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                        return pdf;
                    }
                    else if (productName != null)
                    {
                        // حسب اسم صنف بين تاريخين  
                        var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                        return pdf;

                    }
                    else
                    {
                        // حسب من تاريخ لتاريخ  
                        var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                        return pdf;
                    }
                }
                else if (suplierName != null)
                {
                    // حسب اسم مورد  
                    var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                    return pdf;

                }
                else if (suplierName != null)
                {
                    // حسب اسم productName  
                    var pdf = CreatePDF(suplierName, productName, purNum, oneDate, startDate, endDate);
                    return pdf;

                }
            }
            return Content("Invalid parameters or no data found.", "text/plain");
        }


        public IActionResult CreatePDF(string? suplierName, string? productName, int? purNum, string? oneDate, string startDate, string endDate)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            var compny = dbcontext.TBCompanyInformations.FirstOrDefault();
            var purcheases = new List<TBViewPurchase>();

            var pdfDocument = Document.Create(container =>
            {

                // **حساب الإجماليات**
                if (suplierName == null && productName == null && purNum == null && oneDate == null && startDate == null && endDate == null)
                {
                    purcheases = vmodel.ListViewPurchase = iPurchase.GetAll().GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();

                }
                else
                {
                    if (oneDate != null)
                    {
                        // حسب تاريخ محدد
                        DateTime dt = Convert.ToDateTime(oneDate);
                        purcheases = vmodel.ListViewPurchase = iPurchase.GetAByDetectedDate(dt);
                    }

                    else if (startDate != null && endDate != null)
                    {
                        if (suplierName != null)
                        {
                            // حسب اسم مورد بين تاريخين  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            purcheases = vmodel.ListViewPurchase = iPurchase.GetABySuplierAndPeriod(suplierName, startDt, endD).GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();
                        }
                        else if (productName != null)
                        {
                            // حسب اسم صنف بين تاريخين  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            purcheases = vmodel.ListViewPurchase = iPurchase.GetAByPruductAndPeriod(productName, startDt, endD).GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();

                        }
                        else
                        {
                            // حسب من تاريخ لتاريخ  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            purcheases = vmodel.ListViewPurchase = iPurchase.GetByPeriod(startDt, endD).GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();
                        }
                    }
                    else if (suplierName != null)
                    {
                        // حسب اسم مورد  
                        purcheases = vmodel.ListViewPurchase = iPurchase.GetBySuplier(suplierName).GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();

                    }
                    else if (productName != null)
                    {
                        // حسب اسم productName  
                        purcheases = vmodel.ListViewPurchase = iPurchase.GetByProduct(productName).GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList(); 

                    }
                }
                // زبط هدول معلم 

                var totalQuantity = purcheases.Sum(p => p.TotalQuantity);
                var totalAmount = purcheases.Sum(p => p.TotalAll);

                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                            .Column(header =>
                            {
                                header.Item().AlignRight().PaddingBottom(20).Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(5).Bold();
                                if (suplierName == null && productName == null && purNum == null && oneDate == null && startDate == null && endDate == null)
                                {
                                    header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات العام  ").FontSize(20).Bold();
                                }
                                else
                                {
                                    if (oneDate != null)
                                    {
                                        // حسب تاريخ محدد
                                        header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات حسب تاريخ {oneDate} ").FontSize(20).Bold();
                                    }

                                    else if (startDate != null && endDate != null)
                                    {
                                        if (suplierName != null)
                                        {
                                            // حسب اسم مورد بين تاريخين  
                                            header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات للمورد {suplierName} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
                                        }
                                        else if (productName != null)
                                        {
                                            // حسب اسم صنف بين تاريخين  
                                            header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات للصنف {productName} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();

                                        }
                                        else
                                        {
                                            // حسب من تاريخ لتاريخ  
                                            header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
                                        }
                                    }
                                    else if (suplierName != null)
                                    {
                                        // حسب اسم مورد  
                                        header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات للمورد {suplierName} ").FontSize(20).Bold();

                                    }
                                    else if (productName != null)
                                    {
                                        // حسب اسم productName  
                                        header.Item().Border(1).AlignCenter().Text($"تقرير المشتريات للصنف {productName} ").FontSize(20).Bold();

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
                       
                        content.Item().AlignCenter().Text($"تقرير المشتريات").FontSize(16).Bold();
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
                                columns.RelativeColumn(50); // اسم المنتج
                                columns.ConstantColumn(100);  // رقم الفاتورة
                                columns.ConstantColumn(100);  // رقم الفاتورة
                                columns.ConstantColumn(100);  // رقم الفاتورة
                            });

                            table.Header(header =>
                            {

                                header.Cell().Border(1).AlignCenter().Text("مدخل البيانات").Bold();
                                header.Cell().Border(1).AlignCenter().Text("تاريخ ادخال البيانات").Bold();
                                header.Cell().Border(1).AlignCenter().Text("اجمالي المبلغ").Bold();                      
                                header.Cell().Border(1).AlignCenter().Text("الكمية").Bold();
                                header.Cell().Border(1).AlignCenter().Text("رقم السند الفرعي").Bold();
                                header.Cell().Border(1).AlignCenter().Text("رقم السند").Bold();
                                header.Cell().Border(1).AlignCenter().Text("تاريخ السند").Bold();
                                header.Cell().Border(1).AlignCenter().Text("طريقة الدفع").Bold();
                                header.Cell().Border(1).AlignCenter().Text("اسم المورد").Bold();

                            });

                            foreach (var purcheas in purcheases)
                            {
                                string cachname = string.Empty;
                                if (purcheas.DataEntry != null)
                                {
                                    cachname = dbcontext.VwUsers
                                                         .Where(a => a.Email == purcheas.DataEntry)
                                                         .Select(a => a.Name)
                                                         .FirstOrDefault();
                                }                                        
                                table.Cell().Border(1).AlignCenter().Text(purcheas.DataEntry);
                                table.Cell().Border(1).AlignCenter().Text(purcheas.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
                                table.Cell().Border(1).AlignCenter().Text(purcheas.TotalAll);                     
                                table.Cell().Border(1).AlignCenter().Text(purcheas.TotalQuantity);
                                table.Cell().Border(1).AlignCenter().Text(purcheas.PurchaseSubNumber);
                                table.Cell().Border(1).AlignCenter().Text(purcheas.PurchaseNumber);                              
                                table.Cell().Border(1).AlignCenter().Text(purcheas.PurchaseDate);
                                table.Cell().Border(1).AlignCenter().Text(purcheas.PaymentMethodAr);
                                table.Cell().Border(1).AlignCenter().Text(purcheas.SupplierName);
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
                              
                                header.Cell().AlignCenter().Text("المدير العام").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("المحاسبة").FontSize(12).Bold();
                            });

                            // القيم في السطر الثاني
                          
                            table.Cell().Border(0).PaddingTop(10).AlignCenter().Text($"{compny.NameOner}").FontSize(12);
                            table.Cell().Border(0).PaddingTop(10).AlignCenter().Text($"-----------------").FontSize(12);
                        
                        });

                        // إضافة تاريخ الطباعة أسفل التقرير
                        
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
                return RedirectToAction("MyPurchase");
            }

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewPurchase = iPurchase.GetByPurcheasNm(num);

            if (vmodel.ListViewPurchase == null || !vmodel.ListViewPurchase.Any())
            {
                return RedirectToAction("MyPurchase");
            }

            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();

            var company = vmodel.ListCompanyInformation.FirstOrDefault();
            string companyName = company?.NameCompanyAr ?? "";
            string companyAddress = company?.AddressAr ?? "";
            string companyPhone = company?.Phone ?? "";
            string photoco = company?.Photo ?? "";

            decimal totalAmount = 0;
            int totalQuantity = 0;

            var products = vmodel.ListViewPurchase;
            totalQuantity = products.Sum(p => p.Quantity);
            totalAmount = products.Sum(p => p.Total);

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
    .Column(header =>
    {
        //string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Home", photoco);

        //// التحقق من وجود الصورة قبل إضافتها
        //if (System.IO.File.Exists(imagePath))
        //{
        //    header.Item().AlignLeft()
        //.Image(imagePath)
        //.FitWidth();

        //    header.Item().AlignLeft()
        //        .Height(200, Unit.Centimetre);

        //}
        //else
        //{
        //    header.Item().AlignLeft()
        //        .Text("No Image Available").FontSize(12).Bold();
        //}
        header.Item().PaddingTop(10).AlignRight().Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(5).Bold();
        header.Item().AlignCenter().Text("سند الشراء").FontSize(20).Bold();
        header.Item().AlignCenter().Text(companyName).FontSize(14);
        header.Item().AlignCenter().Text(companyAddress).FontSize(12);
        header.Item().AlignCenter().Text(companyPhone).FontSize(12);
        header.Item().BorderColor(Colors.Black).LineHorizontal(1);
    });


                    page.Content().Column(content =>
                    {
                        var firstProduct = products.FirstOrDefault();
                        string supplierName = firstProduct?.SupplierName ?? "غير متوفر";
                        DateOnly datebound = firstProduct?.PurchaseDate ?? DateOnly.MinValue;


                        content.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text($"رقم السند: {num}").FontSize(16).Bold();
                            row.RelativeItem().AlignCenter().Text($"المورد: {supplierName}").FontSize(16).Bold();
                            row.RelativeItem().AlignRight().Text($"التاريخ: {datebound}").FontSize(16).Bold();
                            


                        });
                       
                        content.Item().BorderColor(Colors.Black).LineHorizontal(1);
                        content.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                            // المورد
                                columns.RelativeColumn(); // اسم الصنف
                                columns.RelativeColumn(); // الكمية
                                columns.RelativeColumn(); // السعر الافرادي
                                columns.RelativeColumn(); // الإجمالي
                            });

                            table.Header(header =>
                            {
                              
                                header.Cell().Border(1).AlignCenter().Text("الإجمالي").Bold();
                                header.Cell().Border(1).AlignCenter().Text("السعر الافرادي").Bold();
                                header.Cell().Border(1).AlignCenter().Text("الكمية").Bold();
                                header.Cell().Border(1).AlignCenter().Text("اسم الصنف").Bold();



                            });

                            foreach (var product in products)
                            {
                              
                        
                                table.Cell().Border(1).AlignCenter().Text($"{product.Total:F3}");
                                table.Cell().Border(1).AlignCenter().Text($"{product.PurchasePrice:F3}");
                                table.Cell().Border(1).AlignCenter().Text($"{product.Quantity:F3}");
                                table.Cell().Border(1).AlignCenter().Text(product.ItemName.ToString());


                            }
                        });

                        content.Item().PaddingTop(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // مجموع الكمية
                                columns.RelativeColumn(); // المجموع العام
                            });

                            table.Header(header =>
                            {
                                header.Cell().AlignCenter().Text("مجموع الكمية").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("المجموع العام").FontSize(12).Bold();
                            });

                            table.Cell().Border(1).AlignCenter().Text($"{totalQuantity}").FontSize(12);
                            table.Cell().Border(1).AlignCenter().Text($"{totalAmount:C}").FontSize(12);
                        });

                      
                    });
                });
            });

            var pdfData = pdfDocument.GeneratePdf();
            return File(pdfData, "application/pdf", "Purchase_Receipt_Report.pdf");
        }


    }
}
