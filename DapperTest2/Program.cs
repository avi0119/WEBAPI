using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Dynamic;


namespace DapperTest2
{
    class Product2
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        // reference 
        public Supplier Supplier { get; set; }
    }
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
    class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }

    class Customer
    {
        public string CustomerId { get; set; }
        public string City { get; set; }
    }

    
 
    class Program
    {
        private static void testcreateNewDynamic()
        {
            List<PropertyNameAndType> dict = new List<PropertyNameAndType>();
            dict.Add(new PropertyNameAndType() { Name="ProductID",Type_=typeof(int),value=1});
            DynamicClass a = new DynamicClass(dict);
            var f = 6;
            var t = f;
        }
        private static void test()
        {
            dynamic sampleObject = new ExpandoObject();
            sampleObject.test = "Dynamic Property";
            Console.WriteLine(sampleObject.test);
            Console.WriteLine(sampleObject.test.GetType());
            // This code example produces the following output: 
            // Dynamic Property 
            // System.String
        }
        static void Main(string[] args)
        {
            testcreateNewDynamic();
            //test();
            //var yourListOfFields = new[] { new { FieldName = "prop1", FieldType = typeof(string) }, new { FieldName = "prop2", FieldType = typeof(string) } };
            var yourListOfFields = new PropertyNameAndType[] { new PropertyNameAndType() { Name = "prop1", Type_ = typeof(string) }, new PropertyNameAndType() { Name = "prop2", Type_ = typeof(string) } };
            var z = MyTypeBuilder.CompileResultType(yourListOfFields, "MyDynamicType");
            var z2 = Activator.CreateInstance(z);
            z2.GetType().GetProperty("prop1").SetValue(z2, "Bob", null);
            var res = z2.GetType().GetProperty("prop1").GetValue(z2,null);
 
            //(MyDynamicType)z2.prop1 = "hello";
            Customer c = new Customer() { CustomerId = "1", City = "TA" };
            var d = ParseProperties(c);
            //using (var conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI;"))
                using (var conn = new SqlConnection("server=OWNER\\SQLEXPRESS;Trusted_Connection=yes;Integrated Security=SSPI;database=Northwind;connection timeout=30"))
            {
                conn.Open();
                //MultipleObjectsExamp(conn);
                ParameterizedQuery2(conn);
                var products = conn.Query<Product>("SELECT * FROM Products");
            }
        }

        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
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
        static void ParameterizedQuery2(SqlConnection conn)
        {
            using (SqlTransaction t = conn.BeginTransaction("InsertIntoUsers"))
            {
  

                    var res = conn.Query<Customer>("SELECT * FROM Customers WHERE Country = @Country   AND ContactTitle = @ContactTitle", new { Country = "Canada", ContactTitle = "Marketing Assistant" }, t);
                    //d.CommandText = "SELECT * FROM Customers WHERE Country = @Country   AND ContactTitle = @ContactTitle";
                    //d.CommandType = System.Data.CommandType.Text;
                    //d.Connection = conn;
                    //d.Transaction = t;
                    //var tres = d.ExecuteReader();
                
                    t.Commit();
            }
            //var customers = conn.Query("SELECT * FROM Customers WHERE Country = @Country   AND ContactTitle = @ContactTitle", new { Country = "Canada", ContactTitle = "Marketing Assistant" });

        }
        static void ParameterizedQuery(SqlConnection conn)
        {

            var customers = conn.Query("SELECT * FROM Customers WHERE Country = @Country   AND ContactTitle = @ContactTitle", new { Country = "Canada", ContactTitle = "Marketing Assistant" });

        }
        static void MultipleObjectsExamp(SqlConnection conn)
        {
            var sql =    @"SELECT * FROM        Orders o        INNER JOIN Customers c            ON c.CustomerID = o.CustomerID    WHERE        c.ContactName = 'Bernardo Batista'";

            var orders = conn.Query<Order, Customer, Order>(sql, (order, customer) => { order.Customer = customer; return order; }, splitOn: "CustomerID");
 
            var firstOrder = orders.First();
 
            Console.WriteLine("Order date: {0}", firstOrder.OrderDate.ToShortDateString());
 
            Console.WriteLine("Customer city: {0}", firstOrder.Customer.City);
        }
        static void InsertAndGetID(SqlConnection conn)
        {

            string sqlQuery = "INSERT INTO [dbo].[Customer]([FirstName],[LastName],[Address],[City]) VALUES (@FirstName,@LastName,@Address,@City)";
            conn.Execute(sqlQuery,
                new
                {
                    //customerEntity.FirstName,
                    //customerEntity.LastName,
                    //customerEntity.Address,
                    //customerEntity.City
                });


        }
    }

    public class PropertyContainer
    {
        private readonly Dictionary<string, object> _ids;
        private readonly Dictionary<string, object> _values;

        #region Properties

        internal IEnumerable<string> IdNames
        {
            get { return _ids.Keys; }
        }

        internal IEnumerable<string> ValueNames
        {
            get { return _values.Keys; }
        }

        internal IEnumerable<string> AllNames
        {
            get { return _ids.Keys.Union(_values.Keys); }
        }

        internal IDictionary<string, object> IdPairs
        {
            get { return _ids; }
        }

        internal IDictionary<string, object> ValuePairs
        {
            get { return _values; }
        }

        internal IEnumerable<KeyValuePair<string, object>> AllPairs
        {
            get { return _ids.Concat(_values); }
        }

        #endregion

        #region Constructor

        internal PropertyContainer()
        {
            _ids = new Dictionary<string, object>();
            _values = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        internal void AddId(string name, object value)
        {
            _ids.Add(name, value);
        }

        internal void AddValue(string name, object value)
        {
            _values.Add(name, value);
        }

        #endregion
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperIgnore : Attribute

    {

    }


    [AttributeUsage(AttributeTargets.Property)]
    public class DapperKey : Attribute
    {
    }

}
