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

    public class EmployeeController : GenericContr<int,Employee, Employee, Supplier, Employee>
    {

        public EmployeeController(IGenericCRUD<Employee, int> iaddprod)
            : base(iaddprod)
        {
            
            numberOfGenerics = 2;
            this.adjuster = new Action<string[], string[], string[], string[]>((tables, left, right, jointype) => { left[1] = "ReportsTo"; jointype[1] = "left outer join"; });
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/Employee
        [HttpGet]
        [Route("api/Employee")]
        override public IEnumerable<Employee> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/Employee/1
        // GET api/<controller>/5
        //[Route("api/Employee/EmployeeID:int")]
        [HttpGet]
        [Route("api/Employee/{productID:int}")]
        override public Employee Get(int productID)
        {
            //classMetaData[typeof(Employee)].primaryKeyLeft = "ReportsTo";
            
            return base.Get(productID);
        }
        ////http://localhost:39402/api/Employee/1
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Employee/{productID:int}")]
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
        ////http://localhost:39402/api/Employee/1
        [HttpPost]
        // POST api/<controller>

        [Route("api/Employee")]
        override public Employee Post([FromBody]Employee value)
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



        ////http://localhost:39402/api/Employee/1
        [HttpPut]
        [Route("api/Employee")]
        // PUT api/<controller>/5
        override public Employee Put([FromBody]Employee value)
        {
            return base.Put(value);

        }
        #endregion //REST
    }
}