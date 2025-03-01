using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewExpense
    {
        public int IdExpense { get; set; }
        public int IdExpenseCategory { get; set; }
        public int IdLevelForeAccount { get; set; }
        public string AccountName { get; set; }
        public string ExpenseCategory { get; set; }
        public int BondNumber { get; set; }
		public DateOnly DateBond { get; set; }
		public string Statement { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public decimal Amount { get; set; }
    }
}
