

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
    public interface IProductCRUD
    {
        int ProductAdd(Product p);
        int ProductUpdate(Product p);
        bool ProductDelete(Product p);
        Product GetProduct(int prodid);
    }


}
