using System.Web.Http.Filters;
using DataAccess;
using System;

namespace Common
{
   public  class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly ITrans _sessionFactory;
        public ActionTransactionHelper(ITrans sessionFactory)
        {
            _sessionFactory = sessionFactory;
            var z = 1;
            var b = 1;
        }
        public bool TransactionHandled { get; private set; }
        public bool SessionClosed { get; private set; }

        public void BeginTransaction()
        {
            //if (!CurrentSessionContext.HasBind(_sessionFactory)) return;
            //var session = _sessionFactory.GetCurrentSession();
            //if (session != null)
            //{
            _sessionFactory.BeginTransaction();
            //}
        }

        public void EndTransaction(HttpActionExecutedContext filterContext)
        {
            //if (!CurrentSessionContext.HasBind(_sessionFactory)) return;
            //var session = _sessionFactory.GetCurrentSession();
            var session = _sessionFactory;
            if (session.Transx == null) return;
            //if (!session.Transaction) return;
            if (!(filterContext.Exception == null))
            {
                session.Transx.Rollback();
            }
            else {

            //session.Flush();
            try
            {
                session.Transx.Commit();
            }
            catch (Exception e)
            {
                session.Transx.Rollback();
              
            }

            }
            TransactionHandled = true;
        }

        public void CloseSession()
        {
            //if (!CurrentSessionContext.HasBind(_sessionFactory)) return;
            //var session = _sessionFactory.GetCurrentSession();
            var session = _sessionFactory;
            session.CloseConnection();
            //session.Dispose();
            //CurrentSessionContext.Unbind(_sessionFactory);
            SessionClosed = true;
        }



    }
}
