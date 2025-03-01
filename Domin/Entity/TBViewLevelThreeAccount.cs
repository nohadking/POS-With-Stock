using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewLevelThreeAccount
    {
        public int IdLevelThreeAccount { get; set; }
        public int IdMainAccount { get; set; }
        public int NumberAccount { get; set; }
        public string AccountName { get; set; }
        public int IdLevelTwoAccount { get; set; }
        public int NumberLevelTwoAccounts { get; set; }
        public string NameLevelTwoAccounts { get; set; }
        public int NumberLevelThreeAccounts { get; set; }
        public string NameLevelThreeAccounts { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool Active { get; set; }
        public bool CurrentState { get; set; }

    }
}
