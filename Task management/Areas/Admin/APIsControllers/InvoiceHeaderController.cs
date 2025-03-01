using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceHeaderController : ControllerBase
    {
        IIInvoseHeder iInvoseHeder;
        UserManager<ApplicationUser> userManager;
        public InvoiceHeaderController(IIInvoseHeder iInvoseHeder, UserManager<ApplicationUser> userManager)
        {
            this.iInvoseHeder = iInvoseHeder;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddHeaderForInvoice(TBInvoseHeder invHed)
        {
            var user = await userManager.GetUserAsync(User);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var invoiceHeader = new TBInvoseHeder
            {
                IdInvoseHeder = invHed.IdInvoseHeder,
                InvoiceNumber = invHed.InvoiceNumber,
                IdPaymentMethod = invHed.IdPaymentMethod,
                DataEntry = user.UserName,
                DateInvos = DateTime.Now,
                DateTimeEntry = DateTime.Now,
                IdUser = invHed.IdUser,
                OutstandingBill = invHed.OutstandingBill,
                CurrentState = invHed.CurrentState,
            };

            iInvoseHeder.saveData(invoiceHeader);
            return Ok(invoiceHeader); 
        }
    }
}
