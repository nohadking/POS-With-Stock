
namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanyInformationController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        public CompanyInformationController(IICompanyInformation iCompanyInformation1,MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
            iCompanyInformation =iCompanyInformation1;
        }
        public IActionResult MYCompanyInformation()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditCompanyInformation(int? IdCompanyInformation)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            if (IdCompanyInformation != null)
            {
                vmodel.CompanyInformation = iCompanyInformation.GetById(Convert.ToInt32(IdCompanyInformation));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditCompanyInformationImage(int? IdCompanyInformation)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            if (IdCompanyInformation != null)
            {
                vmodel.CompanyInformation = iCompanyInformation.GetById(Convert.ToInt32(IdCompanyInformation));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCompanyInformation slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية CompanyInformation تحتوي على القيم
                //if (model.CompanyInformation == null)
                //{
                //    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                //    return RedirectToAction("AddEditCompanyInformation");
                //}

                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdCompanyInformation = model.CompanyInformation.IdCompanyInformation;
                slider.Photo = model.CompanyInformation.Photo;
                slider.NameCompanyAr = model.CompanyInformation.NameCompanyAr;
                slider.NameCompanyEn = model.CompanyInformation.NameCompanyEn;
                slider.Mobile = model.CompanyInformation.Mobile;
                slider.NameOner = model.CompanyInformation.NameOner;
                slider.PhoneOner = model.CompanyInformation.PhoneOner;
                slider.EmailOner = model.CompanyInformation.EmailOner;
                slider.EmailCompany = model.CompanyInformation.EmailCompany;
                slider.ShortDescriptionAr = model.CompanyInformation.ShortDescriptionAr;
                slider.ShortDescriptionEn = model.CompanyInformation.ShortDescriptionEn;
                slider.DescriptionEn = model.CompanyInformation.DescriptionEn;
                slider.DescriptionAr = model.CompanyInformation.DescriptionAr;
                slider.AddressAr = model.CompanyInformation.AddressAr;
                slider.AddressEn = model.CompanyInformation.AddressEn;
                slider.FaceBoock = model.CompanyInformation.FaceBoock;
                slider.Instagram = model.CompanyInformation.Instagram;
                slider.YouTube = model.CompanyInformation.YouTube;
                slider.Mabs = model.CompanyInformation.Mabs;       
                slider.DateTimeEntry = model.CompanyInformation.DateTimeEntry;
                slider.DataEntry = model.CompanyInformation.DataEntry;
                slider.CurrentState = model.CompanyInformation.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdCompanyInformation == 0 || slider.IdCompanyInformation == null)
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

                    // تحقق من تكرار اسم الفئة
                    if (dbcontext.TBCompanyInformations.Where(a => a.NameCompanyEn == slider.NameCompanyEn).ToList().Count > 0)
                    {
                        TempData["NameCompanyEn"] = ResourceWeb.VLCompanyInformationsEnDoplceted;
                        return RedirectToAction("MYCompanyInformation");
                    }

                    if (dbcontext.TBCompanyInformations.Where(a => a.NameCompanyEn == slider.NameCompanyEn).ToList().Count > 0)
                    {
                        TempData["NameCompanyAr"] = ResourceWeb.VLCompanyInformationsArDoplceted;
                        return RedirectToAction("MYCompanyInformation");
                    }

                    // حفظ البيانات
                    var reqwest = iCompanyInformation.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYCompanyInformation");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditCompanyInformation");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.CompanyInformation.Photo;
                        var reqestUpdate2 = iCompanyInformation.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCompanyInformation");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditCompanyInformation", model);
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
                        var reqweistDeletPoto = iCompanyInformation.DELETPHOTO(slider.IdCompanyInformation);
                        var reqestUpdate2 = iCompanyInformation.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCompanyInformation");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYCompanyInformation");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditCompanyInformation", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdCompanyInformation)
        {
            var reqwistDelete = iCompanyInformation.deleteData(IdCompanyInformation);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYCompanyInformation");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYCompanyInformation");
            }
        }
    }
}
