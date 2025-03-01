

namespace Infarstuructre.BL
{
    public interface IIAboutSectionStartHomeContent
    {
        List<TBAboutSectionStartHomeContent> GetAll();
        TBAboutSectionStartHomeContent GetById(int IdAboutSectionStartHomeContent);
        bool saveData(TBAboutSectionStartHomeContent savee);
        bool UpdateData(TBAboutSectionStartHomeContent updatss);
        bool deleteData(int IdAboutSectionStartHomeContent);
        List<TBAboutSectionStartHomeContent> GetAllv(int IdAboutSectionStartHomeContent);
    }
    public class CLSTBAboutSectionStartHomeContent: IIAboutSectionStartHomeContent
    {
        MasterDbcontext dbcontext;
        public CLSTBAboutSectionStartHomeContent(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }


        public List<TBAboutSectionStartHomeContent> GetAll()
        {
            List<TBAboutSectionStartHomeContent> MySlider = dbcontext.TBAboutSectionStartHomeContents.OrderByDescending(n => n.IdAboutSectionStartHomeContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBAboutSectionStartHomeContent GetById(int IdAboutSectionStartHomeContent)
        {
            TBAboutSectionStartHomeContent sslid = dbcontext.TBAboutSectionStartHomeContents.FirstOrDefault(a => a.IdAboutSectionStartHomeContent == IdAboutSectionStartHomeContent);
            return sslid;
        }
        public bool saveData(TBAboutSectionStartHomeContent savee)
        {
            try
            {
                dbcontext.Add<TBAboutSectionStartHomeContent>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBAboutSectionStartHomeContent updatss)
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
        public bool deleteData(int IdAboutSectionStartHomeContent)
        {
            try
            {
                var catr = GetById(IdAboutSectionStartHomeContent);
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
        public List<TBAboutSectionStartHomeContent> GetAllv(int IdAboutSectionStartHomeContent)
        {
            List<TBAboutSectionStartHomeContent> MySlider = dbcontext.TBAboutSectionStartHomeContents.OrderByDescending(n => n.IdAboutSectionStartHomeContent == IdAboutSectionStartHomeContent).Where(a => a.IdAboutSectionStartHomeContent == IdAboutSectionStartHomeContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }


    }
}
