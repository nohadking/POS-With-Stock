using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExpenseCategoryController : Controller
    {
        MasterDbcontext dbcontext;
        IIExpenseCategory iExpenseCategory;
        IICompanyInformation iCompanyInformation;

        public ExpenseCategoryController(MasterDbcontext dbcontext1,IIExpenseCategory iExpenseCategory1,IICompanyInformation iCompanyInformation1)
        {
            dbcontext=dbcontext1;
            iExpenseCategory =iExpenseCategory1;
            iCompanyInformation =iCompanyInformation1;
        }
        public IActionResult MyExpenseCategory()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListExpenseCategory = iExpenseCategory.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult AddExpenseCategory(int? IdExpenseCategory)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListExpenseCategory = iExpenseCategory.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            // تأكد من أن ExpenseCategory مهيأ حتى لو لم يكن هناك ID
            if (vmodel.ExpenseCategory == null)
            {
                vmodel.ExpenseCategory = new TBExpenseCategory(); // أو النوع الصحيح
            }
            if (IdExpenseCategory != null)
            {
                vmodel.ExpenseCategory = iExpenseCategory.GetById(Convert.ToInt32(IdExpenseCategory));
            }
            return View(vmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBExpenseCategory slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdExpenseCategory = model.ExpenseCategory.IdExpenseCategory;
                slider.ExpenseCategory = model.ExpenseCategory.ExpenseCategory;
                slider.Description = model.ExpenseCategory.Description;
                slider.Active = model.ExpenseCategory.Active;
                slider.DataEntry = model.ExpenseCategory.DataEntry;
                slider.DateTimeEntry = model.ExpenseCategory.DateTimeEntry;
                slider.CurrentState = model.ExpenseCategory.CurrentState;
                if (slider.IdExpenseCategory == 0 || slider.IdExpenseCategory == null)
                {
                    if (dbcontext.TBExpenseCategorys.Where(a => a.ExpenseCategory == slider.ExpenseCategory).ToList().Count > 0)
                    {
                        TempData["ExpenseCategory"] = ResourceWeb.VLExpenseCategoryDoplceted;
                        return RedirectToAction("MYCategory");
                    }
                    var reqwest = iExpenseCategory.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyExpenseCategory");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddExpenseCategory");
                    }
                }
                else
                {
                    var reqestUpdate = iExpenseCategory.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyExpenseCategory");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddExpenseCategory");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddExpenseCategory");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdExpenseCategory)
        {
            var reqwistDelete = iExpenseCategory.deleteData(IdExpenseCategory);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyExpenseCategory");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyExpenseCategory");
            }
        }
    }
}
