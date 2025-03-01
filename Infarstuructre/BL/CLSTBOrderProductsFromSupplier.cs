using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIOrderProductsFromSupplier
    {
        List<TBViewOrderProductsFromSupplier> GetAll();
        TBOrderProductsFromSupplier GetById(int IdOrderProductsFromSupplier);
        bool saveData(TBOrderProductsFromSupplier savee);
        bool UpdateData(TBOrderProductsFromSupplier updatss);
        bool deleteData(int IdOrderProductsFromSupplier);
        List<TBViewOrderProductsFromSupplier> GetAllv(int IdOrderProductsFromSupplier);


    }
    public class CLSTBOrderProductsFromSupplier: IIOrderProductsFromSupplier
    {
        MasterDbcontext dbcontext;
        public CLSTBOrderProductsFromSupplier(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewOrderProductsFromSupplier> GetAll()
        {
            List<TBViewOrderProductsFromSupplier> MySlider = dbcontext.ViewOrderProductsFromSupplier.OrderByDescending(n => n.IdOrderProductsFromSupplier).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBOrderProductsFromSupplier GetById(int IdOrderProductsFromSupplier)
        {
            TBOrderProductsFromSupplier sslid = dbcontext.TBOrderProductsFromSuppliers.FirstOrDefault(a => a.IdOrderProductsFromSupplier == IdOrderProductsFromSupplier);
            return sslid;
        }
        public bool saveData(TBOrderProductsFromSupplier savee)
        {
            try
            {
                dbcontext.Add<TBOrderProductsFromSupplier>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBOrderProductsFromSupplier updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdOrderProductsFromSupplier)
        {
            try
            {
                var catr = GetById(IdOrderProductsFromSupplier);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<TBViewOrderProductsFromSupplier> GetAllv(int IdOrderProductsFromSupplier)
        {
            List<TBViewOrderProductsFromSupplier> MySlider = dbcontext.ViewOrderProductsFromSupplier.OrderByDescending(n => n.IdOrderProductsFromSupplier == IdOrderProductsFromSupplier).Where(a => a.IdOrderProductsFromSupplier == IdOrderProductsFromSupplier).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
     
    }
}
