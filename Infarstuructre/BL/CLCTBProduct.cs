

namespace Infarstuructre.BL
{
	public interface IIProduct
	{
		List<TBViewProduct> GetAll();
		TBProduct GetById(int IdProduct);
		bool saveData(TBProduct savee);
		bool UpdateData(TBProduct updatss);
		bool deleteData(int IdProduct);
		List<TBViewProduct> GetAllv(int IdCategory);
		bool DELETPHOTO(int IdProduct);
		bool DELETPHOTOWethError(string PhotoNAme);
		TBViewProduct GetByIdview(int IdProduct);
		List<TBViewProduct> GetByIdviewl(int IdProduct);

    }
	public class CLCTBProduct: IIProduct
	{
		MasterDbcontext dbcontext;
		public CLCTBProduct(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;
		}
		public List<TBViewProduct> GetAll()
		{
			List<TBViewProduct> MySlider = dbcontext.ViewProduct.OrderByDescending(n => n.IdProduct).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBProduct GetById(int IdProduct)
		{
			TBProduct sslid = dbcontext.TBProducts.FirstOrDefault(a => a.IdProduct == IdProduct);
			return sslid;
		}
		public bool saveData(TBProduct savee)
		{
			try
			{
				dbcontext.Add<TBProduct>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBProduct updatss)
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
		public bool deleteData(int IdProduct)
		{
			try
			{
				var catr = GetById(IdProduct);
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
		public List<TBViewProduct> GetAllv(int IdCategory)
		{
			List<TBViewProduct> MySlider = dbcontext.ViewProduct.OrderByDescending(n => n.IdCategory == IdCategory).Where(a => a.IdCategory == IdCategory).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
 
        public bool DELETPHOTO(int IdProduct)
		{
			try
			{
				var catr = GetById(IdProduct);
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
		public TBViewProduct GetByIdview(int IdProduct)
		{
			TBViewProduct sslid = dbcontext.ViewProduct.FirstOrDefault(a => a.IdProduct == IdProduct);
			return sslid;
		}
		public List<TBViewProduct> GetByIdviewl(int IdProduct)
		{
            List<TBViewProduct> sslid = dbcontext.ViewProduct.OrderByDescending(a => a.IdProduct == IdProduct).ToList();
			return sslid;
		}
	}
}
