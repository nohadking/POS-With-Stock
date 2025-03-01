
namespace Infarstuructre.BL
{
    public interface IICategory
    {
        List<TBCategory> GetAll();
        TBCategory GetById(int IdCategory);
        bool saveData(TBCategory savee);
        bool UpdateData(TBCategory updatss);
        bool deleteData(int IdCategory);
        List<TBCategory> GetAllv(int IdCategory);
        bool DELETPHOTO(int IdCategory);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBCategory: IICategory
    {
        MasterDbcontext dbcontext;
        public CLSTBCategory(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBCategory> GetAll()
        {
            List<TBCategory> MySlider = dbcontext.TBCategorys.OrderByDescending(n => n.IdCategory).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBCategory GetById(int IdCategory)
        {
            TBCategory sslid = dbcontext.TBCategorys.FirstOrDefault(a => a.IdCategory == IdCategory);
            return sslid;
        }
        public bool saveData(TBCategory savee)
        {
            try
            {
                dbcontext.Add<TBCategory>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBCategory updatss)
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
        public bool deleteData(int IdCategory)
        {
            try
            {
                var catr = GetById(IdCategory);
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
        public List<TBCategory> GetAllv(int IdCategory)
        {
            List<TBCategory> MySlider = dbcontext.TBCategorys.OrderByDescending(n => n.IdCategory == IdCategory).Where(a => a.IdCategory == IdCategory).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdCategory)
        {
            try
            {
                var catr = GetById(IdCategory);
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
