using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BondTypeController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IIBondType iBondType;
        public BondTypeController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1,IIBondType iBondType1)
        {
            dbcontext = dbcontext1;
           iCompanyInformation = iCompanyInformation1;
            iBondType = iBondType1;

        }
        public IActionResult MyBondType()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBondType = iBondType.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }

        public IActionResult AddBondType(int? IdBondType)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBondType = iBondType.GetAll();
            // تأكد من أن BondType مهيأ حتى لو لم يكن هناك ID
            if (vmodel.BondType == null)
            {
                vmodel.BondType = new TBBondType(); // أو النوع الصحيح
            }
            if (IdBondType != null)
            {
                vmodel.BondType = iBondType.GetById(Convert.ToInt32(IdBondType));
            }
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBBondType slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdBondType = model.BondType.IdBondType;

                slider.BondType = model.BondType.BondType;
                slider.DataEntry = model.BondType.DataEntry;
                slider.DateTimeEntry = model.BondType.DateTimeEntry;
                slider.CurrentState = model.BondType.CurrentState;
                if (slider.IdBondType == 0 || slider.IdBondType == null)
                {
                    if (dbcontext.TBBondTypes.Where(a => a.BondType == slider.BondType).ToList().Count > 0)
                    {
                        TempData["BondType"] = ResourceWeb.VLBondTypeDoplceted;
                        return RedirectToAction("MyBondType");
                    }
                    var reqwest = iBondType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyBondType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddBondType");
                    }
                }
                else
                {
                    var reqestUpdate = iBondType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyBondType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddBondType");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddBondType");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdBondType)
        {
            var reqwistDelete = iBondType.deleteData(IdBondType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyBondType");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyBondType");
            }
        }
    }
}
