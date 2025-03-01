using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhotoAddProdactController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIPhotoAddProdact iPhotoAddProdact;
        IIProduct iProduct;
        public PhotoAddProdactController(IICompanyInformation iCompanyInformation1,IIPhotoAddProdact iPhotoAddProdact1,IIProduct iProduct1)
        {
            iCompanyInformation=iCompanyInformation1;
            iPhotoAddProdact =iPhotoAddProdact1;
            iProduct =iProduct1;
        }
        public IActionResult MYPhotoAddProdact()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            ViewBag.Product = iProduct.GetAll();
            vmodel.ListViewPhotoAddProdact = iPhotoAddProdact.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddEditPhotoAddProdact(int? IdPhotoAddProdact)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            ViewBag.Product = iProduct.GetAll();
            vmodel.ListViewPhotoAddProdact = iPhotoAddProdact.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            if (IdPhotoAddProdact != null)
            {
                vmodel.PhotoAddProdact = iPhotoAddProdact.GetById(Convert.ToInt32(IdPhotoAddProdact));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPhotoAddProdact slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdPhotoAddProdact = model.PhotoAddProdact.IdPhotoAddProdact;
                slider.IdProduct = model.PhotoAddProdact.IdProduct;
                slider.Photo = model.PhotoAddProdact.Photo;
                slider.DateTimeEntry = model.PhotoAddProdact.DateTimeEntry;
                slider.DataEntry = model.PhotoAddProdact.DataEntry;
                slider.CurrentState = model.PhotoAddProdact.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdPhotoAddProdact == 0 || slider.IdPhotoAddProdact == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iPhotoAddProdact.saveData(slider);

                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYPhotoAddProdact");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iPhotoAddProdact.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditPhotoAddProdact");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditPhotoAddProdact");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.PhotoAddProdact.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iPhotoAddProdact.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYPhotoAddProdact");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditPhotoAddProdact");
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
                        var reqweistDeletPoto = iPhotoAddProdact.DELETPHOTO(slider.IdPhotoAddProdact);
                        var reqestUpdate2 = iPhotoAddProdact.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYPhotoAddProdact");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iPhotoAddProdact.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditPhotoAddProdactImage");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYPhotoAddProdact");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPhotoAddProdact)
        {
            var reqwistDelete = iPhotoAddProdact.deleteData(IdPhotoAddProdact);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYPhotoAddProdact");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYPhotoAddProdact");
            }
        }
      
    }
}
