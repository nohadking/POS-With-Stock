using Domin.Entity;
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
        IIPaymentMethod iPaymentMethod;
        IIAccountingRestriction iAccountingRestriction;
        private readonly MasterDbcontext dbcontext;
        IIOrderOnline iOrderOnline;

        public CheckOutController(ILogger<HomeController> logger, IICompanyInformation iCompanyInformation1, IIHomeBackgroundimage iHomeBackgroundimage1, IIDeliveryCompanyPricing iDeliveryCompanyPricing1, IIInvoseHeder iInvoseHeder1, IIPaymentMethod iPaymentMethod1, MasterDbcontext dbcontext, IIOrderOnline iOrderOnline = null, IIAccountingRestriction iAccountingRestriction = null)
        {
            _logger = logger;
            iCompanyInformation = iCompanyInformation1;
            iHomeBackgroundimage = iHomeBackgroundimage1;
            DeliveryCompanyPricing = iDeliveryCompanyPricing1;
            iInvoseHeder = iInvoseHeder1;
            iPaymentMethod = iPaymentMethod1;
            this.dbcontext = dbcontext;
            this.iOrderOnline = iOrderOnline;
            this.iAccountingRestriction = iAccountingRestriction;
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

            var numberAcc = vmodel.ListAccountingRestriction = iAccountingRestriction.GetAll();
            ViewBag.nomberAccMax = numberAcc.Any()
                ? numberAcc.Max(c => c.NumberaccountingRestrictions) + 1
                : 1;

            vmodel.ListPaymentMethod = iPaymentMethod.GetAll();

            var orderType = dbcontext.TBTypeOrders.Find(1);
            ViewBag.orderTypeId = orderType.IdTypeOrder;

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
            vmodel.ListPaymentMethod = iPaymentMethod.GetAll();
            return View(vmodel);
        }
        public IActionResult savedata(TBOrderOnline model)
        {
            var result = iOrderOnline.saveData(model);

            return View();
        }

    }
}
