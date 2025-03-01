using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        IIInvose iInvose;
        UserManager<ApplicationUser> userManager;
        MasterDbcontext dbcontext;
        public InvoiceController(IIInvose iInvose, UserManager<ApplicationUser> userManager, MasterDbcontext dbcontext)
        {
            this.iInvose = iInvose;
            this.userManager = userManager;
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var invoices = iInvose.GetAll();
            return Ok(invoices);
        }


        [HttpGet("{invUnm}")]
        public IActionResult GetIvnoiceInfo(int invUnm)
        {
            var invoice = iInvose.GetByInvoiceNumber(invUnm);
            return Ok(invoice);

        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] TBInvose model)
        {
            var user = await userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var invoice = new TBInvose
            {
                IdInvoseHeder = model.IdInvoseHeder,
                IdProduct = model.IdProduct,
                Quantity = model.Quantity,
                price = model.price,
                total = model.total,
                CurrentState = true,
                DataEntry = user.UserName,
                DateTimeEntry = DateTime.Now,
            };

            iInvose.saveData(invoice);
            return Ok(invoice);
        }

        [HttpPut("{idInvoic}")]
        public async Task<IActionResult> EdidInvoic(int idInvoic, [FromBody] TBInvoic model)
        {
            var invoice = iInvose.GetById(idInvoic);
            if(invoice == null)
                return BadRequest();

            dbcontext.Entry(invoice).CurrentValues.SetValues(model);
            dbcontext.SaveChanges();
            return Ok(invoice);
        }

        [HttpDelete("{idInvoic}")]
        public async Task<IActionResult> DeleteInvoice(int idInvoic)
        {
            var invoice = iInvose.GetById(idInvoic);
            if (invoice == null)
                return BadRequest();

            iInvose.deleteData(idInvoic);

            return Ok(invoice);
        }

        [HttpGet("GetByCasherName/{casherName}")]
        public IActionResult GetByCasherName(string casherName)
        {
            var invoices = iInvose.GetByCacherName(casherName);
            return Ok(invoices);
        }

        [HttpGet("GetByCasherNameAndPayMethod/{casherName}/{payMeth}")]
        public IActionResult GetByCasherNameAndPayMethod(string casherName, string payMeth)
        {
            var invoices = iInvose.GetByCacherNameAndPay(casherName, payMeth);
            return Ok(invoices);
        }

        [HttpGet("GetByPeriodDate/{start}/{end}")]
        public IActionResult GetByPeriodDate(DateTime start, DateTime end)
        {
            var invoices = iInvose.GetByPeriodDate(start, end);
            return Ok(invoices);
        }


        [HttpGet("GetByDateTimeEntry/{date}")]
        public IActionResult GetByDateTimeEntry(DateTime date)
        {
            var invoices = iInvose.GetByDateTimeEntry(date);
            return Ok(invoices);
        }

        [HttpGet("GetByCasherNameAndPayMethAndDateTimeEntry/{casherName}/{payMeth}/{date}")]
        public IActionResult GetByCasherNameAndPayMethAndDateTimeEntry(string casherName, string payMeth, DateTime date)
        {
            var invoices = iInvose.GetByCasherNameAndPayMethAndDateTimeEntry(casherName, payMeth, date);
            return Ok(invoices);
        }

        [HttpGet("GetByCasherNameAndPayMethodAndPeriodDate/{casherName}/{payMeth}/{start}/{end}")]
        public IActionResult GetByCasherNameAndPayMethodAndPeriodDate(string casherName, string payMeth, DateTime start, DateTime end)
        {
            var invoices = iInvose.GetByCasherNameAndPayMethodAndPeriodDate(casherName, payMeth, start, end);
            return Ok(invoices);
        }

    }
}
