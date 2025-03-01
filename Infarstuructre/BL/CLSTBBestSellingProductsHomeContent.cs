

namespace Infarstuructre.BL
{
    public interface IIBestSellingProductsHomeContent
    {
        List<TBBestSellingProductsHomeContent> GetAll();
        List<TBBestSellingProductsHomeContent> GetAllActive();
        TBBestSellingProductsHomeContent GetById(int IdBestSellingProductsHomeContent);
        bool saveData(TBBestSellingProductsHomeContent savee);
        bool UpdateData(TBBestSellingProductsHomeContent updatss);
        bool deleteData(int IdBestSellingProductsHomeContent);
        List<TBBestSellingProductsHomeContent> GetAllv(int IdBestSellingProductsHomeContent);
    }
    public class CLSTBBestSellingProductsHomeContent: IIBestSellingProductsHomeContent
    {
        MasterDbcontext dbcontext;
        public CLSTBBestSellingProductsHomeContent(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }

        public List<TBBestSellingProductsHomeContent> GetAll()
        {
            List<TBBestSellingProductsHomeContent> MySlider = dbcontext.TBBestSellingProductsHomeContents.OrderByDescending(n => n.IdBestSellingProductsHomeContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBBestSellingProductsHomeContent> GetAllActive()
        {
            List<TBBestSellingProductsHomeContent> MySlider = dbcontext.TBBestSellingProductsHomeContents.OrderByDescending(n => n.IdBestSellingProductsHomeContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBBestSellingProductsHomeContent GetById(int IdBestSellingProductsHomeContent)
        {
            TBBestSellingProductsHomeContent sslid = dbcontext.TBBestSellingProductsHomeContents.FirstOrDefault(a => a.IdBestSellingProductsHomeContent == IdBestSellingProductsHomeContent);
            return sslid;
        }
        public bool saveData(TBBestSellingProductsHomeContent savee)
        {
            try
            {
                dbcontext.Add<TBBestSellingProductsHomeContent>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBBestSellingProductsHomeContent updatss)
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
        public bool deleteData(int IdBestSellingProductsHomeContent)
        {
            try
            {
                var catr = GetById(IdBestSellingProductsHomeContent);
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
        public List<TBBestSellingProductsHomeContent> GetAllv(int IdBestSellingProductsHomeContent)
        {
            List<TBBestSellingProductsHomeContent> MySlider = dbcontext.TBBestSellingProductsHomeContents.OrderByDescending(n => n.IdBestSellingProductsHomeContent == IdBestSellingProductsHomeContent).Where(a => a.IdBestSellingProductsHomeContent == IdBestSellingProductsHomeContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

    }
}
