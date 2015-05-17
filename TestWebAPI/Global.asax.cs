using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Http;
namespace TestWebAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // new AutoMapperConfigurator().Configure(
            //    WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
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
            routeTemplate: "api/{controller}/{productID}",
            defaults: new { taskNum = System.Web.Http.RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"

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