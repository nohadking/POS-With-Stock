using Microsoft.AspNetCore.Mvc;

namespace Task_management.Controllers
{
	public class ShopController : Controller
	{
		private readonly ILogger<HomeController> _logger;


		IIPhotoHomeSliderContent iPhotoHomeSliderContent;
		IIHomeSliderContent iHomeSliderContent;
		IICompanyInformation iCompanyInformation;
        IIHomeBackgroundimage iHomeBackgroundimage;
		IIAboutSectionStartShopContent iAboutSectionStartShopContent;
        IICategory iCategory;
		IIProduct iProduct;
        IIPhotoShopLiftSaide iPhotoShopLiftSaide;

        public ShopController(ILogger<HomeController> logger, IIPhotoHomeSliderContent iPhotoHomeSliderContent1, IIHomeSliderContent iHomeSliderContent1,IICompanyInformation iCompanyInformation1,IIHomeBackgroundimage iHomeBackgroundimage1, IIAboutSectionStartShopContent iAboutSectionStartShopContent1,IICategory iCategory1,IIProduct iProduct1,IIPhotoShopLiftSaide iPhotoShopLiftSaide1)
        {
			_logger = logger;
			iPhotoHomeSliderContent = iPhotoHomeSliderContent1;
			iHomeSliderContent = iHomeSliderContent1;
			iCompanyInformation = iCompanyInformation1;
            iHomeBackgroundimage= iHomeBackgroundimage1;
			iAboutSectionStartShopContent = iAboutSectionStartShopContent1;
			iCategory = iCategory1;
            iProduct = iProduct1;
            iPhotoShopLiftSaide = iPhotoShopLiftSaide1;



        }
        public IActionResult MyShop(int page = 1, List<string> selectedCategories = null)
        {
            int pageSize = 12;

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
            vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartShopContent = iAboutSectionStartShopContent.GetAll().Take(1).ToList();
            vmodel.ListCategory = iCategory.GetAll();

            // تصفية المنتجات بناءً على الفئة إذا تم اختيارها
            var products = iProduct.GetAll();
            if (selectedCategories != null && selectedCategories.Any())
            {
                products = products.Where(p => selectedCategories.Contains(p.CategoryNameEn)).ToList();
            }

            // استخدام Pagination للمنتجات
            vmodel.ListViewProduct = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // حساب عدد الصفحات
            var totalProducts = products.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            ViewBag.CurrentPage = page;
            vmodel.ListPhotoShopLiftSaide = iPhotoShopLiftSaide.GetAll().Take(1).ToList();


            return View(vmodel);
        }    
        public IActionResult MyShopAr(int page = 1, List<string> selectedCategoriess = null)
        {
            int pageSize = 12;

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewPhotoHomeSliderContent = iPhotoHomeSliderContent.GetAll();
            vmodel.ListHomeSliderContent = iHomeSliderContent.GetAll();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.ListAboutSectionStartShopContent = iAboutSectionStartShopContent.GetAll().Take(1).ToList();
            vmodel.ListCategory = iCategory.GetAll();

            // تصفية المنتجات بناءً على الفئة إذا تم اختيارها
            var products = iProduct.GetAll();
            if (selectedCategoriess != null && selectedCategoriess.Any())
            {
                products = products.Where(p => selectedCategoriess.Contains(p.CategoryNameAr.Trim())).ToList();
            }

            // استخدام Pagination للمنتجات
            vmodel.ListViewProduct = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // حساب عدد الصفحات
            var totalProducts = products.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            ViewBag.CurrentPage = page;
            vmodel.ListPhotoShopLiftSaide = iPhotoShopLiftSaide.GetAll().Take(1).ToList();


            return View(vmodel);
        }



	}
}
