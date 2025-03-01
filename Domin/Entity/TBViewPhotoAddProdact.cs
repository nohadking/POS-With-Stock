using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewPhotoAddProdact
    {
        public int IdPhotoAddProdact { get; set; }
        public int IdProduct { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public string Photo { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
