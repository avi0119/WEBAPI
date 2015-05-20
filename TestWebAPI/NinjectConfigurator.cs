using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Ninject.Web.Common;
using Ninject;
using DataAccess;


namespace TestWebAPI
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }
        private void AddBindings(IKernel container)
        {
            //ConfigureLog4net(container);
            container.Bind<Common.IDateTime>().To<DateTimeAdapter>().InSingletonScope();
            container.Bind<ITrans>().To<Transaction>().InScope(c => System.Web.HttpContext.Current);
            //container.Bind<ITrans>().To<Transaction>().InTransientScope();
            //container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InScope(c => System.Web.HttpContext.Current);
            //container.Bind<IProduct>().To<ProductGetter>().InTransientScope();
            //container.Bind<IProductCRUD>().To<ProductCRUD>().InTransientScope();
            container.Bind<IGenericCRUD<Product>>().To<GemericCRUD<Product>>().InTransientScope();
            container.Bind<IGenericCRUD<Order>>().To<GemericCRUD<Order>>().InTransientScope();
            container.Bind<IEnumerable<OrderDetail>>().To<IEnumerable<OrderDetail>>().InTransientScope();
            


        }
    }
}