

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeSliderContentController : Controller
    {
        MasterDbcontext dbcontext;
        IIHomeSliderContent iHomeSliderContent;
		IICompanyInformation iCompanyInformation;

		public HomeSliderContentController(MasterDbcontext dbcontext1,IIHomeSliderContent iHomeSliderContent1, IICompanyInformation iCompanyInformation1)
        {
            dbcontext=dbcontext1;
            iHomeSliderContent=iHomeSliderContent1;
			iCompanyInformation = iCompanyInformation1;
		}
        public IActionResult MyHomeSliderContent()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();
            return View(vmodel);
        } 
       public IActionResult AddHomeSliderContent(int? IdHomeSliderContent)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();

            // تأكد من أن HomeSliderContent مهيأ حتى لو لم يكن هناك ID
            if (vmodel.HomeSliderContent == null)
            {
                vmodel.HomeSliderContent = new TBHomeSliderContent(); // أو النوع الصحيح
            }

            if (IdHomeSliderContent != null)
            {
                vmodel.HomeSliderContent = iHomeSliderContent.GetById(Convert.ToInt32(IdHomeSliderContent));
            }

            return View(vmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBHomeSliderContent slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdHomeSliderContent = model.HomeSliderContent.IdHomeSliderContent;
                slider.TitleOneAr = model.HomeSliderContent.TitleOneAr;
                slider.TitleOneEn = model.HomeSliderContent.TitleOneEn;
                slider.TitleTwoEn = model.HomeSliderContent.TitleTwoEn;
                slider.TitleTwoAr = model.HomeSliderContent.TitleTwoAr;
                slider.TitleButtonAr = model.HomeSliderContent.TitleButtonAr;
                slider.TitleButtonEn = model.HomeSliderContent.TitleButtonEn;
                slider.UrlButtonAr = model.HomeSliderContent.UrlButtonAr;
                slider.UrlButtonEn = model.HomeSliderContent.UrlButtonEn;        
               slider.DataEntry = model.HomeSliderContent.DataEntry;
                slider.DateTimeEntry = model.HomeSliderContent.DateTimeEntry;
                slider.CurrentState = model.HomeSliderContent.CurrentState;
                if (slider.IdHomeSliderContent == 0 || slider.IdHomeSliderContent == null)
                {
                    if (dbcontext.TBHomeSliderContents.Where(a => a.TitleOneAr == slider.TitleOneAr).ToList().Count > 0)
                    {
                        TempData["TitleOneAr"] = ResourceWeb.VLTitleOneArDoplceted;
                        return RedirectToAction("AddHomeSliderContent");
                    }

                    if (dbcontext.TBHomeSliderContents.Where(a => a.TitleOneEn == slider.TitleOneEn).ToList().Count > 0)
                    {
                        TempData["TitleOneEn"] = ResourceWeb.VLTitleOneEnDoplceted;
                        return RedirectToAction("AddHomeSliderContent");
                    }
                    var reqwest = iHomeSliderContent.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyHomeSliderContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddHomeSliderContent");
                    }
                }
                else
                {
                    var reqestUpdate = iHomeSliderContent.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyHomeSliderContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddHomeSliderContent");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddHomeSliderContent");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdHomeSliderContent)
        {
            var reqwistDelete = iHomeSliderContent.deleteData(IdHomeSliderContent);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyHomeSliderContent");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyHomeSliderContent");

            }
        }
     
    }
}
