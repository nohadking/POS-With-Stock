using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryShipingAPIController : ControllerBase
    {
        MasterDbcontext dbcontext;
        public DeliveryShipingAPIController(MasterDbcontext dbcontext = null)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("{cityName}")]
        public IActionResult GetDelevaryShippingByCity(string cityName)
        {
            var deliveryCost = dbcontext.Set<TBViewDeliveryCompanyPricing>()
                                         .Where(d => d.AreaName == cityName.Trim())
                                         .FirstOrDefault()?.Pricing;

            if (deliveryCost == null)
            {
                return NotFound(new { Message = "City not found" });
            }

            return Ok(new { Cost = deliveryCost });
        }

    }
}
