

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LevelForeAccountController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIMainAccount iMainAccount;
        IILevelTwoAccount iLevelTwoAccount;
        IIBLevelThreeAccount iBLevelThreeAccount;
        IILevelForeAccount iLevelForeAccount;
        public LevelForeAccountController(MasterDbcontext dbcontext1, IICompanyInformation iCompanyInformation1, IIMainAccount iMainAccount1, IILevelTwoAccount iLevelTwoAccount1,IIBLevelThreeAccount iBLevelThreeAccount1,IILevelForeAccount iLevelForeAccount1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iMainAccount = iMainAccount1;
            iLevelTwoAccount = iLevelTwoAccount1;
            iLevelForeAccount = iLevelForeAccount1;
            iBLevelThreeAccount = iBLevelThreeAccount1;
        }
        public IActionResult MyLevelForeAccount()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();
            ViewBag.minAccount = vmodel.ListMainAccount = iMainAccount.GetAll();
            ViewBag.LevelTwoAccount = vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            ViewBag.LevelThreeAccount = iBLevelThreeAccount.GetAll();
            var numberinvose = vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.LevelForeAccountsNumber) + 0001
        : 0001;       
            return View(vmodel);
        }
        public IActionResult AddMyLevelForeAccount(int? IdLevelForeAccount)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();
            ViewBag.minAccount = vmodel.ListMainAccount = iMainAccount.GetAll();
            ViewBag.LevelTwoAccount = vmodel.ListViewLevelTwoAccount = iLevelTwoAccount.GetAll();
            ViewBag.LevelThreeAccount = iBLevelThreeAccount.GetAll();
            var numberinvose = vmodel.ListViewLevelForeAccount = iLevelForeAccount.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.LevelForeAccountsNumber) + 0001
        : 0001;
            if (IdLevelForeAccount != null)
            {
                vmodel.LevelForeAccount = iLevelForeAccount.GetById(Convert.ToInt32(IdLevelForeAccount));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBLevelForeAccount slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdLevelForeAccount = model.LevelForeAccount.IdLevelForeAccount;
                slider.IdLevelThreeAccount = model.LevelForeAccount.IdLevelThreeAccount;
                slider.IdLevelTwoAccount = model.LevelForeAccount.IdLevelTwoAccount;
                slider.IdMainAccount = model.LevelForeAccount.IdMainAccount;
                slider.AccountName = model.LevelForeAccount.AccountName;
                slider.Active = model.LevelForeAccount.Active;
                slider.DateTimeEntry = model.LevelForeAccount.DateTimeEntry;
                slider.DataEntry = model.LevelForeAccount.DataEntry;
                slider.CurrentState = model.LevelForeAccount.CurrentState;

                //// **جلب بيانات الحسابات من المستوى الثاني**
                //var levelTwoAccount = dbcontext.TBLevelThreeAccounts
                //    .Where(a => a.IdLevelThreeAccount == model.LevelForeAccount.IdLevelThreeAccount)
                //    .Select(a => new
                //    {
                      
                //        AccountNumber = a.NumberAccount
                //    })
                //    .FirstOrDefault(); // الحصول على عنصر واحد فقط

                //if (levelTwoAccount == null)
                //{
                //    TempData["ErrorSave"] = "لم يتم العثور على حساب المستوى الثاني.";
                //    return RedirectToAction("MyLevelForeAccount");
                //}
                //model.LevelForeAccount.AccountNumberlivl = 0;
                //// **جلب الرقم الجديد فقط إذا لم يكن معينًا مسبقًا**
                //if (model.LevelForeAccount.AccountNumberlivl == 0)
                //{
                //    var maxLevelFourAccount = dbcontext.TBLevelForeAccounts
                //        .Where(a => a.IdLevelThreeAccount == slider.IdLevelThreeAccount)
                //        .OrderByDescending(a => a.AccountNumberlivl)
                //        .FirstOrDefault();

                //    // **تحديد الرقم الجديد بناءً على آخر رقم موجود**
                //    long nextNumber = maxLevelFourAccount != null 
                //        ? maxLevelFourAccount.AccountNumberlivl + 1 : 1;

                //    // **دمج الأرقام وتحويلها إلى int مع تنسيق الرقم الجديد بأربعة أرقام**
                //    slider.AccountNumberlivl = long.Parse($"{levelTwoAccount.AccountNumber}{nextNumber.ToString("D4")}");
                //}
                //else
                //{
                    slider.AccountNumberlivl = model.LevelForeAccount.AccountNumberlivl;
                //}

                // **التحقق من التكرار قبل الحفظ**
                if (slider.IdLevelForeAccount == 0 || slider.IdLevelForeAccount == null)
                {
                    if (dbcontext.TBLevelForeAccounts.Any(a => a.AccountNumberlivl == slider.AccountNumberlivl))
                    {
                        TempData["NumberAccount"] = ResourceWeb.VLNumberAccountDoplceted;
                        return RedirectToAction("MyLevelForeAccount");
                    }
                    if (dbcontext.TBLevelForeAccounts.Any(a => a.AccountName == slider.AccountName))
                    {
                        TempData["AccountName"] = ResourceWeb.VLAccountNameDoplceted;
                        return RedirectToAction("MyLevelForeAccount");
                    }

                    var request = iLevelForeAccount.saveData(slider);
                    if (request)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyLevelForeAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddMyLevelForeAccount");
                    }
                }
                else
                {
                    var requestUpdate = iLevelForeAccount.UpdateData(slider);
                    if (requestUpdate)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyLevelForeAccount");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddMyLevelForeAccount");
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
        public IActionResult DeleteData(int IdLevelForeAccount)
        {
            var reqwistDelete = iLevelForeAccount.deleteData(IdLevelForeAccount);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyLevelForeAccount");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyLevelForeAccount");

            }

        }
        [HttpGet]
        public JsonResult GetLevelTwoAccounts(int mainAccountId)
        {
            var levelTwoAccounts = dbcontext.TBLevelTwoAccounts
                .Where(a => a.IdMainAccount == mainAccountId)
                .Select(a => new
                {
                    idLevelTwoAccount = a.IdLevelTwoAccount,
                    accountName = a.AccountName,
                    AccountNumber = a.NumberAccount // إضافة رقم الحساب
                })
                .ToList();

            return Json(levelTwoAccounts);
        }

        [HttpGet]
        public JsonResult GetLevelThreeAccounts(int levelTwoAccountId)
        {
            var levelThreeAccounts = dbcontext.ViewLevelThreeAccount
                .Where(a => a.IdLevelTwoAccount == levelTwoAccountId)
                .Select(a => new
                {
                    idLevelThreeAccount = a.IdLevelThreeAccount,
                    accountName = a.NameLevelThreeAccounts,
                    AccountNumber = a.NumberLevelThreeAccounts // إضافة رقم الحساب
                })
                .ToList();

            return Json(levelThreeAccounts);
        }

        [HttpGet]
        public JsonResult GetNextLevelFourAccountNumber(int levelThreeAccountId)
        {
            // البحث عن الحساب بأعلى رقم موجود في المستوى الرابع
            var maxLevelFourAccount = dbcontext.TBLevelForeAccounts
                .Where(a => a.IdLevelThreeAccount == levelThreeAccountId)
                .OrderByDescending(a => a.AccountNumberlivl)
                .FirstOrDefault();
            string newAccountNumber = "0001"; // الرقم الافتراضي في حال عدم وجود حسابات في المستوى الرابع
            if (maxLevelFourAccount != null)
            {
                // إذا كان هناك حسابات في المستوى الرابع، نأخذ الرقم التالي
                long nextNumber = maxLevelFourAccount.AccountNumberlivl + 1;
                newAccountNumber = nextNumber.ToString().PadLeft(4, '0');
            }
            else
            {
                // إذا لم يكن هناك حسابات في المستوى الرابع، نبحث في المستوى الثالث
                var maxLevelThreeAccount = dbcontext.TBLevelThreeAccounts
                    .Where(a => a.IdLevelThreeAccount == levelThreeAccountId)
                    .FirstOrDefault();
                if (maxLevelThreeAccount != null)
                {
                    // إذا كان هناك حسابات في المستوى الثالث، نأخذ رقم الحساب من المستوى الثالث
                    long baseAccountNumber = maxLevelThreeAccount.NumberAccount;

                    // إضافة "0001" على رقم الحساب في المستوى الثالث
                    long newAccount = long.Parse($"{baseAccountNumber}0001");

                    // تحويل الرقم الجديد إلى string وتنسيقه ليظهر كرقم ذو 8 أرقام
                    newAccountNumber = newAccount.ToString().PadLeft(8, '0');
                }
                else
                {
                    // إذا لم يكن هناك حسابات في المستوى الثالث أيضًا، نبدأ من الرقم الافتراضي 0001
                    newAccountNumber = "0001";
                }
            }
            return Json(new { accountNumber = newAccountNumber });
        }





    }
}
