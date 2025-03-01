using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIStaff iStaff;
        IIBLevelThreeAccount iBLevelThreeAccount;
        public StaffController(MasterDbcontext dbcontext1, IICompanyInformation iCompanyInformation1, IIStaff iStaff1, IIBLevelThreeAccount iBLevelThreeAccount1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iStaff = iStaff1;
            iBLevelThreeAccount = iBLevelThreeAccount1;
        }
        public IActionResult MYStaff()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListStaff = iStaff.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			ViewBag.BLevelThreeAccount = iBLevelThreeAccount.GetAll();
			return View(vmodel);
        }
        public IActionResult AddEditStaff(int? IdStaff)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			ViewBag.BLevelThreeAccount = iBLevelThreeAccount.GetAll();
			vmodel.ListStaff = iStaff.GetAll();
            if (IdStaff != null)
            {
                vmodel.Staff = iStaff.GetById(Convert.ToInt32(IdStaff));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditStaffImage(int? IdStaff)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListStaff = iStaff.GetAll();
            if (IdStaff != null)
            {
                vmodel.Staff = iStaff.GetById(Convert.ToInt32(IdStaff));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBStaff slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية Staff تحتوي على القيم
                if (model.Staff == null)
                {
                    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                    return RedirectToAction("AddEditStaff");
                }
                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdStaff = model.Staff.IdStaff;
                slider.EmployeeFullname = model.Staff.EmployeeFullname;
                slider.EmployeePhone = model.Staff.EmployeePhone;
                slider.EmployeeEmail = model.Staff.EmployeeEmail;
                slider.AccountNumber = model.Staff.AccountNumber;
                slider.JobTitle = model.Staff.JobTitle;
                slider.EmployeeAddress = model.Staff.EmployeeAddress;
                slider.DateTimeEntry = model.Staff.DateTimeEntry;
                slider.DataEntry = model.Staff.DataEntry;
                slider.CurrentState = model.Staff.CurrentState;
                slider.Active = model.Staff.Active;
                slider.Photo = model.Staff.Photo;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdStaff == 0 || slider.IdStaff == null)
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
                    if (dbcontext.TBStaffs.Where(a => a.EmployeeFullname == slider.EmployeeFullname).ToList().Count > 0)
                    {
                        TempData["EmployeeFullname"] = ResourceWeb.VLEmployeeFullnameDoplceted;
                        return RedirectToAction("MYStaff");
                    }

                    if (dbcontext.TBStaffs.Where(a => a.EmployeePhone == slider.EmployeePhone).ToList().Count > 0)
                    {
                        TempData["EmployeePhone"] = ResourceWeb.VLEmployeePhoneDoplceted;
                        return RedirectToAction("MYStaff");
                    }

                    // حفظ البيانات
                    var reqwest = iStaff.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYStaff");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iStaff.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditStaff");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.Staff.Photo;
                        var reqestUpdate2 = iStaff.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYStaff");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditStaff", model);
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
                        var reqweistDeletPoto = iStaff.DELETPHOTO(slider.IdStaff);
                        var reqestUpdate2 = iStaff.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYStaff");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iStaff.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYStaff");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditStaff", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdStaff)
        {
            var reqwistDelete = iStaff.deleteData(IdStaff);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYStaff");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYStaff");
            }
        }

        [HttpGet]

        public IActionResult GetNumberAccount(int idLevelThreeAccount)
        {
            // استرجاع حساب المستوى الثالث باستخدام idLevelThreeAccount
            var levelThreeAccount = dbcontext.TBLevelThreeAccounts
                .FirstOrDefault(x => x.IdLevelThreeAccount == idLevelThreeAccount);

            if (levelThreeAccount != null)
            {
                // البحث عن الحسابات في المستوى الرابع
                var levelFourAccounts = dbcontext.TBLevelForeAccounts
                    .Where(x => x.IdLevelThreeAccount == levelThreeAccount.IdLevelThreeAccount)
                    .OrderByDescending(x => x.AccountNumberlivl)
                    .ToList();

                string newAccountNumber;

                if (levelFourAccounts.Any())
                {
                    // إذا وجدنا حسابات في المستوى الرابع، نأخذ أكبر رقم حساب ونضيف عليه 1
                    long highestAccountNumber = levelFourAccounts.First().AccountNumberlivl;
                    long newAccountNum = highestAccountNumber + 1;
                    newAccountNumber = newAccountNum.ToString("D4");  // تنسيق الرقم ليكون 4 أرقام
                }
                else
                {
                    // إذا لم نجد حسابات في المستوى الرابع، نقوم بإضافة "0001" إلى رقم حساب المستوى الثالث
                    newAccountNumber = levelThreeAccount.NumberAccount + "0001";
                }

                // إرسال رقم الحساب الجديد إلى النموذج (Model) لكي يظهر في الـ View
                //ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

                //var supplier =  vmodel.supplier();
                //{
                //    NumberAccount = newAccountNumber
                //};

                // يمكنك تعديل الـ Model إذا كنت تستخدم `ViewModel`
                return Json(new { success = true, numberAccount = newAccountNumber });
            }

            return Json(new { success = false, message = "حساب المستوى الثالث غير موجود." });
        }



    }
}
