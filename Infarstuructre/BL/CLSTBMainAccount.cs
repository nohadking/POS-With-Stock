

namespace Infarstuructre.BL
{
    public interface IIMainAccount
    {
        List<TBMainAccount> GetAll();
        List<TBMainAccount> GetAllActive();
        TBMainAccount GetById(int IdMainAccount);
        bool saveData(TBMainAccount savee);
        bool UpdateData(TBMainAccount updatss);
        bool deleteData(int IdMainAccount);
        List<TBMainAccount> GetAllv(int IdMainAccount);


    }
    public class CLSTBMainAccount: IIMainAccount
    {
        MasterDbcontext dbcontext;
        public CLSTBMainAccount(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }

        public List<TBMainAccount> GetAll()
        {
            List<TBMainAccount> MySlider = dbcontext.TBMainAccounts.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBMainAccount> GetAllActive()
        {
            List<TBMainAccount> MySlider = dbcontext.TBMainAccounts.OrderByDescending(n => n.IdMainAccount).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
            return MySlider;
        }

        public TBMainAccount GetById(int IdMainAccount)
        {
            TBMainAccount sslid = dbcontext.TBMainAccounts.FirstOrDefault(a => a.IdMainAccount == IdMainAccount);
            return sslid;
        }
        public bool saveData(TBMainAccount savee)
        {
            try
            {
                dbcontext.Add<TBMainAccount>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBMainAccount updatss)
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
        public bool deleteData(int IdMainAccount)
        {
            try
            {
                var catr = GetById(IdMainAccount);
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
        public List<TBMainAccount> GetAllv(int IdMainAccount)
        {
            List<TBMainAccount> MySlider = dbcontext.TBMainAccounts.OrderByDescending(n => n.IdMainAccount == IdMainAccount).Where(a => a.IdMainAccount == IdMainAccount).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }





    }
}
