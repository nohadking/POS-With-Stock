

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UnitController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIUnit iUnit;
        public UnitController(MasterDbcontext dbcontext1, IICompanyInformation iCompanyInformation1, IIUnit iUnit1)
        {
            dbcontext = dbcontext1;
            iCompanyInformation = iCompanyInformation1;
            iUnit = iUnit1;

        }
        public IActionResult MyUnit()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListUnit = iUnit.GetAll();
            return View(vmodel);
        }
        public IActionResult AddUnit(int? IdUnit)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListUnit = iUnit.GetAll();
            if (IdUnit != null)
            {
                vmodel.Unit = iUnit.GetById(Convert.ToInt32(IdUnit));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBUnit slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdUnit = model.Unit.IdUnit;
                slider.Unit = model.Unit.Unit;           
                slider.DateTimeEntry = model.Unit.DateTimeEntry;
                slider.DataEntry = model.Unit.DataEntry;
                slider.CurrentState = model.Unit.CurrentState;           
                if (slider.IdUnit == 0 || slider.IdUnit == null)
                {
                    if (dbcontext.TBUnits.Where(a => a.Unit == slider.Unit).ToList().Count > 0)
                    {
                        TempData["Unit"] = ResourceWeb.VLUnitDoplceted;
                        return RedirectToAction("MyUnit");
                    }
                  
                    var reqwest = iUnit.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyUnit");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddUnit");
                    }
                }
                else
                {
                    var reqestUpdate = iUnit.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyUnit");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddUnit");
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
        public IActionResult DeleteData(int IdUnit)
        {
            var reqwistDelete = iUnit.deleteData(IdUnit);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyUnit");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyUnit");

            }
        }
    }
}
