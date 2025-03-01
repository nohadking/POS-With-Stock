

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CustomerCategorieController : Controller
    {
        MasterDbcontext dbcontext;
        IICustomerCategorie iCustomerCategorie;
		IICompanyInformation iCompanyInformation;
		public CustomerCategorieController(MasterDbcontext dbcontext1,IICustomerCategorie iCustomerCategorie1, IICompanyInformation iCompanyInformation1)
        {
			dbcontext = dbcontext1;
			iCustomerCategorie = iCustomerCategorie1;
			iCompanyInformation = iCompanyInformation1;

		}
		public IActionResult MyCustomerCategorie()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCustomerCategorie = iCustomerCategorie.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
		}
		public IActionResult AddCustomerCategorie(int? IdCustomerCategorie)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCustomerCategorie = iCustomerCategorie.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			if (IdCustomerCategorie != null)
			{
				vmodel.CustomerCategorie = iCustomerCategorie.GetById(Convert.ToInt32(IdCustomerCategorie));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCustomerCategorie slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdCustomerCategorie = model.CustomerCategorie.IdCustomerCategorie;
				slider.CategoryNameAr = model.CustomerCategorie.CategoryNameAr;
				slider.CategoryNameEn = model.CustomerCategorie.CategoryNameEn;
				slider.DateTimeEntry = model.CustomerCategorie.DateTimeEntry;
				slider.DataEntry = model.CustomerCategorie.DataEntry;
				slider.CurrentState = model.CustomerCategorie.CurrentState;
				slider.Active = model.CustomerCategorie.Active;
			
			
				if (slider.IdCustomerCategorie == 0 || slider.IdCustomerCategorie == null)
				{
					if (dbcontext.TBCustomerCategories.Where(a => a.CategoryNameEn == slider.CategoryNameEn).ToList().Count > 0)
					{
						TempData["CategorysEn"] = ResourceWeb.VLCategorysEnDoplceted;
						return RedirectToAction("MyCustomerCategorie");
					}

					if (dbcontext.TBCustomerCategories.Where(a => a.CategoryNameAr == slider.CategoryNameAr).ToList().Count > 0)
					{
						TempData["CategorysAr"] = ResourceWeb.VLCategorysArDoplceted;
						return RedirectToAction("MyCustomerCategorie");
					}
					var reqwest = iCustomerCategorie.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyCustomerCategorie");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddCustomerCategorie");
					}
				}
				else
				{
					var reqestUpdate = iCustomerCategorie.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyCustomerCategorie");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddCustomerCategorie");
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
		public IActionResult DeleteData(int IdCustomerCategorie)
		{
			var reqwistDelete = iCustomerCategorie.deleteData(IdCustomerCategorie);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyCustomerCategorie");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyCustomerCategorie");

			}
		}
	
	}
}
