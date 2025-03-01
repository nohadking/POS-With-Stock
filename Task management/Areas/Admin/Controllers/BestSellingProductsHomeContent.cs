
namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BestSellingProductsHomeContent : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIBestSellingProductsHomeContent iBestSellingProductsHomeContent;
        public BestSellingProductsHomeContent(IICompanyInformation iCompanyInformation1,IIBestSellingProductsHomeContent iBestSellingProductsHomeContent1)
        {
            iCompanyInformation = iCompanyInformation1;
            iBestSellingProductsHomeContent = iBestSellingProductsHomeContent1;
        }
        public IActionResult MyBestSellingProductsHomeContent()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }

        public IActionResult AddBestSellingProductsHomeContent(int? IdBestSellingProductsHomeContent)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll();
            // تأكد من أن BestSellingProductsHomeContent مهيأ حتى لو لم يكن هناك ID
            if (vmodel.BestSellingProductsHomeContent == null)
            {
                vmodel.BestSellingProductsHomeContent = new TBBestSellingProductsHomeContent(); // أو النوع الصحيح
            }
            if (IdBestSellingProductsHomeContent != null)
            {
                vmodel.BestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetById(Convert.ToInt32(IdBestSellingProductsHomeContent));
            }
            return View(vmodel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBBestSellingProductsHomeContent slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdBestSellingProductsHomeContent = model.BestSellingProductsHomeContent.IdBestSellingProductsHomeContent;
                slider.TitleOneAr = model.BestSellingProductsHomeContent.TitleOneAr;
                slider.TitleOneEn = model.BestSellingProductsHomeContent.TitleOneEn;
                slider.TitleTwoEn = model.BestSellingProductsHomeContent.TitleTwoEn;
                slider.TitleTwoAr = model.BestSellingProductsHomeContent.TitleTwoAr;
                slider.DataEntry = model.BestSellingProductsHomeContent.DataEntry;
                slider.DateTimeEntry = model.BestSellingProductsHomeContent.DateTimeEntry;
                slider.CurrentState = model.BestSellingProductsHomeContent.CurrentState;
                if (slider.IdBestSellingProductsHomeContent == 0 || slider.IdBestSellingProductsHomeContent == null)
                {
                    var reqwest = iBestSellingProductsHomeContent.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyBestSellingProductsHomeContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddBestSellingProductsHomeContent");
                    }
                }
                else
                {
                    var reqestUpdate = iBestSellingProductsHomeContent.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyBestSellingProductsHomeContent");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddBestSellingProductsHomeContent");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddBestSellingProductsHomeContent");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdBestSellingProductsHomeContent)
        {
            var reqwistDelete = iBestSellingProductsHomeContent.deleteData(IdBestSellingProductsHomeContent);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyBestSellingProductsHomeContent");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyBestSellingProductsHomeContent");
            }
        }
    }
}
