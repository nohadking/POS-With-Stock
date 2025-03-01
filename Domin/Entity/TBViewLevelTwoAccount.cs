using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewLevelTwoAccount
    {
        public int IdLevelTwoAccount { get; set; }
        public int IdMainAccount { get; set; }
        public int NumberAccount { get; set; }
        public string AccountName { get; set; }
        public int NumberAccountLevelTwo { get; set; }
        public string AccountNameLevelTwo { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool Active { get; set; }
        public bool CurrentState { get; set; }
    }
}
