

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutSectionStartHomeContentController : Controller
    {
        IIAboutSectionStartHomeContent iAboutSectionStartHomeContent;
		IICompanyInformation iCompanyInformation;
		public AboutSectionStartHomeContentController(IIAboutSectionStartHomeContent iAboutSectionStartHomeContent1, IICompanyInformation iCompanyInformation1)
        {
            iAboutSectionStartHomeContent = iAboutSectionStartHomeContent1;
            iCompanyInformation = iCompanyInformation1;
		}
        public IActionResult MyAboutSectionStartHomeContent()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListAboutSectionStartHomeContent = iAboutSectionStartHomeContent.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
        }
      
        public IActionResult AddAboutSectionStartHomeContent(int? IdAboutSectionStartHomeContent)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListAboutSectionStartHomeContent = iAboutSectionStartHomeContent.GetAll();
            // تأكد من أن AboutSectionStartHomeContent مهيأ حتى لو لم يكن هناك ID
            if (vmodel.AboutSectionStartHomeContent == null)
            {
                vmodel.AboutSectionStartHomeContent = new TBAboutSectionStartHomeContent(); // أو النوع الصحيح
            }
            if (IdAboutSectionStartHomeContent != null)
            {
                vmodel.AboutSectionStartHomeContent = iAboutSectionStartHomeContent.GetById(Convert.ToInt32(IdAboutSectionStartHomeContent));
            }
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
     
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBAboutSectionStartHomeContent slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdAboutSectionStartHomeContent = model.AboutSectionStartHomeContent.IdAboutSectionStartHomeContent;
                slider.TitleOneAr = model.AboutSectionStartHomeContent.TitleOneAr;
                slider.TitleOneEn = model.AboutSectionStartHomeContent.TitleOneEn;
                slider.TitleTwoEn = model.AboutSectionStartHomeContent.TitleTwoEn;
                slider.TitleTwoAr = model.AboutSectionStartHomeContent.TitleTwoAr;           
                slider.DataEntry = model.AboutSectionStartHomeContent.DataEntry;
                slider.DateTimeEntry = model.AboutSectionStartHomeContent.DateTimeEntry;
                slider.CurrentState = model.AboutSectionStartHomeContent.CurrentState;
                if (slider.IdAboutSectionStartHomeContent == 0 || slider.IdAboutSectionStartHomeContent == null)
                {
                    var reqwest = iAboutSectionStartHomeContent.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyAboutSectionStartHomeContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddAboutSectionStartHomeContent");
                    }
                }
                else
                {
                    var reqestUpdate = iAboutSectionStartHomeContent.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyAboutSectionStartHomeContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddAboutSectionStartHomeContent");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddAboutSectionStartHomeContent");
            }
        }
    
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdAboutSectionStartHomeContent)
        {
            var reqwistDelete = iAboutSectionStartHomeContent.deleteData(IdAboutSectionStartHomeContent);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyAboutSectionStartHomeContent");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyAboutSectionStartHomeContent");
            }
        }
    
    }
}
