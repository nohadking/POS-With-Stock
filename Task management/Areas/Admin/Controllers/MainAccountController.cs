

using Microsoft.Graph.Communications.CallRecords.MicrosoftGraphCallRecordsGetDirectRoutingCallsWithFromDateTimeWithToDateTime;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MainAccountController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIMainAccount iMainAccount;
        public MainAccountController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IIMainAccount iMainAccount1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iMainAccount = iMainAccount1;
        }
        public IActionResult MyMainAccount()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListMainAccount = iMainAccount.GetAll();
            var numberinvose = vmodel.ListMainAccount = iMainAccount.GetAll();
        
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberAccount) + 1
        : 1;






            return View(vmodel);
        }
        public IActionResult AddMainAccount(int? IdMainAccount)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListMainAccount = iMainAccount.GetAll();
            var numberinvose = vmodel.ListMainAccount = iMainAccount.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberAccount) + 1
        : 1;
            if (IdMainAccount != null)
            {
                vmodel.MainAccount = iMainAccount.GetById(Convert.ToInt32(IdMainAccount));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBMainAccount slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdMainAccount = model.MainAccount.IdMainAccount;
                slider.NumberAccount = model.MainAccount.NumberAccount;
                slider.AccountName = model.MainAccount.AccountName;
                slider.AccountName = model.MainAccount.AccountName;
                slider.DateTimeEntry = model.MainAccount.DateTimeEntry;
                slider.DataEntry = model.MainAccount.DataEntry;
                slider.CurrentState = model.MainAccount.CurrentState;
                slider.Active = model.MainAccount.Active;


                if (slider.IdMainAccount == 0 || slider.IdMainAccount == null)
                {
                    if (dbcontext.TBMainAccounts.Where(a => a.NumberAccount == slider.NumberAccount).ToList().Count > 0)
                    {
                        TempData["NumberAccount"] = ResourceWeb.VLNumberAccountDoplceted;
                        return RedirectToAction("MyMainAccount");
                    }
                    if (dbcontext.TBMainAccounts.Where(a => a.AccountName == slider.AccountName).ToList().Count > 0)
                    {
                        TempData["AccountName"] = ResourceWeb.VLAccountNameDoplceted;
                        return RedirectToAction("MyMainAccount");
                    }
                    var reqwest = iMainAccount.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyMainAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddMainAccount");
                    }
                }
                else
                {
                    var reqestUpdate = iMainAccount.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyMainAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddMainAccount");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect(returnUrl);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdMainAccount)
        {
            var reqwistDelete = iMainAccount.deleteData(IdMainAccount);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyMainAccount");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyMainAccount");

            }
        }
    }
}
