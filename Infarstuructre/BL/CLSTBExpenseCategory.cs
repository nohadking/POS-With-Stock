using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIExpenseCategory
    {
        List<TBExpenseCategory> GetAll();
        TBExpenseCategory GetById(int IdExpenseCategory);
        bool saveData(TBExpenseCategory savee);
        bool UpdateData(TBExpenseCategory updatss);
        bool deleteData(int IdExpenseCategory);
        List<TBExpenseCategory> GetAllv(int IdExpenseCategory);
    }
    public class CLSTBExpenseCategory: IIExpenseCategory
    {
        MasterDbcontext dbcontext;
        public CLSTBExpenseCategory(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBExpenseCategory> GetAll()
        {
            List<TBExpenseCategory> MySlider = dbcontext.TBExpenseCategorys.OrderByDescending(n => n.IdExpenseCategory).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBExpenseCategory GetById(int IdExpenseCategory)
        {
            TBExpenseCategory sslid = dbcontext.TBExpenseCategorys.FirstOrDefault(a => a.IdExpenseCategory == IdExpenseCategory);
            return sslid;
        }
        public bool saveData(TBExpenseCategory savee)
        {
            try
            {
                dbcontext.Add<TBExpenseCategory>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBExpenseCategory updatss)
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
        public bool deleteData(int IdExpenseCategory)
        {
            try
            {
                var catr = GetById(IdExpenseCategory);
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
        public List<TBExpenseCategory> GetAllv(int IdExpenseCategory)
        {
            List<TBExpenseCategory> MySlider = dbcontext.TBExpenseCategorys.OrderByDescending(n => n.IdExpenseCategory == IdExpenseCategory).Where(a => a.IdExpenseCategory == IdExpenseCategory).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
