using System.Web;
using System.Web.Http;


namespace TestWebAPI22
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           // new AutoMapperConfigurator().Configure(
            //    WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            if (exception != null)
            {
             //   var log = WebContainerManager.Get<ILogManager>().GetLog(typeof(WebApiApplication));
              //  log.Error("Unhandled exception.", exception);
            }
        }
    }


     public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enables attribute-based routing
            config.MapHttpAttributeRoutes();
            // Matches route with the taskNum parameter
            config.Routes.MapHttpRoute(
            name: "FindByTaskNumberRoute",
            routeTemplate: "api/{controller}/{producID}",
            defaults: new { taskNum = RouteParameter.Optional }
            );
            //var constraintsResolver = new DefaultInlineConstraintResolver();
            //constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof
            //(ApiVersionConstraint));
            //config.MapHttpAttributeRoutes(constraintsResolver);
            //config.Services.Replace(typeof(IHttpControllerSelector),
            //new NamespaceHttpControllerSelector(config));

            //config.Services.Replace(typeof(ITraceWriter),
            //new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));

            //config.Services.Add(typeof(IExceptionLogger), new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));

            //config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

        }
    }
}
