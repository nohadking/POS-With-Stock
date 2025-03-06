using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.Qualified;
using static Domin.Entity.Helper;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRestrectionController : ControllerBase
    {
        IIAccountingRestriction iAccountingRestriction;
        MasterDbcontext dbcontext;
        IISupplier iSupplier;
        IIExpense iExpense;
        public AccountRestrectionController(IIAccountingRestriction iAccountingRestriction = null, MasterDbcontext dbcontext = null, IISupplier iSupplier = null, IIExpense iExpense = null)
        {
            this.iAccountingRestriction = iAccountingRestriction;
            this.dbcontext = dbcontext;
            this.iSupplier = iSupplier;
            this.iExpense = iExpense;
        }


        [HttpPost]
        public IActionResult AddAccountRe([FromBody] TBAccountingRestriction model)
        {
            var result = iAccountingRestriction.saveData(model);

            var acc2 = new TBAccountingRestriction
            {
                NumberaccountingRestrictions = model.NumberaccountingRestrictions,
                AccountingName = "مبيعات",
                BondType = model.BondType,
                BondNumber = model.BondNumber,
                Debtor = model.creditor,
                creditor = model.Debtor,
                Statement = model.Statement,
                Nouts = model.Nouts,
                DataEntry = model.DataEntry,
                DateTimeEntry = model.DateTimeEntry,
                CurrentState = model.CurrentState,
            };

            iAccountingRestriction.saveData(acc2);

            return Ok(result);
        }


        [HttpGet("/api/AccountRestrection/GetByBondNuAndBondType/{bond}/{type}")]
        public IActionResult GetByBondNuAndBondType(int bond, string type)
        {
            var accountRe = iAccountingRestriction.GetByBondNuAndBondType(bond, type);
            if(accountRe == null)
            {
                return Ok(new TBAccountingRestriction());
            }
            return Ok(accountRe);
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccountRe(int accountId)
        {
            var acc = dbcontext.TBAccountingRestrictions.Find(accountId);
            var BondNum = acc.BondNumber;

            var accs = dbcontext.TBAccountingRestrictions.Where(a => a.BondNumber == BondNum).ToList();

            if (accs == null)
                return NoContent();

		    dbcontext.TBAccountingRestrictions.RemoveRange(accs);
            dbcontext.SaveChanges();

			return Ok(acc);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = iAccountingRestriction.GetAll();
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetBySupAndPeriodDate/{sup}/{start}/{end}")]
        public IActionResult GetBySupAndPeriodDate(string sup, string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var accounts = iAccountingRestriction.GetBySupAndPeriodDate(sup, startDt, endDt);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetByPeriodDate/{start}/{end}")]
        public IActionResult GetByPeriodDate(string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var accounts = iAccountingRestriction.GetByPeriodDate(startDt, endDt);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetBySupAndDetectedDt/{sup}/{date}")]
        public IActionResult GetBySupAndDetectedDt(string sup, string date)
        {
            var dt = Convert.ToDateTime(date);
            var accounts = iAccountingRestriction.GetBySupAndDetectedDt(sup, dt);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetByTypeAndPeriodDate/{type}/{start}/{end}")]
        public IActionResult GetByTypeAndPeriodDate(string type, string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var accounts = iAccountingRestriction.GetByTypeAndPeriodDate(type, startDt, endDt);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetByTypeAndDetectedDt/{type}/{date}")]
        public IActionResult GetByTypeAndDetectedDt(string type, string date)
        {
            var dt = Convert.ToDateTime(date);
            var accounts = iAccountingRestriction.GetByTypeAndDetectedDt(type, dt);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetByDetectedDt/{date}")]
        public IActionResult GetByDetectedDt(string date)
        {
            var dt = Convert.ToDateTime(date);
            var accounts = iAccountingRestriction.GetByDetectedDt(dt);
            return Ok(accounts);
        }


        [HttpGet("/api/AccountRestrection/GetBySup/{sup}")]
        public IActionResult GetBySup(string sup)
        {
            var accounts = iAccountingRestriction.GetBySup(sup);
            return Ok(accounts);
        }

        [HttpGet("/api/AccountRestrection/GetByType/{type}")]
        public IActionResult GetByType(string type)
        {
            var accounts = iAccountingRestriction.GetByType(type);
            return Ok(accounts);
        }
    }
}
