using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderOnlineAPIController : ControllerBase
    {
        MasterDbcontext dbcontext;
        IIOrderOnline iOrderOnline;
        public OrderOnlineAPIController(MasterDbcontext dbcontext = null, IIOrderOnline iOrderOnline = null)
        {
            this.dbcontext = dbcontext;
            this.iOrderOnline = iOrderOnline;
        }

        [HttpPost]
        public IActionResult AddOrderOnline(TBOrderOnline model)
        {
            var result = iOrderOnline.saveData(model);
            return Ok(new { result = result });
        }
    }
}
