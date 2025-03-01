using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_management.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesApiController : ControllerBase
    {
        IIExpense iExpense;
        public ExpensesApiController(IIExpense iExpense)
        {
            this.iExpense = iExpense;
        }

        [HttpGet("/api/ExpensesApi/GetByExpenseAndPeriodDate/{exp}/{start}/{end}")]
        public IActionResult GetByExpenseAndPeriodDate(string exp, string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var expenses = iExpense.GetByExpenseAndPeriodDate(exp, startDt, endDt);
            return Ok(expenses);
        }

        [HttpGet("/api/ExpensesApi/GetByPeriodDate/{start}/{end}")]
        public IActionResult GetByPeriodDate(string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var expenses = iExpense.GetByPeriodDate(startDt, endDt);
            return Ok(expenses);
        }

        [HttpGet("/api/ExpensesApi/GetByCategoryAndPeriodDate/{type}/{date}")]
        public IActionResult GetByCategoryAndPeriodDate(string type, string start, string end)
        {
            var startDt = Convert.ToDateTime(start);
            var endDt = Convert.ToDateTime(end);
            var expenses = iExpense.GetByCategoryAndPeriodDate(type, startDt, endDt);
            return Ok(expenses);
        }

        [HttpGet("/api/ExpensesApi/GetByDetectedDt/{date}")]
        public IActionResult GetByDetectedDt(string date)
        {
            var dt = Convert.ToDateTime(date);
            var expenses = iExpense.GetByDetectedDt(dt);
            return Ok(expenses);
        }


        [HttpGet("/api/ExpensesApi/GetByExpense/{expen}")]
        public IActionResult GetByExpense(string expen)
        {
            var expenses = iExpense.GetByExpense(expen);
            return Ok(expenses);
        }

        [HttpGet("/api/ExpensesApi/GetByCategory/{cate}")]
        public IActionResult GetByCategory(string cate)
        {
            var expenses = iExpense.GetByCategory(cate);
            return Ok(expenses);
        }
    }
}
