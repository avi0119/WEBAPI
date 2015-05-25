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
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/product
        [HttpGet]
        [Route("api/Product")]
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
        override public bool Delete(int productID)
        {

            return base.Delete(productID);
        }

        /*
{,
   
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
        // POST api/<controller>

        [Route("api/product")]
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
        [Route("api/product")]
        // PUT api/<controller>/5
        override public Product Put([FromBody]Product value)
        {
            return base.Put(value);

        }
        #endregion //REST
    }
}