using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurcheasApiController : ControllerBase
    {
        MasterDbcontext dbcontext;
        IIPurchase iPurchase;
        IIClassCard iClassCard;
        public PurcheasApiController(MasterDbcontext dbcontext, IIPurchase iPurchase, IIClassCard iClassCard)
        {
            this.dbcontext = dbcontext;
            this.iPurchase = iPurchase;
            this.iClassCard = iClassCard;
        }
        [HttpGet("/api/PurcheasApi/GetByPurcheasNu/{purchaseNumber}")]
        public IActionResult GetByPurcheasNu(int purchaseNumber)
        {
            var purcheas = iPurchase.GetByPurcheasNu(purchaseNumber);
            return Ok(purcheas);
        }
        [HttpGet("/api/PurcheasApi/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var purcheas = iClassCard.GetById(id);
            return Ok(purcheas);
        }
        [HttpPost]
        public IActionResult AddPurcheas([FromBody] TBPurchase purchase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = iPurchase.saveData(purchase);
            return Ok(result);
        }
        [HttpDelete("DeletePurcheases")]
        public IActionResult DeletePurcheases(List<int> idsList)
        {
            foreach(var id in idsList)
            {
                var purcheas = dbcontext.TBPurchases.Find(id);

                if (purcheas == null)
                    continue;

                dbcontext.TBPurchases.Remove(purcheas);
                dbcontext.SaveChanges();
            }

            return Ok(new TBPurchase());
        }


        [HttpGet("/api/PurcheasApi/GetByPurcheasNm/{nm}")]
        public IActionResult GetByPurcheasNm(int nm)
        {
            var purcheases = iPurchase.GetByPurcheasNm(nm);
            return Ok(purcheases);
        }


        [HttpGet("/api/PurcheasApi/GetByDate/{Sdate}")]
        public IActionResult GetByDate(string Sdate)
        {
            var date = Convert.ToDateTime(Sdate);
            var purcheases = iPurchase.GetAByDetectedDate(date);
            return Ok(purcheases);
        }
        
        [HttpGet("/api/PurcheasApi/GetByPeriodDate/{sdate}/{edate}")]
        public IActionResult GetByPeriodDate(string sdate, string edate)
        {
            var start = Convert.ToDateTime(sdate);
            var end = Convert.ToDateTime(edate);
            var purcheases = iPurchase.GetByPeriod(start, end);
            return Ok(purcheases);
        }

        [HttpGet("/api/PurcheasApi/GetBySupAndPeriodDate/{sup}/{sdate}/{edate}")]
        public IActionResult GetBySupAndPeriodDate(string sup, string sdate, string edate)
        {
            var start = Convert.ToDateTime(sdate);
            var end = Convert.ToDateTime(edate);
            var purcheases = iPurchase.GetABySuplierAndPeriod(sup, start, end);
            return Ok(purcheases);
        }

        [HttpGet("/api/PurcheasApi/GetByItemAndPeriodDate/{item}/{sdate}/{edate}")]
        public IActionResult GetByItemAndPeriodDate(string item, string sdate, string edate)
        {
            var start = Convert.ToDateTime(sdate);
            var end = Convert.ToDateTime(edate);
            var purcheases = iPurchase.GetAByPruductAndPeriod(item, start, end);
            return Ok(purcheases);
        }

        [HttpGet("/api/PurcheasApi/GetByItem/{item}")]
        public IActionResult GetByItem(string item)
        {
            var purcheases = iPurchase.GetByProduct(item);
            return Ok(purcheases);
        }

        [HttpGet("/api/PurcheasApi/GetBySup/{sup}")]
        public IActionResult GetBySup(string sup)
        {
            var purcheases = iPurchase.GetBySuplier(sup);
            return Ok(purcheases);
        }
    }
}
