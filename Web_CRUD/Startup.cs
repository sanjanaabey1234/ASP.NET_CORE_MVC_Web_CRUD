using OfficeOpenXml;
using Web_CRUD.Models;

namespace Web_CRUD
{
    

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Set the EPPlus License Context
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // or LicenseContext.NonCommercial based on your license

            // Other service configurations
            services.AddControllersWithViews();
        }
    }
}
