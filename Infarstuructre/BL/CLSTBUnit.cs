using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIUnit
    {
        List<TBUnit> GetAll();
        List<TBUnit> GetAllActive();
        TBUnit GetById(int IdUnit);
        bool saveData(TBUnit savee);
        bool UpdateData(TBUnit updatss);
        bool deleteData(int IdUnit);
        List<TBUnit> GetAllv(int IdUnit);
    }
    public class CLSTBUnit: IIUnit
    {
        MasterDbcontext dbcontext;
        public CLSTBUnit(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBUnit> GetAll()
        {
            List<TBUnit> MySlider = dbcontext.TBUnits.OrderByDescending(n => n.IdUnit).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBUnit> GetAllActive()
        {
            List<TBUnit> MySlider = dbcontext.TBUnits.OrderByDescending(n => n.IdUnit).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBUnit GetById(int IdUnit)
        {
            TBUnit sslid = dbcontext.TBUnits.FirstOrDefault(a => a.IdUnit == IdUnit);
            return sslid;
        }
        public bool saveData(TBUnit savee)
        {
            try
            {
                dbcontext.Add<TBUnit>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBUnit updatss)
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
        public bool deleteData(int IdUnit)
        {
            try
            {
                var catr = GetById(IdUnit);
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
        public List<TBUnit> GetAllv(int IdUnit)
        {
            List<TBUnit> MySlider = dbcontext.TBUnits.OrderByDescending(n => n.IdUnit == IdUnit).Where(a => a.IdUnit == IdUnit).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

    }
}
