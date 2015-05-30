using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using DataAccess;
using ModelObjects;


namespace TestWebAPI

{

    public class OrderController : GenericContr<Order, Customer, OrderDetail, Order>
    {
        public IGenericCRUD<OrderDetail> _iaddprodOrderDetail;

        public OrderController(IGenericCRUD<Order> iaddprod, IGenericCRUD<OrderDetail> iaddprodOrderDetail)
            : base(iaddprod)
        {
            _iaddprodOrderDetail = iaddprodOrderDetail;
            numberOfGenerics = 3;
            //this.adjuster = new Action<string[], string[], string[], string[]>((tables, left, right, jointype) => { left[2] = "OrderID"; jointype[1] = "left outer join"; });
        }
        #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/Order
        [HttpGet]
        [Route("api/Order")]
        override public IEnumerable<Order> Get()
        {
            return base.Get();
        }

        ////http://localhost:39402/api/Order/10248
        // GET api/<controller>/5
        //[Route("api/Order/OrderID:int")]
        [HttpGet]
        [Route("api/Order/{productID:int}")]
        override public Order Get(int productID)
        {
            //return AltGet(productID);
            return base.Get(productID);
        }
        //private void returnTableNmaesAndIDsBasedOnGeneric(int numberOfGenericTypesParticipating, out string[] tableName, out string[] idFieldName)
        //{
        //    List<string> tableNameList = new List<string>();// { "products", "categories" };
        //    List<string> idFieldNameList = new List<string>();//{ "ProductID", "CategoryID" };

        //    tableNameList.Add(classMetaData[typeof(T)].tableName);
        //    idFieldNameList.Add(classMetaData[typeof(T)].primaryKey);
        //    if (numberOfGenericTypesParticipating > 1)
        //    {
        //        tableNameList.Add(classMetaData[typeof(T2)].tableName);
        //        idFieldNameList.Add(classMetaData[typeof(T2)].primaryKey);
        //    }
        //    if (numberOfGenericTypesParticipating > 2)
        //    {
        //        tableNameList.Add(classMetaData[typeof(T3)].tableName);
        //        idFieldNameList.Add(classMetaData[typeof(T3)].primaryKey);
        //    }
        //    if (numberOfGenericTypesParticipating > 3)
        //    {
        //        tableNameList.Add(classMetaData[typeof(T4)].tableName);
        //        idFieldNameList.Add(classMetaData[typeof(T4)].primaryKey);
        //    }
        //    tableName = tableNameList.ToArray();
        //    idFieldName = idFieldNameList.ToArray();

        //}
        private Order AltGet(int productID) 
        {
            dynamic dl;


            getDelegateTableNamesAndFieldNames_final(out dl,QueryRelatedArgs, numberOfGenerics);
            dynamic param = returnCriteriaParam(((string[])QueryRelatedArgs["idFieldName"])[0], productID);
             
            Order O = _iaddprod.ReturnAllOrders<OrderDetail>(productID, QueryRelatedArgs, param);
            return O;
        }
        ////http://localhost:39402/api/Order/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Order/{productID:int}")]
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
        ////http://localhost:39402/api/Order/77
        [HttpPost]
        // POST api/<controller>

        [Route("api/Order")]
        override public Order Post([FromBody]Order value)
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



        ////http://localhost:39402/api/Order/77
        [HttpPut]
        [Route("api/Order")]
        // PUT api/<controller>/5
        override public Order Put([FromBody]Order value)
        {
            
            return base.Put(value);

        }

        [HttpPost]
        [Route("api/order/x")]
         public int Get([FromBody] CartObject cartobject)
        {
            DateTime now = DateTime.Now;
            Order ord = cartobject.EntityOrder(now);
            Order retord=base.Post(ord);
            List<OrderDetail> listoford = cartobject.EntityOrederDetails(retord.OrderID);
            string tableName = classMetaData[typeof(OrderDetail)].tableName;
            string idFieldName = classMetaData[typeof(OrderDetail)].primaryKeyLeft;


            _iaddprodOrderDetail.Add(listoford,tableName, idFieldName);

            return retord.OrderID;
        }
        #endregion //REST
    }
}