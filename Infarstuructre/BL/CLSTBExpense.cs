using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIExpense
    {
        List<TBViewExpense> GetAll();
        TBExpense GetById(int IdExpense);
        bool  saveData(TBExpense savee);
        bool UpdateData(TBExpense updatss);
        bool deleteData(int IdExpense);
        List<TBViewExpense> GetAllv(int IdExpense);
        TBViewExpense GetByIdview(int IdExpense);

        List<TBViewExpense> GetByExpenseAndPeriodDate(string expense, DateTime start, DateTime end);
        List<TBViewExpense> GetByPeriodDate(DateTime start, DateTime end);
        List<TBViewExpense> GetByCategoryAndPeriodDate(string category, DateTime start, DateTime end);
        List<TBViewExpense> GetByDetectedDt(DateTime date);
        List<TBViewExpense> GetByExpense(string expense);
        List<TBViewExpense> GetByCategory(string category);
    }
    public class CLSTBExpense : IIExpense
    {
        MasterDbcontext dbcontext;
        public CLSTBExpense(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewExpense> GetAll()
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense.OrderByDescending(n => n.IdExpense).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBExpense GetById(int IdExpense)
        {
            TBExpense sslid = dbcontext.TBExpenses.FirstOrDefault(a => a.IdExpense == IdExpense);
            return sslid;
        }
        //public bool saveData(TBExpense savee,TBAccountingRestriction saveaccount)
        //{
        //    try
        //    {
        //        dbcontext.Add<TBExpense>(savee);
        //        dbcontext.Add<TBAccountingRestriction>(savee.);







        //        dbcontext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}


        public bool saveData(TBExpense savee)
        {
            using (var transaction = dbcontext.Database.BeginTransaction()) // استخدام transaction لضمان حفظ البيانات بشكل متكامل
            {
                try
                {
                    // **التحقق من صحة البيانات قبل الحفظ**
                    if (savee == null)
                    {
                        return false; // لا يمكن الحفظ إذا كان الكائن فارغًا
                    }

                    // إضافة البيانات إلى الجدول الأول (TBExpense)
                    dbcontext.TBExpenses.Add(savee); // استخدم DbSet مباشرة

                    // **الحصول على رقم القيد الجديد بشكل صحيح**
                    int max = dbcontext.TBAccountingRestrictions.Any()
                               ? dbcontext.TBAccountingRestrictions.Max(c => c.NumberaccountingRestrictions) + 1
                               : 1;

                    // **استعلامات أكثر كفاءة (استخدام Find بدلاً من FirstOrDefault)**
                    var LavelFore = dbcontext.TBLevelForeAccounts.Find(savee.IdLevelForeAccount);
                    var LavelForecreditor = dbcontext.TBLevelForeAccounts.Find(savee.IdLevelForeAccountcreditor);
                    var expnsevcatrg = dbcontext.TBExpenseCategorys.Find(savee.IdExpenseCategory);

                    // **التحقق من وجود الكائنات قبل استخدامها**
                    if (LavelFore == null || LavelForecreditor == null || expnsevcatrg == null)
                    {
                        // يمكنك تسجيل خطأ هنا أو إرجاع false للإشارة إلى فشل العملية
                        transaction.Rollback(); // التراجع عن أي تغييرات تم إجراؤها
                        return false;
                    }

                    // **إنشاء القيد الأول**
                    var saveaccountDebtor = new TBAccountingRestriction
                    {
                        NumberaccountingRestrictions = max,
                        AccountingName = LavelFore.AccountName,
                        BondType = "سند صرف",
                        BondNumber = savee.BondNumber,
                        Debtor = savee.Amount,
                        creditor = 0,
                        Statement = savee.Statement,
                        Nouts = "سند صرف رقم : " + savee.BondNumber,
                        DataEntry = savee.DataEntry,
                        DateTimeEntry = savee.DateTimeEntry,
                        CurrentState = true
                    };

                    dbcontext.TBAccountingRestrictions.Add(saveaccountDebtor);

                    // **إنشاء القيد الثاني**
                    var saveaccountCreditor = new TBAccountingRestriction
                    {
                        NumberaccountingRestrictions = max,
                        AccountingName = LavelForecreditor.AccountName,
                        BondType = "سند صرف",
                        BondNumber = savee.BondNumber,
                        Debtor = 0,
                        creditor = savee.Amount,
                        Statement = savee.Statement,
                        Nouts = "سند صرف رقم : " + savee.BondNumber,
                        DataEntry = savee.DataEntry,
                        DateTimeEntry = savee.DateTimeEntry,
                        CurrentState = true
                    };

                    dbcontext.TBAccountingRestrictions.Add(saveaccountCreditor);

                    // **حفظ التغييرات مرة واحدة فقط**
                    dbcontext.SaveChanges();

                    transaction.Commit(); // تأكيد جميع التغييرات
                    return true;
                }
                catch (Exception ex)
                {
                    // **تسجيل الخطأ بشكل مفصل (مهم جدًا)**
                    Console.WriteLine($"Error saving data: {ex.Message}\n{ex.StackTrace}"); //  استبدل هذا بتسجيل مناسب في ملف أو قاعدة بيانات
                    transaction.Rollback(); // التراجع عن أي تغييرات تم إجراؤها
                    return false;
                }
            }
        }




        public bool UpdateData(TBExpense updatss)
        {
           
                using (var transaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        var expnsevcatrg = dbcontext.TBExpenseCategorys.FirstOrDefault(a => a.IdExpenseCategory == updatss.IdExpenseCategory);
                        var LavelFore = dbcontext.TBLevelForeAccounts.FirstOrDefault(a => a.IdLevelForeAccount == updatss.IdLevelForeAccount);
                        var LavelForecredteor = dbcontext.TBLevelForeAccounts.FirstOrDefault(a => a.IdLevelForeAccount == updatss.IdLevelForeAccountcreditor);
                        // *** إنشاء قيد محاسبي للمدين ***
                        TBAccountingRestriction debtorEntry = new TBAccountingRestriction();
                        debtorEntry.AccountingName = LavelFore.AccountName;
                        debtorEntry.BondType = "سند صرف";
                        debtorEntry.BondNumber = updatss.BondNumber;
                        debtorEntry.Debtor = updatss.Amount;
                        debtorEntry.creditor = 0;
                        debtorEntry.Statement = updatss.Statement;
                        debtorEntry.Nouts = "سند صرف رقم :" + " " + updatss.BondNumber;
                        debtorEntry.DataEntry = updatss.DataEntry;
                        debtorEntry.DateTimeEntry = updatss.DateTimeEntry;
                        debtorEntry.CurrentState = true;
                        dbcontext.TBAccountingRestrictions.Add(debtorEntry);
                        // *** إنشاء قيد محاسبي للدائن ***
                        TBAccountingRestriction creditorEntry = new TBAccountingRestriction();
                        creditorEntry.AccountingName = LavelForecredteor.AccountName;
                        creditorEntry.BondType = "سند صرف";
                        creditorEntry.BondNumber = updatss.BondNumber;
                        creditorEntry.Debtor = 0;
                        creditorEntry.creditor = updatss.Amount;
                        creditorEntry.Statement = updatss.Statement;
                        creditorEntry.Nouts = "سند صرف رقم :" + " " + updatss.BondNumber;
                        creditorEntry.DataEntry = updatss.DataEntry;
                        creditorEntry.DateTimeEntry = updatss.DateTimeEntry;
                        creditorEntry.CurrentState = true;
                        dbcontext.TBAccountingRestrictions.Add(creditorEntry);
                        // *** تحديث المصروفات ***
                        dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbcontext.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // من الأفضل تسجيل الخطأ في سجل أو إلقاء استثناء أكثر تحديدًا
                        Console.WriteLine($"Error updating data: {ex.Message}");
                        return false;
                    }
                }   
        }
        public bool deleteData(int IdExpense)
        {
            try
            {
                var catr = GetById(IdExpense);
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
        public List<TBViewExpense> GetAllv(int IdExpense)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense.OrderByDescending(n => n.IdExpense == IdExpense).Where(a => a.IdExpense == IdExpense).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBViewExpense GetByIdview(int IdExpense)
        {
            TBViewExpense sslid = dbcontext.ViewExpense.FirstOrDefault(a => a.IdExpense == IdExpense);
            return sslid;
        }

        public List<TBViewExpense> GetByExpenseAndPeriodDate(string expense, DateTime start, DateTime end)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.AccountName == expense).
                Where(a => a.DateBond >= DateOnly.FromDateTime(start) && a.DateBond <= DateOnly.FromDateTime(end))
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewExpense> GetByPeriodDate(DateTime start, DateTime end)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.DateBond >= DateOnly.FromDateTime(start) && a.DateBond <= DateOnly.FromDateTime(end))
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewExpense> GetByCategoryAndPeriodDate(string category, DateTime start, DateTime end)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.ExpenseCategory == category).
                Where(a => a.DateBond >= DateOnly.FromDateTime(start) && a.DateBond <= DateOnly.FromDateTime(end))
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewExpense> GetByDetectedDt(DateTime date)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.DateBond == DateOnly.FromDateTime(date))
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewExpense> GetByExpense(string expense)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.AccountName == expense)
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewExpense> GetByCategory(string category)
        {
            List<TBViewExpense> MySlider = dbcontext.ViewExpense
                .Where(a => a.ExpenseCategory == category)
                .Where(a => a.CurrentState == true).ToList();
            return MySlider; ;
        }
    }
}
