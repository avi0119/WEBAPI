using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using  Dapper;

namespace DataAccess
{
    public class Transaction : ITrans
    {
        private SqlTransaction DbTrans;
        private SqlConnection conn;
        public Transaction()
        {
            var z = 1;
                var d=z;

        }
        public void BeginTransaction()
	    {

            conn = new SqlConnection("server=OWNER\\SQLEXPRESS;Trusted_Connection=yes;Integrated Security=SSPI;database=Northwind;connection timeout=30");
            conn.Open();
            var a = conn.BeginTransaction("SampleTransaction");
            DbTrans = a;
           

	    }

        public void EndTransaction()
        {

            try
            {
                DbTrans.Commit();
            }
            catch (Exception e)
            {
                DbTrans.Rollback();
            }

            

        }

        public SqlTransaction Transx
        { 
            get 
            {
                return DbTrans;
            }
        }
        public SqlConnection Connection
        {
            get
            {
                return conn;
            }
        }

        public SqlCommand ReturnBlankCommand () 
        {
            if (conn == null)
            {
                throw new Exception();
            }
            else
            {
                SqlCommand command = conn.CreateCommand();
                command.Transaction = DbTrans;
                return command;
            }
        }


        public bool CloseConnection()
        {
            bool ret;
            ret=true;
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                ret = false;
                
            }
            return ret;
        }

    }
}
