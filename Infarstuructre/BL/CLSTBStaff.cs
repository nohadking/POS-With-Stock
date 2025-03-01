

namespace Infarstuructre.BL
{
    public interface IIStaff
    {
        List<TBStaff> GetAll();
        TBStaff GetById(int IdStaff);
        bool saveData(TBStaff savee);
        bool UpdateData(TBStaff updatss);
        bool deleteData(int IdStaff);
        List<TBStaff> GetAllv(int IdStaff);
        bool DELETPHOTO(int IdStaff);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBStaff: IIStaff
    {
        MasterDbcontext dbcontext;
        public CLSTBStaff(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBStaff> GetAll()
        {
            List<TBStaff> MySlider = dbcontext.TBStaffs.OrderByDescending(n => n.IdStaff).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBStaff GetById(int IdStaff)
        {
            TBStaff sslid = dbcontext.TBStaffs.FirstOrDefault(a => a.IdStaff == IdStaff);
            return sslid;
        }
        public bool saveData(TBStaff savee)
        {
            try
            {
                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = savee.AccountNumber.ToString();  // تحويل الرقم إلى سلسلة
                                                                     // تحويل رقم الحساب إلى سلسلة نصية


                // حذف آخر 4 أرقام من رقم الحساب
                var newNumber = despletnumber.Substring(0, despletnumber.Length - 4);

                // تحويل السلسلة الناتجة إلى رقم
                var newNumberAsNumber = Convert.ToInt32(newNumber); // حذف آخر 4 أرقام

                // إذا كنت بحاجة إلى تحويله مرة أخرى إلى رقم


                var seletidlivTre = dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.NumberAccount == newNumberAsNumber);

                dbcontext.Add<TBStaff>(savee);
                saveLevelFore.IdMainAccount = seletidlivTre.IdMainAccount;
                saveLevelFore.IdLevelTwoAccount = seletidlivTre.IdLevelTwoAccount;
                saveLevelFore.IdLevelThreeAccount = seletidlivTre.IdLevelThreeAccount;
                saveLevelFore.AccountName = savee.EmployeeFullname;
                saveLevelFore.AccountNumberlivl = savee.AccountNumber;
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
        public bool UpdateData(TBStaff updatss)
        {
            try
            {

                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = updatss.AccountNumber.ToString();  // تحويل الرقم إلى سلسلة                                                                   
                // حذف آخر 4 أرقام من رقم الحساب
                var newNumber = despletnumber.Substring(0, despletnumber.Length - 4);
                // تحويل السلسلة الناتجة إلى رقم
                var newNumberAsNumber = Convert.ToInt32(newNumber); // حذف آخر 4 أرقام
                // إذا كنت بحاجة إلى تحويله مرة أخرى إلى رقم
                var seletidlivTre = dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.NumberAccount == newNumberAsNumber);
                saveLevelFore.IdMainAccount = seletidlivTre.IdMainAccount;
                saveLevelFore.IdLevelTwoAccount = seletidlivTre.IdLevelTwoAccount;
                saveLevelFore.IdLevelThreeAccount = seletidlivTre.IdLevelThreeAccount;
                saveLevelFore.AccountName = updatss.EmployeeFullname;
                saveLevelFore.AccountNumberlivl = updatss.AccountNumber;
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
        public bool deleteData(int IdStaff)
        {
            try
            {
                var catr = GetById(IdStaff);
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
        public List<TBStaff> GetAllv(int IdStaff)
        {
            List<TBStaff> MySlider = dbcontext.TBStaffs.OrderByDescending(n => n.IdStaff == IdStaff).Where(a => a.IdStaff == IdStaff).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdStaff)
        {
            try
            {
                var catr = GetById(IdStaff);
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
