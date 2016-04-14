#region Using

using SmartAdminMvc.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace SmartAdminMvc
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof (HomeController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            },namespaces).RouteHandler = new DashRouteHandler();
            
        }
    }
}