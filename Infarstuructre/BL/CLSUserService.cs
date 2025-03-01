using Infarstuructre.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
	public interface IIUserService
	{
		List<ApplicationUser> GetActiveCustomers();
	}
   public class CLSUserService: IIUserService
	{
        MasterDbcontext dbcontext;
		public CLSUserService(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;

		}
	

		public List<ApplicationUser> GetActiveCustomers()
		{
			// جلب كافة العملاء النشطين
			var activeCustomers = dbcontext.Users
										  .Where(u => u.IsOnline == true)  // أو استخدم أي شرط آخر يناسب حالتك
										  .ToList();

			return activeCustomers;
		}
	}
}
