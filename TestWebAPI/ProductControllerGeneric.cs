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
        IGenericCRUD<Product> _iaddprod;
        // GET api/<controller>
 
        public ProductController(IGenericCRUD<Product> iaddprod)
        {
            //_IProduct=iproduct;
            _iaddprod = iaddprod;
        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/product
        [HttpGet]
        [Route("api/Product")]
        public IEnumerable<Product> Get()
        {

            var z = ObtainProductByID();
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }

        private IEnumerable<Product> ObtainProductByID()
        {
            //id = -1;
            string[] tableName = new string[] { "products", "categories", "suppliers" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            //object param = new { ProductID = id };
            Func<Product, Category, Supplier, Product> dl = new Func<Product, Category, Supplier, Product>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable< Product> p = _iaddprod.Get( tableName, idFieldName,  dl);
            return p;
        }

        private IEnumerable<Product> ObtainProductByID3()
        {
            //id = -1;
            string[] tableName = new string[] { "products", "categories" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID"  };
            //object param = new { ProductID = id };
            Func<Product, Category, Product> dl = new Func<Product, Category,  Product>((prod, cat) => { prod.Category = cat;  return prod; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable<Product> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<Product> ObtainProductByID2()
        {
            string tableName = "products";
            string idFieldName = "ProductID" ;

            IEnumerable<Product> p = _iaddprod.Get(tableName, idFieldName);
            return p;
        }

        ////http://localhost:39402/api/product/77
        // GET api/<controller>/5
        //[Route("api/product/productID:int")]
        [HttpGet]
        [Route("api/Product/{productID:int}")]
        public Product Get(int productID)
        {
 
            var z = ObtainProductByID(productID);
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        private Product ObtainProductByID(int id)
        {
            //id = -1;
            string[] tableName = new string[] { "products", "categories", "suppliers" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            object param = new { ProductID = id };
            Func<Product, Category, Supplier, Product> dl = new Func<Product, Category, Supplier, Product>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            Product p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }
        private Product ObtainProductByID3(int id)
        {
            //id = -1;
            string[] tableName = new string[] { "products", "categories" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID" };
            object param = new { ProductID = id };
            Func<Product, Category,  Product> dl = new Func<Product, Category,  Product>((prod, cat) => { prod.Category = cat; return prod; });

     
            Product p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }

        private Product ObtainProductByID2(int id)
        {

            string tableName = "products";
            string idFieldName = "ProductID";
            object param = new { ProductID = id };
            Product p = _iaddprod.Get(id, tableName, idFieldName, param);
            return p;
        }
 
        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        public bool Delete(int productID)
        {
            var p = ObtainProductByID(productID);
            var res = _iaddprod.DeleteItem(p,"products");
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
        public Product Post([FromBody]Product value)
        {
            //var value = 1;
            var id = _iaddprod.Add(value, "products");
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
 

 
        ////http://localhost:39402/api/product/77
        [HttpPut]
        [Route("api/product")]
        // PUT api/<controller>/5
        public Product Put([FromBody]Product value)
        {
            var id = _iaddprod.UpdateItem(value, "products","ProductId");
            var p = ObtainProductByID(id);
 
            return p;
        }
 
        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}