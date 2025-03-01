

namespace Infarstuructre.BL
{
    public interface IICategoryServic
    {
        List<TBCategoryServic> GetAll();
        TBCategoryServic GetById(int IdCategoryServic);
        bool UpdateData(TBCategoryServic updatss);
        bool deleteData(int IdCategoryServic);
        List<TBCategoryServic> GetAllv(int IdCategoryServic);
        bool DELETPHOTO(int IdCategoryServic);
        bool DELETPHOTOWethError(string PhotoNAme);
        TBCategoryServic GetByIdview(int IdCategoryServic);
        bool saveData(TBCategoryServic savee);

    }
    public class CLSTBCategoryServic: IICategoryServic
    {
        MasterDbcontext dbcontext;
        public CLSTBCategoryServic(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBCategoryServic> GetAll()
        {
            List<TBCategoryServic> MySlider = dbcontext.TBCategoryServics.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBCategoryServic GetById(int IdCategoryServic)
        {
            TBCategoryServic sslid = dbcontext.TBCategoryServics.FirstOrDefault(a => a.IdCategoryServic == IdCategoryServic);
            return sslid;
        }
        public bool saveData(TBCategoryServic savee)
        {
            try
            {
                dbcontext.Add<TBCategoryServic>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBCategoryServic updatss)
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
        public bool deleteData(int IdCategoryServic)
        {
            try
            {
                var catr = GetById(IdCategoryServic);
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
        public List<TBCategoryServic> GetAllv(int IdCategoryServic)
        {
            List<TBCategoryServic> MySlider = dbcontext.TBCategoryServics.OrderByDescending(n => n.IdCategoryServic == IdCategoryServic).Where(a => a.IdCategoryServic == IdCategoryServic).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdCategoryServic)
        {
            try
            {
                var catr = GetById(IdCategoryServic);
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
        public TBCategoryServic GetByIdview(int IdCategoryServic)
        {
            TBCategoryServic sslid = dbcontext.TBCategoryServics.FirstOrDefault(a => a.IdCategoryServic == IdCategoryServic);
            return sslid;
        }
    }
}
