

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CitieController : Controller
	{
		MasterDbcontext dbcontext;
		IICompanyInformation iCompanyInformation;
		IICitie iCitie;
		public CitieController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IICitie iCitie1)
        {
			dbcontext=dbcontext1;
			iCompanyInformation = iCompanyInformation1;
			iCitie = iCitie1;

		}
		public IActionResult MyCitie()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCitie = iCitie.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
		}

		public IActionResult AddCitie(int? IdCitie)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCitie = iCitie.GetAll();
			// تأكد من أن Citie مهيأ حتى لو لم يكن هناك ID
			if (vmodel.Citie == null)
			{
				vmodel.Citie = new TBCitie(); // أو النوع الصحيح
			}
			if (IdCitie != null)
			{
				vmodel.Citie = iCitie.GetById(Convert.ToInt32(IdCitie));
			}
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCitie slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdCitie = model.Citie.IdCitie;
				slider.CitieName = model.Citie.CitieName;
				slider.DataEntry = model.Citie.DataEntry;
				slider.DateTimeEntry = model.Citie.DateTimeEntry;
				slider.CurrentState = model.Citie.CurrentState;
				if (slider.IdCitie == 0 || slider.IdCitie == null)
				{
					if (dbcontext.TBCities.Where(a => a.CitieName == slider.CitieName).ToList().Count > 0)
					{
						TempData["CitieName"] = ResourceWeb.VLCitieNameDoplceted;
						return RedirectToAction("MyCitie");
					}
					var reqwest = iCitie.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyCitie");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddCitie");
					}
				}
				else
				{
					var reqestUpdate = iCitie.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyCitie");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddCitie");
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("AddCitie");
			}
		}

		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdCitie)
		{
			var reqwistDelete = iCitie.deleteData(IdCitie);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyCitie");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyCitie");
			}
		}
	}
}
