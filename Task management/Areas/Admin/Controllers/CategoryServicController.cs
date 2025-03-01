

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryServicController : Controller
    {
        MasterDbcontext dbcontext;
        IICategoryServic iCategoryServic;
		IICompanyInformation iCompanyInformation;
		public CategoryServicController(MasterDbcontext dbcontext1,IICategoryServic iCategoryServic1, IICompanyInformation iCompanyInformation1)
        {
            dbcontext=dbcontext1;
            iCategoryServic=iCategoryServic1;
			iCompanyInformation = iCompanyInformation1;
		}
        public IActionResult MYCategoryServic()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();

			vmodel.ListCategoryServic = iCategoryServic.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditCategoryServic(int? IdCategoryServic)
        {
        
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCategoryServic = iCategoryServic.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			if (IdCategoryServic != null)
            {
                vmodel.CategoryServic = iCategoryServic.GetById(Convert.ToInt32(IdCategoryServic));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCategoryServic slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdCategoryServic = model.CategoryServic.IdCategoryServic;
                slider.TitleOneAr = model.CategoryServic.TitleOneAr;
                slider.TitleOneEn = model.CategoryServic.TitleOneEn;
                slider.TitleTwoAr = model.CategoryServic.TitleTwoAr;
                slider.TitleTwoEn = model.CategoryServic.TitleTwoEn;
                slider.TitleThreAr = model.CategoryServic.TitleThreAr;
                slider.TitleThreEn = model.CategoryServic.TitleThreEn;
                slider.TitleForAr = model.CategoryServic.TitleForAr;
                slider.TitleForEn = model.CategoryServic.TitleForEn;
                slider.TitlefiveAr = model.CategoryServic.TitlefiveAr;
                slider.TitlefiveEn = model.CategoryServic.TitlefiveEn;
                slider.TitleButtonAr = model.CategoryServic.TitleButtonAr;
                slider.TitleButtonEn = model.CategoryServic.TitleButtonEn;
                slider.UrlButtonAr = model.CategoryServic.UrlButtonAr;
                slider.UrlButtonEn = model.CategoryServic.UrlButtonEn;
                slider.Active = model.CategoryServic.Active;   
                slider.Photo = model.CategoryServic.Photo;
                slider.DateTimeEntry = model.CategoryServic.DateTimeEntry;
                slider.DataEntry = model.CategoryServic.DataEntry;
                slider.CurrentState = model.CategoryServic.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdCategoryServic == 0 || slider.IdCategoryServic == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }

                    if (dbcontext.TBCategoryServics.Where(a => a.TitleOneAr == slider.TitleOneAr).ToList().Count > 0)
                    {
                        TempData["TitleOneAr"] = ResourceWeb.VLTitleOneArDoplceted;
                        return RedirectToAction("AddEditCategoryServic");
                    }

                    if (dbcontext.TBCategoryServics.Where(a => a.TitleOneEn == slider.TitleOneEn).ToList().Count > 0)
                    {
                        TempData["TitleOneEn"] = ResourceWeb.VLTitleOneEnDoplceted;
                        return RedirectToAction("AddEditCategoryServic");
                    }


                    var reqwest = iCategoryServic.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYCategoryServic");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iCategoryServic.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditCategoryServic");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditCategoryServic");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.CategoryServic.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iCategoryServic.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCategoryServic");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditCategoryServic");
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
                        var reqweistDeletPoto = iCategoryServic.DELETPHOTO(slider.IdCategoryServic);
                        var reqestUpdate2 = iCategoryServic.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCategoryServic");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iCategoryServic.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditCategoryServicImage");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYCategoryServic");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdCategoryServic)
        {
            var reqwistDelete = iCategoryServic.deleteData(IdCategoryServic);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYCategoryServic");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYCategoryServic");
            }
        }
        //ar 
        public IActionResult MYCategoryServicAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();

			vmodel.ListCategoryServic = iCategoryServic.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditCategoryServicAr(int? IdCategoryServic)
        {
          
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListCategoryServic = iCategoryServic.GetAll();
            if (IdCategoryServic != null)
            {
                vmodel.CategoryServic = iCategoryServic.GetById(Convert.ToInt32(IdCategoryServic));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
    
    }
}
