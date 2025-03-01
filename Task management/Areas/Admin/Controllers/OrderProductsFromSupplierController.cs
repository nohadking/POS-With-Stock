using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OrderProductsFromSupplierController : Controller
    {
        IICompanyInformation iCompanyInformation;
        IIOrderProductsFromSupplier iOrderProductsFromSupplier;
        IISupplier iSupplier;
        IIClassCard  iClassCard;
        IIUnit iUnit;
        IIEmailAlartSetting iEmailAlartSetting;
        MasterDbcontext dbcontext;
        public OrderProductsFromSupplierController(IICompanyInformation iCompanyInformation1,IIOrderProductsFromSupplier iOrderProductsFromSupplier1,IISupplier iSupplier1,IIClassCard  iClassCard1,IIUnit iUnit1,IIEmailAlartSetting iEmailAlartSetting1,MasterDbcontext dbcontext1)
        {
            iCompanyInformation = iCompanyInformation1;
            iOrderProductsFromSupplier = iOrderProductsFromSupplier1;
            iSupplier = iSupplier1;
            iClassCard = iClassCard1;
            iUnit = iUnit1;
            iEmailAlartSetting= iEmailAlartSetting1;
            dbcontext = dbcontext1;
        }
        public IActionResult MyOrderProductsFromSupplier()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            //vmodel.ListViewOrderProductsFromSupplier = iOrderProductsFromSupplier.GetAll() .GroupBy(i => i.NumberOrderProducts) .Select(g => g.First()) .ToList();
            ViewBag.Supplier = vmodel.ListViewSupplier = iSupplier.GetAll();
        
            ViewBag.Unit = vmodel.ListUnit = iUnit.GetAll();
            ViewBag.ClassCard = vmodel.ListViewClassCard = iClassCard.GetAll();

            var numberinvose = vmodel.ListViewOrderProductsFromSupplier = iOrderProductsFromSupplier.GetAll()
    .GroupBy(p => p.NumberOrderProducts) // تجميع حسب رقم السند
    .Select(g => g.First())        // أخذ السجل الأول من كل مجموعة
    .ToList();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberOrderProducts) + 1
        : 1;
            return View(vmodel);
        }
        public IActionResult AddOrderProductsFromSupplier(int? IdOrderProductsFromSupplier)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
        
            ViewBag.Supplier = vmodel.ListViewSupplier = iSupplier.GetAll();
         
            ViewBag.Unit = vmodel.ListUnit = iUnit.GetAll();
            ViewBag.ClassCard = vmodel.ListViewClassCard = iClassCard.GetAll();
            var numberinvose = vmodel.ListViewOrderProductsFromSupplier = iOrderProductsFromSupplier.GetAll().Distinct().ToList();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.NumberOrderProducts) + 1
        : 1;
            if (IdOrderProductsFromSupplier != null)
            {
                vmodel.OrderProductsFromSupplier = iOrderProductsFromSupplier.GetById(Convert.ToInt32(IdOrderProductsFromSupplier));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBOrderProductsFromSupplier slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdOrderProductsFromSupplier = model.OrderProductsFromSupplier.IdOrderProductsFromSupplier;
                slider.IdSupplier = model.OrderProductsFromSupplier.IdSupplier;
                slider.NumberOrderProducts = model.OrderProductsFromSupplier.NumberOrderProducts;
                slider.DateOrderProducts = model.OrderProductsFromSupplier.DateOrderProducts;
                slider.IdProduct = model.OrderProductsFromSupplier.IdProduct;
                slider.Quantity = model.OrderProductsFromSupplier.Quantity;
                slider.IdUnit = model.OrderProductsFromSupplier.IdUnit;
                slider.TotalQuantity = model.OrderProductsFromSupplier.TotalQuantity; 
                //slider.AllQuantity = (model.OrderProductsFromSupplier.Quantity) + (model.OrderProductsFromSupplier.FreeQuantity ?? 0);                                  
                slider.Nouts = model.OrderProductsFromSupplier.Nouts; 
                slider.DateTimeEntry = model.OrderProductsFromSupplier.DateTimeEntry;
                slider.DataEntry = model.OrderProductsFromSupplier.DataEntry;
                slider.CurrentState = model.OrderProductsFromSupplier.CurrentState;
                if (slider.IdOrderProductsFromSupplier == 0 || slider.IdOrderProductsFromSupplier == null)
                {

                    var reqwest = iOrderProductsFromSupplier.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyOrderProductsFromSupplier");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddOrderProductsFromSupplier");
                    }
                }
                else
                {
                    var reqestUpdate = iOrderProductsFromSupplier.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyOrderProductsFromSupplier");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddOrderProductsFromSupplier");
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
        public IActionResult DeleteData(int IdOrderProductsFromSupplier)
        {
            var reqwistDelete = iOrderProductsFromSupplier.deleteData(IdOrderProductsFromSupplier);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyOrderProductsFromSupplier");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyOrderProductsFromSupplier");

            }
        }


        [HttpGet]
        public IActionResult GetSupplierImage(int id)
        {
            var supplier = dbcontext.TBSuppliers.FirstOrDefault(s => s.IdSupplier == id);
            if (supplier != null)
            {
                // المسار الصحيح بناءً على مكان تخزين الصور في wwwroot
                var imageUrl = Url.Content("~/Images/Home/" + supplier.Photo);
                return Json(new { imageUrl });
            }
            return Json(null);
        }

    }
}
