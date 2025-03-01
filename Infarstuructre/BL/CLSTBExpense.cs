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
    public class CLSTBExpense: IIExpense
    {
        MasterDbcontext dbcontext;
        public CLSTBExpense(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
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
            try
            {
                var saveaccount = new TBAccountingRestriction();
                // إضافة البيانات إلى الجدول الأول (TBExpense)
                dbcontext.Add<TBExpense>(savee);

                var max = dbcontext.TBAccountingRestrictions.Any()
               ? dbcontext.TBAccountingRestrictions.Max(c => c.NumberaccountingRestrictions) + 1
               : 1;
                var expnsevcatrg = dbcontext.TBExpenseCategorys.FirstOrDefault(a => a.IdExpenseCategory == savee.IdExpenseCategory);
                var LavelFore = dbcontext.TBLevelForeAccounts.FirstOrDefault(a => a.IdLevelForeAccount == savee.IdLevelForeAccount);


                saveaccount.NumberaccountingRestrictions = max;
                saveaccount.AccountingName = LavelFore.AccountName;
                saveaccount.BondType = "سند صرف";
                saveaccount.BondNumber = savee.BondNumber;
                saveaccount.Debtor = savee.Amount;
                saveaccount.creditor = 0;
                saveaccount.Statement = savee.Statement;
                saveaccount.Nouts = "سند صرف رقم :"+" "+ savee.BondNumber;
                saveaccount.DataEntry = savee.DataEntry;
                saveaccount.DateTimeEntry = savee.DateTimeEntry;
                saveaccount.CurrentState = true;
                // إضافة البيانات إلى الجدول الثاني (TBAccountingRestriction)
                dbcontext.Add<TBAccountingRestriction>(saveaccount);
                // حفظ التغييرات في قاعدة البيانات
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool UpdateData(TBExpense updatss)
        {
            try
            {
             
                var expnsevcatrg = dbcontext.TBExpenseCategorys.FirstOrDefault(a => a.IdExpenseCategory == updatss.IdExpenseCategory);
                var LavelFore = dbcontext.TBLevelForeAccounts.FirstOrDefault(a => a.IdLevelForeAccount == updatss.IdLevelForeAccount);
                var update = dbcontext.TBAccountingRestrictions.FirstOrDefault(a => a.AccountingName == expnsevcatrg.ExpenseCategory && a.BondNumber == updatss.BondNumber);
                update.AccountingName = LavelFore.AccountName;
                update.BondType = "سند صرف";
                update.BondNumber = updatss.BondNumber;
                update.Debtor = updatss.Amount;
                update.creditor = 0;
                update.Statement = updatss.Statement;
                update.Nouts = "سند صرف رقم :" + " " + updatss.BondNumber;
                update.DataEntry = updatss.DataEntry;
                update.DateTimeEntry = updatss.DateTimeEntry;
                update.CurrentState = true;
                dbcontext.Entry(update).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
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
