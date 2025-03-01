

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AreaController : Controller
	{
		MasterDbcontext dbcontext;
		IICompanyInformation iCompanyInformation;
		IICitie iCitie;
		IIArea iArea;
		public AreaController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IICitie iCitie1 ,IIArea iArea1)
        {
			dbcontext = dbcontext1;
			iCompanyInformation = iCompanyInformation1;
			iCitie = iCitie1;
			iArea = iArea1;

		}
		public IActionResult MyArea()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewArea = iArea.GetAll();
			ViewBag.cite = vmodel.ListCitie = iCitie.GetAll();
	

			
			return View(vmodel);
		}
		public IActionResult AddArea(int? IdArea)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewArea = iArea.GetAll();
			ViewBag.cite = vmodel.ListCitie = iCitie.GetAll();
			if (IdArea != null)
			{
				vmodel.Area = iArea.GetById(Convert.ToInt32(IdArea));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBArea slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdArea = model.Area.IdArea;
				slider.IdCitie = model.Area.IdCitie;
				slider.AreaName = model.Area.AreaName;	
				slider.DateTimeEntry = model.Area.DateTimeEntry;
				slider.DataEntry = model.Area.DataEntry;
				slider.CurrentState = model.Area.CurrentState;
				slider.Active = model.Area.Active;
				if (slider.IdArea == 0 || slider.IdArea == null)
				{


					var reqwest = iArea.saveData(slider);
					if (reqwest == true)
					{
					

						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyArea");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddArea");
					}
				}
				else
				{
					var reqestUpdate = iArea.UpdateData(slider);
					if (reqestUpdate == true)
					{
						

						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyArea");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddArea");
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
		public IActionResult DeleteData(int IdArea)
		{
			var reqwistDelete = iArea.deleteData(IdArea);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyArea");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyArea");

			}
		}
	}
}
