using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class PaymentMethodController : Controller
    {
        MasterDbcontext dbcontext;
        IIPaymentMethod iPaymentMethod;
		IICompanyInformation iCompanyInformation;
		public PaymentMethodController(MasterDbcontext dbcontext1,IIPaymentMethod iPaymentMethod1, IICompanyInformation iCompanyInformation1)
        {
			dbcontext = dbcontext1;
            iPaymentMethod = iPaymentMethod1;
			iCompanyInformation = iCompanyInformation1;
		}


		public IActionResult MyPaymentMethod()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListPaymentMethod = iPaymentMethod.GetAll();
			return View(vmodel);
		}
		public IActionResult AddPaymentMethod(int? IdPaymentMethod)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListPaymentMethod = iPaymentMethod.GetAll();
			if (IdPaymentMethod != null)
			{
				vmodel.PaymentMethod = iPaymentMethod.GetById(Convert.ToInt32(IdPaymentMethod));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPaymentMethod slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdPaymentMethod = model.PaymentMethod.IdPaymentMethod;
				slider.PaymentMethodEn = model.PaymentMethod.PaymentMethodEn;
				slider.PaymentMethodAr = model.PaymentMethod.PaymentMethodAr;
				slider.DateTimeEntry = model.PaymentMethod.DateTimeEntry;
				slider.DataEntry = model.PaymentMethod.DataEntry;
				slider.CurrentState = model.PaymentMethod.CurrentState;
				slider.Active = model.PaymentMethod.Active;


				if (slider.IdPaymentMethod == 0 || slider.IdPaymentMethod == null)
				{
					if (dbcontext.TBPaymentMethods.Where(a => a.PaymentMethodEn == slider.PaymentMethodEn).ToList().Count > 0)
					{
						TempData["PaymentMethodEn"] = ResourceWeb.VLPaymentMethodEnDoplceted;
						return RedirectToAction("MyPaymentMethod");
					}
					if (dbcontext.TBPaymentMethods.Where(a => a.PaymentMethodAr == slider.PaymentMethodAr).ToList().Count > 0)
					{
						TempData["PaymentMethodAr"] = ResourceWeb.VLPaymentMethodArDoplceted;
						return RedirectToAction("MyPaymentMethod");
					}
					var reqwest = iPaymentMethod.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyPaymentMethod");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddPaymentMethod");
					}
				}
				else
				{
					var reqestUpdate = iPaymentMethod.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyPaymentMethod");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddPaymentMethod");
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
		public IActionResult DeleteData(int IdPaymentMethod)
		{
			var reqwistDelete = iPaymentMethod.deleteData(IdPaymentMethod);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyPaymentMethod");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyPaymentMethod");

			}
		}
	}
}
