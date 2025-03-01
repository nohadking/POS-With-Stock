using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicProductsAPIController : ControllerBase
    {
        MasterDbcontext dbcontext;
        public PublicProductsAPIController(MasterDbcontext dbcontext = null)
        {
            this.dbcontext = dbcontext;
        }

        [HttpPost("/PublicProductsAPI/GetProductsByCategoyyName")]
        public IActionResult GetProductsByCategoyyName([FromBody] List<string> categoyyName)
        {
            if(categoyyName != null && categoyyName.Any())
            {
                var products = dbcontext.ViewProduct
                    .Where(p => categoyyName.Any(c => c == p.CategoryNameAr.Trim() || c == p.CategoryNameEn.Trim()))
                    .ToList();
                return Ok(products);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = dbcontext.TBProducts.Find(id);
            return Ok(product);
        }
    }
}
