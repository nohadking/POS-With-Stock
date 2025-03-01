using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        MasterDbcontext dbcontext;
        IICategory iCategory;
		IICompanyInformation iCompanyInformation;
		public CategoryController(MasterDbcontext dbcontext1,IICategory iCategory1, IICompanyInformation iCompanyInformation1)
        {
            dbcontext=dbcontext1;
            iCategory=iCategory1;
			iCompanyInformation = iCompanyInformation1;
		}

        public IActionResult MYCategory()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCategory = iCategory.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			return View(vmodel);
        }
        public IActionResult AddEditCategory(int? IdCategory)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListCategory = iCategory.GetAll();

            if (IdCategory != null)
            {
                vmodel.Category = iCategory.GetById(Convert.ToInt32(IdCategory));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditCategoryImage(int? IdCategory)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListCategory = iCategory.GetAll();
            if (IdCategory != null)
            {
                vmodel.Category = iCategory.GetById(Convert.ToInt32(IdCategory));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
      
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCategory slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية Category تحتوي على القيم
                if (model.Category == null)
                {
                    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                    return RedirectToAction("AddEditCategory");
                }

                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdCategory = model.Category.IdCategory;
                slider.CategoryNameAr = model.Category.CategoryNameAr;
                slider.CategoryNameEn = model.Category.CategoryNameEn;
                slider.DateTimeEntry = model.Category.DateTimeEntry;
                slider.DataEntry = model.Category.DataEntry;
                slider.CurrentState = model.Category.CurrentState;
                slider.Active = model.Category.Active;
                slider.Photo = model.Category.Photo;

                var file = HttpContext.Request.Form.Files;
                if (slider.IdCategory == 0 || slider.IdCategory == null)
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
                    if (dbcontext.TBCategorys.Where(a => a.CategoryNameEn == slider.CategoryNameEn).ToList().Count > 0)
                    {
                        TempData["CategorysEn"] = ResourceWeb.VLCategorysEnDoplceted;
                        return RedirectToAction("MYCategory");
                    }

                    if (dbcontext.TBCategorys.Where(a => a.CategoryNameAr == slider.CategoryNameAr).ToList().Count > 0)
                    {
                        TempData["CategorysAr"] = ResourceWeb.VLCategorysArDoplceted;
                        return RedirectToAction("MYCategory");
                    }

                    // حفظ البيانات
                    var reqwest = iCategory.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYCategory");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iCategory.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditCategory");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.Category.Photo;
                        var reqestUpdate2 = iCategory.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCategory");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditCategory", model);
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
                        var reqweistDeletPoto = iCategory.DELETPHOTO(slider.IdCategory);
                        var reqestUpdate2 = iCategory.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCategory");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iCategory.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYCategory");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditCategory", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdCategory)
        {
            var reqwistDelete = iCategory.deleteData(IdCategory);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYCategory");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYCategory");
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Savej(ViewmMODeElMASTER model, TBCategory slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تعيين القيم من النموذج إلى الكائن slider
                slider.IdCategory = slider.IdCategory;
                slider.CategoryNameAr = slider.CategoryNameAr;
                slider.CategoryNameEn = slider.CategoryNameEn;
                slider.DateTimeEntry = slider.DateTimeEntry;
                slider.DataEntry = slider.DataEntry;
                slider.CurrentState = slider.CurrentState;
                slider.Active = slider.Active;

                // التعامل مع الصورة المرفوعة
                var file = Files;
                if (file != null && file.Count > 0)
                {
                    string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                    await file[0].CopyToAsync(fileStream);
                    slider.Photo = Photo;
                    fileStream.Close();
                }
                else
                {
                    slider.Photo = model.Category.Photo;  // إذا لم يتم تحميل صورة جديدة، احتفظ بالصورة القديمة
                }

              

                // تحديث البيانات في قاعدة البيانات
                var updateResult = iCategory.UpdateData(slider);

                if (updateResult)
                {
                    TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                    return RedirectToAction("MYCategory");
                }
                else
                {
                    TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                    return RedirectToAction("AddEditCategory", model);
                }
            }
            catch (Exception ex)
            {
                // التعامل مع أي استثناء قد يحدث
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditCategory", model);
            }
        }

    }
}
