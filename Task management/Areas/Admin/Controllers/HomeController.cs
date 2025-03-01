

using Domin.Entity;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class HomeController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		MasterDbcontext dbcontext;
		private readonly RoleManager<IdentityRole> _roleManager;
		IIUserInformation iUserInformation;
		IICompanyInformation iCompanyInformation;
		IIInvose iInvose;
		IIPurchase iPurchase;
		IIExpense iExpense;
		IISupplier iSupplier;
		IIUserService iUserService;
		IIStaff iStaff;

		public HomeController(UserManager<ApplicationUser> userManager,  MasterDbcontext dbcontext1,  IIUserInformation iUserInformation1, IICompanyInformation iCompanyInformation1,IIInvose iInvose1,IIPurchase iPurchase1,IIExpense iExpense1,IISupplier iSupplier1, RoleManager<IdentityRole> roleManager, IIUserService iUserService1,IIStaff iStaff1)
		{
			_userManager = userManager;
			iUserInformation = iUserInformation1;
			iCompanyInformation = iCompanyInformation1;
			iInvose = iInvose1;
			iPurchase = iPurchase1;
			iExpense = iExpense1;
			iSupplier= iSupplier1;
			_roleManager = roleManager;
			iUserService = iUserService1;
			iStaff = iStaff1;
			dbcontext = dbcontext1;
		}

		public async Task<IActionResult> Index(string userId)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			//vmodel.ListlicationUser = iUserInformation.GetAllByName(user.UserName).Take(1);
			var userd = vmodel.sUser = iUserInformation.GetById(userId);

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return NotFound();
			// الحصول على دور المستخدم
			var role = await _userManager.GetRolesAsync(user);

			ViewBag.UserRole = role.FirstOrDefault();

			var totalDebt = dbcontext.TBAccountingRestrictions
				.Where(a => dbcontext.TBLevelForeAccounts
					.Any(l4 => l4.AccountName == a.AccountingName &&
							   dbcontext.TBLevelThreeAccounts
								   .Any(l3 => l3.IdLevelThreeAccount == l4.IdLevelThreeAccount && l3.AccountName.Contains("الذمم الدائنة"))))
				.Sum(a => a.Debtor - a.creditor);
			TempData["totalDebtor"] = totalDebt;

			///////////////////////////////////////////////////////////////////////////////////
			var l3 = dbcontext.TBLevelThreeAccounts.Select(l => l.AccountName).ToList();

			var accountBalances = new List<AccountBalance>();

			foreach (var accountName in l3)
			{
				var totalDebtt = dbcontext.TBAccountingRestrictions
					.Where(a => dbcontext.TBLevelForeAccounts
						.Any(l4 => l4.AccountName == a.AccountingName &&
								   dbcontext.TBLevelThreeAccounts
									   .Any(l3 => l3.IdLevelThreeAccount == l4.IdLevelThreeAccount &&
												  l3.AccountName == accountName))) 
					.Sum(a => a.Debtor - a.creditor); 

				accountBalances.Add(new AccountBalance
				{
					AccountName = accountName, 
					Debt = totalDebtt
				});
			}

			ViewBag.AccountBalances = accountBalances;

			var labels = accountBalances.Select(a => a.AccountName).ToList();
			var data = accountBalances.Select(a => a.Debt).ToList();
			ViewBag.Labels = labels;
			ViewBag.Data = data;


			// جلب البيانات من الـ View أو من المصدر المطلوب
			var total = vmodel.ListViewInvose = iInvose.GetAll();
			// حساب المجموع من القائمة أو قاعدة البيانات
			var totalAmount = total.Sum(a => a.total);
			if (totalAmount > 0)
			{
				ViewBag.TotalAmount = totalAmount;
			}
			else
			{
				ViewBag.TotalAmount = 0;
			}

			// حساب الأصناف الأكثر مبيعًا وعدد المبيعات
			var topSellingItems = total
				.GroupBy(item => item.IdProduct) // افترضنا أن هناك ProductId لكل منتج
				.Select(group => new
				{
					ProductId = group.Key,
					ProductName = group.FirstOrDefault().ProductNameAr, // تأكد أن المنتج يحتوي على اسم
					TotalSales = group.Sum(item => item.total), // حساب مجموع المبيعات لكل منتج
					SalesCount = group.Sum(item => item.Quantity), // حساب إجمالي الكمية المباعة لكل منتج (افترض أن الكمية مخزنة في 'Quantity')
					ProductImage = group.FirstOrDefault().Photo // إضافة صورة المنتج
				})
				.OrderByDescending(item => item.SalesCount) // ترتيب الأصناف حسب إجمالي المبيعات
				/*.Take(10)*/ // عرض الأصناف الخمسة الأكثر مبيعًا
				.ToList();

			if(topSellingItems!=null)
			{
                ViewBag.TopSellingItems = topSellingItems;
            }
			else
			{
                ViewBag.TopSellingItems = null;
            }

			
			// إضافة الأصناف الأكثر مبيعًا إلى ViewBag

			//إجمالي الكمية المباعة 
			var Quantity = total.Sum(a => a.Quantity);
			if (Quantity>0)
			{
                ViewBag.Quantity = Quantity;
            }
			else
			{
                ViewBag.Quantity =0;
            }



            var maxnomb = total.DefaultIfEmpty(new TBViewInvose { InvoiceNumber = 0 }).Max(a => a.InvoiceNumber);
            ViewBag.max = maxnomb > 0 ? maxnomb : 1;



            var TotalPrchaase=vmodel.ListViewPurchase= iPurchase.GetAll().GroupBy(i => i.PurchaseNumber).Select(g => g.First()).ToList();
			var allPrchaase = TotalPrchaase.Sum(a => a.TotalAll);
			if (allPrchaase > 0) { ViewBag.totaallpurches = allPrchaase; }
			else 
			{ ViewBag.totaallpurches = 0; }
		


			var maxpurshess = TotalPrchaase.DefaultIfEmpty(new TBViewPurchase { PurchaseNumber = 0 }).Max(a => a.PurchaseNumber);
			ViewBag.maxpurs = maxpurshess > 0 ? maxpurshess : 1;




			var exching=vmodel.ListViewExpense= iExpense.GetAll();
			var totalexchinh = exching.Sum(a => a.Amount);
			if (totalexchinh > 0)
				{ ViewBag.totalExch = totalexchinh; }
			else {  ViewBag.totalExch = 0; };

			//جلب عدد الموردين 

			var supler=vmodel.ListViewSupplier= iSupplier.GetAll();
			var contSuplaer= supler.Count();
			if (contSuplaer > 0) {ViewBag.contsupler = contSuplaer; }
			else { ViewBag.contsupler = 0; };		

			

			//جلب  عدد العملاء

			var contcus= iUserInformation.GetAllByRole("Customer");
			var totalcontcu= contcus.Count();
			ViewBag.cont = totalcontcu;
			var online= iUserService.GetActiveCustomers();
			var onnn = online
			.GroupBy(item => item.Id) // افترضنا أن هناك ProductId لكل منتج
			.Select(group => new
			{
				ProductId = group.Key,
				name = group.FirstOrDefault().Name, // تأكد أن المنتج يحتوي على اسم
				email = group.FirstOrDefault().Email, // تأكد أن المنتج يحتوي على اسم
				PhoneNumber = group.FirstOrDefault().PhoneNumber, // تأكد أن المنتج يحتوي على اسم
			 // حساب إجمالي الكمية المباعة لكل منتج (افترض أن الكمية مخزنة في 'Quantity')
				ProductImage = group.FirstOrDefault().ImageUser // إضافة صورة المنتج
			})
			.OrderByDescending(item => item.name) // ترتيب الأصناف حسب إجمالي المبيعات
			/*.Take(10)*/ // عرض الأصناف الخمسة الأكثر مبيعًا
			.ToList();

			ViewBag.onlineuser = onnn;
			ViewBag.Profit = totalAmount  - totalexchinh;






			//عدد الموظفين 
			var staf =  vmodel.ListStaff = iStaff.GetAll();
			var contstaff= supler.Count();
			if (contstaff > 0) { ViewBag.contstaff = contstaff; }
			else { ViewBag.contstaff = 0; };
			return View(vmodel);


		}


		[AllowAnonymous]
		public IActionResult Denied()
        {
            return View();
        }
    }
}
public class AccountBalance
{
	public string AccountName { get; set; }
	public decimal Debt { get; set; }
}
