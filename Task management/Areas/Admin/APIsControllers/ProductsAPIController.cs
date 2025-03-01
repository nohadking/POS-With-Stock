using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        IIProduct iProduct;
        MasterDbcontext dbcontext;
        UserManager<ApplicationUser> userManager;
        public ProductsAPIController(IIProduct iProduct, MasterDbcontext dbcontext, UserManager<ApplicationUser> userManager)
        {
            this.iProduct = iProduct;
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = iProduct.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = iProduct.GetById(id);
            return Ok(product);
        }

        [HttpGet("GetProductByViewId/{id}")]
        public async Task<IActionResult> GetProductByViewId(int id)
        {
            var product = iProduct.GetByIdview(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(TBProduct model, List<IFormFile> file)
        {
            var user = await userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dbcontext.TBProducts.Where(a => a.ProductNameEn == model.ProductNameEn).ToList().Count > 0)
            {
                return BadRequest("Product already exist");
            }

            if (dbcontext.TBProducts.Where(a => a.ProductNameAr == model.ProductNameAr).ToList().Count > 0)
            {
                return BadRequest("Product already exist");
            }

            if (file.Count() > 0)
            {
                string photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", photo), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.Photo = photo;
                fileStream.Close();
            }
            else
                return BadRequest("must upload photo");

            var product = new TBProduct
            {
                Active = model.Active,
                CurrentState = model.CurrentState,
                DataEntry = user.UserName,
                DateTimeEntry = DateTime.Now,
                DescriptionAr = model.DescriptionAr,
                DescriptionEn = model.DescriptionEn,
                IdCategory = model.IdCategory,
                Photo = model.Photo,
                price = model.price,
                ProductNameAr = model.ProductNameAr,
                ProductNameEn = model.ProductNameEn,

            };

            iProduct.saveData(product);
            return Ok(product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, TBProduct model, List<IFormFile> file)
        {
            var product = iProduct.GetById(id);
            if (product == null)
                return NotFound();

            if(file.Count() > 0)
            {
                var imgName =  Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var serverFolder = @"wwwroot/Images/Home";
                var folderTosave = Path.Combine(serverFolder, imgName);
                FileStream fs = new FileStream(folderTosave, FileMode.Create);
                file[0].CopyTo(fs);
                model.Photo = imgName;
            }
            else
            {
                model.Photo = product.Photo;
            }

            dbcontext.Entry(product).CurrentValues.SetValues(model);
            dbcontext.SaveChanges();
            return Ok(product); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletProduct(int id)
        {
            var product = iProduct.GetById(id);
            if (product == null)
                return NotFound();

            iProduct.deleteData(id);
            return Ok();
        }
    }
}
