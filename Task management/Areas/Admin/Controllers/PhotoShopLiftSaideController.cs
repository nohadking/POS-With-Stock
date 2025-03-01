

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhotoShopLiftSaideController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIPhotoShopLiftSaide iPhotoShopLiftSaide;
        public PhotoShopLiftSaideController(IICompanyInformation iCompanyInformation1,IIPhotoShopLiftSaide iPhotoShopLiftSaide1)
        {
            iCompanyInformation=iCompanyInformation1;
            iPhotoShopLiftSaide =iPhotoShopLiftSaide1;
        }
        public IActionResult MYPhotoShopLiftSaide()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ListPhotoShopLiftSaide = iPhotoShopLiftSaide.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddEditPhotoShopLiftSaide(int? IdPhotoShopLiftSaide)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListPhotoShopLiftSaide = iPhotoShopLiftSaide.GetAll();
            if (IdPhotoShopLiftSaide != null)
            {
                vmodel.PhotoShopLiftSaide = iPhotoShopLiftSaide.GetById(Convert.ToInt32(IdPhotoShopLiftSaide));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPhotoShopLiftSaide slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdPhotoShopLiftSaide = model.PhotoShopLiftSaide.IdPhotoShopLiftSaide;
                slider.Photo = model.PhotoShopLiftSaide.Photo;
                slider.DateTimeEntry = model.PhotoShopLiftSaide.DateTimeEntry;
                slider.DataEntry = model.PhotoShopLiftSaide.DataEntry;
                slider.CurrentState = model.PhotoShopLiftSaide.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdPhotoShopLiftSaide == 0 || slider.IdPhotoShopLiftSaide == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iPhotoShopLiftSaide.saveData(slider);

                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYPhotoShopLiftSaide");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iPhotoShopLiftSaide.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditPhotoShopLiftSaide");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditPhotoShopLiftSaide");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.PhotoShopLiftSaide.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iPhotoShopLiftSaide.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYPhotoShopLiftSaide");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditPhotoShopLiftSaide");
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
                        var reqweistDeletPoto = iPhotoShopLiftSaide.DELETPHOTO(slider.IdPhotoShopLiftSaide);
                        var reqestUpdate2 = iPhotoShopLiftSaide.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYPhotoShopLiftSaide");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iPhotoShopLiftSaide.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditPhotoShopLiftSaideImage");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYPhotoShopLiftSaide");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPhotoShopLiftSaide)
        {
            var reqwistDelete = iPhotoShopLiftSaide.deleteData(IdPhotoShopLiftSaide);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYPhotoShopLiftSaide");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYPhotoShopLiftSaide");
            }
        }
    }
}
