

namespace Infarstuructre.BL
{
    public interface IIBLevelThreeAccount
    {
        List<TBViewLevelThreeAccount> GetAll();
        TBLevelThreeAccount GetById(int IdLevelThreeAccount);
        bool saveData(TBLevelThreeAccount savee);
        bool UpdateData(TBLevelThreeAccount updatss);
        bool deleteData(int IdLevelThreeAccount);
        List<TBViewLevelThreeAccount> GetAllv(int IdLevelThreeAccount);
        TBViewLevelThreeAccount GetByIdview(int IdLevelThreeAccount);


    }




    public class CLSTBLevelThreeAccount: IIBLevelThreeAccount
    {
        MasterDbcontext dbcontext;
        public CLSTBLevelThreeAccount(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewLevelThreeAccount> GetAll()
        {
            List<TBViewLevelThreeAccount> MySlider = dbcontext.ViewLevelThreeAccount.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBLevelThreeAccount GetById(int IdLevelThreeAccount)
        {
            TBLevelThreeAccount sslid = dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.IdLevelThreeAccount == IdLevelThreeAccount);
            return sslid;
        }
        public bool saveData(TBLevelThreeAccount savee)
        {
            try
            {
                dbcontext.Add<TBLevelThreeAccount>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBLevelThreeAccount updatss)
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
        public bool deleteData(int IdLevelThreeAccount)
        {
            try
            {
                var catr = GetById(IdLevelThreeAccount);
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
        public List<TBViewLevelThreeAccount> GetAllv(int IdLevelThreeAccount)
        {
            List<TBViewLevelThreeAccount> MySlider = dbcontext.ViewLevelThreeAccount.OrderByDescending(n => n.IdLevelThreeAccount == IdLevelThreeAccount).Where(a => a.IdLevelThreeAccount == IdLevelThreeAccount).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewLevelThreeAccount GetByIdview(int IdLevelThreeAccount)
        {
            TBViewLevelThreeAccount sslid = dbcontext.ViewLevelThreeAccount.FirstOrDefault(a => a.IdLevelThreeAccount == IdLevelThreeAccount);
            return sslid;
        }
    }
}
