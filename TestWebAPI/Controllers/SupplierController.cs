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

    public class SupplierController : GenericContr<Supplier, Employee, Supplier, Supplier>
    {

        public SupplierController()
        {
            numberOfGenerics = 1;
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/Supplier
        [HttpGet]
        [Route("api/Supplier")]
        override public IEnumerable<Supplier> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/Supplier/1
        // GET api/<controller>/5
        //[Route("api/Supplier/SupplierID:int")]
        [HttpGet]
        [Route("api/Supplier/{productID:int}")]
        override public Supplier Get(int productID)
        {
            return base.Get(productID);
        }
        ////http://localhost:39402/api/Supplier/1
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Supplier/{productID:int}")]
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
        ////http://localhost:39402/api/Supplier/1
        [HttpPost]
        // POST api/<controller>

        [Route("api/Supplier")]
        override public Supplier Post([FromBody]Supplier value)
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



        ////http://localhost:39402/api/Supplier/1
        [HttpPut]
        [Route("api/Supplier")]
        // PUT api/<controller>/5
        override public Supplier Put([FromBody]Supplier value)
        {
            return base.Put(value);

        }
        #endregion //REST
    }
}