using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassCardAPIController : ControllerBase
    {
        IIClassCard iClassCard;
        public ClassCardAPIController(IIClassCard iClassCard)
        {
            this.iClassCard = iClassCard;
        }


        [HttpGet("/api/ClassCardAPI/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var purcheas = iClassCard.GetById(id);
            return Ok(purcheas);
        }
    }
}
