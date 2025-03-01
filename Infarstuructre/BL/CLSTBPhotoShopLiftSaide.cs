using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIPhotoShopLiftSaide
    {
        List<TBPhotoShopLiftSaide> GetAll();
        TBPhotoShopLiftSaide GetById(int IdPhotoShopLiftSaide);
        bool saveData(TBPhotoShopLiftSaide savee);
        bool UpdateData(TBPhotoShopLiftSaide updatss);
        bool deleteData(int IdPhotoShopLiftSaide);
        List<TBPhotoShopLiftSaide> GetAllv(int IdPhotoShopLiftSaide);
        bool DELETPHOTO(int IdPhotoShopLiftSaide);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBPhotoShopLiftSaide: IIPhotoShopLiftSaide
    {
        MasterDbcontext dbcontext;
        public CLSTBPhotoShopLiftSaide(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBPhotoShopLiftSaide> GetAll()
        {
            List<TBPhotoShopLiftSaide> MySlider = dbcontext.TBPhotoShopLiftSaides.OrderByDescending(n => n.IdPhotoShopLiftSaide).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBPhotoShopLiftSaide GetById(int IdPhotoShopLiftSaide)
        {
            TBPhotoShopLiftSaide sslid = dbcontext.TBPhotoShopLiftSaides.FirstOrDefault(a => a.IdPhotoShopLiftSaide == IdPhotoShopLiftSaide);
            return sslid;
        }
        public bool saveData(TBPhotoShopLiftSaide savee)
        {
            try
            {
                dbcontext.Add<TBPhotoShopLiftSaide>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBPhotoShopLiftSaide updatss)
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
        public bool deleteData(int IdPhotoShopLiftSaide)
        {
            try
            {
                var catr = GetById(IdPhotoShopLiftSaide);
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
        public List<TBPhotoShopLiftSaide> GetAllv(int IdPhotoShopLiftSaide)
        {
            List<TBPhotoShopLiftSaide> MySlider = dbcontext.TBPhotoShopLiftSaides.OrderByDescending(n => n.IdPhotoShopLiftSaide == IdPhotoShopLiftSaide).Where(a => a.IdPhotoShopLiftSaide == IdPhotoShopLiftSaide).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdPhotoShopLiftSaide)
        {
            try
            {
                var catr = GetById(IdPhotoShopLiftSaide);
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
