using System.Web;
using System.Web.Mvc;

namespace NorgesEnergi_wep_app
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
