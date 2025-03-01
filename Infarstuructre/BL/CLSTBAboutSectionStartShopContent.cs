

namespace Infarstuructre.BL
{
    public interface IIAboutSectionStartShopContent
    {
        List<TBAboutSectionStartShopContent> GetAll();
        TBAboutSectionStartShopContent GetById(int IdAboutSectionStartShopContent);
        bool saveData(TBAboutSectionStartShopContent savee);
        bool UpdateData(TBAboutSectionStartShopContent updatss);
        bool deleteData(int IdAboutSectionStartShopContent);
        List<TBAboutSectionStartShopContent> GetAllv(int IdAboutSectionStartShopContent);
        bool DELETPHOTO(int IdAboutSectionStartShopContent);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBAboutSectionStartShopContent: IIAboutSectionStartShopContent
    {
        MasterDbcontext dbcontext;  
        public CLSTBAboutSectionStartShopContent(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBAboutSectionStartShopContent> GetAll()
        {
            List<TBAboutSectionStartShopContent> MySlider = dbcontext.TBAboutSectionStartShopContents.OrderByDescending(n => n.IdAboutSectionStartShopContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBAboutSectionStartShopContent GetById(int IdAboutSectionStartShopContent)
        {
            TBAboutSectionStartShopContent sslid = dbcontext.TBAboutSectionStartShopContents.FirstOrDefault(a => a.IdAboutSectionStartShopContent == IdAboutSectionStartShopContent);
            return sslid;
        }
        public bool saveData(TBAboutSectionStartShopContent savee)
        {
            try
            {
                dbcontext.Add<TBAboutSectionStartShopContent>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBAboutSectionStartShopContent updatss)
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
        public bool deleteData(int IdAboutSectionStartShopContent)
        {
            try
            {
                var catr = GetById(IdAboutSectionStartShopContent);
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
        public List<TBAboutSectionStartShopContent> GetAllv(int IdAboutSectionStartShopContent)
        {
            List<TBAboutSectionStartShopContent> MySlider = dbcontext.TBAboutSectionStartShopContents.OrderByDescending(n => n.IdAboutSectionStartShopContent == IdAboutSectionStartShopContent).Where(a => a.IdAboutSectionStartShopContent == IdAboutSectionStartShopContent).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdAboutSectionStartShopContent)
        {
            try
            {
                var catr = GetById(IdAboutSectionStartShopContent);
                //using (FileStream fs = new FileStream(catr.Photo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                if (!string.IsNullOrEmpty(catr.Photo))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", catr.Photo);
                    if (System.IO.File.Exists(oldFilePath))
                    {


                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DELETPHOTOWethError(string PhotoNAme)
        {
            try
            {
                if (!string.IsNullOrEmpty(PhotoNAme))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", PhotoNAme);
                    if (System.IO.File.Exists(oldFilePath))
                    {


                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                // يفضل ألا تترك البرنامج يتجاوز الأخطاء بصمت، يفضل تسجيل الخطأ أو إعادة رميه
                return false;
            }
        }
    }
}
