

using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LevelTwoAccountController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIMainAccount iMainAccount;
        IILevelTwoAccount iLevelTwoAccount;
        public LevelTwoAccountController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1, IIMainAccount iMainAccount1,IILevelTwoAccount iLevelTwoAccount1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iMainAccount = iMainAccount1;
            iLevelTwoAccount = iLevelTwoAccount1;
        }
        public IActionResult MyLevelTwoAccount()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();      
            var numberinvose = vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberAccount) +001
        : 001;
            ViewBag.minAccount = vmodel.ListMainAccount = iMainAccount.GetAll();
            return View(vmodel);
        }
        public IActionResult AddLevelTwoAccount(int? IdLevelTwoAccount)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            var numberinvose = vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberAccount) + 1
        : 001;
            vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            ViewBag.minAccount = vmodel.ListMainAccount = iMainAccount.GetAll();
            if (IdLevelTwoAccount != null)
            {
                vmodel.LevelTwoAccount = iLevelTwoAccount.GetById(Convert.ToInt32(IdLevelTwoAccount));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBLevelTwoAccount slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdLevelTwoAccount = model.LevelTwoAccount.IdLevelTwoAccount;
                slider.IdMainAccount = model.LevelTwoAccount.IdMainAccount;
                slider.NumberAccount = model.LevelTwoAccount.NumberAccount;
                slider.AccountName = model.LevelTwoAccount.AccountName;
                slider.Active = model.LevelTwoAccount.Active;
                slider.DateTimeEntry = model.LevelTwoAccount.DateTimeEntry;
                slider.DataEntry = model.LevelTwoAccount.DataEntry;
                slider.CurrentState = model.LevelTwoAccount.CurrentState;
                if (slider.IdLevelTwoAccount == 0 || slider.IdLevelTwoAccount == null)
                {

                    if (dbcontext.TBLevelTwoAccounts.Where(a => a.NumberAccount == slider.NumberAccount).ToList().Count > 0)
                    {
                        TempData["NumberAccount"] = ResourceWeb.VLNumberAccountDoplceted;
                        return RedirectToAction("MyMainAccount");
                    }
                    if (dbcontext.TBLevelTwoAccounts.Where(a => a.AccountName == slider.AccountName).ToList().Count > 0)
                    {
                        TempData["AccountName"] = ResourceWeb.VLAccountNameDoplceted;
                        return RedirectToAction("MyMainAccount");
                    }
                    var reqwest = iLevelTwoAccount.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyLevelTwoAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddLevelTwoAccount");
                    }
                }
                else
                {
                    var reqestUpdate = iLevelTwoAccount.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyLevelTwoAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddLevelTwoAccount");
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
        public IActionResult DeleteData(int IdLevelTwoAccount)
        {
            var reqwistDelete = iLevelTwoAccount.deleteData(IdLevelTwoAccount);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyLevelTwoAccount");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyLevelTwoAccount");

            }
        }

        [HttpGet]
        public JsonResult GetNextLevelTwoAccountNumber(int mainAccountId)
        {
            // البحث عن أكبر رقم حساب تابع لهذا الحساب الرئيسي في جدول المستوى الثاني
            var maxAccount = dbcontext.TBLevelTwoAccounts
                                      .Where(a => a.IdMainAccount == mainAccountId)
                                      .OrderByDescending(a => a.NumberAccount)
                                      .FirstOrDefault();

            if (maxAccount != null)
            {
                return Json(new { maxAccountNumber = maxAccount.NumberAccount });
            }
            else
            {
                // إذا لم تكن هناك حسابات تابعة، نعيد قيمة فارغة أو رقم مبدئي
                return Json(new { maxAccountNumber = "" });
            }
        }
    }
}
