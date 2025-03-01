
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class PhotoHomeSliderContentController : Controller
    {
        IIHomeSliderContent iHomeSliderContent;
		IIPhotoHomeSliderContent iPhotoHomeSliderContent;
		IICompanyInformation iCompanyInformation;
		public PhotoHomeSliderContentController(IIHomeSliderContent iHomeSliderContent1,IIPhotoHomeSliderContent iPhotoHomeSliderContent1, IICompanyInformation iCompanyInformation1)
        {
            iHomeSliderContent = iHomeSliderContent1;
			iPhotoHomeSliderContent = iPhotoHomeSliderContent1;
			iCompanyInformation = iCompanyInformation1;
		}
    
		public IActionResult MYPhotoHomeSliderContent()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			ViewBag.HomeSliderContent = iHomeSliderContent.GetAll();
			vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
		}
		public IActionResult AddEditPhotoHomeSliderContent(int? IdPhotoHomeSliderContent)
		{
			ViewBag.HomeSliderContent = iHomeSliderContent.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
			if (IdPhotoHomeSliderContent != null)
			{
				vmodel.PhotoHomeSliderContent = iPhotoHomeSliderContent.GetById(Convert.ToInt32(IdPhotoHomeSliderContent));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPhotoHomeSliderContent slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdPhotoHomeSliderContent = model.PhotoHomeSliderContent.IdPhotoHomeSliderContent;
				slider.IdHomeSliderContent = model.PhotoHomeSliderContent.IdHomeSliderContent;			
				slider.Photo = model.PhotoHomeSliderContent.Photo;
				slider.DateTimeEntry = model.PhotoHomeSliderContent.DateTimeEntry;
				slider.DataEntry = model.PhotoHomeSliderContent.DataEntry;
				slider.CurrentState = model.PhotoHomeSliderContent.CurrentState;
				var file = HttpContext.Request.Form.Files;
				if (slider.IdPhotoHomeSliderContent == 0 || slider.IdPhotoHomeSliderContent == null)
				{				
					if (file.Count() > 0)
					{
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
					}
					var reqwest = iPhotoHomeSliderContent.saveData(slider);

					if (reqwest == true)
					{						
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MYPhotoHomeSliderContent");
					}
					else
					{
						if (file.Count() > 0)
						{
							var PhotoNAme = slider.Photo;
							var delet = iPhotoHomeSliderContent.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
							return RedirectToAction("AddEditPhotoHomeSliderContent");
						}
						else
						{
							TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
							return RedirectToAction("AddEditPhotoHomeSliderContent");
						}
					}
				}
				else//في حال التعديل
				{
					if (file.Count() == 0)// في حال لا توجد صورة 
					{
						slider.Photo = model.PhotoHomeSliderContent.Photo;
						//TempData["Message"] = ResourceWeb.VLimageuplode;
						var reqestUpdate2 = iPhotoHomeSliderContent.UpdateData(slider);
						if (reqestUpdate2 == true)
						{
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYPhotoHomeSliderContent");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							//var delet = iService.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditPhotoHomeSliderContent");
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
						var reqweistDeletPoto = iPhotoHomeSliderContent.DELETPHOTO(slider.IdPhotoHomeSliderContent);
						var reqestUpdate2 = iPhotoHomeSliderContent.UpdateData(slider);
						if (reqestUpdate2 == true)
						{
							
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYPhotoHomeSliderContent");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							var delet = iPhotoHomeSliderContent.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditPhotoHomeSliderContentImage");
						}
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("MYPhotoHomeSliderContent");
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdPhotoHomeSliderContent)
		{
			var reqwistDelete = iPhotoHomeSliderContent.deleteData(IdPhotoHomeSliderContent);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MYPhotoHomeSliderContent");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MYPhotoHomeSliderContent");
			}
		}
		//ar 
		public IActionResult MYPhotoHomeSliderContentAr()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			ViewBag.HomeSliderContent = iHomeSliderContent.GetAll();
			vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
			return View(vmodel);
		}
		public IActionResult AddEditPhotoHomeSliderContentAr(int? IdPhotoHomeSliderContent)
		{
						ViewBag.HomeSliderContent = iHomeSliderContent.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
			if (IdPhotoHomeSliderContent != null)
			{
				vmodel.PhotoHomeSliderContent = iPhotoHomeSliderContent.GetById(Convert.ToInt32(IdPhotoHomeSliderContent));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBPhotoHomeSliderContent slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdPhotoHomeSliderContent = model.PhotoHomeSliderContent.IdPhotoHomeSliderContent;
				slider.IdHomeSliderContent = model.PhotoHomeSliderContent.IdHomeSliderContent;
				slider.Photo = model.PhotoHomeSliderContent.Photo;
				slider.DateTimeEntry = model.PhotoHomeSliderContent.DateTimeEntry;
				slider.DataEntry = model.PhotoHomeSliderContent.DataEntry;
				slider.CurrentState = model.PhotoHomeSliderContent.CurrentState;			
				var file = HttpContext.Request.Form.Files;
				if (slider.IdPhotoHomeSliderContent == 0 || slider.IdPhotoHomeSliderContent == null)
				{
					
					if (file.Count() > 0)
					{
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
					}
					var reqwest = iPhotoHomeSliderContent.saveData(slider);
					if (reqwest == true)
					{					
						TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
						return RedirectToAction("MYPhotoHomeSliderContentAr");
					}
					else
					{
						if (file.Count() > 0)
						{
							var PhotoNAme = slider.Photo;
							var delet = iPhotoHomeSliderContent.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
							return RedirectToAction("AddEditPhotoHomeSliderContentAr");
						}
						else
						{
							TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
							return RedirectToAction("AddEditPhotoHomeSliderContentAr");
						}
					}
				}
				else//في حال التعديل
				{
					if (file.Count() == 0)// في حال لا توجد صورة 
					{
						slider.Photo = model.PhotoHomeSliderContent.Photo;
						//TempData["Message"] = ResourceWeb.VLimageuplode;
						var reqestUpdate2 = iPhotoHomeSliderContent.UpdateData(slider);
						if (reqestUpdate2 == true)
						{
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYPhotoHomeSliderContentAr");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							//var delet = iService.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditPhotoHomeSliderContentAr");
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
						var reqweistDeletPoto = iPhotoHomeSliderContent.DELETPHOTO(slider.IdPhotoHomeSliderContent);
						var reqestUpdate2 = iPhotoHomeSliderContent.UpdateData(slider);
						if (reqestUpdate2 == true)
						{				
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYPhotoHomeSliderContentAr");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							var delet = iPhotoHomeSliderContent.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditPhotoHomeSliderContentImageAr");
						}
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("MYPhotoHomeSliderContentAr");
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteDataAr(int IdPhotoHomeSliderContent)
		{
			var reqwistDelete = iPhotoHomeSliderContent.deleteData(IdPhotoHomeSliderContent);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MYPhotoHomeSliderContentAr");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MYPhotoHomeSliderContentAr");
			}
		}
	}
}
