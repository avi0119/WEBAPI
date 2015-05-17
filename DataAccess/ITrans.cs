using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface ITrans
    {


         void BeginTransaction();

         void EndTransaction();


         SqlTransaction Transx { get;  }
         SqlConnection Connection { get; }



         SqlCommand ReturnBlankCommand();
 


         bool CloseConnection();
  
    }
}
