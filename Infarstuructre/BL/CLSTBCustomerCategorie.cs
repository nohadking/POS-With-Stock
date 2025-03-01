

namespace Infarstuructre.BL
{
	public interface IICustomerCategorie
	{
		List<TBCustomerCategorie> GetAll();
		TBCustomerCategorie GetById(int IdCustomerCategorie);
		bool saveData(TBCustomerCategorie savee);
		bool UpdateData(TBCustomerCategorie updatss);
		bool deleteData(int IdCustomerCategorie);
		List<TBCustomerCategorie> GetAllv(int IdCustomerCategorie);
	}

	public class CLSTBCustomerCategorie: IICustomerCategorie
	{
		MasterDbcontext dbcontext;
		public CLSTBCustomerCategorie(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;

		}
		public List<TBCustomerCategorie> GetAll()
		{
			List<TBCustomerCategorie> MySlider = dbcontext.TBCustomerCategories.OrderByDescending(n => n.IdCustomerCategorie).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBCustomerCategorie GetById(int IdCustomerCategorie)
		{
			TBCustomerCategorie sslid = dbcontext.TBCustomerCategories.FirstOrDefault(a => a.IdCustomerCategorie == IdCustomerCategorie);
			return sslid;
		}
		public bool saveData(TBCustomerCategorie savee)
		{
			try
			{
				dbcontext.Add<TBCustomerCategorie>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBCustomerCategorie updatss)
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
		public bool deleteData(int IdCustomerCategorie)
		{
			try
			{
				var catr = GetById(IdCustomerCategorie);
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
		public List<TBCustomerCategorie> GetAllv(int IdCustomerCategorie)
		{
			List<TBCustomerCategorie> MySlider = dbcontext.TBCustomerCategories.OrderByDescending(n => n.IdCustomerCategorie == IdCustomerCategorie).Where(a => a.IdCustomerCategorie == IdCustomerCategorie).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
	}
}
