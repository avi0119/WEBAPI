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
    [UnitOfWorkActionFilter]
    public class ProductController : ApiController
    {
        //IProduct _IProduct;
        IProductCRUD _iaddprod;
        // GET api/<controller>

        public ProductController(IProductCRUD iaddprod)
        {
            //_IProduct=iproduct;
            _iaddprod = iaddprod;
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        ////http://localhost:39402/api/product/77
        // GET api/<controller>/5
        public Product Get(int productID)
        {
            var z = _iaddprod.GetProduct(productID);
            return z;
            //return "value";
        }


        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        public bool Delete(int productID)
        {
            var p = _iaddprod.GetProduct(productID);
            var res=_iaddprod.ProductDelete(p);
            return res;
            //return "value";
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
        // POST api/<controller>

        [Route("api/product")]
        public Product Post( [FromBody]Product value)
        {
            //var value = 1;
             var id=_iaddprod.ProductAdd(value);
             var p = _iaddprod.GetProduct(id);
            var b = value;
            return p;
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
         
         */



        ////http://localhost:39402/api/product/77
        [HttpPut]
        [Route("api/product")]
        // PUT api/<controller>/5
        public Product Put([FromBody]Product value)
        {
            var id = _iaddprod.ProductUpdate(value);
            var p = _iaddprod.GetProduct(id);
 
            return p;
        }

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}