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
    public class GenericContr<T, T2, T3, T4> : ApiController
    {
        //IProduct _IProduct;
        IGenericCRUD<T> _iaddprod;
        // GET api/<controller>

         public GenericContr(IGenericCRUD<T> iaddprod)
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
        virtual public IEnumerable<T> Get()
        {

            var z = ObtainProductByID();
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }

        private IEnumerable<T> ObtainProductByID()
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories", "suppliers" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            ////object param = new { ProductID = id };
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>(
            //    (prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; 
            //});
            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);
            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            IEnumerable<T> p = _iaddprod.Get<T2, T3>(tableName, idFieldName, dl);
            return p;
        }

        private IEnumerable<T> ObtainProductByID3()
        {

            string[] tableName = new string[] { "products", "categories" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID" };
 
            //Func<T, T2, T> dl = new Func<T, T2,  T>((prod, cat) => { prod.Category = cat; return prod; });

            Func<T, T2,  T> dl;
            string[] tableName2;
            string[] idFieldName2;
            getDelegateTableNamesAndFieldNames2(out dl, out tableName2, out idFieldName2);

            IEnumerable<T> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID2()
        {
            string tableName = "products";
            string idFieldName = "ProductID";

            IEnumerable<T> p = _iaddprod.Get(tableName, idFieldName);
            return p;
        }

        ////http://localhost:39402/api/product/77
        // GET api/<controller>/5
        //[Route("api/product/productID:int")]
        [HttpGet]
        [Route("api/Product/{productID:int}")]
        public T Get(int productID)
        {
            //Func<T, T2, T3, T> dl; 
            //string[] tableName; 
            //string[] idFieldName;
            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);
            T z = ObtainProductByID(productID, dl,  tableName,  idFieldName);
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        private T ObtainProductByID(int id, Func<T, T2, T3, T> dl, string[] tableName, string[] idFieldName)
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories", "suppliers" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            object param = new { ProductID = id };
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => {
            //    prod.Category = cat; prod.Supplier = Supplier; return prod; 
            //});

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            T p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }
        private T ObtainProductByID3(int id)
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };
            
            //Func<T, T2, T> dl = new Func<T, T2,  T>((prod, cat) => { prod.Category = cat; return prod; });

            Func<T, T2,  T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames2(out dl, out tableName, out idFieldName);
            object param = new { ProductID = id };
            T p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }

        private T ObtainProductByID2(int id)
        {

            string tableName = "products";
            string idFieldName = "ProductID";
            object param = new { ProductID = id };
            T p = _iaddprod.Get(id, tableName, idFieldName, param);
            return p;
        }

        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        public bool Delete(int productID)
        {
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };

            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);

            T p = ObtainProductByID(productID,dl,tableName,idFieldName);
            var res = _iaddprod.DeleteItem(p, "products");
            return res;
            //return "value";
        }
        private void getDelegateTableNamesAndFieldNames(out Func<T, T2, T3, T> dl, out string[] tableName, out string[] idFieldName)
        {

             PropertyContainer[] arr = ReturnallPropertyContainer(typeof(T), typeof(T2), typeof(T3));
             string prop2 = (arr[0]).FindPropertyNameOFGivenType(typeof(T2));
             string prop3 = (arr[0]).FindPropertyNameOFGivenType(typeof(T3));

             dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => {
                 //prod.Category = cat;
                 //prod.Supplier = Supplier;
                 prod.GetType().GetProperty(prop2).SetValue(prod,cat);
                 prod.GetType().GetProperty(prop3).SetValue(prod, Supplier);

                 return prod; 
             });
             tableName = new string[] { "products", "categories" };
             idFieldName = new string[] { "ProductID", "CategoryID" };
        }
        private void getDelegateTableNamesAndFieldNames2(out Func<T, T2,  T> dl, out string[] tableName, out string[] idFieldName)
        {

            PropertyContainer[] arr = ReturnallPropertyContainer(typeof(T), typeof(T2));
            string prop2 = (arr[0]).FindPropertyNameOFGivenType(typeof(T2));

            dl = new Func<T, T2,  T>((prod, cat) => 
            {
                prod.GetType().GetProperty(prop2).SetValue(prod, cat);
                //prod.Category = cat; 
                return prod; 
            });
            tableName = new string[] { "products", "categories" };
            idFieldName = new string[] { "ProductID", "CategoryID" };
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
        ////http://localhost:39402/api/product/77
        [HttpPost]
        // POST api/<controller>

        [Route("api/product")]
        public T Post([FromBody]T value)
        {
            //var value = 1;
            var id = _iaddprod.Add(value, "products");


            string[] tableName = new string[] { "products", "categories" };
            string[] idFieldName = new string[] { "ProductID", "CategoryID" };


            Func<T, T2, T3, T> dl; string[] tableName2; string[] idFieldName2;
            getDelegateTableNamesAndFieldNames(out dl, out tableName2, out idFieldName2);

            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });

            var p = ObtainProductByID(id, dl,tableName, idFieldName);
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
        public T Put([FromBody]T value)
        {
            var id = _iaddprod.UpdateItem(value, "products", "ProductId");


            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);

            T p = ObtainProductByID(id, dl, tableName, idFieldName);

            return p;
        }
        private static PropertyContainer[] ReturnallPropertyContainer(params Type[] types)
        {
            List<PropertyContainer> list = new List<PropertyContainer>();
            foreach (Type a in types)
            {
                PropertyContainer b = ParseProperties(a);
                list.Add(b);
            }
            return list.ToArray();
        }
        private static PropertyContainer ParseProperties(Type obj)
        {

            var propertyContainer = new PropertyContainer();
            var typeName =obj.Name;
            var validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) };
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {

                // Skip reference types (but still include string!)

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))

                    continue;



                // Skip methods without a public setter

                if (property.GetSetMethod() == null)

                    continue;



                // Skip methods specifically ignored

                if (property.IsDefined(typeof(DapperIgnore), false))

                    continue;



                var name = property.Name;

                var value = obj.GetProperty(property.Name).GetType();



                if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                {

                    propertyContainer.AddId(name, value);

                }

                else
                {

                    propertyContainer.AddValue(name, value);

                }

            }



            return propertyContainer;

        }
    }
}