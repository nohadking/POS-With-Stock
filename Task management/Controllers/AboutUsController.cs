using Microsoft.AspNetCore.Mvc;

namespace Task_management.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IICompanyInformation iCompanyInformation;
        IIHomeBackgroundimage iHomeBackgroundimage;
        IIStaff iStaff;
        public AboutUsController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IIHomeBackgroundimage iHomeBackgroundimage1,IIStaff iStaff1)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;
            iHomeBackgroundimage = iHomeBackgroundimage1;
            iStaff = iStaff1;
        }
        public IActionResult AboutUs()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.ListStaff = iStaff.GetAll();

            return View(vmodel);
        }  
        public IActionResult AboutUsAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            vmodel.ListStaff = iStaff.GetAll();

            return View(vmodel);
        }
    }
}
