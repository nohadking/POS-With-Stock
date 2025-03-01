

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class DeliveryCompanyPricingController : Controller
	{
		MasterDbcontext dbcontext;
		IICompanyInformation iCompanyInformation;
		IIDeliveryCompanie iDeliveryCompanie;
		IIArea iArea;
		IIDeliveryCompanyPricing iDeliveryCompanyPricing;
		public DeliveryCompanyPricingController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IIDeliveryCompanie iDeliveryCompanie1,IIArea iArea1,IIDeliveryCompanyPricing iDeliveryCompanyPricing1)
        {
			dbcontext = dbcontext1;
			iCompanyInformation = iCompanyInformation1;
			iDeliveryCompanie = iDeliveryCompanie1;
			iArea = iArea1;
			iDeliveryCompanyPricing = iDeliveryCompanyPricing1;		

		}
		public IActionResult MyDeliveryCompanyPricing()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

			vmodel.ListViewDeliveryCompanyPricing = iDeliveryCompanyPricing.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			ViewBag.DeliveryCompanie = vmodel.ListDeliveryCompanie = iDeliveryCompanie.GetAll();
			ViewBag.area = vmodel.ListViewArea = iArea.GetAll();

			return View(vmodel);
		}

		public IActionResult AddDeliveryCompanyPricing(int? IdDeliveryCompanyPricing)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListViewDeliveryCompanyPricing = iDeliveryCompanyPricing.GetAll();
			// تأكد من أن DeliveryCompanyPricing مهيأ حتى لو لم يكن هناك ID
			if (vmodel.DeliveryCompanyPricing == null)
			{
				vmodel.DeliveryCompanyPricing = new TBDeliveryCompanyPricing(); // أو النوع الصحيح
			}
			if (IdDeliveryCompanyPricing != null)
			{
				vmodel.DeliveryCompanyPricing = iDeliveryCompanyPricing.GetById(Convert.ToInt32(IdDeliveryCompanyPricing));
			}
			vmodel.ListViewDeliveryCompanyPricing = iDeliveryCompanyPricing.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			ViewBag.DeliveryCompanie = vmodel.ListDeliveryCompanie = iDeliveryCompanie.GetAll();
			ViewBag.area = vmodel.ListViewArea = iArea.GetAll(); ;
			return View(vmodel);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBDeliveryCompanyPricing slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdDeliveryCompanyPricing = model.DeliveryCompanyPricing.IdDeliveryCompanyPricing;
				slider.IdDeliveryCompanie = model.DeliveryCompanyPricing.IdDeliveryCompanie;
				slider.IdArea = model.DeliveryCompanyPricing.IdArea;
				slider.Pricing = model.DeliveryCompanyPricing.Pricing;
				slider.DataEntry = model.DeliveryCompanyPricing.DataEntry;
				slider.DateTimeEntry = model.DeliveryCompanyPricing.DateTimeEntry;
				slider.CurrentState = model.DeliveryCompanyPricing.CurrentState;
				if (slider.IdDeliveryCompanyPricing == 0 || slider.IdDeliveryCompanyPricing == null)
				{
					if (dbcontext.TBDeliveryCompanyPricings.Where(a => a.IdDeliveryCompanie == slider.IdDeliveryCompanie).Where(a => a.IdArea == slider.IdArea).ToList().Count > 0)
					{
						TempData["Area"] = ResourceWeb.VLDeliveryCompanyPricingNameDoplceted;
						return RedirectToAction("MyDeliveryCompanyPricing");
					}
					var reqwest = iDeliveryCompanyPricing.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyDeliveryCompanyPricing");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddDeliveryCompanyPricing");
					}
				}
				else
				{
					var reqestUpdate = iDeliveryCompanyPricing.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyDeliveryCompanyPricing");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddDeliveryCompanyPricing");
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("AddDeliveryCompanyPricing");
			}
		}

		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdDeliveryCompanyPricing)
		{
			var reqwistDelete = iDeliveryCompanyPricing.deleteData(IdDeliveryCompanyPricing);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyDeliveryCompanyPricing");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyDeliveryCompanyPricing");
			}
		}
	}
}
