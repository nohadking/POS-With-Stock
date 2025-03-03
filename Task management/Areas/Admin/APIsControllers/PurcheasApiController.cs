using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurcheasApiController : ControllerBase
    {
        MasterDbcontext dbcontext;
        IIPurchase iPurchase;
        IIClassCard iClassCard;
        IIStock iStock;
        public PurcheasApiController(MasterDbcontext dbcontext, IIPurchase iPurchase, IIClassCard iClassCard, IIStock iStock)
        {
            this.dbcontext = dbcontext;
            this.iPurchase = iPurchase;
            this.iClassCard = iClassCard;
            this.iStock = iStock;
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

            var stock = new TBStock
            {
                IdWarehouse = purchase.IdWarehouse,
                BondType = "سند شراء",
                BondDate = DateOnly.FromDateTime(DateTime.Now),
                BondNumber = purchase.PurchaseNumber,
                CurrentState = purchase.CurrentState,
                DataEntry = purchase.DataEntry,
                IdProduct = purchase.IdProduct,
                InputQuantity = purchase.Quantity,
                OutputQuantity = 0,
                DateTimeEntry = DateTime.Now
            };

            var result0 = iStock.saveData(stock);

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

                var bindNumber = purcheas.PurchaseNumber;

                var stocks = dbcontext.TBStocks.Where(s => s.BondNumber == bindNumber).ToList();

                if (stocks != null)
                {
                    dbcontext.TBStocks.RemoveRange(stocks);
                }

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
