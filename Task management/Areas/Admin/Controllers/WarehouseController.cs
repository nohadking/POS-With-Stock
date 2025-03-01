using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WarehouseController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIUserInformation iUserInformation;
        IIWarehouse iWarehouse;
        IIBLevelThreeAccount iBLevelThreeAccount;
        public WarehouseController(MasterDbcontext dbcontext1,IICompanyInformation  iCompanyInformation1, IIUserInformation iUserInformation1,IIWarehouse iWarehouse1,IIBLevelThreeAccount iBLevelThreeAccount1 )
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iUserInformation = iUserInformation1;
            iWarehouse = iWarehouse1;
            iBLevelThreeAccount = iBLevelThreeAccount1;
        }
        public IActionResult MyWarehouse()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            ViewBag.BLevelThreeAccount = iBLevelThreeAccount.GetAll();
            vmodel.ListViewWarehouse = iWarehouse.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            ViewBag.user = iUserInformation.GetAllByRole("Warehousekeeper");
            return View(vmodel);
        }

        public IActionResult AddWarehouse(int? IdWarehouse)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewWarehouse = iWarehouse.GetAll();
            // تأكد من أن Warehouse مهيأ حتى لو لم يكن هناك ID
            if (vmodel.Warehouse == null)
            {
                vmodel.Warehouse = new TBWarehouse(); // أو النوع الصحيح
            }
            if (IdWarehouse != null)
            {
                vmodel.Warehouse = iWarehouse.GetById(Convert.ToInt32(IdWarehouse));
            }
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            ViewBag.BLevelThreeAccount = iBLevelThreeAccount.GetAll();
            ViewBag.user = iUserInformation.GetAllByRole("Warehousekeeper");
            return View(vmodel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBWarehouse slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdWarehouse = model.Warehouse.IdWarehouse;


                slider.WarehouseName = model.Warehouse.WarehouseName;
                slider.PhoneNumber = model.Warehouse.PhoneNumber;
                slider.MasterMail = model.Warehouse.MasterMail;
                slider.AccountNumber = model.Warehouse.AccountNumber;
                slider.UserId = model.Warehouse.UserId;
                slider.Active = model.Warehouse.Active;
                slider.DataEntry = model.Warehouse.DataEntry;
                slider.DateTimeEntry = model.Warehouse.DateTimeEntry;
                slider.CurrentState = model.Warehouse.CurrentState;
                if (slider.IdWarehouse == 0 || slider.IdWarehouse == null)
                {
                    if (dbcontext.TBWarehouses.Where(a => a.WarehouseName == slider.WarehouseName).ToList().Count > 0)
                    {
                        TempData["WarehouseName"] = ResourceWeb.VLWarehouseNameDoplceted;
                        return RedirectToAction("MyWarehouse");
                    }
                    var reqwest = iWarehouse.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyWarehouse");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddWarehouse");
                    }
                }
                else
                {
                    var reqestUpdate = iWarehouse.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyWarehouse");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddWarehouse");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddWarehouse");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdWarehouse)
        {
            var reqwistDelete = iWarehouse.deleteData(IdWarehouse);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyWarehouse");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyWarehouse");
            }
        }
        [HttpGet]

        public IActionResult GetNumberAccount(int idLevelThreeAccount)
        {
            // استرجاع حساب المستوى الثالث باستخدام idLevelThreeAccount
            var levelThreeAccount = dbcontext.TBLevelThreeAccounts
                .FirstOrDefault(x => x.IdLevelThreeAccount == idLevelThreeAccount);

            if (levelThreeAccount != null)
            {
                // البحث عن الحسابات في المستوى الرابع
                var levelFourAccounts = dbcontext.TBLevelForeAccounts
                    .Where(x => x.IdLevelThreeAccount == levelThreeAccount.IdLevelThreeAccount)
                    .OrderByDescending(x => x.AccountNumberlivl)
                    .ToList();

                string newAccountNumber;

                if (levelFourAccounts.Any())
                {
                    // إذا وجدنا حسابات في المستوى الرابع، نأخذ أكبر رقم حساب ونضيف عليه 1
                    long highestAccountNumber = levelFourAccounts.First().AccountNumberlivl;
                    long newAccountNum = highestAccountNumber + 1;
                    newAccountNumber = newAccountNum.ToString("D4");  // تنسيق الرقم ليكون 4 أرقام
                }
                else
                {
                    // إذا لم نجد حسابات في المستوى الرابع، نقوم بإضافة "0001" إلى رقم حساب المستوى الثالث
                    newAccountNumber = levelThreeAccount.NumberAccount + "0001";
                }

                // إرسال رقم الحساب الجديد إلى النموذج (Model) لكي يظهر في الـ View
                //ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

                //var supplier =  vmodel.supplier();
                //{
                //    NumberAccount = newAccountNumber
                //};

                // يمكنك تعديل الـ Model إذا كنت تستخدم `ViewModel`
                return Json(new { success = true, numberAccount = newAccountNumber });
            }

            return Json(new { success = false, message = "حساب المستوى الثالث غير موجود." });
        }

    }
}
