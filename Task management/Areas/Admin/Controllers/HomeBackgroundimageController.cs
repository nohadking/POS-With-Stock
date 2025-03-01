

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class HomeBackgroundimageController : Controller
    {
		IICompanyInformation iCompanyInformation;
		IIHomeBackgroundimage iHomeBackgroundimage;
		public HomeBackgroundimageController(IICompanyInformation iCompanyInformation1,IIHomeBackgroundimage iHomeBackgroundimage1)
        {
			iCompanyInformation=iCompanyInformation1;
			iHomeBackgroundimage =iHomeBackgroundimage1;

		}
		public IActionResult MYHomeBackgroundimage()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
		
			vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
		}
		public IActionResult AddEditHomeBackgroundimage(int? IdHomeBackgroundimage)
		{
		
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll();
			if (IdHomeBackgroundimage != null)
			{
				vmodel.HomeBackgroundimage = iHomeBackgroundimage.GetById(Convert.ToInt32(IdHomeBackgroundimage));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBHomeBackgroundimage slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdHomeBackgroundimage = model.HomeBackgroundimage.IdHomeBackgroundimage;
				slider.Photo = model.HomeBackgroundimage.Photo;
				slider.DateTimeEntry = model.HomeBackgroundimage.DateTimeEntry;
				slider.DataEntry = model.HomeBackgroundimage.DataEntry;
				slider.CurrentState = model.HomeBackgroundimage.CurrentState;
				var file = HttpContext.Request.Form.Files;
				if (slider.IdHomeBackgroundimage == 0 || slider.IdHomeBackgroundimage == null)
				{
					if (file.Count() > 0)
					{
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
					}
					var reqwest = iHomeBackgroundimage.saveData(slider);

					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MYHomeBackgroundimage");
					}
					else
					{
						if (file.Count() > 0)
						{
							var PhotoNAme = slider.Photo;
							var delet = iHomeBackgroundimage.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
							return RedirectToAction("AddEditHomeBackgroundimage");
						}
						else
						{
							TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
							return RedirectToAction("AddEditHomeBackgroundimage");
						}
					}
				}
				else//في حال التعديل
				{
					if (file.Count() == 0)// في حال لا توجد صورة 
					{
						slider.Photo = model.HomeBackgroundimage.Photo;
						//TempData["Message"] = ResourceWeb.VLimageuplode;
						var reqestUpdate2 = iHomeBackgroundimage.UpdateData(slider);
						if (reqestUpdate2 == true)
						{
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYHomeBackgroundimage");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							//var delet = iService.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditHomeBackgroundimage");
						}
					}
					else
					{
						//في حال رفع صورة   
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
						var reqweistDeletPoto = iHomeBackgroundimage.DELETPHOTO(slider.IdHomeBackgroundimage);
						var reqestUpdate2 = iHomeBackgroundimage.UpdateData(slider);
						if (reqestUpdate2 == true)
						{

							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYHomeBackgroundimage");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							var delet = iHomeBackgroundimage.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditHomeBackgroundimageImage");
						}
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("MYHomeBackgroundimage");
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdHomeBackgroundimage)
		{
			var reqwistDelete = iHomeBackgroundimage.deleteData(IdHomeBackgroundimage);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MYHomeBackgroundimage");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MYHomeBackgroundimage");
			}
		}

	}
}
