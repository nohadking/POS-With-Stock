

using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandProductController : Controller
    {
        MasterDbcontext dbcontext;
        IIBrandProduct iBrandProduct;
		IICompanyInformation iCompanyInformation;
		public BrandProductController(MasterDbcontext dbcontext1,IIBrandProduct iBrandProduct1, IICompanyInformation iCompanyInformation1)
        {
            dbcontext = dbcontext1;
            iBrandProduct = iBrandProduct1;
			iCompanyInformation = iCompanyInformation1;
		}
        public IActionResult MYBrandProduct()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListBrandProduct = iBrandProduct.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditBrandProduct(int? IdBrandProduct)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBrandProduct = iBrandProduct.GetAll();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			if (IdBrandProduct != null)
            {
                vmodel.BrandProduct = iBrandProduct.GetById(Convert.ToInt32(IdBrandProduct));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBBrandProduct slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdBrandProduct = model.BrandProduct.IdBrandProduct;
                slider.TitelOneEn = model.BrandProduct.TitelOneEn;
                slider.TitelOneAr = model.BrandProduct.TitelOneAr;
                slider.Photo = model.BrandProduct.Photo;
                slider.URlButtonEn = model.BrandProduct.URlButtonEn;
                slider.URlButtonAr = model.BrandProduct.URlButtonAr;
                slider.DateTimeEntry = model.BrandProduct.DateTimeEntry;
                slider.DataEntry = model.BrandProduct.DataEntry;
                slider.CurrentState = model.BrandProduct.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdBrandProduct == 0 || slider.IdBrandProduct == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    if (dbcontext.TBBrandProducts.Where(a => a.TitelOneEn == slider.TitelOneEn).ToList().Count > 0)
                    {
                        TempData["TitleOneEn"] = ResourceWeb.VLTitleOneEnDoplceted;
                        return RedirectToAction("AddEditBrandProduct");
                    }

                    if (dbcontext.TBBrandProducts.Where(a => a.TitelOneAr == slider.TitelOneAr).ToList().Count > 0)
                    {
                        TempData["TitleOneAr"] = ResourceWeb.VLTitleOneArDoplceted;
                        return RedirectToAction("AddEditBrandProduct");
                    }
                    var reqwest = iBrandProduct.saveData(slider);

                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYBrandProduct");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iBrandProduct.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditBrandProduct");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditBrandProduct");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.BrandProduct.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iBrandProduct.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYBrandProduct");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditBrandProduct");
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
                        var reqweistDeletPoto = iBrandProduct.DELETPHOTO(slider.IdBrandProduct);
                        var reqestUpdate2 = iBrandProduct.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYBrandProduct");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iBrandProduct.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditBrandProductImage");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYBrandProduct");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdBrandProduct)
        {
            var reqwistDelete = iBrandProduct.deleteData(IdBrandProduct);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYBrandProduct");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYBrandProduct");
            }
        }
        //ar 
        public IActionResult MYBrandProductAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ListBrandProduct = iBrandProduct.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditBrandProductAr(int? IdBrandProduct)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBrandProduct = iBrandProduct.GetAll();
            if (IdBrandProduct != null)
            {
                vmodel.BrandProduct = iBrandProduct.GetById(Convert.ToInt32(IdBrandProduct));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
   
  
    }
}
