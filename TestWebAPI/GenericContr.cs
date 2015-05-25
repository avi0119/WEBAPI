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
            numberOfGenerics = 1;
            buildClassMetaDataDictionary();
            IGenericCRUD<T> iaddprod =( IGenericCRUD<T>) (GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenericCRUD<T>)));
            ////this(iaddprod);
            _iaddprod = iaddprod;
            var z = 1;
            var t = z;
        }
        int numberOfGenerics_;
        public int numberOfGenerics
        {
            get
            {
                return numberOfGenerics_;
            }
            set
            {
                numberOfGenerics_ = value;
            }
        }
         #region  Rest

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        ////http://localhost:39402/api/product
        [HttpGet]
        [Route("api/Product")]
        virtual public IEnumerable<T> Get()
        {
            var z = ObtainProductByID_final(numberOfGenerics);
            return z;
        }

        ////http://localhost:39402/api/product/77
        // GET api/<controller>/5
        //[Route("api/product/productID:int")]
        [HttpGet]
        [Route("api/Product/{productID:int}")]
        virtual public T Get(int productID)
        {

            dynamic dl;
  
            string[] tableName;
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames_final(out dl, out tableName, out idFieldName, numberOfGenerics);

            T z = ObtainProductByID_FINAL(productID, dl, tableName, idFieldName, numberOfGenerics);
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Product/{productID:int}")]
        virtual public bool Delete(int productID)
        {
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };

            dynamic dl;

            string[] tableName;
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames_final(out dl, out tableName, out idFieldName, numberOfGenerics);
            //getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);

            T p = ObtainProductByID_FINAL(productID, dl, tableName, idFieldName,numberOfGenerics);
            var res = _iaddprod.DeleteItem(p, tableName[0]);
            return res;
            //return "value";
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
        virtual public T Post([FromBody]T value)
        {
            dynamic dl;
            string[] tableName;
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames_final(out dl, out tableName, out idFieldName, numberOfGenerics);
            var id = _iaddprod.Add(value, tableName[0]);
            var p = ObtainProductByID_FINAL(id, dl, tableName, idFieldName, numberOfGenerics);
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
        virtual public T Put([FromBody]T value)
        {



            dynamic dl; 
            string[] tableName;
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames_final(out dl, out tableName, out idFieldName,numberOfGenerics);
            var id = _iaddprod.UpdateItem(value, tableName[0], idFieldName[0]);
            T p = ObtainProductByID_FINAL(id, dl, tableName, idFieldName, numberOfGenerics);
            

            return p;
        }
        #endregion //REST
        private void buildClassMetaDataDictionary()
        {
            classMetaData.Add(typeof(Product), new ClassTypeMetaData { tableName = "products", primaryKey = "ProductID", primaryKeyAppearingInSelf = "ProductID" });
            classMetaData.Add(typeof(Category), new ClassTypeMetaData { tableName = "categories", primaryKey = "CategoryID", primaryKeyAppearingInSelf = "CategoryID" });
            classMetaData.Add(typeof(Supplier), new ClassTypeMetaData { tableName = "suppliers", primaryKey = "SupplierID", primaryKeyAppearingInSelf = "SupplierID" });
            classMetaData.Add(typeof(Order), new ClassTypeMetaData { tableName = "orders", primaryKey = "OrderID", primaryKeyAppearingInSelf = "OrderID" });
            classMetaData.Add(typeof(Employee), new ClassTypeMetaData { tableName = "employees", primaryKey = "EmployeeID", primaryKeyAppearingInSelf = "ReportsTo" });

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

        private IEnumerable<T> ObtainProductByID_final(int numberofgenerics)
        {

            if (numberofgenerics == 1)
            {
                return ObtainProductByID2();
            }
            if (numberofgenerics == 2)
            {
                return ObtainProductByID3();
            }
            return ObtainProductByID();
        }
        private IEnumerable<T> ObtainProductByID()
        {

            Func<T, T2, T3, T> dl;
            string[] tableName; 
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);
            IEnumerable<T> p = _iaddprod.Get<T2, T3>(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID3()
        {


            string[] tableName;
            string[] idFieldName ;          
            Func<T, T2,  T> dl;
            getDelegateTableNamesAndFieldNames2(out dl, out tableName, out idFieldName);

            IEnumerable<T> p = _iaddprod.Get(tableName, idFieldName, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID2()
        {
            Func<T, T2, T3, T> dl; 
            string[] tableName;
            string[] idFieldName;
            getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);
            IEnumerable<T> p = _iaddprod.Get(tableName[0], idFieldName[0]);
            return p;
        }


        protected T ObtainProductByID_FINAL(int id, dynamic dl, string[] tableName, string[] idFieldName,int numberofGenerics)
        {
            if (numberofGenerics == 3)
            {
                return ObtainProductByID( id,  dl,  tableName,  idFieldName);
            }
            if (numberofGenerics == 1)
            {
                return ObtainProductByID2(id, dl, tableName, idFieldName);
            }
            // it's 2 generics
            return ObtainProductByID3(id, dl, tableName, idFieldName);

        }

        protected T ObtainProductByID(int id, Func<T, T2, T3, T> dl, string[] tableName, string[] idFieldName)
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
        protected T ObtainProductByID3(int id, Func<T, T2, T> dl, string[] tableName, string[] idFieldName)
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
        protected T ObtainProductByID2(int id, Func<T, T2, T> dl, string[] tableName, string[] idFieldName)
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
        protected void getDelegateTableNamesAndFieldNames_final(out dynamic dl, out string[] tableName, out string[] idFieldName, int numberofGenerics)
        {
            if (numberofGenerics == 3)
            {
                Func<T, T2, T3, T> dl_ ;
                getDelegateTableNamesAndFieldNames(out dl_, out tableName, out  idFieldName);
                dl = dl_;
                return;
            }
            //if (numberofGenerics == 3)
            //{
                Func<T, T2,  T> dl__ ;
                getDelegateTableNamesAndFieldNames2(out dl__, out tableName, out  idFieldName);
                dl = dl__;
            //}
 
        }
        protected void getDelegateTableNamesAndFieldNames(out Func<T, T2, T3, T> dl, out string[] tableName, out string[] idFieldName)
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
        protected void getDelegateTableNamesAndFieldNames2(out Func<T, T2, T> dl, out string[] tableName, out string[] idFieldName)
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
    public class ClassTypeMetaData
    {
        public string tableName { get; set; }
        public string primaryKey { get; set; }
        public string primaryKeyAppearingInSelf { get; set; }
    }
}