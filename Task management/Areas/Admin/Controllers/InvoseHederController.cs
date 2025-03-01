

using Org.BouncyCastle.Crypto;
using System.ComponentModel;

namespace Task_management.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class InvoseHederController : Controller
	{
		MasterDbcontext dbcontext;
		IIInvoseHeder iInvoseHeder;
		IIUserInformation iUserInformation;
		IICustomerCategorie iCustomerCategorie;
		IICompanyInformation iCompanyInformation;
		public InvoseHederController(MasterDbcontext dbcontext1,IIInvoseHeder iInvoseHeder1, IIUserInformation iUserInformation1,IICustomerCategorie iCustomerCategorie1, IICompanyInformation iCompanyInformation1)
        {
			dbcontext=dbcontext1;
			iInvoseHeder= iInvoseHeder1;
			iUserInformation = iUserInformation1;
			iCustomerCategorie = iCustomerCategorie1;
			iCompanyInformation = iCompanyInformation1;
		}
		public IActionResult MyInvoseHeder()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewInvoseHede = iInvoseHeder.GetAll();
			return View(vmodel);
		}
		public IActionResult AddInvoseHeder(int? IdInvoseHeder)
		{
			ViewBag.user = iUserInformation.GetAllByRole("Customer");
	
			ViewBag.CustomerCategorie = iCustomerCategorie.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListCompanyInformation = iCompanyInformation.GetAll().Take(1).ToList();
			vmodel.ListViewInvoseHede = iInvoseHeder.GetAll();
			if (IdInvoseHeder != null)
			{
				vmodel.InvoseHeder = iInvoseHeder.GetById(Convert.ToInt32(IdInvoseHeder));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBInvoseHeder slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdInvoseHeder = model.InvoseHeder.IdInvoseHeder;		
				slider.IdUser = model.InvoseHeder.IdUser;			
				slider.DateInvos = model.InvoseHeder.DateInvos;
				slider.InvoiceNumber = model.InvoseHeder.InvoiceNumber;	
				slider.OutstandingBill = model.InvoseHeder.OutstandingBill;
				slider.DataEntry = model.InvoseHeder.DataEntry;
				slider.DateTimeEntry = model.InvoseHeder.DateTimeEntry;
				slider.CurrentState = model.InvoseHeder.CurrentState;
				if (slider.IdInvoseHeder == 0 || slider.IdInvoseHeder == null)
				{
					if (dbcontext.TBInvoseHeders.Where(a => a.InvoiceNumber == slider.InvoiceNumber).ToList().Count > 0)
					{
						TempData["InvoiceNumber"] = ResourceWeb.VLInvoiceNumberDoplceted;
						return RedirectToAction("AddInvoseHeder", model);
					}							
					var reqwest = iInvoseHeder.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyInvoseHeder");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddInvoseHeder");
					}
				}
				else
				{
					var reqestUpdate = iInvoseHeder.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyInvoseHeder");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddInvoseHeder");
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("AddInvoseHeder");
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdInvoseHeder)
		{
			var reqwistDelete = iInvoseHeder.deleteData(IdInvoseHeder);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyInvoseHeder");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyInvoseHeder");
			}
		}
	}
}
