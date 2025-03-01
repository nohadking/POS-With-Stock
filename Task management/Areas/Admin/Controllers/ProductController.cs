
namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
	{
		MasterDbcontext dbcontext;
		IICategory category;
		IIProduct iproduct;
		IICompanyInformation iCompanyInformation;
		public ProductController(MasterDbcontext dbcontext1, IICategory iCategory1, IIProduct iProduct1, IICompanyInformation iCompanyInformation1)
        {
			dbcontext=dbcontext1;
			category = iCategory1;
			iproduct = iProduct1;
			iCompanyInformation = iCompanyInformation1;
		}
		public IActionResult MYProduct()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewProduct = iproduct.GetAll();
            ViewBag.category = category.GetAll();
            return View(vmodel);
		}
		public IActionResult AddEditProduct(int? IdProduct)
		{
			ViewBag.category= category.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewProduct = iproduct.GetAll();
			if (IdProduct != null)
			{
				vmodel.Product = iproduct.GetById(Convert.ToInt32(IdProduct));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		public IActionResult AddEditProductImage(int? IdProduct)
		{
			ViewBag.category = category.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewProduct = iproduct.GetAll();
			if (IdProduct != null)
			{
				vmodel.Product = iproduct.GetById(Convert.ToInt32(IdProduct));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProduct slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				// تأكد من أن الخاصية Product تحتوي على القيم
				if (model.Product == null)
				{
					TempData["ErrorSave"] = "بيانات الفئة غير صحيحة.";
					return RedirectToAction("AddEditProduct");
				}
				// نسخ القيم من النموذج إلى الكائن slider
				slider.IdProduct = model.Product.IdProduct;
				slider.IdCategory = model.Product.IdCategory;
				slider.Photo = model.Product.Photo;
				slider.ProductNameAr = model.Product.ProductNameAr;
				slider.ProductNameEn = model.Product.ProductNameEn;
				slider.DescriptionAr = model.Product.DescriptionAr;
				slider.DescriptionEn = model.Product.DescriptionEn;
				slider.price = model.Product.price;
				slider.DateTimeEntry = model.Product.DateTimeEntry;
				slider.DataEntry = model.Product.DataEntry;
				slider.CurrentState = model.Product.CurrentState;
				slider.Active = model.Product.Active;
				var file = HttpContext.Request.Form.Files;
				if (slider.IdProduct == 0 || slider.IdProduct == null)
				{
					if (file.Count() > 0)
					{
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
					}
					else
					{
						TempData["Message"] = ResourceWeb.VLimageuplode;
						return Redirect(returnUrl);
					}
					// تحقق من تكرار اسم الفئة
					if (dbcontext.TBProducts.Where(a => a.ProductNameEn == slider.ProductNameEn).ToList().Count > 0)
					{
						TempData["ProductsEn"] = ResourceWeb.VLProductsEnDoplceted;
						return RedirectToAction("MYProduct");
					}

					if (dbcontext.TBProducts.Where(a => a.ProductNameAr == slider.ProductNameAr).ToList().Count > 0)
					{
						TempData["ProductsAr"] = ResourceWeb.VLProductsArDoplceted;
						return RedirectToAction("MYProduct");
					}
					// حفظ البيانات
					var reqwest = iproduct.saveData(slider);
					if (reqwest)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MYProduct");
					}
					else
					{
						var PhotoNAme = slider.Photo;
						var delet = iproduct.DELETPHOTOWethError(PhotoNAme);
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddEditProduct");
					}
				}
				else
				{
					// تحديث البيانات
					if (file.Count() == 0)
					{
						slider.Photo = model.Product.Photo;
						var reqestUpdate2 = iproduct.UpdateData(slider);
						if (reqestUpdate2)
						{
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYProduct");
						}
						else
						{
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("AddEditProduct", model);
						}
					}
					else
					{
						string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
						var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
						file[0].CopyTo(fileStream);
						slider.Photo = Photo;
						fileStream.Close();
						// حذف الصورة القديمة إذا لزم الأمر
						var reqweistDeletPoto = iproduct.DELETPHOTO(slider.IdProduct);
						var reqestUpdate2 = iproduct.UpdateData(slider);
						if (reqestUpdate2)
						{
							TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
							return RedirectToAction("MYProduct");
						}
						else
						{
							var PhotoNAme = slider.Photo;
							var delet = iproduct.DELETPHOTOWethError(PhotoNAme);
							TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
							return RedirectToAction("MYProduct");
						}
					}
				}
			}
			catch (Exception)
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("AddEditProduct", model);
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdProduct)
		{
			var reqwistDelete = iproduct.deleteData(IdProduct);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MYProduct");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MYProduct");
			}
		}
	
	}
}