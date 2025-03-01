using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIClassCard
    {
        List<TBViewClassCard> GetAll();
        TBClassCard GetById(int IdClassCard);
        bool saveData(TBClassCard savee);
        bool UpdateData(TBClassCard updatss);
        bool deleteData(int IdClassCard);
        List<TBViewClassCard> GetAllv(int IdClassCard);
        bool DELETPHOTO(int IdClassCard);
        bool DELETPHOTOWethError(string PhotoNAme);
        TBViewClassCard GetByIdview(int IdClassCard);
    }
    public class CLSTBClassCard: IIClassCard
    {
        MasterDbcontext dbcontext;
        public CLSTBClassCard(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }


        public List<TBViewClassCard> GetAll()
        {
            List<TBViewClassCard> MySlider = dbcontext.ViewClassCard.OrderByDescending(n => n.IdClassCard).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBClassCard GetById(int IdClassCard)
        {
            TBClassCard sslid = dbcontext.TBClassCards.FirstOrDefault(a => a.IdClassCard == IdClassCard);
            return sslid;
        }
        public bool saveData(TBClassCard savee)
        {
            try
            {
                dbcontext.Add<TBClassCard>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBClassCard updatss)
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
        public bool deleteData(int IdClassCard)
        {
            try
            {
                var catr = GetById(IdClassCard);
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
        public List<TBViewClassCard> GetAllv(int IdClassCard)
        {
            List<TBViewClassCard> MySlider = dbcontext.ViewClassCard.OrderByDescending(n => n.IdClassCard == IdClassCard).Where(a => a.IdClassCard == IdClassCard).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdClassCard)
        {
            try
            {
                var catr = GetById(IdClassCard);
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
        public TBViewClassCard GetByIdview(int IdClassCard)
        {
            TBViewClassCard sslid = dbcontext.ViewClassCard.FirstOrDefault(a => a.IdClassCard == IdClassCard);
            return sslid;
        }
    }
}
