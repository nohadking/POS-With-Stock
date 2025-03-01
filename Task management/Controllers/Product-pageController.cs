

using static QuestPDF.Helpers.Colors;

namespace Task_management.Controllers
{
    public class Product_pageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IICompanyInformation iCompanyInformation;
        IICategory iCategory;
        IIProduct iProduct;
        IIHomeBackgroundimage iHomeBackgroundimage;
        IIInvose iInvose;
        IIBestSellingProductsHomeContent iBestSellingProductsHomeContent;
        IIPhotoAddProdact iPhotoAddProdact;
        public Product_pageController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IICategory iCategory1, IIProduct iProduct1,IIHomeBackgroundimage iHomeBackgroundimage1,IIInvose iInvose1,IIBestSellingProductsHomeContent iBestSellingProductsHomeContent1,IIPhotoAddProdact iPhotoAddProdact1)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;
            iCategory = iCategory1;
            iProduct = iProduct1;   
            iHomeBackgroundimage = iHomeBackgroundimage1;
            iInvose = iInvose1;
            iBestSellingProductsHomeContent = iBestSellingProductsHomeContent1;
            iPhotoAddProdact = iPhotoAddProdact1;
        }
        public IActionResult MYProduct(int ProductId)
        {
         



            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListCategory = iCategory.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.Productsng = iProduct.GetByIdview(ProductId);
            vmodel.ListViewProduct = iProduct.GetAll();
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll().Take(1).ToList();

            var total = vmodel.ListViewInvose = iInvose.GetAll();
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

            vmodel.ListViewPhotoAddProdact = iPhotoAddProdact.GetAllv(ProductId);



            return View(vmodel);
        }  
        public IActionResult MYProductAr(int ProductId)
        {

            //كود جلب الاكثر مبيعا 
         

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();          
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListCategory = iCategory.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.Productsng = iProduct.GetByIdview(ProductId);
            vmodel.ListViewProduct = iProduct.GetAll();
            vmodel.ListBestSellingProductsHomeContent = iBestSellingProductsHomeContent.GetAll().Take(1).ToList();

            var total = vmodel.ListViewInvose = iInvose.GetAll();
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
            vmodel.ListViewPhotoAddProdact = iPhotoAddProdact.GetAllv(ProductId);
            return View(vmodel);
        }
    }
}
