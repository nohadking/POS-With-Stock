
namespace Infarstuructre.BL
{
    public interface IIWarehouse
    {
        List<TBViewWarehouse> GetAll();
        TBWarehouse GetById(int IdWarehouse);
        bool saveData(TBWarehouse savee);
        bool UpdateData(TBWarehouse updatss);
        bool deleteData(int IdWarehouse);
        List<TBViewWarehouse> GetAllv(int IdWarehouse);
        TBViewWarehouse GetByIdview(int IdWarehouse);
    }
    public class CLSTBWarehouse: IIWarehouse
    {
        MasterDbcontext dbcontext;
        public CLSTBWarehouse(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewWarehouse> GetAll()
        {
            List<TBViewWarehouse> MySlider = dbcontext.ViewWarehouse.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBWarehouse GetById(int IdWarehouse)
        {
            TBWarehouse sslid = dbcontext.TBWarehouses.FirstOrDefault(a => a.IdWarehouse == IdWarehouse);
            return sslid;
        }
        public bool saveData(TBWarehouse savee)
        {
            try
            {
                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = savee.AccountNumber.ToString();  // تحويل الرقم إلى سلسلة                                                                    // تحويل رقم الحساب إلى سلسلة نصي
                // حذف آخر 4 أرقام من رقم الحساب
                var newNumber = despletnumber.Substring(0, despletnumber.Length - 4);
                // تحويل السلسلة الناتجة إلى رقم
                var newNumberAsNumber = Convert.ToInt32(newNumber); // حذف آخر 4 أرقام
                // إذا كنت بحاجة إلى تحويله مرة أخرى إلى رقم
                var seletidlivTre = dbcontext.TBLevelThreeAccounts.FirstOrDefault(a => a.NumberAccount == newNumberAsNumber);
                dbcontext.Add<TBWarehouse>(savee);
                saveLevelFore.IdMainAccount = seletidlivTre.IdMainAccount;
                saveLevelFore.IdLevelTwoAccount = seletidlivTre.IdLevelTwoAccount;
                saveLevelFore.IdLevelThreeAccount = seletidlivTre.IdLevelThreeAccount;
                saveLevelFore.AccountName = savee.WarehouseName;
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
        public bool UpdateData(TBWarehouse updatss)
        {
            try
            {
                var saveLevelFore = new TBLevelForeAccount();
                var selectLevelThreeAccount = new TBLevelThreeAccount();
                var despletnumber = updatss.AccountNumber.ToString();  // تحويل الرقم إلى سلسلة
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
                saveLevelFore.AccountName = updatss.WarehouseName;
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
        public bool deleteData(int IdWarehouse)
        {
            try
            {
                var catr = GetById(IdWarehouse);
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
        public List<TBViewWarehouse> GetAllv(int IdWarehouse)
        {
            List<TBViewWarehouse> MySlider = dbcontext.ViewWarehouse.OrderByDescending(n => n.IdWarehouse == IdWarehouse).Where(a => a.IdWarehouse == IdWarehouse).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewWarehouse GetByIdview(int IdWarehouse)
        {
            TBViewWarehouse sslid = dbcontext.ViewWarehouse.FirstOrDefault(a => a.IdWarehouse == IdWarehouse);
            return sslid;
        }

    }
}
