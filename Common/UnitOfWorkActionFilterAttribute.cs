using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace Common
{
    public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
    {
        public static int counter=0;
        public int id;
        public UnitOfWorkActionFilterAttribute():base()
        {
            counter = counter + 1;
            id = counter;
            var z = 1;
            var k = z;
        }
        public virtual IActionTransactionHelper ActionTransactionHelper
        {

            get
            {
                //var a = WebContainerManager.Get<IActionTransactionHelper>();
                var b = WebContainerManager.Get<IActionTransactionHelper>();
                return b;
            }

        }
        public override bool AllowMultiple
        {
            get { return false; }
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.Headers.Add("myheader","avi");
            ActionTransactionHelper.BeginTransaction();
           
           
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ActionTransactionHelper.EndTransaction(actionExecutedContext);
            ActionTransactionHelper.CloseSession();
        }
    }
}
