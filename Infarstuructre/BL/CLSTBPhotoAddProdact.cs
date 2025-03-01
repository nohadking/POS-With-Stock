

namespace Infarstuructre.BL
{
    public interface IIPhotoAddProdact
    {
        List<TBViewPhotoAddProdact> GetAll();
        TBPhotoAddProdact GetById(int IdPhotoAddProdact);
        bool saveData(TBPhotoAddProdact savee);
        bool UpdateData(TBPhotoAddProdact updatss);
        bool deleteData(int IdPhotoAddProdact);
        bool DELETPHOTO(int IdPhotoAddProdact);
        bool DELETPHOTOWethError(string PhotoNAme);
        List<TBViewPhotoAddProdact> GetAllv(int ProductId);
        TBViewPhotoAddProdact GetByIdview(int IdPhotoAddProdact);
      
    }
    public class CLSTBPhotoAddProdact: IIPhotoAddProdact
    {
        MasterDbcontext dbcontext;

        public CLSTBPhotoAddProdact(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewPhotoAddProdact> GetAll()
        {
            List<TBViewPhotoAddProdact> MySlider = dbcontext.ViewPhotoAddProdact.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBPhotoAddProdact GetById(int IdPhotoAddProdact)
        {
            TBPhotoAddProdact sslid = dbcontext.TBPhotoAddProdacts.FirstOrDefault(a => a.IdPhotoAddProdact == IdPhotoAddProdact);
            return sslid;
        }
        public bool saveData(TBPhotoAddProdact savee)
        {
            try
            {
                dbcontext.Add<TBPhotoAddProdact>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBPhotoAddProdact updatss)
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
        public bool deleteData(int IdPhotoAddProdact)
        {
            try
            {
                var catr = GetById(IdPhotoAddProdact);
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
        public List<TBViewPhotoAddProdact> GetAllv(int ProductId)
        {
            List<TBViewPhotoAddProdact> MySlider = dbcontext.ViewPhotoAddProdact.OrderByDescending(n => n.IdProduct == ProductId).Where(a => a.IdProduct == ProductId).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdPhotoAddProdact)
        {
            try
            {
                var catr = GetById(IdPhotoAddProdact);
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
        public TBViewPhotoAddProdact GetByIdview(int IdPhotoAddProdact)
        {
            TBViewPhotoAddProdact sslid = dbcontext.ViewPhotoAddProdact.FirstOrDefault(a => a.IdPhotoAddProdact == IdPhotoAddProdact);
            return sslid;
        }

    }
}
