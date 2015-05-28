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
            container.Bind<IGenericCRUD<Category>>().To<GemericCRUD<Category>>().InTransientScope();
            container.Bind<IGenericCRUD<Employee>>().To<GemericCRUD<Employee>>().InTransientScope();
            container.Bind<IGenericCRUD<Supplier>>().To<GemericCRUD<Supplier>>().InTransientScope();
            container.Bind<IGenericCRUD<Order>>().To<GemericCRUD<Order>>().InTransientScope();
            container.Bind<IEnumerable<OrderDetail>>().To<IEnumerable<OrderDetail>>().InTransientScope();
            container.Bind<IEnumerable<OrderDetail>>().To<IEnumerable<OrderDetail>>().InTransientScope();
            container.Bind<IGenericCRUD<Shipper>>().To<GemericCRUD<Shipper>>().InTransientScope();
            


            //IGenericCRUD<T> iaddprod = (IGenericCRUD<T>)(GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenericCRUD<T>)));

            container.Bind<IGenericController<Product, Category, Supplier, Customer>>().To<GenericContr<Product, Category, Supplier, Customer>>().InRequestScope();

            //GenericContr<Product, Category, Supplier, Customer>
            


        }
    }
}