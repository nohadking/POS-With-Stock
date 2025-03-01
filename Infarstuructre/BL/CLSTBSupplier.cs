

using System.Net.WebSockets;

namespace Infarstuructre.BL
{
    public interface IISupplier
    {
        List<TBViewSupplier> GetAll();
        TBSupplier GetById(int IdSupplier);
        bool saveData(TBSupplier savee);
        bool UpdateData(TBSupplier updatss);
        bool deleteData(int IdSupplier);
        List<TBViewSupplier> GetAllv(int IdSupplier);
        bool DELETPHOTO(int IdSupplier);
        bool DELETPHOTOWethError(string PhotoNAme);
        TBViewSupplier GetByIdview(int IdSupplier);

    }
    public class CLSTBSupplier: IISupplier
    {
        MasterDbcontext dbcontext;
        public CLSTBSupplier(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewSupplier> GetAll()
        {
            List<TBViewSupplier> MySlider = dbcontext.ViewSupplier.OrderByDescending(n => n.IdSupplier).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBSupplier GetById(int IdSupplier)
        {
            TBSupplier sslid = dbcontext.TBSuppliers.FirstOrDefault(a => a.IdSupplier == IdSupplier);
            return sslid;
        }
        public bool saveData(TBSupplier savee)
        {
            try
            {
                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = savee.NumberAccount.ToString();  // تحويل الرقم إلى سلسلة
                                                                     // تحويل رقم الحساب إلى سلسلة نصية
              

                // حذف آخر 4 أرقام من رقم الحساب
                var newNumber = despletnumber.Substring(0, despletnumber.Length - 4);

                // تحويل السلسلة الناتجة إلى رقم
                var newNumberAsNumber = Convert.ToInt32(newNumber); // حذف آخر 4 أرقام

                // إذا كنت بحاجة إلى تحويله مرة أخرى إلى رقم
             

                var seletidlivTre=dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.NumberAccount== newNumberAsNumber);

                dbcontext.Add<TBSupplier>(savee);
                saveLevelFore.IdMainAccount = seletidlivTre.IdMainAccount;
                saveLevelFore.IdLevelTwoAccount = seletidlivTre.IdLevelTwoAccount;
                saveLevelFore.IdLevelThreeAccount = seletidlivTre.IdLevelThreeAccount;
                saveLevelFore.AccountName = savee.SupplierName;
                saveLevelFore.AccountNumberlivl = savee.NumberAccount;
                saveLevelFore.Active = true;
                saveLevelFore.CurrentState = true;
                saveLevelFore.DataEntry = savee.DataEntry;
                saveLevelFore.DateTimeEntry = savee.DateTimeEntry;
                dbcontext.Add<TBLevelForeAccount>(saveLevelFore);
                // حفظ التغييرات في قاعدة البيانات
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBSupplier updatss)
        {
            try
            {

                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = updatss.NumberAccount.ToString();  // تحويل الرقم إلى سلسلة
                                                                     // تحويل رقم الحساب إلى سلسلة نصية


                // حذف آخر 4 أرقام من رقم الحساب
                var newNumber = despletnumber.Substring(0, despletnumber.Length - 4);

                // تحويل السلسلة الناتجة إلى رقم
                var newNumberAsNumber = Convert.ToInt32(newNumber); // حذف آخر 4 أرقام

                // إذا كنت بحاجة إلى تحويله مرة أخرى إلى رقم


                var seletidlivTre = dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.NumberAccount == newNumberAsNumber);

             
                saveLevelFore.IdMainAccount = seletidlivTre.IdMainAccount;
                saveLevelFore.IdLevelTwoAccount = seletidlivTre.IdLevelTwoAccount;
                saveLevelFore.IdLevelThreeAccount = seletidlivTre.IdLevelThreeAccount;
                saveLevelFore.AccountName = updatss.SupplierName;
                saveLevelFore.AccountNumberlivl = updatss.NumberAccount;
                saveLevelFore.Active = true;
                saveLevelFore.CurrentState = true;
                saveLevelFore.DataEntry = updatss.DataEntry;
                saveLevelFore.DateTimeEntry = updatss.DateTimeEntry;
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.Entry(saveLevelFore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdSupplier)
        {
            try
            {
                var catr = GetById(IdSupplier);
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
        public List<TBViewSupplier> GetAllv(int IdSupplier)
        {
            List<TBViewSupplier> MySlider = dbcontext.ViewSupplier.OrderByDescending(n => n.IdSupplier == IdSupplier).Where(a => a.IdSupplier == IdSupplier).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdSupplier)
        {
            try
            {
                var catr = GetById(IdSupplier);
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
        public TBViewSupplier GetByIdview(int IdSupplier)
        {
            TBViewSupplier sslid = dbcontext.ViewSupplier.FirstOrDefault(a => a.IdSupplier == IdSupplier);
            return sslid;
        }
    }
}
