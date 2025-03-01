

namespace Infarstuructre.BL
{
	public interface IIPhotoHomeSliderContent
	{
		List<TBViewPhotoHomeSliderContent> GetAll();
		TBPhotoHomeSliderContent GetById(int IdPhotoHomeSliderContent);
		bool saveData(TBPhotoHomeSliderContent savee);
		bool UpdateData(TBPhotoHomeSliderContent updatss);
		bool deleteData(int IdPhotoHomeSliderContent);
		List<TBViewPhotoHomeSliderContent> GetAllv(int IdPhotoHomeSliderContent);
		bool DELETPHOTO(int IdPhotoHomeSliderContent);
		bool DELETPHOTOWethError(string PhotoNAme);
		TBViewPhotoHomeSliderContent GetByIdview(int IdPhotoHomeSliderContent);
	}
	public class CLSTBPhotoHomeSliderContent: IIPhotoHomeSliderContent
	{
		MasterDbcontext dbcontext;
		public CLSTBPhotoHomeSliderContent(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;
		}
		public List<TBViewPhotoHomeSliderContent> GetAll()
		{
			List<TBViewPhotoHomeSliderContent> MySlider = dbcontext.ViewPhotoHomeSliderContent.Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBPhotoHomeSliderContent GetById(int IdPhotoHomeSliderContent)
		{
			TBPhotoHomeSliderContent sslid = dbcontext.TBPhotoHomeSliderContents.FirstOrDefault(a => a.IdPhotoHomeSliderContent == IdPhotoHomeSliderContent);
			return sslid;
		}
		public bool saveData(TBPhotoHomeSliderContent savee)
		{
			try
			{
				dbcontext.Add<TBPhotoHomeSliderContent>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBPhotoHomeSliderContent updatss)
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
		public bool deleteData(int IdPhotoHomeSliderContent)
		{
			try
			{
				var catr = GetById(IdPhotoHomeSliderContent);
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
		public List<TBViewPhotoHomeSliderContent> GetAllv(int IdPhotoHomeSliderContent)
		{
			List<TBViewPhotoHomeSliderContent> MySlider = dbcontext.ViewPhotoHomeSliderContent.OrderByDescending(n => n.IdPhotoHomeSliderContent == IdPhotoHomeSliderContent).Where(a => a.IdPhotoHomeSliderContent == IdPhotoHomeSliderContent).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public bool DELETPHOTO(int IdPhotoHomeSliderContent)
		{
			try
			{
				var catr = GetById(IdPhotoHomeSliderContent);
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
		public TBViewPhotoHomeSliderContent GetByIdview(int IdPhotoHomeSliderContent)
		{
			TBViewPhotoHomeSliderContent sslid = dbcontext.ViewPhotoHomeSliderContent.FirstOrDefault(a => a.IdPhotoHomeSliderContent == IdPhotoHomeSliderContent);
			return sslid;
		}
	}
}
