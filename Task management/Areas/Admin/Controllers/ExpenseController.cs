using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using System.Threading.Tasks;
using Tafqeet;
using System.ComponentModel.DataAnnotations;


namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ExpenseController : Controller
	{
		IIExpense iExpense;
		IICompanyInformation iCompanyInformation;
		IIExpenseCategory iExpenseCategory;
		IILevelForeAccount iLevelForeAccount;
		MasterDbcontext dbcontext;
		IIAccountingRestriction iAccountingRestriction;
		public ExpenseController(IIExpense iExpense1, IICompanyInformation iCompanyInformation1, IIExpenseCategory iExpenseCategory1, IILevelForeAccount iLevelForeAccount1, MasterDbcontext dbcontext, IIAccountingRestriction iAccountingRestriction)
		{
			iExpense = iExpense1;
			iCompanyInformation = iCompanyInformation1;
			iExpenseCategory = iExpenseCategory1;
			iLevelForeAccount = iLevelForeAccount1;
			this.dbcontext = dbcontext;
			this.iAccountingRestriction = iAccountingRestriction;
		}
		public IActionResult MyExpense()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewExpense = iExpense.GetAll();
			ViewBag.ExpenseCategory = vmodel.ListExpenseCategory = iExpenseCategory.GetAll();
			ViewBag.LevelForeAccount = vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();

			ViewBag.Expense = vmodel.ListViewExpense = iExpense.GetAll().GroupBy(i => i.AccountName).Select(g => g.First()).ToList();



			var numberinvose = vmodel.ListViewExpense = iExpense.GetAll();
			ViewBag.nomberMax = numberinvose.Any()
		? numberinvose.Max(c => c.BondNumber) + 1
		: 1;
			return View(vmodel);
		}
		public IActionResult AddExpense(int? IdExpense)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewExpense = iExpense.GetAll();
			ViewBag.ExpenseCategory = vmodel.ListExpenseCategory = iExpenseCategory.GetAll();
			ViewBag.LevelForeAccount = vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();
			ViewBag.Expense = vmodel.ListViewExpense = iExpense.GetAll().GroupBy(i => i.AccountName).Select(g => g.First()).ToList();
			vmodel.ListViewExpense = iExpense.GetAll();
			ViewBag.ExpenseCategory = vmodel.ListExpenseCategory = iExpenseCategory.GetAll();
			if (IdExpense != null)
			{
				vmodel.Expense = iExpense.GetById(Convert.ToInt32(IdExpense));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBExpense slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdExpense = model.Expense.IdExpense;
				slider.IdExpenseCategory = model.Expense.IdExpenseCategory;
				slider.IdLevelForeAccount = model.Expense.IdLevelForeAccount;
				slider.BondNumber = model.Expense.BondNumber;
				slider.DateBond = model.Expense.DateBond;
				slider.Statement = model.Expense.Statement;
				slider.Amount = model.Expense.Amount;
				slider.DateTimeEntry = model.Expense.DateTimeEntry;
				slider.DataEntry = model.Expense.DataEntry;
				slider.CurrentState = model.Expense.CurrentState;
				if (slider.IdExpense == 0 || slider.IdExpense == null)
				{
					var reqwest = iExpense.saveData(slider);
					if (reqwest == true)
					{
						//var acc = new TBAccountingRestriction
						//{
						//	NumberaccountingRestrictions = 0,
						//	AccountingName = "i",
						//	BondType = "g",
						//	BondNumber = 9,
						//	Debtor = 0,
						//	creditor = 0,
						//	Statement = "",
						//	Nouts = "",
						//	DataEntry = model.Expense.DataEntry,
						//	DateTimeEntry = model.Expense.DateTimeEntry,
						//	CurrentState = true,
						//};

						//dbcontext.TBAccountingRestrictions.Add(acc);
						//dbcontext.SaveChanges();

						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyExpense");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddExpense");
					}
				}
				else
				{
					var reqestUpdate = iExpense.UpdateData(slider);
					if (reqestUpdate == true)
					{
						//var oldAcc = iAccountingRestriction.GetByBondNuAndBondType(model.Expense.BondNumber, "سند شراء");
						//if (oldAcc != null)
						//{
						//	dbcontext.TBAccountingRestrictions.Remove(oldAcc);

						//	var acc = new TBAccountingRestriction
						//	{
						//		NumberaccountingRestrictions = 0,
						//		AccountingName = "i",
						//		BondType = "g",
						//		BondNumber = 9,
						//		Debtor = 0,
						//		creditor = 0,
						//		Statement = "",
						//		Nouts = "",
						//		DataEntry = model.Expense.DataEntry,
						//		DateTimeEntry = model.Expense.DateTimeEntry,
						//		CurrentState = true,
						//	};

						//	dbcontext.TBAccountingRestrictions.Add(acc);
						//	dbcontext.SaveChanges();
						//}

						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyExpense");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddExpense");
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
		public IActionResult DeleteData(int IdExpense)
		{
			var reqwistDelete = iExpense.deleteData(IdExpense);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyExpense");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyExpense");

			}
		}



		////// print pdf ///////////////////
		///expense
		[HttpGet]
		public IActionResult GenerateExpensePdf(string? expense, string? cate, string? oneDate, string? startDate, string? endDate)
		{
			// تقرير عام
			if (expense == null && cate == null && oneDate == null && startDate == null && endDate == null)
			{
				var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
				return pdf;
			}
			else
			{
				if (oneDate != null)
				{
					// حسب تاريخ محدد
					var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
					return pdf;
				}

				else if (startDate != null && endDate != null)
				{
					if (expense != null)
					{
						// بين تاريخين و حساب
						var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
						return pdf;
					}

					else if (cate != null)
					{
						// حسب فئة السند بين تاريخين  
						var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
						return pdf;
					}

					else
					{
						//  بين تاريخين  
						var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
						return pdf;
					}
				}
				else if (expense != null)
				{
					// حسب حساب  
					var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
					return pdf;

				}
				else if (cate != null)
				{
					// حسب فئة السند  
					var pdf = CreatePDF(expense, cate, oneDate, startDate, endDate);
					return pdf;

				}
			}
			return Content("Invalid parameters or no data found.", "text/plain");
		}


		public IActionResult CreatePDF(string? expense, string? category, string? oneDate, string startDate, string endDate)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			var compny = dbcontext.TBCompanyInformations.FirstOrDefault();
			var expenses = new List<TBViewExpense>();

			var pdfDocument = Document.Create(container =>
			{

				// **حساب الإجماليات**
				if (expense == null && category == null && oneDate == null && startDate == null && endDate == null)
				{
					expenses = vmodel.ListViewExpense = iExpense.GetAll();
				}
				else
				{
					if (oneDate != null)
					{
						// حسب تاريخ محدد
						DateTime dt = Convert.ToDateTime(oneDate);
						expenses = vmodel.ListViewExpense = iExpense.GetByDetectedDt(dt);

					}

					else if (startDate != null && endDate != null)
					{
						if (expense != null)
						{
							// حسب الصرف بين تاريخين  
							DateTime startDt = Convert.ToDateTime(startDate);
							DateTime endD = Convert.ToDateTime(endDate);
							expenses = vmodel.ListViewExpense = iExpense.GetByExpenseAndPeriodDate(expense, startDt, endD);
						}

						else if (category != null)
						{
							// حسب فئة الصرف بين تاريخين  
							DateTime startDt = Convert.ToDateTime(startDate);
							DateTime endD = Convert.ToDateTime(endDate);
							expenses = vmodel.ListViewExpense = iExpense.GetByCategoryAndPeriodDate(category, startDt, endD);
						}

						else
						{
							// حسب من تاريخ لتاريخ  
							DateTime startDt = Convert.ToDateTime(startDate);
							DateTime endD = Convert.ToDateTime(endDate);
							expenses = vmodel.ListViewExpense = iExpense.GetByPeriodDate(startDt, endD);
						}
					}
					else if (expense != null)
					{
						// حسب صرفية  
						expenses = vmodel.ListViewExpense = iExpense.GetByExpense(expense);

					}

					else if (category != null)
					{
						// حسب فئة الصرف  
						expenses = vmodel.ListViewExpense = iExpense.GetByCategory(category);

					}
				}
				// زبط هدول معلم 

				//var totalQuantity = accounts.Sum(p => p.Quantity);
				//var totalAmount = accounts.Sum(p => p.TotalAll);

				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.DefaultTextStyle(x => x.FontSize(12));


					page.Header()
					.Column(header =>
					{
						header.Item().PaddingTop(10).AlignRight().Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}").FontSize(10).Bold();
						if (expense == null && category == null && oneDate == null && startDate == null && endDate == null)
						{
							header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف العام ").FontSize(20).Bold();
						}
						else
						{
							if (oneDate != null)
							{
								// حسب تاريخ محدد
								header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف لتاريخ {oneDate} ").FontSize(20).Bold();
							}

							else if (startDate != null && endDate != null)
							{
								if (expense != null)
								{
									// حسب اسم مورد بين تاريخين  
									header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف لـ {expense} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
								}

								else if (category != null)
								{
									// حسب نوع السند بين تاريخين  
									header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف لفئة  {category} بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();

								}

								else
								{
									// حسب من تاريخ لتاريخ  
									header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف بين تاريخ {startDate} وتاريخ {endDate} ").FontSize(20).Bold();
								}
							}
							else if (expense != null)
							{
								// حسب اسم مورد  
								header.Item().Border(1).AlignCenter().Text($"تقرير مصاريف {expense} ").FontSize(20).Bold();

							}

							else if (category != null)
							{
								// حسب نوع السند  
								header.Item().Border(1).AlignCenter().Text($"تقرير المصاريف لفئة: {category} ").FontSize(20).Bold();

							}
						}

						if (compny != null)
						{
							header.Item().Border(1).AlignCenter().Text($" {compny.NameCompanyAr}").FontSize(14);
							header.Item().Border(1).AlignCenter().Text($" {compny.AddressAr}").FontSize(12);
							header.Item().Border(1).AlignCenter().Text($" {compny.Mobile}").FontSize(12);
						}
					});
					decimal totalAmount = expenses.Sum(e => e.Amount);

					page.Content().Column(content =>
					{
						content.Item().AlignCenter().Text($"تقرير المصاريف").FontSize(16).Bold();
						content.Item().AlignCenter().Text("----------------------------------------------").FontSize(12).Bold();
						content.Item().Table(table =>
						{
							table.ColumnsDefinition(columns =>
							{
								columns.RelativeColumn(); // مدخل البيانات
								columns.RelativeColumn(); // تاريخ الادخال
								columns.RelativeColumn(); // البيان
								columns.RelativeColumn(); // المبلغ
								columns.RelativeColumn(); // رقم السند
								columns.RelativeColumn(); // فئة الحساب
								columns.RelativeColumn(); // اسم الحساب
							});

							table.Header(header =>
							{

								header.Cell().Border(1).AlignCenter().Text("مدخل البيانات").Bold();
								header.Cell().Border(1).AlignCenter().Text("تاريخ ادخال البيانات").Bold();
								header.Cell().Border(1).AlignCenter().Text("البيان").Bold();
								header.Cell().Border(1).AlignCenter().Text("المبلغ").Bold();
								header.Cell().Border(1).AlignCenter().Text("رقم السند").Bold();
								header.Cell().Border(1).AlignCenter().Text("فئة الحساب").Bold();
								header.Cell().Border(1).AlignCenter().Text("اسم الحساب").Bold();

							});

							foreach (var ex in expenses)
							{
								string cachname = string.Empty;
								if (ex.DataEntry != null)
								{
									cachname = dbcontext.VwUsers
														 .Where(a => a.Email == ex.DataEntry)
														 .Select(a => a.Name)
														 .FirstOrDefault();
								}


								table.Cell().Border(1).AlignCenter().Text(cachname);
								table.Cell().Border(1).AlignCenter().Text(ex.DateTimeEntry.ToString("yyyy-MM-dd HH:mm:ss"));
								table.Cell().Border(1).AlignCenter().Text(ex.Statement);
								table.Cell().Border(1).AlignCenter().Text(ex.Amount);
								table.Cell().Border(1).AlignCenter().Text(ex.BondNumber);
								table.Cell().Border(1).AlignCenter().Text(ex.ExpenseCategory);
								table.Cell().Border(1).AlignCenter().Text(ex.AccountName);







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
								header.Cell().AlignCenter().Text("المحاسبة").FontSize(12).Bold();
								header.Cell().AlignCenter().Text("المجموع العام").FontSize(12).Bold();
							});

							// القيم في السطر الثاني
							table.Cell().Border(0).AlignCenter().Text($"----------------").FontSize(12);
							table.Cell().Border(0).AlignCenter().Text($"${totalAmount:C}").FontSize(12);
						});

						// إضافة تاريخ الطباعة أسفل التقرير



					});
					page.Footer().AlignCenter().Text(txt =>
					{
						txt.Span("الصفحة ").FontSize(10).Bold();
						txt.CurrentPageNumber().FontSize(10).Bold(); // رقم الصفحة الحالية
						txt.Span(" من ").FontSize(10).Bold();
						txt.TotalPages().FontSize(10).Bold(); // إجمالي عدد الصفحات
					});

				});
			});
			var pdfData = pdfDocument.GeneratePdf();
			return File(pdfData, "application/pdf", "Report.pdf");
		}


		public async Task<IActionResult> CreateDirectPdf(int num)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			var expense = dbcontext.ViewExpense.Where(a => a.BondNumber == num).FirstOrDefault();
			var comp = dbcontext.TBCompanyInformations.Where(a => a.CurrentState == true).FirstOrDefault();

			if (expense != null)
			{
				var pdfDocument = Document.Create(container =>
				{
					container.Page(page =>
					{
						page.Size(PageSizes.A5.Landscape());
						page.Margin(20);
						page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

						page.Content().Column(column =>
						{
							// Header Section (Logo and Company Details)
							column.Item().Row(row =>
							{
								// Company Logo - في المنتصف

								// Company Details - المحتوى العربي على اليسار والإنجليزي على اليمين
								row.RelativeItem(4).Column(subColumn =>
								{
									// المحتوى العربي على اليسار
									subColumn.Item().AlignLeft().Text($"{comp.NameCompanyAr}").FontSize(14).Bold();
									subColumn.Item().AlignLeft().Text($"{comp.AddressAr}").FontSize(10);
									subColumn.Item().AlignLeft().Text($"{comp.Phone}").FontSize(10);
									subColumn.Item().AlignLeft().Text($"{comp.EmailCompany}").FontSize(10);
								});
								string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Home", "1016e7c8-9a31-41c9-baeb-cb3e67a62cdd.jpg");
								// التحقق من وجود الصورة قبل إضافتها
								var hit = 75;
								// التحقق من وجود الصورة قبل إضافتها
								if (System.IO.File.Exists(imagePath))
								{
									row.RelativeItem(2).AlignCenter()

											.Image(imagePath); ;
									// ضبط حجم الصورة إلى 75x75 بكسل
								}
								else
								{
									// عرض نص بديل إذا لم يتم العثور على الصورة
									row.RelativeItem(2).AlignCenter()
									   .Text("No Image Available").FontSize(12).Bold();
								}
								row.RelativeItem(4).Column(subColumn =>
								{
									// المحتوى الإنجليزي على اليمين
									subColumn.Item().AlignRight().Text($"{comp.NameCompanyEn}").FontSize(14).Bold();
									subColumn.Item().AlignRight().Text($"{comp.AddressEn}").FontSize(10);
									subColumn.Item().AlignRight().Text($"{comp.Phone}").FontSize(10);
									subColumn.Item().AlignRight().Text($"{comp.EmailCompany}").FontSize(10);
								});
							});

							column.Item().BorderColor(Colors.Black).LineHorizontal(1);
							column.Item().Height(10);
							// Voucher Title
							column.Item().AlignCenter().Text("سند صرف").FontSize(18).Bold();

							column.Item().Text($"{expense.Amount}").AlignCenter().FontSize(15).Bold().FontColor(Colors.Black);
							column.Item().Text("--------------------------").AlignCenter().FontSize(12).FontColor(Colors.Black);
							column.Item().Height(10);
							// Date and Voucher Number
							column.Item().Row(row =>
							{
								// إضافة التاريخ مع محاذاة صحيحة
								row.RelativeItem(1).AlignRight().PaddingRight(0).Text($"التاريخ: {expense.DateBond:yyyy/MM/dd}").Bold();

								// إضافة رقم السند مع محاذاة صحيحة
								row.RelativeItem(1).AlignRight().Text($"رقم السند: {expense.BondNumber}").Bold();
							});

							column.Item().Height(10);

							// Payment Details
							column.Item().Row(row =>
							{
								row.RelativeItem(3).AlignLeft().Text("Paid to Mr./Mrs.:").Bold();
								row.RelativeItem(5).Border(0).PaddingLeft(5).PaddingRight(5).AlignCenter().AlignCenter().Text($"{expense.AccountName}");
								row.RelativeItem(3).AlignRight().Text(":الاسم المدفوع له").Bold();

							});
							column.Item().Text("----------------------------------------------------------------------------------------------------------------------------------------").AlignCenter().FontSize(8).FontColor(Colors.Black);
							column.Item().Row(row =>
							{
								// تحويل المبلغ إلى نص مكتوب باللغة العربية
								string amountInWords = TafqeetHelper.ConvertToArabic((decimal)expense.Amount);



								// عرض المبلغ بالتفقيط (كتابة النص)
								row.RelativeItem(3).AlignLeft().Text("Amount in Words:").Bold();
								row.RelativeItem(5).Border(0).PaddingLeft(5).PaddingRight(5).AlignCenter().Text($"{amountInWords}");
								row.RelativeItem(3).AlignRight().Text(":المبلغ كتابة").Bold();
							});
							column.Item().Text("----------------------------------------------------------------------------------------------------------------------------------------").AlignCenter().FontSize(8).FontColor(Colors.Black);


							column.Item().Row(row =>
							{
								row.RelativeItem(3).AlignLeft().Text("On Settlement of:").Bold();
								row.RelativeItem(5).Border(0).PaddingLeft(5).PaddingRight(5).AlignCenter().Text($"{expense.Statement}");

								row.RelativeItem(3).AlignRight().Text(":التفاصيل").Bold();

							});
							column.Item().Text("----------------------------------------------------------------------------------------------------------------------------------------").AlignCenter().FontSize(8).FontColor(Colors.Black);


							column.Item().Height(10);

							// Payment Methods
						

							column.Item().Height(15);

							// Signatures
							column.Item().Row(row =>
							{
								row.RelativeItem(3).Column(subColumn =>
								{
									subColumn.Item().BorderTop(1).Width(100);
									subColumn.Item().AlignCenter().Text("التوقيع").Bold();
									subColumn.Item().AlignCenter().Text("Signature").FontSize(10);
									subColumn.Item().Text("--------------------------").AlignCenter().FontSize(12).FontColor(Colors.Black);

								});

								row.RelativeItem(3).Column(subColumn =>
								{
									subColumn.Item().BorderTop(1).Width(100);
									subColumn.Item().AlignCenter().Text("الصراف").Bold();
									subColumn.Item().AlignCenter().Text("Cashier").FontSize(10);
									subColumn.Item().Text("--------------------------").AlignCenter().FontSize(12).FontColor(Colors.Black);
								});

								row.RelativeItem(3).Column(subColumn =>
								{
									subColumn.Item().BorderTop(1).Width(100);
									subColumn.Item().AlignCenter().Text("المدير").Bold();
									subColumn.Item().AlignCenter().Text("Manager").FontSize(10);
									subColumn.Item().Text("--------------------------").AlignCenter().FontSize(12).FontColor(Colors.Black);
								});

							});

						});


					});
                  
                });


				return File(pdfDocument.GeneratePdf(), "application/pdf", "Payment_Voucher.pdf");
			}

			return RedirectToAction("MyExpense");
		}





	}
}





