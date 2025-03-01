using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task_management.Models;

namespace Task_management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

      
        IIPhotoHomeSliderContent iPhotoHomeSliderContent;
        IIHomeSliderContent iHomeSliderContent;
        IIServiceSectionStartHomeContent iServiceSectionStartHomeContent;
        IIAboutSectionStartHomeContent iAboutSectionStartHomeContent;
        IICategoryServic iCategoryServic;
        IIBrandProduct iBrandProduct;
        IICompanyInformation iCompanyInformation;
        IIInvose iInvose;
        IIBestSellingProductsHomeContent iBestSellingProductsHomeContent;
        IIHomeBackgroundimage iHomeBackgroundimage;
        IICategory iCategory;
        IIProduct iProduct;
        MasterDbcontext dbcontext;
        IISupplier iSupplier;
        IIHomeImageProdact iHomeImageProdact;    
        public HomeController(ILogger<HomeController> logger, IIPhotoHomeSliderContent iPhotoHomeSliderContent1, IIHomeSliderContent iHomeSliderContent1, IIServiceSectionStartHomeContent iServiceSectionStartHomeContent1, IIAboutSectionStartHomeContent iAboutSectionStartHomeContent1, IICategoryServic iCategoryServic1, IIBrandProduct iBrandProduct1,IICompanyInformation iCompanyInformation1,IIInvose iInvose1,IIBestSellingProductsHomeContent iBestSellingProductsHomeContent1,IIHomeBackgroundimage iHomeBackgroundimage1,IICategory iCategory1,IIProduct iProduct1,MasterDbcontext dbcontext1,IISupplier iSupplier1,IIHomeImageProdact iHomeImageProdact1)
        {
            _logger = logger;
            iPhotoHomeSliderContent = iPhotoHomeSliderContent1;
            iHomeSliderContent = iHomeSliderContent1;
            iServiceSectionStartHomeContent = iServiceSectionStartHomeContent1;
            iAboutSectionStartHomeContent = iAboutSectionStartHomeContent1;
            iCategoryServic = iCategoryServic1;
            iBrandProduct = iBrandProduct1;
            iCompanyInformation= iCompanyInformation1;
            iInvose = iInvose1;
            iBestSellingProductsHomeContent     = iBestSellingProductsHomeContent1;
            iHomeBackgroundimage = iHomeBackgroundimage1;
            iCategory = iCategory1;
            iProduct = iProduct1;
            dbcontext   = dbcontext1;
            iSupplier = iSupplier1;
            iHomeImageProdact = iHomeImageProdact1;
        }

        public IActionResult Index(int? categoryId)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
            vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();
            vmodel.ListServiceSectionStartHomeContent = iServiceSectionStartHomeContent.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartHomeContent = iAboutSectionStartHomeContent.GetAll().Take(1).ToList();
            vmodel.ListCategoryServic = iCategoryServic.GetAll();
            vmodel.ListBrandProduct = iBrandProduct.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            // جلب كل  المبيعا 
            var total = vmodel.ListViewInvose = iInvose.GetAll(); 
            var totalAmount = total.Sum(a => a.total);
            ViewBag.TotalAmount = totalAmount;
            //كود جلب الاكثر مبيعا 
            var topSellingItems = total
                .GroupBy(item => item.IdProduct) 
                .Select(group => new
                {
                    ProductId = group.Key,
                    ProductName = group.FirstOrDefault().ProductNameAr, 
                    ProductNameEn = group.FirstOrDefault().ProductNameEn, 
                    TotalSales = group.Sum(item => item.total), 
                    SalesCount = group.Sum(item => item.Quantity), 
                    ProductImage = group.FirstOrDefault().Photo, 
                    Price = group.FirstOrDefault().price 
                    
                                                               
                })
                .OrderByDescending(item => item.SalesCount) 
                //.Take(10)
                .ToList();

            ViewBag.TopSellingItems = topSellingItems;
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            ViewBag.Category = vmodel.ListCategory = iCategory.GetAll();
            vmodel.ListViewProduct = iProduct.GetAll();
            if (categoryId.HasValue)
            {
                // جلب المنتجات بناءً على الـ categoryId
                var getprod = vmodel.ListViewProduct = iProduct.GetAllv(categoryId.Value);

                // إرسال البيانات إلى ViewBag
                ViewBag.Products = getprod;
            }
            else
            {
                // جلب كافة المنتجات في حال لم يتم تحديد فئة
                var getprod = vmodel.ListViewProduct = iProduct.GetAll();

                // إرسال البيانات إلى ViewBag
                ViewBag.Products = getprod;
            }
            vmodel.ListViewSupplier = iSupplier.GetAll();
            vmodel.ListHomeImageProdact = iHomeImageProdact.GetAll().Take(1).ToList();

            return View(vmodel);
        }
        public IActionResult IndexAr(int? categoryId)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
            vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();
            vmodel.ListServiceSectionStartHomeContent = iServiceSectionStartHomeContent.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartHomeContent = iAboutSectionStartHomeContent.GetAll().Take(1).ToList();
            vmodel.ListCategoryServic = iCategoryServic.GetAll();
            vmodel.ListBrandProduct = iBrandProduct.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            // جلب كل  المبيعا 
            var total = vmodel.ListViewInvose = iInvose.GetAll();
            var totalAmount = total.Sum(a => a.total);
            ViewBag.TotalAmount = totalAmount;
            //كود جلب الاكثر مبيعا 
            var topSellingItems = total
                .GroupBy(item => item.IdProduct)
                .Select(group => new
                {
                    ProductId = group.Key,
                    ProductName = group.FirstOrDefault().ProductNameAr,
                    ProductNameEn = group.FirstOrDefault().ProductNameEn,
                    TotalSales = group.Sum(item => item.total),
                    SalesCount = group.Sum(item => item.Quantity),
                    ProductImage = group.FirstOrDefault().Photo,
                    Price = group.FirstOrDefault().price

                })
                .OrderByDescending(item => item.SalesCount)
                //.Take(10)
                .ToList();

            ViewBag.TopSellingItems = topSellingItems;
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            ViewBag.Category = vmodel.ListCategory = iCategory.GetAll();
            vmodel.ListViewProduct = iProduct.GetAll();
            if (categoryId.HasValue)
            {
                // جلب المنتجات بناءً على الـ categoryId
                var getprod = vmodel.ListViewProduct = iProduct.GetAllv(categoryId.Value);

                // إرسال البيانات إلى ViewBag
                ViewBag.Products = getprod;
            }
            else
            {
                // جلب كافة المنتجات في حال لم يتم تحديد فئة
                var getprod = vmodel.ListViewProduct = iProduct.GetAll();

                // إرسال البيانات إلى ViewBag
                ViewBag.Products = getprod;
            }
            vmodel.ListViewSupplier = iSupplier.GetAll();
            vmodel.ListHomeImageProdact = iHomeImageProdact.GetAll().Take(1).ToList();
            return View(vmodel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("/Home/GetProductsByCategoryId/{CategoryId}")]
        public IActionResult GetProductsByCategoryId(int CategoryId)
        {
            var products = dbcontext.ViewProduct.Where(p => p.IdCategory == CategoryId).ToList();
            return Ok(products);
        }
    }
}
