using Microsoft.AspNetCore.Mvc;

namespace Task_management.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IICompanyInformation iCompanyInformation;
        IIHomeBackgroundimage iHomeBackgroundimage;
        IIDeliveryCompanyPricing DeliveryCompanyPricing;
        IIInvoseHeder iInvoseHeder;
        public CheckOutController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IIHomeBackgroundimage iHomeBackgroundimage1, IIDeliveryCompanyPricing iDeliveryCompanyPricing1, IIInvoseHeder iInvoseHeder1)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;
            iHomeBackgroundimage = iHomeBackgroundimage1;
            DeliveryCompanyPricing = iDeliveryCompanyPricing1;
            iInvoseHeder = iInvoseHeder1;
        }
        public IActionResult MYCheckOut()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            ViewBag.arae = vmodel.ListViewDeliveryCompanyPricing = DeliveryCompanyPricing.GetAll().Distinct().ToList();
            var numberinvose = vmodel.ListViewInvoseHede = iInvoseHeder.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
        ? numberinvose.Max(c => c.InvoiceNumber) + 1
        : 1;

            return View(vmodel);
        }
        public IActionResult MYCheckOutAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformation = iCompanyInformation.GetAll();
            vmodel.ListHomeBackgroundimage = iHomeBackgroundimage.GetAll().Take(1).ToList();
            ViewBag.arae = vmodel.ListViewDeliveryCompanyPricing = DeliveryCompanyPricing.GetAll().Distinct().ToList();
            var numberinvose = vmodel.ListViewInvoseHede = iInvoseHeder.GetAll();
            ViewBag.nomberMax = numberinvose.Any()
            ? numberinvose.Max(c => c.InvoiceNumber) + 1
            : 1;
            return View(vmodel);
        }
    }
}
