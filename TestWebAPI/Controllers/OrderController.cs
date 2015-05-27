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
    [RoutePrefix("api/order")]
    [UnitOfWorkActionFilter]
    public class OrderController : ApiController
    {
        //IProduct _IProduct;
        IGenericCRUD<Order> _iaddprod;
        // GET api/<controller>

        public OrderController(IGenericCRUD<Order> iaddprod)
        {
            //_IProduct=iproduct;
            _iaddprod = iaddprod;
        }
        //public OrderController()
        //{
        //    //_IProduct=iproduct;
        //    var service = WebContainerManager.GetDependencyResolver().GetService(typeof(IGenericCRUD<Order>));
        //    _iaddprod = (IGenericCRUD<Order>)service;
        //    var z = 1;
        //    var b = z;
        //}
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/order
        [HttpGet]
        [Route("")]
        public IEnumerable<Order> Get()
        {

            var z = ObtainProductByID4();
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        private IEnumerable<Order> ObtainProductByID4()
        {
            //id = -1;
            string[] tableName = new string[] { "orders", "customers", "employees","Order Details" };
            string[] idFieldName = new string[] { "OrderID", "CustomerID", "EmployeeID","OrderID" };
            //object param = new { ProductID = id };
            Func<Order, Customer, Employee, IEnumerable<OrderDetail>, Order> dl = new Func<Order, Customer, Employee, IEnumerable<OrderDetail>, Order>((ord, cust, Emp, orddetail) => { ord.Customer = cust; ord.Employee = Emp; ord.Items = orddetail; return ord; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable<Order> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<Order> ObtainProductByID()
        {
            //id = -1;
            string[] tableName = new string[] { "orders", "customers", "employees" };
            string[] idFieldName = new string[] { "OrderID", "CustomerID", "EmployeeID" };
            //object param = new { ProductID = id };
            Func<Order, Customer, Employee, Order> dl = new Func<Order, Customer, Employee, Order>((ord, cust, Emp) => { ord.Customer = cust; ord.Employee = Emp; return ord; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable<Order> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }

        private IEnumerable<Order> ObtainProductByID3()
        {
            //id = -1;
            string[] tableName = new string[] { "orders", "customers" };
            string[] idFieldName = new string[] { "OrderID", "CustomerID" };
            //object param = new { ProductID = id };
            Func<Order, Customer, Order> dl = new Func<Order, Customer, Order>((ord, cust) => { ord.Customer = cust; return ord; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable<Order> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<Order> ObtainProductByID2()
        {
            string tableName = "products";
            string idFieldName = "ProductID";

            IEnumerable<Order> p = _iaddprod.Get(tableName, idFieldName);
            return p;
        }

        ////http://localhost:39402/api/order/10248
        // GET api/<controller>/5
        //[Route("api/order/orderID:int")]
        [HttpGet]
        [Route("{productID:int}")]
        public Order Get(int productID)
        {

            var z = ObtainProductByID(productID);
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        private Order ObtainProductByID(int id)
        {
            //id = -1;
            string[] tableName = new string[] { "orders", "customers", "employees" };
            string[] idFieldName = new string[] { "OrderID", "CustomerID", "EmployeeID" };
            object param = new { ProductID = id };
            Func<Order, Customer, Employee, Order> dl = new Func<Order, Customer, Employee, Order>((ord, cust, Emp) => { ord.Customer = cust; ord.Employee = Emp; return ord; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            Order p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }
        private Order ObtainProductByID3(int id)
        {

            string[] tableName = new string[] { "orders", "customers" };
            string[] idFieldName = new string[] { "OrderID", "CustomerID" };
            object param = new { ProductID = id };
            Func<Order, Customer, Order> dl = new Func<Order, Customer, Order>((ord, cust) => { ord.Customer = cust; return ord; });


            Order p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }

        private Order ObtainProductByID2(int id)
        {

            string tableName = "orders";
            string idFieldName = "OrderID";
            object param = new { ProductID = id };
            Order p = _iaddprod.Get(id, tableName, idFieldName, param);
            return p;
        }

        ////http://localhost:39402/api/order/80
        // GET api/<controller>/5
        [HttpDelete]
        public bool Delete(int productID)
        {
            var p = ObtainProductByID(productID);
            var res = _iaddprod.DeleteItem(p, "orders");
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
        ////http://localhost:39402/api/order/10248
        [HttpPost]
        // POST api/<controller>

        [Route("")]
        public Order Post([FromBody]Order value)
        {
            //var value = 1;
            var id = _iaddprod.Add(value, "orders");
            var p = ObtainProductByID(id);
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



        ////http://localhost:39402/api/order/10248
        [HttpPut]
        [Route("")]
        // PUT api/<controller>/5
        public Order Put([FromBody]Order value)
        {
            var id = _iaddprod.UpdateItem(value, "orders", "OrderId");
            var p = ObtainProductByID(id);

            return p;
        }

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}