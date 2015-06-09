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
            container.Bind<IGenericCRUD<Product, int>>().To<GemericCRUD<Product, int>>().InTransientScope();
            container.Bind<IGenericCRUD<Category, int>>().To<GemericCRUD<Category, int>>().InTransientScope();
            container.Bind<IGenericCRUD<Employee, int>>().To<GemericCRUD<Employee, int>>().InTransientScope();
            container.Bind<IGenericCRUD<Supplier, int>>().To<GemericCRUD<Supplier, int>>().InTransientScope();
            container.Bind<IGenericCRUD<User, int>>().To<GemericCRUD<User, int>>().InTransientScope();

           
            container.Bind<IEnumerable<Customer>>().To<IEnumerable<Customer>>().InTransientScope();

            container.Bind<IGenericCRUD<Shipper, int>>().To<GemericCRUD<Shipper, int>>().InTransientScope();

            container.Bind<IGenericCRUD<Customer, int>>().To<GemericCRUD<Customer, int>>().InTransientScope();
            container.Bind<IGenericCRUD<OrderDetail, int>>().To<GemericCRUD<OrderDetail, int>>().InTransientScope();
            container.Bind<IGenericCRUD<Order, int>>().To<GemericCRUD<Order, int>>().InTransientScope();

            container.Bind<IBasicSecurityService>().To<BasicSecurityService>().InRequestScope();
            

            //IGenericCRUD<T> iaddprod = (IGenericCRUD<T>)(GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenericCRUD<T>)));

            container.Bind<IGenericController<int,Product, Category, Supplier, Customer>>().To<GenericContr<int,Product, Category, Supplier, Customer>>().InRequestScope();

            //GenericContr<Product, Category, Supplier, Customer>
            


        }
    }
}