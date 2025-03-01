

using Domin.Entity;

namespace Infarstuructre.BL
{
	public interface IIPurchase
	{
		List<TBViewPurchase> GetAll();
		TBPurchase GetById(int IdPurchase);
        List<TBPurchase> GetByPurcheasNu(int purchaseNu);
		bool saveData(TBPurchase savee);
		bool UpdateData(TBPurchase updatss);
		bool deleteData(int IdPurchase);
		List<TBViewPurchase> GetAllv(int IdPurchase);
		TBViewPurchase GetByIdview(int IdPurchase);
        List<TBViewPurchase> GetAByDetectedDate(DateTime date);
        List<TBViewPurchase> GetABySuplierAndPeriod(string suplierNm, DateTime sdate, DateTime edate);
        List<TBViewPurchase> GetAByPruductAndPeriod(string productName, DateTime sdate, DateTime edate);
        List<TBViewPurchase> GetByPeriod(DateTime sdate, DateTime edate);
        List<TBViewPurchase> GetByProduct(string productName);
        List<TBViewPurchase> GetBySuplier(string suplierNm);
        List<TBViewPurchase> GetByPurcheasNm(int purNm);
	}
	public class CLSTBPurchase: IIPurchase
	{
		MasterDbcontext dbcontext;
		public CLSTBPurchase(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;

		}

		public List<TBViewPurchase> GetAll()
		{
			try
			{
                List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase).Where(a => a.CurrentState == true).ToList();
                return MySlider;
            }
			catch (Exception)
			{

				throw;
			}
		
		}
		public TBPurchase GetById(int IdPurchase)
		{
			TBPurchase sslid = dbcontext.TBPurchases.FirstOrDefault(a => a.IdPurchase == IdPurchase);
			return sslid;
		}
		public bool saveData(TBPurchase savee)
		{
			try
			{
				dbcontext.Add<TBPurchase>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBPurchase updatss)
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
		public bool deleteData(int IdPurchase)
		{
			try
			{
				var catr = GetById(IdPurchase);
				catr.CurrentState = false;
				TBPurchase dele = dbcontext.TBPurchases.Where(a => a.PurchaseNumber == IdPurchase).FirstOrDefault();
				dbcontext.TBPurchases.Remove(dele);
				dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public List<TBViewPurchase> GetAllv(int IdPurchase)
		{
			List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase == IdPurchase).Where(a => a.IdPurchase == IdPurchase).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBViewPurchase GetByIdview(int IdPurchase)
		{
			TBViewPurchase sslid = dbcontext.ViewPurchase.FirstOrDefault(a => a.IdPurchase == IdPurchase);
			return sslid;
		}

        public List<TBPurchase> GetByPurcheasNu(int purchaseNu)
        {
            List<TBPurchase> sslid = dbcontext.TBPurchases.Where(a => a.PurchaseNumber == purchaseNu).ToList();
            return sslid;
        }

        public List<TBViewPurchase> GetAByDetectedDate(DateTime date)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
				.Where(a => a.PurchaseDate == DateOnly.FromDateTime(date)).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewPurchase> GetABySuplierAndPeriod(string suplierNm, DateTime sdate, DateTime edate)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
                .Where(a => a.SupplierName == suplierNm && a.PurchaseDate >= DateOnly.FromDateTime(sdate) && a.PurchaseDate <= DateOnly.FromDateTime(edate))
				.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewPurchase> GetAByPruductAndPeriod(string productName, DateTime sdate, DateTime edate)
		{
			List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
				.Where(a => a.ItemName == productName && a.PurchaseDate >= DateOnly.FromDateTime(sdate) && a.PurchaseDate <= DateOnly.FromDateTime(edate))
				.Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}

        public List<TBViewPurchase> GetByPeriod(DateTime sdate, DateTime edate)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
                .Where(a => a.PurchaseDate >= DateOnly.FromDateTime(sdate) && a.PurchaseDate <= DateOnly.FromDateTime(edate))
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewPurchase> GetByProduct(string productName)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
                .Where(a => a.ItemName == productName)
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewPurchase> GetBySuplier(string suplierNm)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase.OrderByDescending(n => n.IdPurchase)
                .Where(a => a.SupplierName == suplierNm)
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewPurchase> GetByPurcheasNm(int purNm)
        {
            List<TBViewPurchase> MySlider = dbcontext.ViewPurchase
                .Where(a => a.PurchaseNumber == purNm)
                .Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
