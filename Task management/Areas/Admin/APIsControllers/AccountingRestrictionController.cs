using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingRestrictionController : ControllerBase
    {
        MasterDbcontext dbcontext;
        IIAccountingRestriction iAccountingRestriction;
        public AccountingRestrictionController(MasterDbcontext dbcontext, IIAccountingRestriction iAccountingRestriction)
        {
            this.dbcontext = dbcontext;
            this.iAccountingRestriction = iAccountingRestriction;
        }


        [HttpPost]
        public IActionResult AddAccountingRestriction([FromBody] TBAccountingRestriction model)
        {
            var result = iAccountingRestriction.saveData(model);
            return Ok(result);
        }
    }
}
