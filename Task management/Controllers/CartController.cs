using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IICompanyInformation iCompanyInformation;            
        IIHomeBackgroundimage iHomeBackgroundimage;    
    
        public CartController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IIHomeBackgroundimage iHomeBackgroundimage1)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;            
            iHomeBackgroundimage = iHomeBackgroundimage1;             
        }
        public IActionResult MyCart()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            return View(vmodel);
        }  
        public IActionResult MyCartAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
          
            return View(vmodel);
        }
    }
}
