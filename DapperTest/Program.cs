using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

using System.Configuration;

using System.Data;


namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectAndQueryWithDapper();
        }

        static void ConnectAndQueryWithDapper()
        {

            string conString;

            conString = ConfigurationManager.ConnectionStrings["NWConnectiom"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {


                sqlConnection.Open();

                IEnumerable products = sqlConnection.Query("Select   [ProductID] ,[ProductName],[SupplierID],[CategoryID],[QuantityPerUnit] ,[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued] from  Products");

                foreach (var product in products)
                {
                    //ObjectDumper.Write(product);
                    Console.Write((Product)product);
                }

                sqlConnection.Close(); 


            }

            // string ConnectionString = new Configuration().ConnectionString;
            //using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            //{
            //    sqlConnection.Open();

            //    IEnumerable products =
            //        sqlConnection.Query("Selectom Products");

            //    foreach (Product product in products)
            //    {
            //        ObjectDumper.Write(product);
            //    }

            //    sqlConnection.Close();

        }
        static void ConnectAndQueryNoDapper()
        {


            string conString;

            conString = ConfigurationManager.ConnectionStrings["NWConnectiom"].ConnectionString;

            using (SqlConnection myConnection = new SqlConnection(conString))
            {
                string queryString = "select top 10 * from products";
                SqlCommand command = new SqlCommand(queryString, myConnection);
                command.Connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        // write the data on to the screen
                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}",
                            // call the objects from their index
                        reader[0], reader[1], reader[2], reader[3]));
                    }
                }

            }



        }

    }
}
