using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class StockController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIStock iStock;
        IIWarehouse  iWarehouse;
        IIProduct iProduct;
        public StockController(IICompanyInformation iCompanyInformation1,IIStock iStock1,IIWarehouse iWarehouse1,IIProduct iProduct1)
        {
            iCompanyInformation=iCompanyInformation1;
            iStock =iStock1;
            iWarehouse=iWarehouse1;
            iProduct =iProduct1;
        }
        public IActionResult MyStock()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewStock = iStock.GetAll();
            ViewBag.Product = vmodel.ListViewProduct = iProduct.GetAll();
            ViewBag.Warehouse = vmodel.ListViewWarehouse = iWarehouse.GetAll();
            return View(vmodel);
        }
        public IActionResult AddMyStock(int? IdStock)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
         
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListViewStock = iStock.GetAll();
            ViewBag.Product = vmodel.ListViewProduct = iProduct.GetAll();
            ViewBag.Warehouse = vmodel.ListViewWarehouse = iWarehouse.GetAll();
            if (IdStock != null)
            {
                vmodel.Stock = iStock.GetById(Convert.ToInt32(IdStock));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBStock slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdStock = model.Stock.IdStock;
                slider.IdWarehouse = model.Stock.IdWarehouse;
                slider.BondType = model.Stock.BondType;
                slider.BondNumber = model.Stock.BondNumber;
                slider.BondDate = model.Stock.BondDate;
                slider.IdProduct = model.Stock.IdProduct;
                slider.InputQuantity = model.Stock.InputQuantity;
                slider.OutputQuantity = model.Stock.OutputQuantity;
                slider.DateTimeEntry = model.Stock.DateTimeEntry;
                slider.DataEntry = model.Stock.DataEntry;
                slider.CurrentState = model.Stock.CurrentState;
                if (slider.IdStock == 0 || slider.IdStock == null)
                {               
                    var request = iStock.saveData(slider);
                    if (request)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyStock");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddMyStock");
                    }
                }
                else
                {
                    var requestUpdate = iStock.UpdateData(slider);
                    if (requestUpdate)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyStock");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddMyStock");
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
        public IActionResult DeleteData(int IdStock)
        {
            var reqwistDelete = iStock.deleteData(IdStock);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyStock");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyStock");

            }

        }
    }
}
