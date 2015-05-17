using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;


namespace DataAccess
{
    public interface IProduct
    {
         Product GetProduct(int ProductID);
    }
    public class ProductGetter:IProduct
    {
        ITrans _trans;
        public ProductGetter(ITrans Trans)
        {
            _trans = Trans;
        }
        public Product GetProduct(int prodid)
        {
            SqlConnection conn =  _trans.Connection;
            //var res = conn.Query<Product>("SELECT *  FROM [Northwind].[dbo].[Products] where [ProductID]=@ProductID", new { ProductID = prodid }, _trans.Transx);
            string sql = "SELECT *  FROM [Northwind].[dbo].[Products] p inner join [Northwind].[dbo].[categories] c on p.CategoryID=c.CategoryID inner join [Northwind].[dbo].[suppliers] s on p.supplierid=s.supplierid  where [ProductID]=@ProductID";

            var res = conn.Query<Product, Category, Supplier, Product>(sql, (prod, cat, Supplier) => { prod.Category = cat; prod.Supplier = Supplier; return prod; }, new { ProductID = prodid }, _trans.Transx, splitOn: "CategoryID,supplierid");
            return res.SingleOrDefault();
           

        }
    }
}
