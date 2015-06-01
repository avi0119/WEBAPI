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

    public class CustomerController : GenericContr<int,Customer, Category, Supplier, Customer>
    {

        public CustomerController(IGenericCRUD<Customer, int> iaddprod)
            : base(iaddprod)
        {

            numberOfGenerics = 1;
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/customer
        [HttpGet]
        [Route("api/customer")]
        override public IEnumerable<Customer> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/customer/1
        // GET api/<controller>/5
        //[Route("api/customer/customerID:int")]
        [HttpGet]
        [Route("api/customer/{CustomerID:int}")]
        override public Customer Get(int CustomerID)
        {
            return base.Get(CustomerID);
            //string myString = CustomerID.ToString();
            //return Get(myString);
        }

        [HttpGet]
        [Route("api/customer/{CustomerID}")]
        public Customer Get(string CustomerID)
        {
            //return base.Get(CustomerID);
            var s= 5;
            var d = s;
            return new Customer();

        }

        ////http://localhost:39402/api/customer/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/customer/{CustomerID:int}")]
        override public bool Delete(int CustomerID)
        {

            return base.Delete(CustomerID);
        }

        /*
{
   
"CustomerName":"my new Customer",  
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
        ////http://localhost:39402/api/customer
        [HttpPost]
        [Route("api/customer/")]
        //[Route("api/customer/{value:Customer}")]
        override public Customer Post([FromBody]Customer value)
        {
            return base.Post(value);
        }


        /*
{
"Customerid":80,    
"CustomerName":"my updated prod", 
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



        ////http://localhost:39402/api/customer/77
        [HttpPut]
        [Route("api/customer")]
        // PUT api/<controller>/5
        override public Customer Put([FromBody]Customer value)
        {
            return base.Put(value);

        }

        //[HttpPost]
        //[Route("api/customer/x")]
        //override public void Get([FromBody]IEnumerable<Customer> CustomerID)
        //{
        //    var g = 6;
        //    var t = g;
        //}
        #endregion //REST
    }
}