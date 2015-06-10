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

    public class UserController : GenericContr<int, User, DBClaim, Supplier, Customer>
    {

        public UserController(IGenericCRUD<User, int> iaddprod)
            : base(iaddprod)
        {

            numberOfGenerics = 2;

  
 
            //this.adjuster = new Action<string[], string[], string[], string[]>((tables, left, right, jointype) => { left[1] = "CustomerID2"; right[1] = "CustomerID_"; jointype[1] = "left outer join"; });
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/user
        [HttpGet]
        [Route("api/user")]
        override public IEnumerable<User> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/user/77
        // GET api/<controller>/5
        //[Route("api/user/userID:int")]
        [HttpGet]
        [Route("api/user/{UserID:int}")]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        override public User Get(int UserID)
        {

            //return base.Get(UserID);
            return AltGet(UserID);
        }

        private User AltGet(int UserID)
        {
            dynamic dl;


            getDelegateTableNamesAndFieldNames_final(out dl, QueryRelatedArgs, numberOfGenerics);
            dynamic param = returnCriteriaParam(((string[])QueryRelatedArgs["idFieldName"])[0], UserID);

            User O = _iaddprod.ReturnAllOrders<DBClaim>(UserID, QueryRelatedArgs, param);
            return O;
        }

        ////http://localhost:39402/api/user/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/user/{UserID:int}")]
        override public bool Delete(int UserID)
        {

            return base.Delete(UserID);
        }

        /*
{
   
"UserName":"my new User",  
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
        ////http://localhost:39402/api/user
        [HttpPost]
        [Route("api/user/")]
        //[Route("api/user/{value:User}")]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        override public User Post([FromBody]User value)
        {
            if (value.username == "avisemah@gmail.com" && value.password == "12345")
            {
                return value;

            }
            else
            {
                throw new  Exception("user name and/or pw are not accurate");
            }
            //return base.Post(value);
            
        }


        /*
{
"Userid":80,    
"UserName":"my updated prod", 
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



        ////http://localhost:39402/api/user/77
        [HttpPut]
        [Route("api/user")]
        // PUT api/<controller>/5
        override public User Put([FromBody]User value)
        {
            return base.Put(value);

        }

        //[HttpPost]
        //[Route("api/user/x")]
        //override public void Get([FromBody]IEnumerable<User> UserID)
        //{
        //    var g = 6;
        //    var t = g;
        //}
        #endregion //REST
    }
}