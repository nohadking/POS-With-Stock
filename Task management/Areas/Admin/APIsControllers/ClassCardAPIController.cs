using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassCardAPIController : ControllerBase
    {
        IIClassCard iClassCard;
        IIProduct iProduct;
        public ClassCardAPIController(IIClassCard iClassCard, IIProduct iProduct)
        {
            this.iClassCard = iClassCard;
            this.iProduct = iProduct;
        }


        [HttpGet("/api/ClassCardAPI/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var purcheas = iProduct.GetByIdview(id);
            return Ok(purcheas);
        }
    }
}
