using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Capas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
