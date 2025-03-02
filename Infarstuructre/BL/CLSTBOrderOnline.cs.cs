

namespace Infarstuructre.BL
{
    public interface IIOrderOnline
    {
        List<TBViewOrderOnline> GetAll();
        TBOrderOnline GetById(int IdOrderOnline);
        bool saveData(TBOrderOnline savee);
        bool UpdateData(TBOrderOnline updatss);
        bool deleteData(int IdOrderOnline);
        List<TBViewOrderOnline> GetAllv(int IdOrderOnline);
        TBViewOrderOnline GetByIdview(int IdOrderOnline);
    }
    public class CLSTBOrderOnline: IIOrderOnline
    {
        MasterDbcontext dbcontext;
        public CLSTBOrderOnline(MasterDbcontext dbcontext1)
        {
            dbcontext   = dbcontext1;
        }
        public List<TBViewOrderOnline> GetAll()
        {
            List<TBViewOrderOnline> MySlider = dbcontext.ViewOrderOnline.OrderByDescending(n => n.IdOrderOnline).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBOrderOnline GetById(int IdOrderOnline)
        {
            TBOrderOnline sslid = dbcontext.TBOrderOnlines.FirstOrDefault(a => a.IdOrderOnline == IdOrderOnline);
            return sslid;
        }
        public bool saveData(TBOrderOnline savee)
        {
            try
            {
                dbcontext.Add<TBOrderOnline>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBOrderOnline updatss)
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
        public bool deleteData(int IdOrderOnline)
        {
            try
            {
                var catr = GetById(IdOrderOnline);
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
        public List<TBViewOrderOnline> GetAllv(int IdOrderOnline)
        {
            List<TBViewOrderOnline> MySlider = dbcontext.ViewOrderOnline.OrderByDescending(n => n.IdOrderOnline == IdOrderOnline).Where(a => a.IdOrderOnline == IdOrderOnline).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBViewOrderOnline GetByIdview(int IdOrderOnline)
        {
            TBViewOrderOnline sslid = dbcontext.ViewOrderOnline.FirstOrDefault(a => a.IdOrderOnline == IdOrderOnline);
            return sslid;
        }
    }
}
