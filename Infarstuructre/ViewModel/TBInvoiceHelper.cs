using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class TBInvoic
    {
        public int IdInvoseHeder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
    }
}
