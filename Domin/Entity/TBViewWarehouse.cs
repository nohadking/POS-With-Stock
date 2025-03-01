using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
   public class TBViewWarehouse
    {
        public int IdWarehouse { get; set; }
        public string WarehouseName { get; set; }
        public string PhoneNumber { get; set; }
        public string MasterMail { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumberUser { get; set; }
        public long AccountNumber { get; set; }
        public bool Active { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
