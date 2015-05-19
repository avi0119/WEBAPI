using System;
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

            //return p.ProductId;

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



        public T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName, object param, Func<T, T2, T3, T> dl2)
        {



            //param = new  { ProductID = prodid };

            var filteredItems = idFieldName.Where((p, i) => i > 0);

            var splitOn = String.Join(",", (filteredItems.ToArray<string>()));



            SqlConnection conn = _trans.Connection;

            //var res = conn.Query<Product>("SELECT *  FROM [Northwind].[dbo].[Products] where [ProductID]=@ProductID", new { ProductID = prodid }, _trans.Transx);

            string sql = string.Format("SELECT *  FROM [{0}] p inner join [{2}] c on p.{3}=c.{3} inner join [{4}] s on p.{5}=s.{5}  where [{1}]=@{1}", tableName[0], idFieldName[0], tableName[1], idFieldName[1], tableName[2], idFieldName[2]);



            //var res = conn.Query<Product, Category, Supplier, Product>(sql, (prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; }, new { ProductID = prodid }, _trans.Transx, splitOn: "CategoryID,supplierid");

            //Func<Product, Category, Supplier, Product> dl2 = new Func<Product,Category, Supplier, Product>((prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; });

            var res = conn.Query<T, T2, T3, T>(sql, dl2, param, _trans.Transx, splitOn: splitOn);

            return res.SingleOrDefault();

        }



        private static PropertyContainer ParseProperties(T obj)
        {

            var propertyContainer = new PropertyContainer();



            var typeName = typeof(T).Name;

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

    }

}


