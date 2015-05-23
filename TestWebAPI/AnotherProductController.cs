using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using DataAccess;
namespace TestWebAPI
{

    public class ProductController : GenericContr<Product, Category, Supplier, Customer>
    {
        //public ProductController()
        //{
        //    var z = 6;
        //    var e = z;
        //}
    }
}