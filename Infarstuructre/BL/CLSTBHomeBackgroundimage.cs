

namespace Infarstuructre.BL
{
	public interface IIHomeBackgroundimage
	{
		List<TBHomeBackgroundimage> GetAll();
		TBHomeBackgroundimage GetById(int IdHomeBackgroundimage);
		bool saveData(TBHomeBackgroundimage savee);
		bool UpdateData(TBHomeBackgroundimage updatss);
		bool deleteData(int IdHomeBackgroundimage);
		bool DELETPHOTO(int IdHomeBackgroundimage);
		bool DELETPHOTOWethError(string PhotoNAme);
		List<TBHomeBackgroundimage> GetAllv(int IdHomeBackgroundimage);
	}
	public class CLSTBHomeBackgroundimage: IIHomeBackgroundimage
	{
		MasterDbcontext dbcontext;
		public CLSTBHomeBackgroundimage(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;

		}
		public List<TBHomeBackgroundimage> GetAll()
		{
			List<TBHomeBackgroundimage> MySlider = dbcontext.TBHomeBackgroundimages.OrderByDescending(n => n.IdHomeBackgroundimage).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBHomeBackgroundimage GetById(int IdHomeBackgroundimage)
		{
			TBHomeBackgroundimage sslid = dbcontext.TBHomeBackgroundimages.FirstOrDefault(a => a.IdHomeBackgroundimage == IdHomeBackgroundimage);
			return sslid;
		}
		public bool saveData(TBHomeBackgroundimage savee)
		{
			try
			{
				dbcontext.Add<TBHomeBackgroundimage>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBHomeBackgroundimage updatss)
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
		public bool deleteData(int IdHomeBackgroundimage)
		{
			try
			{
				var catr = GetById(IdHomeBackgroundimage);
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
		public List<TBHomeBackgroundimage> GetAllv(int IdHomeBackgroundimage)
		{
			List<TBHomeBackgroundimage> MySlider = dbcontext.TBHomeBackgroundimages.OrderByDescending(n => n.IdHomeBackgroundimage == IdHomeBackgroundimage).Where(a => a.IdHomeBackgroundimage == IdHomeBackgroundimage).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public bool DELETPHOTO(int IdHomeBackgroundimage)
		{
			try
			{
				var catr = GetById(IdHomeBackgroundimage);
				//using (FileStream fs = new FileStream(catr.Photo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				//{
				if (!string.IsNullOrEmpty(catr.Photo))
				{
					// إذا كان هناك صورة قديمة، قم بمسحها من الملف
					var oldFilePath = Path.Combine(@"wwwroot/Images/Home", catr.Photo);
					if (System.IO.File.Exists(oldFilePath))
					{


						// استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
						using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
						{
							System.Threading.Thread.Sleep(200);
							GC.Collect();
							GC.WaitForPendingFinalizers();
						}

						System.IO.File.Delete(oldFilePath);
					}
				}
				//}
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
		public bool DELETPHOTOWethError(string PhotoNAme)
		{
			try
			{
				if (!string.IsNullOrEmpty(PhotoNAme))
				{
					// إذا كان هناك صورة قديمة، قم بمسحها من الملف
					var oldFilePath = Path.Combine(@"wwwroot/Images/Home", PhotoNAme);
					if (System.IO.File.Exists(oldFilePath))
					{


						// استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
						using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
						{
							System.Threading.Thread.Sleep(200);
							GC.Collect();
							GC.WaitForPendingFinalizers();
						}

						System.IO.File.Delete(oldFilePath);
					}
				}

				return true;
			}
			catch (Exception)
			{
				// يفضل ألا تترك البرنامج يتجاوز الأخطاء بصمت، يفضل تسجيل الخطأ أو إعادة رميه
				return false;
			}
		}
	}
}
