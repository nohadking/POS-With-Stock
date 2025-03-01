

namespace Infarstuructre.BL
{
    public interface IIServiceSectionStartHomeContent
    {
        List<TBServiceSectionStartHomeContent> GetAll();
        TBServiceSectionStartHomeContent GetById(int IServiceSectionStartContent);
        bool saveData(TBServiceSectionStartHomeContent savee);
        bool UpdateData(TBServiceSectionStartHomeContent updatss);
        bool deleteData(int IServiceSectionStartContent);
        List<TBServiceSectionStartHomeContent> GetAllv(int IServiceSectionStartContent);

    }
    public class CLSTBServiceSectionStartHomeContent: IIServiceSectionStartHomeContent
    {
        MasterDbcontext dbcontext;
        public CLSTBServiceSectionStartHomeContent(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBServiceSectionStartHomeContent> GetAll()
        {
            List<TBServiceSectionStartHomeContent> MySlider = dbcontext.TBServiceSectionStartHomeContents.OrderByDescending(n => n.IServiceSectionStartContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBServiceSectionStartHomeContent GetById(int IServiceSectionStartContent)
        {
            TBServiceSectionStartHomeContent sslid = dbcontext.TBServiceSectionStartHomeContents.FirstOrDefault(a => a.IServiceSectionStartContent == IServiceSectionStartContent);
            return sslid;
        }
        public bool saveData(TBServiceSectionStartHomeContent savee)
        {
            try
            {
                dbcontext.Add<TBServiceSectionStartHomeContent>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBServiceSectionStartHomeContent updatss)
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
        public bool deleteData(int IServiceSectionStartContent)
        {
            try
            {
                var catr = GetById(IServiceSectionStartContent);
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
        public List<TBServiceSectionStartHomeContent> GetAllv(int IServiceSectionStartContent)
        {
            List<TBServiceSectionStartHomeContent> MySlider = dbcontext.TBServiceSectionStartHomeContents.OrderByDescending(n => n.IServiceSectionStartContent == IServiceSectionStartContent).Where(a => a.IServiceSectionStartContent == IServiceSectionStartContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
