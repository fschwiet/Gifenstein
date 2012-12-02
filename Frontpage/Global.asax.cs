using System.Net.Http.Formatting;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Frontpage.App_Start;
using Newtonsoft.Json;

namespace Frontpage
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
                GetJsonSerializationSettings();
        }

        public static JsonSerializerSettings GetJsonSerializationSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Binder = new SingleAssemblyJsonTypeBinder(typeof(MvcApplication).Assembly);
            settings.TypeNameHandling = TypeNameHandling.Objects;
            return settings;
        }

    }
}