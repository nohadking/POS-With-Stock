using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIAccountingRestriction
    {
        List<TBAccountingRestriction> GetAll();
  
        TBAccountingRestriction GetById(int IdaccountingRestrictions);
        TBAccountingRestriction GetByBondNuAndBondType(int bond, string type);
        bool saveData(TBAccountingRestriction savee);
        bool UpdateData(TBAccountingRestriction updatss);
        bool deleteData(int IdaccountingRestrictions);
        List<TBAccountingRestriction> GetAllv(int IdaccountingRestrictions);

        List<TBAccountingRestriction> GetBySupAndPeriodDate(string sup, DateTime start, DateTime end);
        List<TBAccountingRestriction> GetByPeriodDate(DateTime start, DateTime end);
        List<TBAccountingRestriction> GetBySupAndDetectedDt(string sup, DateTime date);
        List<TBAccountingRestriction> GetByTypeAndPeriodDate(string type, DateTime start, DateTime end);
        List<TBAccountingRestriction> GetByTypeAndDetectedDt(string type, DateTime date);
        List<TBAccountingRestriction> GetByDetectedDt(DateTime date);
        List<TBAccountingRestriction> GetBySup(string sup);
        List<TBAccountingRestriction> GetByType(string type);

    }
    public class CLSTBAccountingRestriction: IIAccountingRestriction
    {
        MasterDbcontext dbcontext;
        public CLSTBAccountingRestriction(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBAccountingRestriction> GetAll()
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    
        public TBAccountingRestriction GetById(int IdaccountingRestrictions)
        {
            TBAccountingRestriction sslid = dbcontext.TBAccountingRestrictions.FirstOrDefault(a => a.IdaccountingRestrictions == IdaccountingRestrictions);
            return sslid;
        }
        public bool saveData(TBAccountingRestriction savee)
        {
            try
            {
                dbcontext.Add<TBAccountingRestriction>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBAccountingRestriction updatss)
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
        public bool deleteData(int IdaccountingRestrictions)
        {
            try
            {
                var catr = GetById(IdaccountingRestrictions);
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
        public bool DeleteDatacus(int idAccountingRestrictions, string nameBound)
        {
            try
            {
                var catr = GetById(idAccountingRestrictions);
                if (catr == null)
                {
                    // السجل غير موجود
                    return false;
                }

                // تعطيل السجل الحالي
                catr.CurrentState = false;
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                // البحث عن السجل المطلوب حذفه
                var dele = dbcontext.TBAccountingRestrictions
                            .FirstOrDefault(a => a.BondNumber == idAccountingRestrictions && a.BondType == nameBound);

                if (dele != null)
                {
                    dbcontext.TBAccountingRestrictions.Remove(dele);
                }

                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ لتسهيل عملية التصحيح
                Console.WriteLine($"Error in DeleteDatacus: {ex.Message}");
                return false;
            }
        }

        public List<TBAccountingRestriction> GetAllv(int IdaccountingRestrictions)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions.OrderByDescending(n => n.IdaccountingRestrictions == IdaccountingRestrictions).Where(a => a.IdaccountingRestrictions == IdaccountingRestrictions).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBAccountingRestriction GetByBondNuAndBondType(int bond, string type)
        {
            TBAccountingRestriction MySlider = dbcontext.TBAccountingRestrictions.FirstOrDefault(a => a.BondType == type && a.BondNumber == bond);
            return MySlider;
        }

        public List<TBAccountingRestriction> GetBySupAndPeriodDate(string sup, DateTime start, DateTime end)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.AccountingName == sup).Where(a => a.DateTimeEntry.Date >= start.Date && a.DateTimeEntry.Date <= end.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetByPeriodDate(DateTime start, DateTime end)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.DateTimeEntry.Date >= start.Date && a.DateTimeEntry.Date <= end.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetBySupAndDetectedDt(string sup, DateTime date)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.AccountingName == sup).Where(a => a.DateTimeEntry.Date == date.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetByTypeAndPeriodDate(string type, DateTime start, DateTime end)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.BondType == type).Where(a => a.DateTimeEntry.Date >= start.Date && a.DateTimeEntry.Date <= end.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetByTypeAndDetectedDt(string type, DateTime date)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.BondType == type).Where(a => a.DateTimeEntry.Date == date.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetByDetectedDt(DateTime date)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.DateTimeEntry.Date == date.Date).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetBySup(string sup)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.AccountingName == sup).ToList();
            return MySlider;
        }

        public List<TBAccountingRestriction> GetByType(string type)
        {
            List<TBAccountingRestriction> MySlider = dbcontext.TBAccountingRestrictions
                .Where(a => a.BondType == type).ToList();
            return MySlider;
        }
    }
}
