using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DeliveryCompanieController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIDeliveryCompanie iDeliveryCompanie;
        public DeliveryCompanieController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IIDeliveryCompanie iDeliveryCompanie1)
        {
            dbcontext=dbcontext1;
            iCompanyInformation=iCompanyInformation1;
            iDeliveryCompanie =iDeliveryCompanie1;
        }
   
        public IActionResult MYDeliveryCompanie()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListDeliveryCompanie = iDeliveryCompanie.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddEditDeliveryCompanie(int? IdDeliveryCompanie)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListDeliveryCompanie = iDeliveryCompanie.GetAll();

            if (IdDeliveryCompanie != null)
            {
                vmodel.DeliveryCompanie = iDeliveryCompanie.GetById(Convert.ToInt32(IdDeliveryCompanie));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditDeliveryCompanieImage(int? IdDeliveryCompanie)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListDeliveryCompanie = iDeliveryCompanie.GetAll();
            if (IdDeliveryCompanie != null)
            {
                vmodel.DeliveryCompanie = iDeliveryCompanie.GetById(Convert.ToInt32(IdDeliveryCompanie));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBDeliveryCompanie slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية DeliveryCompanie تحتوي على القيم
                if (model.DeliveryCompanie == null)
                {
                    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                    return RedirectToAction("AddEditDeliveryCompanie");
                }

                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdDeliveryCompanie = model.DeliveryCompanie.IdDeliveryCompanie;
                slider.DeliveryCompanie = model.DeliveryCompanie.DeliveryCompanie;
                slider.PhoneNumber = model.DeliveryCompanie.PhoneNumber;
                slider.OnerName = model.DeliveryCompanie.OnerName;
                slider.OnerPhone = model.DeliveryCompanie.OnerPhone;
                slider.EmilCompanie = model.DeliveryCompanie.EmilCompanie;
                slider.EmilOner = model.DeliveryCompanie.EmilOner;
                slider.DeliveryCompanieAddress = model.DeliveryCompanie.DeliveryCompanieAddress;  
                slider.DateTimeEntry = model.DeliveryCompanie.DateTimeEntry;
                slider.DataEntry = model.DeliveryCompanie.DataEntry;
                slider.CurrentState = model.DeliveryCompanie.CurrentState;
                slider.Active = model.DeliveryCompanie.Active;
                slider.Photo = model.DeliveryCompanie.Photo;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdDeliveryCompanie == 0 || slider.IdDeliveryCompanie == null)
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
                    if (dbcontext.TBDeliveryCompanies.Where(a => a.DeliveryCompanie == slider.DeliveryCompanie).ToList().Count > 0)
                    {
                        TempData["DeliveryCompanie"] = ResourceWeb.VLDeliveryCompanieDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }
                    if (dbcontext.TBDeliveryCompanies.Where(a => a.PhoneNumber == slider.PhoneNumber).ToList().Count > 0)
                    {
                        TempData["PhoneNumber"] = ResourceWeb.VLPhoneNumberDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }

                    if (dbcontext.TBDeliveryCompanies.Where(a => a.OnerName == slider.OnerName).ToList().Count > 0)
                    {
                        TempData["OnerName"] = ResourceWeb.VLOnerNameDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }
                    if (dbcontext.TBDeliveryCompanies.Where(a => a.OnerPhone == slider.OnerPhone).ToList().Count > 0)
                    {
                        TempData["OnerPhone"] = ResourceWeb.VLOnerPhoneDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }
                    if (dbcontext.TBDeliveryCompanies.Where(a => a.EmilCompanie == slider.EmilCompanie).ToList().Count > 0)
                    {
                        TempData["EmilCompanie"] = ResourceWeb.VLEmilCompanieDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }   
                    if (dbcontext.TBDeliveryCompanies.Where(a => a.EmilOner == slider.EmilOner).ToList().Count > 0)
                    {
                        TempData["EmilOner"] = ResourceWeb.VLEmilOnerDoplceted;
                        return RedirectToAction("MYDeliveryCompanie");
                    }
                    // حفظ البيانات
                    var reqwest = iDeliveryCompanie.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYDeliveryCompanie");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iDeliveryCompanie.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditDeliveryCompanie");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.DeliveryCompanie.Photo;
                        var reqestUpdate2 = iDeliveryCompanie.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYDeliveryCompanie");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditDeliveryCompanie", model);
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
                        var reqweistDeletPoto = iDeliveryCompanie.DELETPHOTO(slider.IdDeliveryCompanie);
                        var reqestUpdate2 = iDeliveryCompanie.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYDeliveryCompanie");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iDeliveryCompanie.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYDeliveryCompanie");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditDeliveryCompanie", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdDeliveryCompanie)
        {
            var reqwistDelete = iDeliveryCompanie.deleteData(IdDeliveryCompanie);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYDeliveryCompanie");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYDeliveryCompanie");
            }
        }
   
    }
}
