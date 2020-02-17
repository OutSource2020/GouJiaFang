using System.Web;
using System.Web.Mvc;
using web1.Filters;

namespace web1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SignatureFilter());
            filters.Add(new MyExceptionFilter());
            // filters.Add(new HandleErrorAttribute());
        }
    }
}
