﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

using System.Data;
using System.Reflection;

namespace DataAccess
{

    public class GemericCRUD<T> : IGenericCRUD<T>
    {



        ITrans _trans;

        public GemericCRUD(ITrans Trans)
        {

            _trans = Trans;

        }



        public int Add(T p, string tableName)
        {

            var id = Insert(p, tableName);



            return id;

        }


        protected int Insert(T obj, string tableNAme)
        {

            var propertyContainer = ParseProperties(obj);

            var sql = string.Format("INSERT INTO [{0}] ({1})   VALUES (@{2}) SELECT CAST(scope_identity() AS int)", tableNAme, string.Join(", ", propertyContainer.ValueNames), string.Join(", @", propertyContainer.ValueNames));

            var id = _trans.Connection.Query<int>(sql, propertyContainer.ValuePairs, _trans.Transx, commandType: CommandType.Text).First();

            return id;

        }



        public int UpdateItem(T p, string tableName, string idFieldName)
        {
            Update(p, tableName);
            return (int)GetPropValue(p, idFieldName);
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        protected void Update(T obj, string tableName)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}]   SET {1} WHERE {2}", tableName, sqlValuePairs, sqlIdPairs);
            Execute(CommandType.Text, sql, propertyContainer.AllPairs);
        }

        public bool DeleteItem(T p, string tableName)
        {

            try
            {
                Delete(p, tableName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        protected void Delete(T obj, string tableNAme)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", tableNAme, sqlIdPairs);
            Execute(CommandType.Text, sql, propertyContainer.IdPairs);
        }
        public IEnumerable<T> Get<T2, T3, T4>(Dictionary<string, object> dictArgs, Func<T, T2, T3, T4, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];


            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();
            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));

            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p inner join [{1}] c on p.{2}=c.{7} {10} [{3}] s on p.{4}=s.{8}    {11} [{5}] v on p.{6}=v.{9}  ", tableName[0], tableName[1], idFieldName[1], tableName[2], idFieldName[2], tableName[3], idFieldName[3], idFieldName_right[1], idFieldName_right[2], idFieldName_right[3], joinType[1], joinType[2]);
            var res = conn.Query<T, T2, T3,T4, T>(sql, dl2, transaction: _trans.Transx, splitOn: splitOn);
            return res;
        }
        public T Get<T2, T3, T4>(int prodid, Dictionary<string, object> dictArgs, object param, Func<T, T2, T3, T4, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];
            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();
            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));
            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p {11} [{2}] c on p.{3}=c.{8} {12} [{4}] s on p.{5}=s.{9}    {13} [{6}] v on p.{7}=v.{10}  where p.[{1}]=@{1}", tableName[0], idFieldName[0], tableName[1], idFieldName[1], tableName[2], idFieldName[2], tableName[3], idFieldName[3], idFieldName_right[1], idFieldName_right[2], idFieldName_right[3], joinType[1], joinType[2], joinType[3]);
            var res = conn.Query<T, T2, T3, T4, T>(sql, dl2, param, _trans.Transx, splitOn: splitOn);

            return res.SingleOrDefault();

        }


        public T Get<T2>(int prodid, Dictionary<string, object> dictArgs, object param, Func<T, T2, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];
            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();

            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));

            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p {5} [{2}] c on p.{3}=c.{4}    where p.[{1}]=@{1}", tableName[0], idFieldName[0], tableName[1], idFieldName[1], idFieldName_right[1], joinType[1]);
            var res = conn.Query<T, T2,  T>(sql, dl2, param, _trans.Transx, splitOn: splitOn);

            return res.SingleOrDefault();
        }
        public IEnumerable<T> Get<T2>(Dictionary<string, object> dictArgs, Func<T, T2, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];
            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();
            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));

            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p {5} [{2}] c on p.{3}=c.{4}     ", tableName[0], idFieldName[0], tableName[1], idFieldName[1], idFieldName_right[1], joinType[1]);
            var res = conn.Query<T, T2, T>(sql, dl2, transaction: _trans.Transx, splitOn: splitOn);
            return res;


        }

        public T Get<T2, T3>(int prodid, Dictionary<string, object> dictArgs, object param, Func<T, T2, T3, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];

            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();
            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));

            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p {8} [{2}] c on p.{3}=c.{6} {9} [{4}] s on p.{5}=s.{7}  where p.[{1}]=@{1}", tableName[0], idFieldName[0], tableName[1], idFieldName[1], tableName[2], idFieldName[2], idFieldName_right[1], idFieldName_right[2], joinType[1], joinType[2]);
            var res = conn.Query<T, T2, T3, T>(sql, dl2, param, _trans.Transx, splitOn: splitOn);

            return res.SingleOrDefault();

        }


        public IEnumerable<T> Get<T2, T3>(Dictionary<string, object> dictArgs, Func<T, T2, T3, T> dl2)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName = (string[])dictArgs["idFieldName"];
            string[] idFieldName_right = (string[])dictArgs["idFieldName_right"];
            string[] joinType = (string[])dictArgs["joinType"];


            var filteredItems = idFieldName.Where((p, i) => i > 0);
            var filteredItems_right = idFieldName_right.Where((p, i) => i > 0).ToArray();
            var splitOn = String.Join(",", (filteredItems_right.ToArray<string>()));


            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p {7} [{1}] c on p.{2}=c.{5} {8} [{3}] s on p.{4}=s.{6}  ", tableName[0], tableName[1], idFieldName[1], tableName[2], idFieldName[2], idFieldName_right[1], idFieldName_right[2], joinType[1], joinType[2]);

            var res = conn.Query<T, T2, T3, T>(sql, dl2,  transaction:_trans.Transx, splitOn: splitOn);
            return res;


        }

        public T Get(int prodid, string tableName, string idFieldName, object param)
        {
            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p      where p.[{1}]=@{1}", tableName, idFieldName);
            var res = conn.Query<T>(sql, param, _trans.Transx);
            return res.SingleOrDefault();
        }
        public IEnumerable<T> Get(string tableName, string idFieldName)
        {
            SqlConnection conn = _trans.Connection;
            string sql = string.Format("SELECT *  FROM [{0}] p   ", tableName, idFieldName);
            var res = conn.Query<T>(sql, transaction: _trans.Transx);
            return res;
        }




        private static PropertyContainer ParseProperties(T obj)
        {

            var propertyContainer = new PropertyContainer();
            var typeName = typeof(T).Name;
            var validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) ,string.Format("{0}ID", typeName), string.Format("{0}_ID", typeName)};
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

                var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);



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

        protected int Execute(CommandType commandType, string sql, object parameters = null)
        {

            //using (var connection = GetOpenConnection())

            //{

            return _trans.Connection.Execute(sql, parameters, _trans.Transx, commandType: commandType);

            //}

        }

        private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
        {

            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();

            return string.Join(separator, pairs);

        }


        public T ReturnAllOrders<T2>(int prodid, Dictionary<string,object> dictArgs, object param)
        {
            string[] tableName = (string[])dictArgs["tableName"];
            string[] idFieldName=(string[])dictArgs["idFieldName"];
            string[] idFieldName_right=(string[])dictArgs["idFieldName_right"];

            //param = new  { ProductID = prodid };

            var filteredItems = idFieldName.Where((p, i) => i > 0);

            var splitOn = String.Join(",", (filteredItems.ToArray<string>()));



            SqlConnection conn = _trans.Connection;

            //var res = conn.Query<Product>("SELECT *  FROM [Northwind].[dbo].[Products] where [ProductID]=@ProductID", new { ProductID = prodid }, _trans.Transx);

            string sql = string.Format("SELECT p.*,c.*  FROM [{0}] p inner join [Order Details] c on p.{3}=c.{3}   where p.[{1}]=@{1}", tableName[0], idFieldName[0], tableName[1], idFieldName[0]);



            //var res = conn.Query<Product, Category, Supplier, Product>(sql, (prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; }, new { ProductID = prodid }, _trans.Transx, splitOn: "CategoryID,supplierid");

            //Func<Product, Category, Supplier, Product> dl2 = new Func<Product,Category, Supplier, Product>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });

            //var res = conn.Query<T, T2, T3, T4, T>(sql, dl2, param, _trans.Transx, splitOn: splitOn);
            string PrimaryKey = idFieldName[0];
            string propertyname2=typeof(T2).Name+ "s";
            Func<T, int> parentKeySelector = new Func<T, int>(a => { return (int)typeof(T).GetProperty(PrimaryKey).GetValue(a,null); });
            Func<T, IList<T2>> childSelector = new Func<T, IList<T2>>(a => { 
                                                                                IList<T2> il= (IList<T2>)typeof(T).GetProperty(propertyname2).GetValue(a,null);
                                                                                if (il == null)
                                                                                {
                                                                                    il = new List<T2>();
                                                                                    typeof(T).GetProperty(propertyname2).SetValue(a, il);
                                                                                }
                                                                                return il; 
                                                                            }); 

            var res2 = conn.QueryParentChild<T,T2,int>(sql,  parentKeySelector, childSelector, param, _trans.Transx, splitOn: splitOn );   
         
            return res2.SingleOrDefault();

        }








    }

    public static class DapperExtensions
    {
        public static IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(
            this IDbConnection connection,
            string sql,
            Func<TParent, TParentKey> parentKeySelector,
            Func<TParent, IList<TChild>> childSelector,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            connection.Query<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }
    } 

}


