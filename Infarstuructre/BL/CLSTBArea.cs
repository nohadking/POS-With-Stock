

namespace Infarstuructre.BL
{
    public interface IIArea
    {
        List<TBViewArea> GetAll();
        TBArea GetById(int IdArea);
        bool saveData(TBArea savee);
        bool UpdateData(TBArea updatss);
        bool deleteData(int IdArea);
        List<TBViewArea> GetAllv(int IdArea);
        TBViewArea GetByIdview(int IdArea);

    }
    public class CLSTBArea: IIArea
    {
        MasterDbcontext dbcontext;
        public CLSTBArea(MasterDbcontext dbcontet1)
        {
            dbcontext = dbcontet1;
        }
        public List<TBViewArea> GetAll()
        {
            List<TBViewArea> MySlider = dbcontext.ViewArea.OrderByDescending(n => n.IdArea).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBArea GetById(int IdArea)
        {
            TBArea sslid = dbcontext.TBAreas.FirstOrDefault(a => a.IdArea == IdArea);
            return sslid;
        }
        public bool saveData(TBArea savee)
        {
            try
            {
                dbcontext.Add<TBArea>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBArea updatss)
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
        public bool deleteData(int IdArea)
        {
            try
            {
                var catr = GetById(IdArea);
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
        public List<TBViewArea> GetAllv(int IdArea)
        {
            List<TBViewArea> MySlider = dbcontext.ViewArea.OrderByDescending(n => n.IdArea == IdArea).Where(a => a.IdArea == IdArea).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBViewArea GetByIdview(int IdArea)
        {
            TBViewArea sslid = dbcontext.ViewArea.FirstOrDefault(a => a.IdArea == IdArea);
            return sslid;
        }

    }
}
