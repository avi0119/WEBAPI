using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

using System.Data;

namespace DataAccess
{
    public class ProductCRUD : IProductCRUD
    {
        ITrans _trans;
        public ProductCRUD(ITrans Trans)
        {
            _trans = Trans;
        }
        public int ProductAdd(Product p)
        {
            var id = Insert<Product>(p, "products");
            //return GetProduct(id);
            return id;

        }

        public int ProductUpdate(Product p)
        {
            Update(p, "products");
            return p.ProductId;
        }

        public bool ProductDelete(Product p)
        {


            try
            {
                Delete(p, "products");
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        /// <summary>
        /// Automatic generation of SELECT statement, BUT only for simple equality criterias!
        /// Example: Select<LogItem>(new {Class = "Client"})
        /// For more complex criteria it is necessary to call GetItems method with custom SQL statement.
        /// </summary>
        protected IEnumerable<T> Select<T>(object criteria = null, string tableName = null)
        {
            var properties = ParseProperties(criteria);
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", tableName, sqlPairs);
            return GetItems<T>(CommandType.Text, sql, properties.AllPairs);
        }



        protected void Update<T>(T obj, string tableName)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}]   SET {1} WHERE {2}", tableName, sqlValuePairs, sqlIdPairs);
            Execute(CommandType.Text, sql, propertyContainer.AllPairs);
        }

        protected void Delete<T>(T obj, string tableNAme)
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", tableNAme, sqlIdPairs);
            Execute(CommandType.Text, sql, propertyContainer.IdPairs);

        }
        // Example: GetBySql<Activity>( "SELECT * 
        //FROM Activities WHERE Id = @activityId", new {activityId = 15} ).FirstOrDefault();
        protected IEnumerable<T> GetItems<T>(CommandType commandType, string sql, object parameters = null)
        {
            //using (var connection = GetOpenConnection())
            //{
            return _trans.Connection.Query<T>(sql, parameters, _trans.Transx, commandType: commandType);
            //}
        }

        protected int Execute(CommandType commandType, string sql, object parameters = null)
        {
            //using (var connection = GetOpenConnection())
            //{
            return _trans.Connection.Execute(sql, parameters, _trans.Transx, commandType: commandType);
            //}
        }
        protected int Insert<T>(T obj, string tableNAme)
        {
            var propertyContainer = ParseProperties(obj);
            var sql = string.Format("INSERT INTO [{0}] ({1})   VALUES (@{2}) SELECT CAST(scope_identity() AS int)", tableNAme, string.Join(", ", propertyContainer.ValueNames), string.Join(", @", propertyContainer.ValueNames));

            //using (var connection = GetOpenConnection())
            //{
            //var id = _trans.Connection.Query<int> (sql, propertyContainer.ValuePairs, commandType: CommandType.Text).First();

            var id = _trans.Connection.Query<int>(sql, propertyContainer.ValuePairs, _trans.Transx, commandType: CommandType.Text).First();
            return id;

            //SetId(obj, id, propertyContainer.IdPairs);
            //}
        }

        /// <summary>
        /// Create a commaseparated list of value pairs on 
        /// the form: "key1=@value1, key2=@value2, ..."
        /// </summary>
        private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
        {
            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        private void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
        {
            if (propertyPairs.Count == 1)
            {
                var propertyName = propertyPairs.Keys.First();
                var propertyInfo = obj.GetType().GetProperty(propertyName);
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(obj, id, null);
                }
            }
        }



        public Product GetProduct(int prodid)
        {
            SqlConnection conn = _trans.Connection;
            //var res = conn.Query<Product>("SELECT *  FROM [Northwind].[dbo].[Products] where [ProductID]=@ProductID", new { ProductID = prodid }, _trans.Transx);
            string sql = "SELECT *  FROM [Northwind].[dbo].[Products] p inner join [Northwind].[dbo].[categories] c on p.CategoryID=c.CategoryID inner join [Northwind].[dbo].[suppliers] s on p.supplierid=s.supplierid  where [ProductID]=@ProductID";

            var res = conn.Query<Product, Category, Supplier, Product>(sql, (prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; }, new { ProductID = prodid }, _trans.Transx, splitOn: "CategoryID,supplierid");
            return res.SingleOrDefault();


        }

        private static PropertyContainer ParseProperties<T>(T obj)
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


    }
}
