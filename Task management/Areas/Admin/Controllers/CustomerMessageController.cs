using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerMessageController : Controller
    {
        IICustomerMessage iCustomerMessage;
        IICompanyInformation iCompanyInformation;
        public CustomerMessageController(IICustomerMessage iCustomerMessage1, IICompanyInformation iCompanyInformation1)
        {
            iCustomerMessage=iCustomerMessage1;
            iCompanyInformation =iCompanyInformation1; 
        }
        public IActionResult MyCustomerMessage()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListCustomerMessage = iCustomerMessage.GetAll();
            return View(vmodel);
        }
        public IActionResult AddCustomerMessage(int? IdCustomerMessage)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListCustomerMessage = iCustomerMessage.GetAll();
            if (IdCustomerMessage != null)
            {
                vmodel.CustomerMessage = iCustomerMessage.GetById(Convert.ToInt32(IdCustomerMessage));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCustomerMessage slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdCustomerMessage = model.CustomerMessage.IdCustomerMessage;
                slider.FullName = model.CustomerMessage.FullName;
                slider.MessageTitle = model.CustomerMessage.MessageTitle;
                slider.PhoneNumber = model.CustomerMessage.PhoneNumber;
                slider.EmailMessage = model.CustomerMessage.EmailMessage;
                slider.Message = model.CustomerMessage.Message;
                slider.DataEntry = model.CustomerMessage.DataEntry;
                slider.DateTimeEntry = model.CustomerMessage.DateTimeEntry;
                slider.CurrentState = model.CustomerMessage.CurrentState; 
                if (slider.IdCustomerMessage == 0 || slider.IdCustomerMessage == null)
                {     
                    var reqwest = iCustomerMessage.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyCustomerMessage");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddCustomerMessage");
                    }
                }
                else
                {
                    var reqestUpdate = iCustomerMessage.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyCustomerMessage");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddCustomerMessage");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect(returnUrl);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteData(int IdCustomerMessage)
        {
            var reqwistDelete = iCustomerMessage.deleteData(IdCustomerMessage);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyCustomerMessage");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyCustomerMessage");

            }
        }
    }
}
