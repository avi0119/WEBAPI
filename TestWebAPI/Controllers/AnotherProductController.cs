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

    public class ProductController : GenericContr<int,Product, Category, Supplier, Customer>
    {

        public ProductController(IGenericCRUD<Product, int> iaddprod)
            : base(iaddprod)
        {
            
            numberOfGenerics = 3;
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/product
        [HttpGet]
        [Route("api/Product")]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        override public IEnumerable<Product> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/product/77
        // GET api/<controller>/5
        //[Route("api/product/productID:int")]
        [HttpGet]
        [Route("api/Product/{productID:int}")]
        override public Product Get(int productID)
        {
            return base.Get(productID);
        }
        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Product/{productID:int}")]
        override public bool Delete(int productID)
        {

            return base.Delete(productID);
        }

        /*
{
   
"ProductName":"my new product",  
"SupplierID":5,     
"CategoryID":1,     
"QuantityPerUnit":"12 boxes",
"UnitPrice":12,     
"UnitsInStock":55,
"UnitsOnOrde"r:3,
"ReorderLevel":2,
"Discontinued":0
 
        
 }
        
    
                 */
        ////http://localhost:39402/api/product/77
        [HttpPost]
        [Route("api/product/{ProductID:int}")]
        //[Route("api/product/{value:Product}")]
        override public Product Post(int ProductID, [FromBody]Product value)
        {
            value.ProductID = ProductID;
            return base.Put(value);
        }


        /*
{
"Productid":80,    
"ProductName":"my updated prod", 
"SupplierID":5,     
"CategoryID":1,     
"QuantityPerUnit":"12 boxes",
"UnitPrice":12,     
"UnitsInStock":55,
"UnitsOnOrde":3,
"ReorderLevel":2,
"Discontinued":0
        
 }
         * do not for get to put in the header the following:
          Content-Type: application/json; charset=utf-8
        
         */
                  
        ////http://localhost:39402/api/product
        [HttpPost]
        [Route("api/product/")]
        //[Route("api/product/{value:Product}")]
        override public Product Post([FromBody]Product value)
        {
            return base.Post(value);
        }


        /*
{
"Productid":80,    
"ProductName":"my updated prod", 
"SupplierID":5,     
"CategoryID":1,     
"QuantityPerUnit":"12 boxes",
"UnitPrice":12,     
"UnitsInStock":55,
"UnitsOnOrde":3,
"ReorderLevel":2,
"Discontinued":0
        
 }
         * do not for get to put in the header the following:
          Content-Type: application/json; charset=utf-8
        
         */

                 
        ////http://localhost:39402/api/product/77
        [HttpPut]
        [Route("api/product/{ProductID:int}")]
        //[Route("api/product/{value:Product}")]
        override public Product Put(int ProductID, [FromBody]Product value)
        {
            return base.Put(value);
        }


        ////http://localhost:39402/api/product/77
        [HttpPut]
        [Route("api/product")]
        // PUT api/<controller>/5
        override public Product Put([FromBody]Product value)
        {
            return base.Put(value);

        }

        //[HttpPost]
        //[Route("api/product/x")]
        //override public void Get([FromBody]IEnumerable<Product> productID)
        //{
        //    var g = 6;
        //    var t = g;
        //}
        #endregion //REST
    }
}