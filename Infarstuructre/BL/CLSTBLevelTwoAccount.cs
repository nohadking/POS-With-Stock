using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IILevelTwoAccount
    {
        List<TBViewLevelTwoAccount> GetAll();
        TBLevelTwoAccount GetById(int IdLevelTwoAccount);
        bool saveData(TBLevelTwoAccount savee);
        bool UpdateData(TBLevelTwoAccount updatss);
        bool deleteData(int IdLevelTwoAccount);
        List<TBViewLevelTwoAccount> GetAllv(int IdLevelTwoAccount);
        TBViewLevelTwoAccount GetByIdview(int IdLevelTwoAccount);
    }
    public class CLSTBLevelTwoAccount: IILevelTwoAccount
    {
        MasterDbcontext dbcontext;
        public CLSTBLevelTwoAccount(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBViewLevelTwoAccount> GetAll()
        {
            List<TBViewLevelTwoAccount> MySlider = dbcontext.ViewLevelTwoAccount.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBLevelTwoAccount GetById(int IdLevelTwoAccount)
        {
            TBLevelTwoAccount sslid = dbcontext.TBLevelTwoAccounts.FirstOrDefault(a => a.IdLevelTwoAccount == IdLevelTwoAccount);
            return sslid;
        }
        public bool saveData(TBLevelTwoAccount savee)
        {
            try
            {
                dbcontext.Add<TBLevelTwoAccount>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBLevelTwoAccount updatss)
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
        public bool deleteData(int IdLevelTwoAccount)
        {
            try
            {
                var catr = GetById(IdLevelTwoAccount);
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
        public List<TBViewLevelTwoAccount> GetAllv(int IdLevelTwoAccount)
        {
            List<TBViewLevelTwoAccount> MySlider = dbcontext.ViewLevelTwoAccount.OrderByDescending(n => n.IdLevelTwoAccount == IdLevelTwoAccount).Where(a => a.IdLevelTwoAccount == IdLevelTwoAccount).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewLevelTwoAccount GetByIdview(int IdLevelTwoAccount)
        {
            TBViewLevelTwoAccount sslid = dbcontext.ViewLevelTwoAccount.FirstOrDefault(a => a.IdLevelTwoAccount == IdLevelTwoAccount);
            return sslid;
        }
    }
}
