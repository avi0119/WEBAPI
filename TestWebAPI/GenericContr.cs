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
    public class GenericContr<T, T2, T3, T4> : ApiController, IGenericController<T, T2, T3, T4>
    {
        //IProduct _IProduct;
        IGenericCRUD<T> _iaddprod;
        Dictionary<Type, ClassTypeMetaData> classMetaData=new Dictionary<Type, ClassTypeMetaData>();
        // GET api/<controller>
        public GenericContr()
        {
            buildClassMetaDataDictionary();
            IGenericCRUD<T> iaddprod =( IGenericCRUD<T>) (GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenericCRUD<T>)));
            ////this(iaddprod);
            _iaddprod = iaddprod;
            var z = 1;
            var t = z;
        }
        private void buildClassMetaDataDictionary()
        {
            classMetaData.Add(typeof(Product), new ClassTypeMetaData {tableName="products",primaryKey="ProductID" });
            classMetaData.Add(typeof(Category), new ClassTypeMetaData { tableName = "categories", primaryKey = "CategoryID" });
            classMetaData.Add(typeof(Supplier), new ClassTypeMetaData { tableName = "suppliers", primaryKey = "SupplierID" });
            classMetaData.Add(typeof(Order), new ClassTypeMetaData { tableName = "orders", primaryKey = "OrderID" });

        }
        private void returnTableNmaesAndIDsBasedOnGeneric(int numberOfGenericTypesParticipating, out string[] tableName, out string[] idFieldName)
        {
            List<string>  tableNameList = new List<string> ();// { "products", "categories" };
            List<string> idFieldNameList = new List<string>();//{ "ProductID", "CategoryID" };

            tableNameList.Add(classMetaData[typeof(T)].tableName);
            idFieldNameList.Add(classMetaData[typeof(T)].primaryKey);
            if (numberOfGenericTypesParticipating >1)
            {
                tableNameList.Add(classMetaData[typeof(T2)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T2)].primaryKey);
            }
            if (numberOfGenericTypesParticipating > 2)
            {
                tableNameList.Add(classMetaData[typeof(T3)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T3)].primaryKey);
            }
            if (numberOfGenericTypesParticipating > 3)
            {
                tableNameList.Add(classMetaData[typeof(T4)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T4)].primaryKey);
            }
            tableName = tableNameList.ToArray();
            idFieldName = idFieldNameList.ToArray();
         
        }
        
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

            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };
            string[] tableName;
            string[] idFieldName ;
            //Func<T, T2, T> dl = new Func<T, T2,  T>((prod, cat) => { prod.Category = cat; return prod; });
             
            Func<T, T2,  T> dl;

            getDelegateTableNamesAndFieldNames2(out dl, out tableName, out idFieldName);

            IEnumerable<T> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID2()
        {
            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);
            //string tableName = "products";
            //string idFieldName = "ProductID";

            IEnumerable<T> p = _iaddprod.Get(tableName[0], idFieldName[0]);
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
            Func<T, T2, T3, T> dl; 
            //Func<T, T2,  T> dl; 
            string[] tableName;
            string[] idFieldName;
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
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(idFieldName[0], id);
            //MyType param = new MyType { ProductID = id };
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => {
            //    prod.Category = cat; prod.Supplier = Supplier; return prod; 
            //});

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            T p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }
        private T ObtainProductByID3(int id, Func<T, T2, T> dl, string[] tableName, string[] idFieldName)
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };
            
            //Func<T, T2, T> dl = new Func<T, T2,  T>((prod, cat) => { prod.Category = cat; return prod; });

            //Func<T, T2,  T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames2(out dl, out tableName, out idFieldName);
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(idFieldName[0], id);
            T p = _iaddprod.Get(id, tableName, idFieldName, param, dl);
            return p;
        }

        private T ObtainProductByID2(int id, Func<T, T2,  T> dl, string[] tableName, string[] idFieldName)
        {
            //Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames2(out dl, out tableName, out idFieldName);
            //string tableName = "products";
            //string idFieldName = "ProductID";
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(idFieldName[0], id);
            T p = _iaddprod.Get(id, tableName[0], idFieldName[0], param);
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
            var res = _iaddprod.DeleteItem(p, tableName[0]);
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
             //tableName = new string[] { "products", "categories","suppliers" };
             //idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
             returnTableNmaesAndIDsBasedOnGeneric(3,out   tableName, out  idFieldName);
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
            //tableName = new string[] { "products", "categories" };
            //idFieldName = new string[] { "ProductID", "CategoryID" };
            returnTableNmaesAndIDsBasedOnGeneric(2, out   tableName, out  idFieldName);
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
            string[] tableName;//= new string[] { "products", "categories" };
            string[] idFieldName;// = new string[] { "ProductID", "CategoryID" };
            Func<T, T2, T3, T> dl;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);


            var id = _iaddprod.Add(value, tableName[0]);







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
         * do not for get to put in the header the following:
          Content-Type: application/json; charset=utf-8
        
         */



        ////http://localhost:39402/api/product/77
        [HttpPut]
        [Route("api/product")]
        // PUT api/<controller>/5
        public T Put([FromBody]T value)
        {
            


            Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);

            var id = _iaddprod.UpdateItem(value, tableName[0], idFieldName[0]);
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
            var properties = obj.GetProperties();
            foreach (var property in properties)
            {

                // Skip reference types (but still include string!)

                //if (property.PropertyType.IsClass && property.PropertyType != typeof(string))

                //    continue;



                // Skip methods without a public setter

                if (property.GetSetMethod() == null)

                    continue;



                // Skip methods specifically ignored

                if (property.IsDefined(typeof(DapperIgnore), false))

                    continue;



                var name = property.Name;

                var value = obj.GetProperty(property.Name).PropertyType;



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
        private  dynamic returnCriteriaParam(string propname,int id)
        {
            List<PropertyNameAndType> dict = new List<PropertyNameAndType>();
            dict.Add(new PropertyNameAndType() { Name = propname, Type_ = typeof(int), value = id });
            DynamicClass a = new DynamicClass(dict);
            return a.resultObject;
        }
    }
    public class MyType
    {
        public int ProductID { get; set; }
    }
    public class ClassTypeMetaData
    {
        public string tableName { get; set; }
        public string primaryKey { get; set; }
    }
}