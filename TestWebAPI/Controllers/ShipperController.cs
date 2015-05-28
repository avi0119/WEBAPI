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

    public class ShipperController : GenericContr<Shipper, Shipper, Supplier, Shipper>
    {
        public ShipperController(IGenericCRUD<Shipper> iaddprod)
            : base(iaddprod)
        {

            numberOfGenerics = 1;
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/Shipper
        [HttpGet]
        [Route("api/Shipper")]
        override public IEnumerable<Shipper> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/Shipper/1
        // GET api/<controller>/5
        //[Route("api/Shipper/CategoryID:int")]
        [HttpGet]
        [Route("api/Shipper/{productID:int}")]
        override public Shipper Get(int productID)
        {


            return base.Get(productID);
        }
        ////http://localhost:39402/api/Shipper/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Shipper/{productID:int}")]
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
        ////http://localhost:39402/api/Shipper/1
        [HttpPost]
        // POST api/<controller>

        [Route("api/Shipper")]
        override public Shipper Post([FromBody]Shipper value)
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



        ////http://localhost:39402/api/Shipper/1
        [HttpPut]
        [Route("api/Shipper")]
        // PUT api/<controller>/5
        override public Shipper Put([FromBody]Shipper value)
        {
            return base.Put(value);

        }
        #endregion //REST
    }
}