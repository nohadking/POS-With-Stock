using Infarstuructre.BL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ServiceExtensions
{
	public static void AddCustomServices(this IServiceCollection services)
	{
		services.AddScoped<IICompanyInformation, CLSTBCompanyInformation>();
	}
}

