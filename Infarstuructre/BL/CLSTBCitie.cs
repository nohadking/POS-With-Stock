using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
	public interface IICitie
	{
		List<TBCitie> GetAll();
		TBCitie GetById(int IdCitie);
		bool saveData(TBCitie savee);
		bool UpdateData(TBCitie updatss);
		bool deleteData(int IdCitie);
		List<TBCitie> GetAllv(int IdCitie);
	}
	public class CLSTBCitie: IICitie
	{
		MasterDbcontext dbcontext;
		public CLSTBCitie(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;

		}
		public List<TBCitie> GetAll()
		{
			List<TBCitie> MySlider = dbcontext.TBCities.OrderByDescending(n => n.IdCitie).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBCitie GetById(int IdCitie)
		{
			TBCitie sslid = dbcontext.TBCities.FirstOrDefault(a => a.IdCitie == IdCitie);
			return sslid;
		}
		public bool saveData(TBCitie savee)
		{
			try
			{
				dbcontext.Add<TBCitie>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBCitie updatss)
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
		public bool deleteData(int IdCitie)
		{
			try
			{
				var catr = GetById(IdCitie);
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
		public List<TBCitie> GetAllv(int IdCitie)
		{
			List<TBCitie> MySlider = dbcontext.TBCities.OrderByDescending(n => n.IdCitie == IdCitie).Where(a => a.IdCitie == IdCitie).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
	}
}
