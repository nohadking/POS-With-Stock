

namespace Infarstuructre.BL
{
    public interface IILevelForeAccount
    {
        List<TBViewLevelForeAccount> GetAll();
        TBLevelForeAccount GetById(int IdLevelForeAccount);
        bool saveData(TBLevelForeAccount savee);
        bool UpdateData(TBLevelForeAccount updatss);
        bool deleteData(int IdLevelForeAccount);
        List<TBViewLevelForeAccount> GetAllv(int IdLevelForeAccount);
        TBViewLevelForeAccount GetByIdview(int IdLevelForeAccount);

    }
    public class CLSTBLevelForeAccount: IILevelForeAccount
    {
        MasterDbcontext dbcontext;
        public CLSTBLevelForeAccount(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewLevelForeAccount> GetAll()
        {
            List<TBViewLevelForeAccount> MySlider = dbcontext.ViewLevelForeAccount.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBLevelForeAccount GetById(int IdLevelForeAccount)
        {
            TBLevelForeAccount sslid = dbcontext.TBLevelForeAccounts.FirstOrDefault(a => a.IdLevelForeAccount == IdLevelForeAccount);
            return sslid;
        }
        public bool saveData(TBLevelForeAccount savee)
        {
            try
            {
                dbcontext.Add<TBLevelForeAccount>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBLevelForeAccount updatss)
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
        public bool deleteData(int IdLevelForeAccount)
        {
            try
            {
                var catr = GetById(IdLevelForeAccount);
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
        public List<TBViewLevelForeAccount> GetAllv(int IdLevelForeAccount)
        {
            List<TBViewLevelForeAccount> MySlider = dbcontext.ViewLevelForeAccount.OrderByDescending(n => n.IdLevelForeAccount == IdLevelForeAccount).Where(a => a.IdLevelForeAccount == IdLevelForeAccount).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewLevelForeAccount GetByIdview(int IdLevelForeAccount)
        {
            TBViewLevelForeAccount sslid = dbcontext.ViewLevelForeAccount.FirstOrDefault(a => a.IdLevelForeAccount == IdLevelForeAccount);
            return sslid;
        }
    }
}
