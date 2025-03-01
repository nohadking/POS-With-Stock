

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypeOrderController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        IITypeOrder iTypeOrder;
        public TypeOrderController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1, IITypeOrder iTypeOrder1)
        {
            dbcontext=dbcontext1;
            iCompanyInformation=iCompanyInformation1;
            iTypeOrder=iTypeOrder1;
        }
        public IActionResult MyTypeOrder()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypeOrder = iTypeOrder.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            return View(vmodel);
        }

        public IActionResult AddTypeOrder(int? IdTypeOrder)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypeOrder = iTypeOrder.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            // تأكد من أن TypeOrder مهيأ حتى لو لم يكن هناك ID
            if (vmodel.TypeOrder == null)
            {
                vmodel.TypeOrder = new TBTypeOrder(); // أو النوع الصحيح
            }
            if (IdTypeOrder != null)
            {
                vmodel.TypeOrder = iTypeOrder.GetById(Convert.ToInt32(IdTypeOrder));
            }
            return View(vmodel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTypeOrder slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTypeOrder = model.TypeOrder.IdTypeOrder;
                slider.TypeOrderEn = model.TypeOrder.TypeOrderEn;
                slider.TypeOrderAr = model.TypeOrder.TypeOrderAr;
                slider.Active = model.TypeOrder.Active;
                slider.DataEntry = model.TypeOrder.DataEntry;
                slider.DateTimeEntry = model.TypeOrder.DateTimeEntry;
                slider.CurrentState = model.TypeOrder.CurrentState;
                if (slider.IdTypeOrder == 0 || slider.IdTypeOrder == null)
                {
                    if (dbcontext.TBTypeOrders.Where(a => a.TypeOrderEn == slider.TypeOrderEn).ToList().Count > 0)
                    {
                        TempData["TypeOrderEn"] = ResourceWeb.VLTypeOrderEnDoplceted;
                        return RedirectToAction("MyTypeOrder");
                    }

                    if (dbcontext.TBTypeOrders.Where(a => a.TypeOrderAr == slider.TypeOrderAr).ToList().Count > 0)
                    {
                        TempData["TypeOrderAr"] = ResourceWeb.VLTypeOrderArDoplceted;
                        return RedirectToAction("MyTypeOrder");
                    }
                    var reqwest = iTypeOrder.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTypeOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddTypeOrder");
                    }
                }
                else
                {
                    var reqestUpdate = iTypeOrder.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTypeOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddTypeOrder");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddTypeOrder");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdTypeOrder)
        {
            var reqwistDelete = iTypeOrder.deleteData(IdTypeOrder);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTypeOrder");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTypeOrder");
            }
        }
    }
}
