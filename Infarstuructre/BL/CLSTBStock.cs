using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
   public interface IIStock
    {
        List<TBViewStock> GetAll();
        TBStock GetById(int IdStock);
        bool saveData(TBStock savee);
        bool UpdateData(TBStock updatss);
        bool deleteData(int IdStock);
        List<TBViewStock> GetAllv(int IdStock);
        TBViewStock GetByIdview(int IdStock);
    }
    public class CLSTBStock: IIStock
    {
        MasterDbcontext dbcontext;
        public CLSTBStock(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewStock> GetAll()
        {
            List<TBViewStock> MySlider = dbcontext.ViewStock.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBStock GetById(int IdStock)
        {
            TBStock sslid = dbcontext.TBStocks.FirstOrDefault(a => a.IdStock == IdStock);
            return sslid;
        }
        public bool saveData(TBStock savee)
        {
            try
            {
                dbcontext.Add<TBStock>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBStock updatss)
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
        public bool deleteData(int IdStock)
        {
            try
            {
                var catr = GetById(IdStock);
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
        public List<TBViewStock> GetAllv(int IdStock)
        {
            List<TBViewStock> MySlider = dbcontext.ViewStock.OrderByDescending(n => n.IdStock == IdStock).Where(a => a.IdStock == IdStock).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewStock GetByIdview(int IdStock)
        {
            TBViewStock sslid = dbcontext.ViewStock.FirstOrDefault(a => a.IdStock == IdStock);
            return sslid;
        }

    }
}
