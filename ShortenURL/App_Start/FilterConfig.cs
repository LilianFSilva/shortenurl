using ShortenURL.Filters;
using System.Web;
using System.Web.Mvc;

namespace ShortenURL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ShortenErrorFilter());
        }
    }
}
