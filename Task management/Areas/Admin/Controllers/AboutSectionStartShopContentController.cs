using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutSectionStartShopContentController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIAboutSectionStartShopContent   iAboutSectionStartShopContent;
        public AboutSectionStartShopContentController(IICompanyInformation  iCompanyInformation1,IIAboutSectionStartShopContent iAboutSectionStartShopContent1)
        {
            iCompanyInformation = iCompanyInformation1;
            iAboutSectionStartShopContent = iAboutSectionStartShopContent1;
        }
        public IActionResult MYAboutSectionStartShopContent()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListAboutSectionStartShopContent = iAboutSectionStartShopContent.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddEditAboutSectionStartShopContent(int? IdAboutSectionStartShopContent)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartShopContent = iAboutSectionStartShopContent.GetAll();

            if (IdAboutSectionStartShopContent != null)
            {
                vmodel.AboutSectionStartShopContent = iAboutSectionStartShopContent.GetById(Convert.ToInt32(IdAboutSectionStartShopContent));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditAboutSectionStartShopContentImage(int? IdAboutSectionStartShopContent)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartShopContent = iAboutSectionStartShopContent.GetAll();
            if (IdAboutSectionStartShopContent != null)
            {
                vmodel.AboutSectionStartShopContent = iAboutSectionStartShopContent.GetById(Convert.ToInt32(IdAboutSectionStartShopContent));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBAboutSectionStartShopContent slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية AboutSectionStartShopContent تحتوي على القيم
                if (model.AboutSectionStartShopContent == null)
                {
                    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                    return RedirectToAction("AddEditAboutSectionStartShopContent");
                }
                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdAboutSectionStartShopContent = model.AboutSectionStartShopContent.IdAboutSectionStartShopContent;
                slider.TitleOneAr = model.AboutSectionStartShopContent.TitleOneAr;
                slider.TitleOneEn = model.AboutSectionStartShopContent.TitleOneEn;
                slider.TitleTwoAr = model.AboutSectionStartShopContent.TitleTwoAr;
                slider.TitleTwoEn = model.AboutSectionStartShopContent.TitleTwoEn;
                slider.DescriptionAr = model.AboutSectionStartShopContent.DescriptionAr;
                slider.DescriptionEn = model.AboutSectionStartShopContent.DescriptionEn;
                slider.DateTimeEntry = model.AboutSectionStartShopContent.DateTimeEntry;
                slider.DataEntry = model.AboutSectionStartShopContent.DataEntry;
                slider.CurrentState = model.AboutSectionStartShopContent.CurrentState;
                slider.Photo = model.AboutSectionStartShopContent.Photo;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdAboutSectionStartShopContent == 0 || slider.IdAboutSectionStartShopContent == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    else
                    {
                        TempData["Message"] = ResourceWeb.VLimageuplode;
                        return Redirect(returnUrl);
                    }             
                    // حفظ البيانات
                    var reqwest = iAboutSectionStartShopContent.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYAboutSectionStartShopContent");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iAboutSectionStartShopContent.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditAboutSectionStartShopContent");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.AboutSectionStartShopContent.Photo;
                        var reqestUpdate2 = iAboutSectionStartShopContent.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYAboutSectionStartShopContent");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditAboutSectionStartShopContent", model);
                        }
                    }
                    else
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                        // حذف الصورة القديمة إذا لزم الأمر
                        var reqweistDeletPoto = iAboutSectionStartShopContent.DELETPHOTO(slider.IdAboutSectionStartShopContent);
                        var reqestUpdate2 = iAboutSectionStartShopContent.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYAboutSectionStartShopContent");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iAboutSectionStartShopContent.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYAboutSectionStartShopContent");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditAboutSectionStartShopContent", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdAboutSectionStartShopContent)
        {
            var reqwistDelete = iAboutSectionStartShopContent.deleteData(IdAboutSectionStartShopContent);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYAboutSectionStartShopContent");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYAboutSectionStartShopContent");
            }
        }
  
    }
}
