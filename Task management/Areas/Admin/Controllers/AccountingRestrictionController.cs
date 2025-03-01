using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing.Printing;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountingRestrictionController : Controller
    {
        MasterDbcontext dbcontext;
        IIAccountingRestriction iAccountingRestriction;
        IICompanyInformation iCompanyInformation;
        IILevelForeAccount iLevelForeAccount;
        public AccountingRestrictionController(MasterDbcontext dbcontext1,IIAccountingRestriction iAccountingRestriction1,IICompanyInformation iCompanyInformation1,IILevelForeAccount iLevelForeAccount1)
        {
            dbcontext=dbcontext1;
            iAccountingRestriction =iAccountingRestriction1;
            iCompanyInformation = iCompanyInformation1;
            iLevelForeAccount = iLevelForeAccount1;
        }
        public IActionResult MyAccountingRestriction()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
            ViewBag.LevelForeAccount = iLevelForeAccount.GetAll();
            ViewBag.AccountingRestriction = vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll().GroupBy(i => i.AccountingName).Select(g => g.First()).ToList();







            var numberinvose = vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberaccountingRestrictions) + 1
        : 1;

           
            return View(vmodel);
        }
        public IActionResult AddAccountingRestriction(int? IdaccountingRestrictions)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
            ViewBag.LevelForeAccount = iLevelForeAccount.GetAll();
            if (IdaccountingRestrictions != null)
            {
                vmodel.AccountingRestriction = iAccountingRestriction.GetById(Convert.ToInt32(IdaccountingRestrictions));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBAccountingRestriction slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdaccountingRestrictions = model.AccountingRestriction.IdaccountingRestrictions;
                slider.NumberaccountingRestrictions = model.AccountingRestriction.NumberaccountingRestrictions;
                slider.AccountingName = model.AccountingRestriction.AccountingName;
                slider.BondType = model.AccountingRestriction.BondType;
                slider.BondNumber = model.AccountingRestriction.BondNumber;
                slider.Debtor = model.AccountingRestriction.Debtor;
                slider.creditor = model.AccountingRestriction.creditor;
                slider.Statement = model.AccountingRestriction.Statement;
                slider.Nouts = model.AccountingRestriction.Nouts;       
                slider.DateTimeEntry = model.AccountingRestriction.DateTimeEntry;
                slider.DataEntry = model.AccountingRestriction.DataEntry;
                slider.CurrentState = model.AccountingRestriction.CurrentState;            
                if (slider.IdaccountingRestrictions == 0 || slider.IdaccountingRestrictions == null)
                {            
                    var reqwest = iAccountingRestriction.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyAccountingRestriction");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddAccountingRestriction");
                    }
                }
                else
                {
                    var reqestUpdate = iAccountingRestriction.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyAccountingRestriction");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddAccountingRestriction");
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
        public IActionResult DeleteData(int IdaccountingRestrictions)
        {
            var reqwistDelete = iAccountingRestriction.deleteData(IdaccountingRestrictions);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyAccountingRestriction");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyAccountingRestriction");

            }
        }

        ////// print pdf ///////////////////
        ///
        [HttpGet]
        public IActionResult GenerateAccountPdf(string? suplier, string? type, string? oneDate, string? startDate, string? endDate)
        {
            // تقرير عام
            if (suplier == null && type == null && oneDate == null && startDate == null && endDate == null)
            {
                var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                return pdf;
            }
            else
            {
                if (oneDate != null)
                {
                    // حسب تاريخ محدد
                    var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                    return pdf;
                }

                else if (startDate != null && endDate != null)
                {
                    if (suplier != null)
                    {
                        //   كشف حساب بين تاريخين  
                        var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                        return pdf;
                    }

                    else if (type != null)
                    {
                        // حسب نوع السند بين تاريخين  
                        var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                        return pdf;
                    }

                    else
                    {
                        //  بين تاريخين  
                        var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                        return pdf;
                    }
                }
                else if (suplier != null)
                {
                    // كشف حساب  
                    var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                    return pdf;

                }
                else if (type != null)
                {
                    // حسب نوع السند  
                    var pdf = CreatePDF(suplier, type, oneDate, startDate, endDate);
                    return pdf;

                }
            }
            return Content("Invalid parameters or no data found.", "text/plain");
        }


        //public IActionResult CreatePDF(string? suplier, string? type, string? oneDate, string startDate, string endDate)
        //{
        //    ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
        //    var compny = dbcontext.TBCompanyInformations.FirstOrDefault();
        //    var accounts = new List<TBAccountingRestriction>();

        //    var pdfDocument = Document.Create(container =>
        //    {

        //        // **حساب الإجماليات**
        //        if (suplier == null && type == null && oneDate == null && startDate == null && endDate == null)
        //        {
        //            accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
        //        }
        //        else
        //        {
        //            if (oneDate != null)
        //            {
        //                // حسب تاريخ محدد
        //                DateTime dt = Convert.ToDateTime(oneDate);
        //                accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByDetectedDt(dt);
        //            }

        //            else if (startDate != null && endDate != null)
        //            {
        //                if (suplier != null)
        //                {
        //                    // حسب اسم مورد بين تاريخين  
        //                    DateTime startDt = Convert.ToDateTime(startDate);
        //                    DateTime endD = Convert.ToDateTime(endDate);
        //                    accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetBySupAndPeriodDate(suplier, startDt, endD);
        //                }

        //                else if (type != null)
        //                {
        //                    // حسب نوع السند بين تاريخين  
        //                    DateTime startDt = Convert.ToDateTime(startDate);
        //                    DateTime endD = Convert.ToDateTime(endDate);
        //                    accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByTypeAndPeriodDate(type, startDt, endD);
        //                }

        //                else
        //                {
        //                    // حسب من تاريخ لتاريخ  
        //                    DateTime startDt = Convert.ToDateTime(startDate);
        //                    DateTime endD = Convert.ToDateTime(endDate);
        //                    accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByPeriodDate(startDt, endD);
        //                }
        //            }
        //            else if (suplier != null)
        //            {
        //                // كشف حساب  
        //                accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetBySup(suplier);

        //            }

        //            else if (type != null)
        //            {
        //                // حسب نوع السند  
        //                accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByType(type);

        //            }
        //        }
        //        // زبط هدول معلم 

        //        //var totalQuantity = accounts.Sum(p => p.Quantity);
        //        //var totalAmount = accounts.Sum(p => p.TotalAll);

        //        container.Page(page =>
        //        {
        //            page.Size(PageSizes.A4.Landscape());
        //            page.Margin(2, Unit.Centimetre);
        //            page.DefaultTextStyle(x => x.FontSize(12));

        //            page.Header()
        //                    .Column(header =>
        //                    {
        //                        header.Item().AlignRight().PaddingBottom(20).Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(5).Bold();
        //                        if (suplier == null && type == null && oneDate == null && startDate == null && endDate == null)
        //                        {
        //                            header.Item().Border(1).AlignCenter().Text($"تقرير الحركات العام ").FontSize(20).Bold();
        //                        }
        //                        else
        //                        {
        //                            if (oneDate != null)
        //                            {
        //                                // حسب تاريخ محدد
        //                                header.Item().Border(1).AlignCenter().Text($"تقرير الحركات لتاريخ {oneDate} ").FontSize(20).Bold();
        //                            }

        //                            else if (startDate != null && endDate != null)
        //                            {
        //                                if (suplier != null)
        //                                {
        //                                    // حسب اسم مورد بين تاريخين  
        //                                    header.Item().Border(1).AlignCenter().Text($"كشف حساب  {suplier} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
        //                                }

        //                                else if (type != null)
        //                                {
        //                                    // حسب نوع السند بين تاريخين  
        //                                    header.Item().Border(1).AlignCenter().Text($"تقرير الحركات لنوع السند  {type} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();

        //                                }

        //                                else
        //                                {
        //                                    // حسب من تاريخ لتاريخ  
        //                                    header.Item().Border(1).AlignCenter().Text($"تقرير الحركات بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
        //                                }
        //                            }
        //                            else if (suplier != null)
        //                            {
        //                                // حسب اسم مورد  
        //                                header.Item().Border(1).AlignCenter().Text($"كشف حساب  {suplier} ").FontSize(20).Bold();

        //                            }

        //                            else if (type != null)
        //                            {
        //                                // حسب نوع السند  
        //                                header.Item().Border(1).AlignCenter().Text($"تقرير الحركات حسب نوع السند: {type} ").FontSize(20).Bold();

        //                            }
        //                        }

        //                        if (compny != null)
        //                        {
        //                            header.Item().Border(1).AlignCenter().Text($" {compny.NameCompanyAr}").FontSize(14);
        //                            header.Item().Border(1).AlignCenter().Text($" {compny.AddressAr}").FontSize(12);
        //                            header.Item().Border(1).AlignCenter().Text($" {compny.Mobile}").FontSize(12);
        //                        }
        //                    });

        //            page.Content().Column(content =>
        //            {
        //                content.Item().AlignCenter().Text($"تقرير الحركات").FontSize(16).Bold();
        //                content.Item().AlignCenter().Text("----------------------------------------------").FontSize(12).Bold();
        //                content.Item().Table(table =>
        //                {
        //                    table.ColumnsDefinition(columns =>
        //                    {
        //                        columns.ConstantColumn(100); 
        //                        columns.ConstantColumn(100); 
        //                        columns.ConstantColumn(100); 
        //                        columns.ConstantColumn(50);  
        //                        columns.ConstantColumn(50); 
        //                        columns.RelativeColumn(50); 
        //                        columns.ConstantColumn(100);  
        //                        columns.ConstantColumn(100); 
        //                        columns.ConstantColumn(100); 
        //                    });

        //                    table.Header(header =>
        //                    {








        //                        header.Cell().Border(1).AlignCenter().Text("مدخل البيانات").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("تاريخ ادخال البيانات").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("البيان").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("المدين").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("الدائن").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("رقم السند").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("نوع السند").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("اسم الحساب").Bold();
        //                        header.Cell().Border(1).AlignCenter().Text("رقم القيد ").Bold();
        //                    });

        //                    foreach (var acc in accounts)
        //                    {
        //                        string cachname = string.Empty;
        //                        if (acc.DataEntry != null)
        //                        {
        //                            cachname = dbcontext.VwUsers
        //                                                 .Where(a => a.Email == acc.DataEntry)
        //                                                 .Select(a => a.Name)
        //                                                 .FirstOrDefault();
        //                        }

        //                        table.Cell().Border(1).AlignCenter().Text(cachname);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        table.Cell().Border(1).AlignCenter().Text(acc.Statement);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.creditor);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.Debtor);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.BondNumber);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.BondType);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.AccountingName);
        //                        table.Cell().Border(1).AlignCenter().Text(acc.NumberaccountingRestrictions);
        //                    }
        //                });

        //                content.Item().PaddingTop(10);


        //                // **إضافة الفوتر في نهاية التقرير**
        //                content.Item().PaddingTop(20).Table(table =>
        //                {
        //                    // تعريف الأعمدة
        //                    table.ColumnsDefinition(columns =>
        //                    {
        //                        columns.RelativeColumn(); // العمود الأول: مجموع الكمية
        //                        columns.RelativeColumn(); // العمود الثاني: المجموع العام
        //                    });

        //                    // المسميات في السطر الأول
        //                    table.Header(header =>
        //                    {
        //                        header.Cell().AlignCenter().Text("مجموع الكمية").FontSize(12).Bold();
        //                        header.Cell().AlignCenter().Text("المجموع العام").FontSize(12).Bold();
        //                    });

        //                    // القيم في السطر الثاني
        //                    //table.Cell().Border(1).AlignCenter().Text($"{totalQuantity}").FontSize(12);
        //                    //table.Cell().Border(1).AlignCenter().Text($"${totalAmount}").FontSize(12);
        //                });

        //                // إضافة تاريخ الطباعة أسفل التقرير

        //            });


        //        });
        //    });
        //    var pdfData = pdfDocument.GeneratePdf();
        //    return File(pdfData, "application/pdf", "Report.pdf");
        //}
        public IActionResult CreatePDF(string? suplier, string? type, string? oneDate, string startDate, string endDate)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            var compny = dbcontext.TBCompanyInformations.FirstOrDefault();
            var accounts = new List<TBAccountingRestriction>();

            var pdfDocument = Document.Create(container =>
            {

                // **حساب الإجماليات**
                if (suplier == null && type == null && oneDate == null && startDate == null && endDate == null)
                {
                    accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
                }
                else
                {
                    if (oneDate != null)
                    {
                        // حسب تاريخ محدد
                        DateTime dt = Convert.ToDateTime(oneDate);
                        accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByDetectedDt(dt);
                    }

                    else if (startDate != null && endDate != null)
                    {
                        if (suplier != null)
                        {
                            // حسب اسم مورد بين تاريخين  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetBySupAndPeriodDate(suplier, startDt, endD);
                        }

                        else if (type != null)
                        {
                            // حسب نوع السند بين تاريخين  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByTypeAndPeriodDate(type, startDt, endD);
                        }

                        else
                        {
                            // حسب من تاريخ لتاريخ  
                            DateTime startDt = Convert.ToDateTime(startDate);
                            DateTime endD = Convert.ToDateTime(endDate);
                            accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByPeriodDate(startDt, endD);
                        }
                    }
                    else if (suplier != null)
                    {
                        // كشف حساب  
                        accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetBySup(suplier);

                    }

                    else if (type != null)
                    {
                        // حسب نوع السند  
                        accounts = vmodel.ListAccountingRestriction = iAccountingRestriction.GetByType(type);

                    }
                }

                // حساب مجموع المدين والدائن
                var totalDebtor = accounts.Sum(p => p.Debtor);
                var totalCreditor = accounts.Sum(p => p.creditor);

                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                          .Column(header =>
                          {
                              header.Item().AlignRight().PaddingBottom(20).Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(5).Bold();
                              if (compny != null)
                              {
                                  header.Item().Border(1).AlignCenter().Text($" {compny.NameCompanyAr}").FontSize(14);
                                  header.Item().Border(1).AlignCenter().Text($" {compny.AddressAr}").FontSize(12);
                                  header.Item().Border(1).AlignCenter().Text($" {compny.Mobile}").FontSize(12);
                              }
                             
                              if (suplier == null && type == null && oneDate == null && startDate == null && endDate == null)
                              {
                                  header.Item().Border(1).AlignCenter().Text($"تقرير الحركات العام ").FontSize(20).Bold();
                              }
                              else
                              {
                                  if (oneDate != null)
                                  {
                                      // حسب تاريخ محدد
                                      header.Item().Border(1).AlignCenter().Text($"تقرير الحركات لتاريخ {oneDate} ").FontSize(20).Bold();
                                  }
                                  else if (startDate != null && endDate != null)
                                  {
                                      if (suplier != null)
                                      {
                                          // حسب اسم مورد بين تاريخين  
                                          header.Item().Border(1).AlignCenter().Text($"كشف حساب  {suplier} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
                                      }
                                      else if (type != null)
                                      {
                                          // حسب نوع السند بين تاريخين  
                                          header.Item().Border(1).AlignCenter().Text($"تقرير الحركات لنوع السند  {type} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
                                      }
                                      else
                                      {
                                          // حسب من تاريخ لتاريخ  
                                          header.Item().Border(1).AlignCenter().Text($"تقرير الحركات بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
                                      }
                                  }
                                  else if (suplier != null)
                                  {
                                      // حسب اسم مورد  
                                      header.Item().Border(1).AlignCenter().Text($"كشف حساب  {suplier} ").FontSize(20).Bold();
                                  }
                                  else if (type != null)
                                  {
                                      // حسب نوع السند  
                                      header.Item().Border(1).AlignCenter().Text($"تقرير الحركات حسب نوع السند: {type} ").FontSize(20).Bold();
                                  }
                              }

                           
                          });

                    page.Content().Column(content =>
                    {
                        content.Item().AlignCenter().Text($"تقرير الحركات").FontSize(16).Bold();
                        content.Item().AlignCenter().Text("----------------------------------------------").FontSize(12).Bold();
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Border(1).AlignCenter().Text("مدخل البيانات").Bold();
                                header.Cell().Border(1).AlignCenter().Text("تاريخ ادخال البيانات").Bold();
                                header.Cell().Border(1).AlignCenter().Text("البيان").Bold();
                              
                                header.Cell().Border(1).AlignCenter().Text("الدائن").Bold();
                                header.Cell().Border(1).AlignCenter().Text("المدين").Bold();
                                header.Cell().Border(1).AlignCenter().Text("رقم السند").Bold();
                                header.Cell().Border(1).AlignCenter().Text("نوع السند").Bold();
                                header.Cell().Border(1).AlignCenter().Text("اسم الحساب").Bold();
                                header.Cell().Border(1).AlignCenter().Text("رقم القيد ").Bold();
                            });

                            foreach (var acc in accounts)
                            {
                                string cachname = string.Empty;
                                if (acc.DataEntry != null)
                                {
                                    cachname = dbcontext.VwUsers
                                                        .Where(a => a.Email == acc.DataEntry)
                                                        .Select(a => a.Name)
                                                        .FirstOrDefault();
                                }

                                table.Cell().Border(1).AlignCenter().Text(cachname);
                                table.Cell().Border(1).AlignCenter().Text(acc.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
                                table.Cell().Border(1).AlignCenter().Text(acc.Statement);
                                table.Cell().Border(1).AlignCenter().Text(acc.creditor.ToString("N2"));
                                table.Cell().Border(1).AlignCenter().Text(acc.Debtor.ToString("N2"));
                                table.Cell().Border(1).AlignCenter().Text(acc.BondNumber);
                                table.Cell().Border(1).AlignCenter().Text(acc.BondType);
                                table.Cell().Border(1).AlignCenter().Text(acc.AccountingName);
                                table.Cell().Border(1).AlignCenter().Text(acc.NumberaccountingRestrictions);
                            }
                        });

                        content.Item().PaddingTop(10);

                        // **إضافة الفوتر في نهاية التقرير**
                        content.Item().PaddingTop(20).Table(table =>
                        {
                            // تعريف الأعمدة
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // العمود الأول: مجموع المدين
                                columns.RelativeColumn(); // العمود الثاني: مجموع الدائن
                                columns.RelativeColumn(); // العمود الثاني: مجموع الدائن
                            });

                            // المسميات في السطر الأول
                            table.Header(header =>
                            {
                             
                               
                                header.Cell().AlignCenter().Text("الرصيد").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("مجموع الدائن").FontSize(12).Bold();
                                header.Cell().AlignCenter().Text("مجموع المدين").FontSize(12).Bold();
                            });

                            // القيم في السطر الثاني
                            table.Cell().Border(1).AlignCenter().Text($"{totalDebtor- totalCreditor:N3}").FontSize(12); // عرض المجموع مع تنسيق الأرقام
                            table.Cell().Border(1).AlignCenter().Text($"{totalCreditor:N3}").FontSize(12); // عرض المجموع مع تنسيق الأرقام
                            table.Cell().Border(1).AlignCenter().Text($"{totalDebtor:N3}").FontSize(12);  // عرض المجموع مع تنسيق الأرقام


                        });

                        // إضافة تاريخ الطباعة أسفل التقرير
                    });

                });
            });

            var pdfData = pdfDocument.GeneratePdf();
            return File(pdfData, "application/pdf", "Report.pdf");
        }


        //public async Task<IActionResult> CreateDirectPdf(int num)
        //{
        //    if (num == null)
        //    {
        //        return RedirectToAction("MyInvose");
        //    }

        //    var invoice = dbcontext.ViewInvose.FirstOrDefault(i => i.InvoiceNumber == num);

        //    // جلب بيانات الفاتورة
        //    ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
        //    vmodel.ListViewInvose = iInvose.GetByInvoiceNumber(Convert.ToInt32(num));

        //    if (vmodel.ListViewInvose == null || !vmodel.ListViewInvose.Any())
        //    {
        //        return RedirectToAction("MyInvose");
        //    }

        //    vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();

        //    string companyName = vmodel.ListCompanyInformation[0].NameCompanyAr;
        //    string companyAddress = vmodel.ListCompanyInformation[0].AddressAr;
        //    string companyPhone = vmodel.ListCompanyInformation[0].Phone;
        //    string companyTaxInfo = "VAT No: 123456789";
        //    decimal totalAmount = 0;
        //    // إعداد مستند الطباعة
        //    //PrintDocument pd = new PrintDocument
        //    //{
        //    //    PrinterSettings = { PrinterName = "Microsoft Print to PDF" }
        //    //};
        //    //PrintDocument pd = new PrintDocument
        //    //{
        //    //    PrinterSettings = { PrinterName = null } // استخدام الطابعة الافتراضية
        //    //};

        //    // إعداد مستند الطباعة
        //    PrintDocument pd = new PrintDocument
        //    {
        //        PrinterSettings = { PrinterName = new PrinterSettings().PrinterName } // تعيين الطابعة الافتراضية
        //    };
        //    // تعيين حجم الورق الحراري (عرض 8 سم - 80 مم)
        //    pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 320, 1180); // 80mm x 118mm تقريبا كحجم ورقة حرارية
        //    pd.PrintPage += (sender, e) =>
        //    {
        //        Font headerFont = new Font("Arial", 10, FontStyle.Bold);
        //        Font bodyFont = new Font("Arial", 8);
        //        Font totalFont = new Font("Arial", 8, FontStyle.Bold);
        //        int yPosition = 10; // الموضع الأولي للطباعة
        //        int leftMargin = 10; // الهامش الأيسر
        //        // إضافة هيدر الفاتورة بمعلومات الشركة
        //        e.Graphics.DrawString(companyName, headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        e.Graphics.DrawString(companyAddress, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
        //        e.Graphics.DrawString(companyPhone, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
        //        e.Graphics.DrawString(companyTaxInfo, bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;
        //        // إضافة عنوان الفاتورة
        //        e.Graphics.DrawString("Invoice Receipt", headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        e.Graphics.DrawString($"Invoice No: {num}", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
        //        e.Graphics.DrawString($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        // طباعة تفاصيل المنتجات كجدول
        //        e.Graphics.DrawString("Product Details:", headerFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;
        //        // رأس الجدول
        //        e.Graphics.DrawString("الصنف", bodyFont, Brushes.Black, leftMargin, yPosition);
        //        e.Graphics.DrawString("الكمية", bodyFont, Brushes.Black, leftMargin + 65, yPosition); // المسافة بين الأعمدة
        //        e.Graphics.DrawString("السعر", bodyFont, Brushes.Black, leftMargin + 90, yPosition);
        //        e.Graphics.DrawString("الأجمالي", bodyFont, Brushes.Black, leftMargin + 140, yPosition);
        //        yPosition += 15;

        //        e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;

        //        foreach (var product in vmodel.ListViewInvose)
        //        {
        //            // طباعة تفاصيل كل منتج
        //            e.Graphics.DrawString(product.ProductNameAr, bodyFont, Brushes.Black, leftMargin, yPosition);
        //            e.Graphics.DrawString(product.Quantity.ToString(), bodyFont, Brushes.Black, leftMargin + 65, yPosition);
        //            e.Graphics.DrawString($"{product.price:F3}", bodyFont, Brushes.Black, leftMargin + 90, yPosition);
        //            e.Graphics.DrawString($"{product.total:F3}", bodyFont, Brushes.Black, leftMargin + 140, yPosition);

        //            yPosition += 15;

        //            totalAmount += product.total; // حساب المجموع الإجمالي
        //        }
        //        e.Graphics.DrawString("----------------------------------------------------", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 15;

        //        // طباعة المجاميع
        //        //e.Graphics.DrawString($"Total Amount: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        //e.Graphics.DrawString($"VAT (15%): {totalAmount * 0.15M:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        //e.Graphics.DrawString($"Grand Total: {totalAmount * 1.15M:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;

        //        e.Graphics.DrawString($"Total Amount: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        //e.Graphics.DrawString($"VAT (15%): {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //        e.Graphics.DrawString($"Grand Total: {totalAmount:C}", totalFont, Brushes.Black, leftMargin, yPosition); yPosition += 30;

        //        // إضافة رسالة شكر
        //        e.Graphics.DrawString("Thank you for shopping with us!", bodyFont, Brushes.Black, leftMargin, yPosition); yPosition += 20;
        //    };

        //    // تنفيذ عملية الطباعة
        //    await Task.Run(() => pd.Print());
        //    // إعادة التوجيه إلى صفحة MyPOS بعد الطباعة
        //    return RedirectToAction("MyPOS");
        //}

    }
}
