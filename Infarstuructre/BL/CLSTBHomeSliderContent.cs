

namespace Infarstuructre.BL
{
    public interface IIHomeSliderContent
    {
        List<TBHomeSliderContent> GetAll();
        TBHomeSliderContent GetById(int IdHomeSliderContent);
        bool saveData(TBHomeSliderContent savee);
        bool UpdateData(TBHomeSliderContent updatss);
        bool deleteData(int IdHomeSliderContent);
        List<TBHomeSliderContent> GetAllv(int IdHomeSliderContent);
    }
    public class CLSTBHomeSliderContent: IIHomeSliderContent
    {
        MasterDbcontext dbcontext;
        public CLSTBHomeSliderContent(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }

        public List<TBHomeSliderContent> GetAll()
        {
            List<TBHomeSliderContent> MySlider = dbcontext.TBHomeSliderContents.OrderByDescending(n => n.IdHomeSliderContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBHomeSliderContent GetById(int IdHomeSliderContent)
        {
            TBHomeSliderContent sslid = dbcontext.TBHomeSliderContents.FirstOrDefault(a => a.IdHomeSliderContent == IdHomeSliderContent);
            return sslid;
        }
        public bool saveData(TBHomeSliderContent savee)
        {
            try
            {
                dbcontext.Add<TBHomeSliderContent>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBHomeSliderContent updatss)
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
        public bool deleteData(int IdHomeSliderContent)
        {
            try
            {
                var catr = GetById(IdHomeSliderContent);
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
        public List<TBHomeSliderContent> GetAllv(int IdHomeSliderContent)
        {
            List<TBHomeSliderContent> MySlider = dbcontext.TBHomeSliderContents.OrderByDescending(n => n.IdHomeSliderContent == IdHomeSliderContent).Where(a => a.IdHomeSliderContent == IdHomeSliderContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

    }
}
