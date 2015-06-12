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
    public class GenericContr<T0,T, T2, T3, T4> : ApiController, IGenericController<T0,T, T2, T3, T4>
    {

        public Action<string[], string[], string[], string[]> adjuster;
        public Dictionary<string, object> QueryRelatedArgs = new Dictionary<string, object>();
        public IGenericCRUD<T,int> _iaddprod;
        public Dictionary<Type, ClassTypeMetaData> classMetaData=new Dictionary<Type, ClassTypeMetaData>();
        // GET api/<controller>
        public GenericContr()
        {
            var z = 5;
            var hj = z;
        }
        public GenericContr(IGenericCRUD<T,int> iaddprod)
        {
            numberOfGenerics = 1;
            buildClassMetaDataDictionary();
            //IGenericCRUD<T> iaddprod =( IGenericCRUD<T>) (GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenericCRUD<T>)));
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
        [HttpPost]
        [Route("api/Product/")]
        virtual public void Get([FromBody]IEnumerable<Product> productID)
        {
            var g = 6;
            var t = g;
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
            string[] idFieldName_right;
            getDelegateTableNamesAndFieldNames_final(out dl, QueryRelatedArgs, numberOfGenerics);

            T z = ObtainProductByID_FINAL(productID, dl, QueryRelatedArgs, numberOfGenerics);
            //var z = ObtainProductByID(productID);
            return z;
            //return "value";
        }
        ////http://localhost:39402/api/product/80
        // GET api/<controller>/5
        [HttpDelete]
        [Route("api/Product/{productID}")]
        virtual public bool Delete(int productID)
        {
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };

            dynamic dl;

            string[] tableName;
            string[] idFieldName;
            string[] idFieldName_right;
            getDelegateTableNamesAndFieldNames_final(out dl, QueryRelatedArgs, numberOfGenerics);
            //getDelegateTableNamesAndFieldNames(out dl, out tableName, out idFieldName);

            T p = ObtainProductByID_FINAL(productID, dl, QueryRelatedArgs, numberOfGenerics);
            var res = _iaddprod.DeleteItem(p, ((string[])QueryRelatedArgs["tableName"])[0]);
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

            getDelegateTableNamesAndFieldNames_final(out dl, QueryRelatedArgs, numberOfGenerics);
            var id = _iaddprod.Add(value, ((string[])QueryRelatedArgs["tableName"])[0]);
            var p = ObtainProductByID_FINAL(id, dl, QueryRelatedArgs, numberOfGenerics);
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
            string[] idFieldName_right;
            getDelegateTableNamesAndFieldNames_final(out dl, QueryRelatedArgs,numberOfGenerics);
            var id = _iaddprod.UpdateItem(value, ((string[])QueryRelatedArgs["tableName"])[0], ((string[])QueryRelatedArgs["idFieldName"])[0]);
            T p = ObtainProductByID_FINAL(id, dl, QueryRelatedArgs, numberOfGenerics);
            

            return p;
        }
        #endregion //REST
        private void buildClassMetaDataDictionary()
        {
            classMetaData.Add(typeof(Product), new ClassTypeMetaData { tableName = "products", primaryKeyLeft = "ProductID", primaryKeyRight = "ProductID", joinType="left outer join" });
            classMetaData.Add(typeof(Category), new ClassTypeMetaData { tableName = "categories", primaryKeyLeft = "CategoryID", primaryKeyRight = "CategoryID", joinType = "Left Outer Join" });
            classMetaData.Add(typeof(Supplier), new ClassTypeMetaData { tableName = "suppliers", primaryKeyLeft = "SupplierID", primaryKeyRight = "SupplierID", joinType = "Left Outer Join" });
            classMetaData.Add(typeof(Order), new ClassTypeMetaData { tableName = "orders", primaryKeyLeft = "OrderID", primaryKeyRight = "OrderID", joinType = "left outer join" });
            classMetaData.Add(typeof(Employee), new ClassTypeMetaData { tableName = "employees", primaryKeyLeft = "EmployeeID", primaryKeyRight = "EmployeeID", joinType = "left outer join" });
            classMetaData.Add(typeof(Customer), new ClassTypeMetaData { tableName = "customers", primaryKeyLeft = "CustomerID_", primaryKeyRight = "CustomerID", joinType = "left outer join" });
            classMetaData.Add(typeof(OrderDetail), new ClassTypeMetaData { tableName = "Order Details", primaryKeyLeft = "OrderID", primaryKeyRight = "OrderID", joinType = "left outer join" });
            classMetaData.Add(typeof(Shipper), new ClassTypeMetaData { tableName = "shippers", primaryKeyLeft = "ShipperID", primaryKeyRight = "ShipperID", joinType = "left outer join" });
            classMetaData.Add(typeof(User), new ClassTypeMetaData { tableName = "users", primaryKeyLeft = "UserID", primaryKeyRight = "UserID", joinType = "left outer join" });
            classMetaData.Add(typeof(DBClaim), new ClassTypeMetaData { tableName = "viewUserClaimsAndDescription", primaryKeyLeft = "UserID", primaryKeyRight = "UserID", joinType = "left outer join" });
            classMetaData.Add(typeof(DBClaimType), new ClassTypeMetaData { tableName = "ClaimTypes", primaryKeyLeft = "ClaimID", primaryKeyRight = "ClaimID", joinType = "left outer join" });
            
        }
        public void returnTableNmaesAndIDsBasedOnGeneric(int numberOfGenericTypesParticipating, Dictionary<string,object> aQueryRelatedArgs)
        {
            string[] tableName; string[] idFieldName; string[] idFieldName_right; string[] joinType;
            List<string>  tableNameList = new List<string> ();// { "products", "categories" };
            List<string> idFieldNameList = new List<string>();//{ "ProductID", "CategoryID" };
            List<string> idFieldNameList_right = new List<string>();//{ "ProductID", "CategoryID" };
            List<string> joinTypeList = new List<string>();//{ "ProductID", "CategoryID" };

            tableNameList.Add(classMetaData[typeof(T)].tableName);
            idFieldNameList.Add(classMetaData[typeof(T)].primaryKeyLeft);
            idFieldNameList_right.Add(classMetaData[typeof(T)].primaryKeyRight);
            joinTypeList.Add(classMetaData[typeof(T)].joinType);
            if (numberOfGenericTypesParticipating >1)
            {
                tableNameList.Add(classMetaData[typeof(T2)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T2)].primaryKeyLeft);
                idFieldNameList_right.Add(classMetaData[typeof(T2)].primaryKeyRight);
                joinTypeList.Add(classMetaData[typeof(T2)].joinType);
                
            }
            if (numberOfGenericTypesParticipating > 2)
            {
                tableNameList.Add(classMetaData[typeof(T3)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T3)].primaryKeyLeft);
                idFieldNameList_right.Add(classMetaData[typeof(T3)].primaryKeyRight);
                joinTypeList.Add(classMetaData[typeof(T3)].joinType);
            }
            if (numberOfGenericTypesParticipating > 3)
            {
                tableNameList.Add(classMetaData[typeof(T4)].tableName);
                idFieldNameList.Add(classMetaData[typeof(T4)].primaryKeyLeft);
                idFieldNameList_right.Add(classMetaData[typeof(T4)].primaryKeyRight);
                joinTypeList.Add(classMetaData[typeof(T4)].joinType);
            }

            tableName = tableNameList.ToArray();
            idFieldName = idFieldNameList.ToArray();
            idFieldName_right = idFieldNameList_right.ToArray();
            joinType = joinTypeList.ToArray();
            if (adjuster == null)
            {
            }
            else
            {
                adjuster(tableName, idFieldName, idFieldName_right, joinType);
            }
            object test;
            if (aQueryRelatedArgs.TryGetValue("tableName", out test) == false) 
            
            
            {
                aQueryRelatedArgs.Add("tableName", tableName); 
            
            }
            if (aQueryRelatedArgs.TryGetValue("idFieldName", out test) == false)
            {
                aQueryRelatedArgs.Add("idFieldName", idFieldName);
            }
            if (aQueryRelatedArgs.TryGetValue("idFieldName_right", out test) == false) aQueryRelatedArgs.Add("idFieldName_right", idFieldName_right);
            if (aQueryRelatedArgs.TryGetValue("joinType", out test) == false) aQueryRelatedArgs.Add("joinType", joinType);
         
        }
        
        //public GenericContr(IGenericCRUD<T> iaddprod)
        //{
        //    //_IProduct=iproduct;
        //    _iaddprod = iaddprod;
        //}

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
            string[] idFieldName_right;
            getDelegateTableNamesAndFieldNames(out dl,QueryRelatedArgs);
            IEnumerable<T> p = _iaddprod.Get<T2, T3>(QueryRelatedArgs, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID3()
        {


            string[] tableName;
            string[] idFieldName ;
            string[] idFieldName_right;
            Func<T, T2,  T> dl;
            getDelegateTableNamesAndFieldNames2(out dl, QueryRelatedArgs);

            IEnumerable<T> p = _iaddprod.Get(QueryRelatedArgs, dl);
            return p;
        }
        private IEnumerable<T> ObtainProductByID2()
        {
            Func<T, T2, T3, T> dl; 
            string[] tableName;
            string[] idFieldName;
            string[] idFieldName_right;
            getDelegateTableNamesAndFieldNames(out dl, QueryRelatedArgs);
            IEnumerable<T> p = _iaddprod.Get(((string[])QueryRelatedArgs["tableName"])[0], ((string[])QueryRelatedArgs["idFieldName"])[0]);
            return p; 
        }


        protected T ObtainProductByID_FINAL(int id, dynamic dl, Dictionary<string, object> QueryRelatedArgs, int numberofGenerics)
        {
            if (numberofGenerics == 3)
            {
                return ObtainProductByID(id, dl, QueryRelatedArgs);
            }
            if (numberofGenerics == 1)
            {
                return ObtainProductByID2(id, dl, QueryRelatedArgs);
            }
            // it's 2 generics
            return ObtainProductByID3(id, dl, QueryRelatedArgs);

        }

        protected T ObtainProductByID(int id, Func<T, T2, T3, T> dl, Dictionary<string, object> QueryRelatedArgs)
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories", "suppliers" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(((string[])QueryRelatedArgs["idFieldName"])[0], id);
            //MyType param = new MyType { ProductID = id };
            //Func<T, T2, T3, T> dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => {
            //    prod.Category = cat; prod.Supplier = Supplier; return prod; 
            //});

            //T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2);
            T p = _iaddprod.Get(id, QueryRelatedArgs, param, dl);
            return p;
        }
        protected T ObtainProductByID3(int id, Func<T, T2, T> dl, Dictionary<string, object> QueryRelatedArgs)
        {
            //id = -1;
            //string[] tableName = new string[] { "products", "categories" };
            //string[] idFieldName = new string[] { "ProductID", "CategoryID" };
            
            //Func<T, T2, T> dl = new Func<T, T2,  T>((prod, cat) => { prod.Category = cat; return prod; });

            //Func<T, T2,  T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames2(out dl, QueryRelatedArgs);
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(((string[])QueryRelatedArgs["idFieldName"])[0], id);
            T p = _iaddprod.Get(id, QueryRelatedArgs, param, dl);
            return p;
        }
        protected T ObtainProductByID2(int id, Func<T, T2, T> dl, Dictionary<string, object> QueryRelatedArgs)
        {
            //Func<T, T2, T3, T> dl; string[] tableName; string[] idFieldName;
            getDelegateTableNamesAndFieldNames2(out dl, QueryRelatedArgs);
            //string tableName = "products";
            //string idFieldName = "ProductID";
            //object param = new { ProductID = id };
            dynamic param = returnCriteriaParam(((string[])QueryRelatedArgs["idFieldName"])[0], id); 
            T p = _iaddprod.Get(id, ((string[])QueryRelatedArgs["tableName"])[0], ((string[])QueryRelatedArgs["idFieldName"])[0], param);
            return p;
        }
        protected void getDelegateTableNamesAndFieldNames_final(out dynamic dl, Dictionary<string, object> QueryRelatedArg, int numberofGenerics)
        {
            if (numberofGenerics == 3)
            {
                Func<T, T2, T3, T> dl_ ;
                getDelegateTableNamesAndFieldNames(out dl_, QueryRelatedArg);
                dl = dl_;
                return;
            }
            //if (numberofGenerics == 3)
            //{
                Func<T, T2,  T> dl__ ;
                getDelegateTableNamesAndFieldNames2(out dl__, QueryRelatedArg);
                dl = dl__;
            //}
 
        }
        protected void getDelegateTableNamesAndFieldNames(out Func<T, T2, T3, T> dl, Dictionary<string, object> QueryRelatedArgs)
        {
            returnTableNmaesAndIDsBasedOnGeneric(3, QueryRelatedArgs);
             PropertyContainer[] arr = ReturnallPropertyContainer(typeof(T), typeof(T2), typeof(T3));
             string prop2 = (arr[0]).FindPropertyNameOFGivenType(typeof(T2), ((string[])QueryRelatedArgs["idFieldName"])[0]);
             string prop3 = (arr[0]).FindPropertyNameOFGivenType(typeof(T3), ((string[])QueryRelatedArgs["idFieldName"])[0]);

             string prop2_enum = (arr[0]).FindIEnumerabkePropertyNameOFGivenType<T2>( ((string[])QueryRelatedArgs["idFieldName"])[0]);
             string prop3_enum = (arr[0]).FindIEnumerabkePropertyNameOFGivenType<T3>(((string[])QueryRelatedArgs["idFieldName"])[0]);

             Func<T, IList<T2>> childSelector2 = new Func<T, IList<T2>>(a =>
             {
                 IList<T2> il = (IList<T2>)typeof(T).GetProperty(prop2_enum).GetValue(a, null);
                 if (il == null)
                 {
                     il = new List<T2>();
                     typeof(T).GetProperty(prop2_enum).SetValue(a, il);
                 }
                 return il;
             });
             Func<T, IList<T3>> childSelector3 = new Func<T, IList<T3>>(a =>
             {
                 IList<T3> il = (IList<T3>)typeof(T).GetProperty(prop3_enum).GetValue(a, null);
                 if (il == null)
                 {
                     il = new List<T3>();
                     typeof(T).GetProperty(prop3_enum).SetValue(a, il);
                 }
                 return il;
             });

             Dictionary<int, T> cache = new Dictionary<int, T>();

             string PrimaryKey = (string)((string[])QueryRelatedArgs["idFieldName"])[0];
             Type t = typeof(T).GetProperty(PrimaryKey).GetType();
             //Dictionary<t, T> test = new Dictionary<t, T>();

             Func<T, int> parentKeySelector = new Func<T, int>(a => { return (int)typeof(T).GetProperty(PrimaryKey).GetValue(a, null); });
             dl = new Func<T, T2, T3, T>((prod, cat, Supplier) => {

                 if (!cache.ContainsKey(parentKeySelector(prod)))
                 {
                     cache.Add(parentKeySelector(prod), prod);
                 }
                 T cachedParent = cache[parentKeySelector(prod)];

                 if (!(prop2 == null))
                 {
                     cachedParent.GetType().GetProperty(prop2).SetValue(cachedParent, cat);
                 }
                 else
                 {

                     IList<T2> children = childSelector2(cachedParent);
                     children.Add(cat);

                 }
                 if (!(prop3 == null))
                 {
                     cachedParent.GetType().GetProperty(prop3).SetValue(cachedParent, Supplier);
                 }
                 else
                 {
                     IList<T3> children = childSelector3(cachedParent);
                     children.Add(Supplier);
                 }

                 return cachedParent;
             });
             QueryRelatedArgs.Add("DictionaryOfKesAndResults",cache);
             //tableName = new string[] { "products", "categories","suppliers" };
             //idFieldName = new string[] { "ProductID", "CategoryID", "SupplierID" };
            
        }
        protected void getDelegateTableNamesAndFieldNames2(out Func<T, T2, T> dl, Dictionary<string, object> QueryRelatedArgs)
        {
            returnTableNmaesAndIDsBasedOnGeneric(2, QueryRelatedArgs);
            PropertyContainer[] arr = ReturnallPropertyContainer(typeof(T), typeof(T2), typeof(T3));
            string prop2 = (arr[0]).FindPropertyNameOFGivenType(typeof(T2), ((string[])QueryRelatedArgs["idFieldName"])[0]);


            string prop2_enum = (arr[0]).FindIEnumerabkePropertyNameOFGivenType<T2>(((string[])QueryRelatedArgs["idFieldName"])[0]);


            Func<T, IList<T2>> childSelector2 = new Func<T, IList<T2>>(a =>
            {
                IList<T2> il = (IList<T2>)typeof(T).GetProperty(prop2_enum).GetValue(a, null);
                if (il == null)
                {
                    il = new List<T2>();
                    typeof(T).GetProperty(prop2_enum).SetValue(a, il);
                }
                return il;
            });



            Dictionary<int, T> cache = new Dictionary<int, T>();

            string PrimaryKey = (string)((string[])QueryRelatedArgs["idFieldName"])[0];

            Func<T, int> parentKeySelector = new Func<T, int>(a => { return (int)typeof(T).GetProperty(PrimaryKey).GetValue(a, null); });
            dl = new Func<T, T2,  T>((prod, cat) =>
            {

                if (!cache.ContainsKey(parentKeySelector(prod)))
                {
                    cache.Add(parentKeySelector(prod), prod);
                }
                T cachedParent = cache[parentKeySelector(prod)];

                if (!(prop2 == null))
                {
                    cachedParent.GetType().GetProperty(prop2).SetValue(cachedParent, cat);
                }
                else
                {

                    IList<T2> children = childSelector2(cachedParent);
                    children.Add(cat);

                }

                return cachedParent;
            });
            object test;
            if (QueryRelatedArgs.TryGetValue("DictionaryOfKesAndResults", out test) == false) 
            { 
            QueryRelatedArgs.Add("DictionaryOfKesAndResults", cache);
        }

        }
        protected void getDelegateTableNamesAndFieldNames2_old(out Func<T, T2, T> dl, Dictionary<string,object> QueryRelatedArgs)
        {
            returnTableNmaesAndIDsBasedOnGeneric(2, QueryRelatedArgs);
            PropertyContainer[] arr = ReturnallPropertyContainer(typeof(T), typeof(T2));
            string prop2 = (arr[0]).FindPropertyNameOFGivenType(typeof(T2), ((string[])QueryRelatedArgs["idFieldName"])[0]);

            dl = new Func<T, T2,  T>((prod, cat) => 
            {
                prod.GetType().GetProperty(prop2).SetValue(prod, cat);
                //prod.Category = cat; 
                return prod; 
            });
            //tableName = new string[] { "products", "categories" };
            //idFieldName = new string[] { "ProductID", "CategoryID" };
            
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
        public static PropertyContainer ParseProperties(Type obj)
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
        public  dynamic returnCriteriaParam(string propname,int id)
        {
            List<PropertyNameAndType> dict = new List<PropertyNameAndType>();
            dict.Add(new PropertyNameAndType() { Name = propname, Type_ = typeof(int), value = id });
            DynamicClass a = new DynamicClass(dict);
            return a.resultObject;
        }


        virtual public T Post(int productID, T value)
        {
            return Post(value);
        }

        virtual public T Put(int productID, T value)
        {
            return Put(value);
        }
    }
    public class ClassTypeMetaData
    {
        public string tableName { get; set; }
        public string primaryKeyLeft { get; set; }// this is the key on the dispayed object which is the left side of an inner join so to speak
        public string primaryKeyRight { get; set; }// this is the key on the right side of an inner join so to speak
        public string WhatISMyObjectPropertyWithinResults { get; set; }
        public string joinType { get; set; }

    }
}