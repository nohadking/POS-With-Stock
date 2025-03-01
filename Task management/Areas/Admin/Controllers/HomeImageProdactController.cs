

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeImageProdactController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIHomeImageProdact iHomeImageProdact;
        public HomeImageProdactController(IICompanyInformation iCompanyInformation1, IIHomeImageProdact iHomeImageProdact1)
        {
            iCompanyInformation = iCompanyInformation1;
            iHomeImageProdact = iHomeImageProdact1;
        }
        public IActionResult MYHomeImageProdact()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ListHomeImageProdact = iHomeImageProdact.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddEditHomeImageProdact(int? IdHomeImageProdact)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeImageProdact = iHomeImageProdact.GetAll();
            if (IdHomeImageProdact != null)
            {
                vmodel.HomeImageProdact = iHomeImageProdact.GetById(Convert.ToInt32(IdHomeImageProdact));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBHomeImageProdact slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdHomeImageProdact = model.HomeImageProdact.IdHomeImageProdact;
                slider.Photo = model.HomeImageProdact.Photo;
                slider.DateTimeEntry = model.HomeImageProdact.DateTimeEntry;
                slider.DataEntry = model.HomeImageProdact.DataEntry;
                slider.CurrentState = model.HomeImageProdact.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdHomeImageProdact == 0 || slider.IdHomeImageProdact == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iHomeImageProdact.saveData(slider);

                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYHomeImageProdact");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iHomeImageProdact.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditHomeImageProdact");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditHomeImageProdact");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.HomeImageProdact.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iHomeImageProdact.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYHomeImageProdact");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditHomeImageProdact");
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
                        var reqweistDeletPoto = iHomeImageProdact.DELETPHOTO(slider.IdHomeImageProdact);
                        var reqestUpdate2 = iHomeImageProdact.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYHomeImageProdact");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iHomeImageProdact.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditHomeImageProdactImage");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYHomeImageProdact");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdHomeImageProdact)
        {
            var reqwistDelete = iHomeImageProdact.deleteData(IdHomeImageProdact);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYHomeImageProdact");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYHomeImageProdact");
            }
        }
    }
}
