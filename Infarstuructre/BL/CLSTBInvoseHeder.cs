

namespace Infarstuructre.BL
{
	public interface IIInvoseHeder
	{
		List<TBViewInvoseHeder> GetAll();
		TBInvoseHeder GetById(int IdInvoseHeder);
		bool saveData(TBInvoseHeder savee);
		bool UpdateData(TBInvoseHeder updatss);
		bool deleteData(int IdInvoseHeder);
		List<TBViewInvoseHeder> GetAllv(int IdInvoseHeder);
		TBViewInvoseHeder GetByIdview(string IdUser);
	}
	public class CLSTBInvoseHeder: IIInvoseHeder
	{
		MasterDbcontext dbcontext;
		public CLSTBInvoseHeder(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;

		}
		public List<TBViewInvoseHeder> GetAll()
		{
			List<TBViewInvoseHeder> MySlider = dbcontext.ViewInvoseHeder.OrderByDescending(n => n.IdInvoseHeder).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBInvoseHeder GetById(int IdInvoseHeder)
		{
			TBInvoseHeder sslid = dbcontext.TBInvoseHeders.FirstOrDefault(a => a.IdInvoseHeder == IdInvoseHeder);
			return sslid;
		}
		public bool saveData(TBInvoseHeder savee)
		{
			try
			{
				dbcontext.Add<TBInvoseHeder>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBInvoseHeder updatss)
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
		public bool deleteData(int IdInvoseHeder)
		{
			try
			{
				var catr = GetById(IdInvoseHeder);
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
		public List<TBViewInvoseHeder> GetAllv(int IdInvoseHeder)
		{
			List<TBViewInvoseHeder> MySlider = dbcontext.ViewInvoseHeder.OrderByDescending(n => n.IdInvoseHeder == IdInvoseHeder).Where(a => a.IdInvoseHeder == IdInvoseHeder).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBViewInvoseHeder GetByIdview(string IdUser)
		{
			TBViewInvoseHeder sslid = dbcontext.ViewInvoseHeder.FirstOrDefault(a => a.IdUser == IdUser);
			return sslid;
		}
	}
}
