using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
	public interface IIPaymentMethod
	{
		List<TBPaymentMethod> GetAll();
		TBPaymentMethod GetById(int IdPaymentMethod);
		bool saveData(TBPaymentMethod savee);
		bool UpdateData(TBPaymentMethod updatss);
		bool deleteData(int IdPaymentMethod);
		List<TBPaymentMethod> GetAllv(int IdPaymentMethod);
		List<TBPaymentMethod> GetAllActive();
	}
	public class CLSTBPaymentMethod: IIPaymentMethod
	{
		MasterDbcontext dbcontext;
		public CLSTBPaymentMethod(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;
		}
		public List<TBPaymentMethod> GetAll()
		{
			List<TBPaymentMethod> MySlider = dbcontext.TBPaymentMethods.OrderByDescending(n => n.IdPaymentMethod).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}

		public List<TBPaymentMethod> GetAllActive()
		{
			List<TBPaymentMethod> MySlider = dbcontext.TBPaymentMethods.OrderByDescending(n => n.IdPaymentMethod).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
			return MySlider;
		}

		public TBPaymentMethod GetById(int IdPaymentMethod)
		{
			TBPaymentMethod sslid = dbcontext.TBPaymentMethods.FirstOrDefault(a => a.IdPaymentMethod == IdPaymentMethod);
			return sslid;
		}
		public bool saveData(TBPaymentMethod savee)
		{
			try
			{
				dbcontext.Add<TBPaymentMethod>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBPaymentMethod updatss)
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
		public bool deleteData(int IdPaymentMethod)
		{
			try
			{
				var catr = GetById(IdPaymentMethod);
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
		public List<TBPaymentMethod> GetAllv(int IdPaymentMethod)
		{
			List<TBPaymentMethod> MySlider = dbcontext.TBPaymentMethods.OrderByDescending(n => n.IdPaymentMethod == IdPaymentMethod).Where(a => a.IdPaymentMethod == IdPaymentMethod).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}

	}
}
