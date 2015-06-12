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

    public class ClaimTypeController : GenericContr<int, DBClaimType, DBClaimType, Supplier, DBClaimType>
    {
        public ClaimTypeController(IGenericCRUD<DBClaimType, int> iaddprod)
            : base(iaddprod)
        {

            numberOfGenerics = 1;
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/ClaimType
        [HttpGet]
        [Route("api/ClaimType")]
        override public IEnumerable<DBClaimType> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/ClaimType/1
        // GET api/<controller>/5
        //[Route("api/ClaimType/ClaimTypeID:int")]
        [HttpGet]
        [Route("api/ClaimType/{productID:int}")]
        override public DBClaimType Get(int productID)
        {


            return base.Get(productID);
        }
        ////http://localhost:39402/api/ClaimType/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/ClaimType/{productID:int}")]
        override public bool Delete(int productID)
        {

            return base.Delete(productID);
        }

        /*
{,
   
"ProductName":"my new product",  
"SupplierID":5,     
"DBClaimTypeID":1,     
"QuantityPerUnit":"12 boxes",
"UnitPrice":12,     
"UnitsInStock":55,
"UnitsOnOrde"r:3,
"ReorderLevel":2,
"Discontinued":0
 
        
 }
        
         */
        ////http://localhost:39402/api/ClaimType/1
        [HttpPost]
        // POST api/<controller>

        [Route("api/ClaimType")]
        override public DBClaimType Post([FromBody]DBClaimType value)
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



        ////http://localhost:39402/api/ClaimType/1
        [HttpPut]
        [Route("api/ClaimType")]
        // PUT api/<controller>/5
        override public DBClaimType Put([FromBody]DBClaimType value)
        {
            return base.Put(value);

        }
        #endregion //REST
    }
}