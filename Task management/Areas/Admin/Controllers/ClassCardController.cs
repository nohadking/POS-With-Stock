

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ClassCardController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIClassCard iClassCard;
        IIUnit iUnit;
        public ClassCardController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IIClassCard iClassCard1,IIUnit iUnit1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iClassCard = iClassCard1;
            iUnit = iUnit1; 
        }
        public IActionResult MYClassCard()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewClassCard = iClassCard.GetAll();
            ViewBag.Unit = iUnit.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditClassCard(int? IdClassCard)
        {
            ViewBag.Unit = iUnit.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewClassCard = iClassCard.GetAll();
            if (IdClassCard != null)
            {
                vmodel.ClassCard = iClassCard.GetById(Convert.ToInt32(IdClassCard));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBClassCard slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                // تأكد من أن الخاصية ClassCard تحتوي على القيم
                if (model.ClassCard == null)
                {
                    TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
                    return RedirectToAction("AddEditClassCard");
                }
                // نسخ القيم من النموذج إلى الكائن slider
                slider.IdClassCard = model.ClassCard.IdClassCard;
                slider.IdUnit = model.ClassCard.IdUnit;
                slider.Photo = model.ClassCard.Photo;
                slider.ItemName = model.ClassCard.ItemName;
                slider.CodeItem = model.ClassCard.CodeItem;
                slider.ProductionDate = model.ClassCard.ProductionDate;
                slider.ExpiryDate = model.ClassCard.ExpiryDate;         
                slider.DateTimeEntry = model.ClassCard.DateTimeEntry;
                slider.DataEntry = model.ClassCard.DataEntry;
                slider.CurrentState = model.ClassCard.CurrentState;
                slider.Active = model.ClassCard.Active;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdClassCard == 0 || slider.IdClassCard == null)
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
                    if (dbcontext.TBClassCards.Where(a => a.ItemName == slider.ItemName).ToList().Count > 0)
                    {
                        TempData["ItemName"] = ResourceWeb.VLItemNameDoplceted;
                        return RedirectToAction("MYClassCard");
                    }

              
                    // حفظ البيانات
                    var reqwest = iClassCard.saveData(slider);
                    if (reqwest)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYClassCard");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iClassCard.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditClassCard");
                    }
                }
                else
                {
                    // تحديث البيانات
                    if (file.Count() == 0)
                    {
                        slider.Photo = model.ClassCard.Photo;
                        var reqestUpdate2 = iClassCard.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYClassCard");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditClassCard", model);
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
                        var reqweistDeletPoto = iClassCard.DELETPHOTO(slider.IdClassCard);
                        var reqestUpdate2 = iClassCard.UpdateData(slider);
                        if (reqestUpdate2)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYClassCard");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iClassCard.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("MYClassCard");
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddEditClassCard", model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdClassCard)
        {
            var reqwistDelete = iClassCard.deleteData(IdClassCard);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYClassCard");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYClassCard");
            }
        }
    }
}
