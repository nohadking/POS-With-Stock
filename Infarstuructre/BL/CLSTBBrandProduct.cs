
namespace Infarstuructre.BL
{
	public interface IIBrandProduct
	{
		List<TBBrandProduct> GetAll();
		TBBrandProduct GetById(int IdBrandProduct);
		bool saveData(TBBrandProduct savee);
		bool UpdateData(TBBrandProduct updatss);
		bool deleteData(int IdBrandProduct);
		List<TBBrandProduct> GetAllv(int IdBrandProduct);
		bool DELETPHOTO(int IdBrandProduct);
		bool DELETPHOTOWethError(string PhotoNAme);
	}
	public class CLSTBBrandProduct: IIBrandProduct
	{
        MasterDbcontext dbcontext;
		public CLSTBBrandProduct(MasterDbcontext dbcontaxt1)
        {
			dbcontext= dbcontaxt1;

		}

		public List<TBBrandProduct> GetAll()
		{
			List<TBBrandProduct> MySlider = dbcontext.TBBrandProducts.OrderByDescending(n => n.IdBrandProduct).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBBrandProduct GetById(int IdBrandProduct)
		{
			TBBrandProduct sslid = dbcontext.TBBrandProducts.FirstOrDefault(a => a.IdBrandProduct == IdBrandProduct);
			return sslid;
		}
		public bool saveData(TBBrandProduct savee)
		{
			try
			{
				dbcontext.Add<TBBrandProduct>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBBrandProduct updatss)
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
		public bool deleteData(int IdBrandProduct)
		{
			try
			{
				var catr = GetById(IdBrandProduct);
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
		public List<TBBrandProduct> GetAllv(int IdBrandProduct)
		{
			List<TBBrandProduct> MySlider = dbcontext.TBBrandProducts.OrderByDescending(n => n.IdBrandProduct == IdBrandProduct).Where(a => a.IdBrandProduct == IdBrandProduct).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public bool DELETPHOTO(int IdBrandProduct)
		{
			try
			{
				var catr = GetById(IdBrandProduct);
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
