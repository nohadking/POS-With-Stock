using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIHomeImageProdact
    {
        List<TBHomeImageProdact> GetAll();
        TBHomeImageProdact GetById(int IdHomeImageProdact);
        bool saveData(TBHomeImageProdact savee);
        bool UpdateData(TBHomeImageProdact updatss);
        bool deleteData(int IdHomeImageProdact);
        List<TBHomeImageProdact> GetAllv(int IdHomeImageProdact);
        bool DELETPHOTO(int IdHomeImageProdact);
        bool DELETPHOTOWethError(string PhotoNAme);

    }
    public class CLSTBHomeImageProdact: IIHomeImageProdact
    {
        MasterDbcontext dbcontext;
        public CLSTBHomeImageProdact(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBHomeImageProdact> GetAll()
        {
            List<TBHomeImageProdact> MySlider = dbcontext.TBHomeImageProdacts.OrderByDescending(n => n.IdHomeImageProdact).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBHomeImageProdact GetById(int IdHomeImageProdact)
        {
            TBHomeImageProdact sslid = dbcontext.TBHomeImageProdacts.FirstOrDefault(a => a.IdHomeImageProdact == IdHomeImageProdact);
            return sslid;
        }
        public bool saveData(TBHomeImageProdact savee)
        {
            try
            {
                dbcontext.Add<TBHomeImageProdact>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBHomeImageProdact updatss)
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
        public bool deleteData(int IdHomeImageProdact)
        {
            try
            {
                var catr = GetById(IdHomeImageProdact);
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
        public List<TBHomeImageProdact> GetAllv(int IdHomeImageProdact)
        {
            List<TBHomeImageProdact> MySlider = dbcontext.TBHomeImageProdacts.OrderByDescending(n => n.IdHomeImageProdact == IdHomeImageProdact).Where(a => a.IdHomeImageProdact == IdHomeImageProdact).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdHomeImageProdact)
        {
            try
            {
                var catr = GetById(IdHomeImageProdact);
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
