using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewLevelForeAccount
    {
        public int IdLevelForeAccount { get; set; }
        public int IdLevelThreeAccount { get; set; }
        public int LevelThreeAccountsNumber { get; set; }
        public string LevelThreeAccountsName { get; set; }
        public int IdLevelTwoAccount { get; set; }
        public int LevelTwoAccountsNumber { get; set; }
        public string LevelTwoAccountsName { get; set; }
        public int IdMainAccount { get; set; }
        public int MineAccountsNumber { get; set; }
        public string MineAccountsName { get; set; }
        public long LevelForeAccountsNumber { get; set; }
        public string LevelForeAccountsName { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool Active { get; set; }
        public bool CurrentState { get; set; }

    }
}
