

namespace Infarstuructre.BL
{
    public interface IIDeliveryCompanie
    {
        List<TBDeliveryCompanie> GetAll();
        TBDeliveryCompanie GetById(int IdDeliveryCompanie);
        bool saveData(TBDeliveryCompanie savee);
        bool UpdateData(TBDeliveryCompanie updatss);
        bool deleteData(int IdDeliveryCompanie);
        List<TBDeliveryCompanie> GetAllv(int IdDeliveryCompanie);
        bool DELETPHOTO(int IdDeliveryCompanie);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBDeliveryCompanie: IIDeliveryCompanie
    {
        MasterDbcontext dbcontext;
        public CLSTBDeliveryCompanie(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBDeliveryCompanie> GetAll()
        {
            List<TBDeliveryCompanie> MySlider = dbcontext.TBDeliveryCompanies.OrderByDescending(n => n.IdDeliveryCompanie).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBDeliveryCompanie GetById(int IdDeliveryCompanie)
        {
            TBDeliveryCompanie sslid = dbcontext.TBDeliveryCompanies.FirstOrDefault(a => a.IdDeliveryCompanie == IdDeliveryCompanie);
            return sslid;
        }
        public bool saveData(TBDeliveryCompanie savee)
        {
            try
            {
                dbcontext.Add<TBDeliveryCompanie>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBDeliveryCompanie updatss)
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
        public bool deleteData(int IdDeliveryCompanie)
        {
            try
            {
                var catr = GetById(IdDeliveryCompanie);
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
        public List<TBDeliveryCompanie> GetAllv(int IdDeliveryCompanie)
        {
            List<TBDeliveryCompanie> MySlider = dbcontext.TBDeliveryCompanies.OrderByDescending(n => n.IdDeliveryCompanie == IdDeliveryCompanie).Where(a => a.IdDeliveryCompanie == IdDeliveryCompanie).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdDeliveryCompanie)
        {
            try
            {
                var catr = GetById(IdDeliveryCompanie);
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
