using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
	public interface IIDeliveryCompanyPricing
	{
		List<TBViewDeliveryCompanyPricing> GetAll();
		TBDeliveryCompanyPricing GetById(int IdDeliveryCompanyPricing);
		bool saveData(TBDeliveryCompanyPricing savee);
		bool UpdateData(TBDeliveryCompanyPricing updatss);
		bool deleteData(int IdDeliveryCompanyPricing);
		List<TBViewDeliveryCompanyPricing> GetAllv(int IdDeliveryCompanyPricing);
		TBViewDeliveryCompanyPricing GetByIdview(int IdDeliveryCompanyPricing);
	}
	public class CLSTBDeliveryCompanyPricing: IIDeliveryCompanyPricing
	{
		MasterDbcontext dbcontext;
		public CLSTBDeliveryCompanyPricing(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;

		}
		public List<TBViewDeliveryCompanyPricing> GetAll()
		{
			List<TBViewDeliveryCompanyPricing> MySlider = dbcontext.ViewDeliveryCompanyPricing.Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBDeliveryCompanyPricing GetById(int IdDeliveryCompanyPricing)
		{
			TBDeliveryCompanyPricing sslid = dbcontext.TBDeliveryCompanyPricings.FirstOrDefault(a => a.IdDeliveryCompanyPricing == IdDeliveryCompanyPricing);
			return sslid;
		}
		public bool saveData(TBDeliveryCompanyPricing savee)
		{
			try
			{
				dbcontext.Add<TBDeliveryCompanyPricing>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBDeliveryCompanyPricing updatss)
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
		public bool deleteData(int IdDeliveryCompanyPricing)
		{
			try
			{
				var catr = GetById(IdDeliveryCompanyPricing);
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
		public List<TBViewDeliveryCompanyPricing> GetAllv(int IdDeliveryCompanyPricing)
		{
			List<TBViewDeliveryCompanyPricing> MySlider = dbcontext.ViewDeliveryCompanyPricing.OrderByDescending(n => n.IdDeliveryCompanyPricing == IdDeliveryCompanyPricing).Where(a => a.IdDeliveryCompanyPricing == IdDeliveryCompanyPricing).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBViewDeliveryCompanyPricing GetByIdview(int IdDeliveryCompanyPricing)
		{
			TBViewDeliveryCompanyPricing sslid = dbcontext.ViewDeliveryCompanyPricing.FirstOrDefault(a => a.IdDeliveryCompanyPricing == IdDeliveryCompanyPricing);
			return sslid;
		}

	}
}
