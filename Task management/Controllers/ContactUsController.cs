using Microsoft.AspNetCore.Mvc;

namespace Task_management.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IICompanyInformation iCompanyInformation;
        IIHomeBackgroundimage iHomeBackgroundimage;
        public ContactUsController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IIHomeBackgroundimage iHomeBackgroundimage1)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;
            iHomeBackgroundimage = iHomeBackgroundimage1;
        }
        public IActionResult ContactUs()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            return View(vmodel);
        }  
        public IActionResult ContactUsAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            return View(vmodel);
        }
    }
}
