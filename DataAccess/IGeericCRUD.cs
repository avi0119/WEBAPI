using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGeericCRUD<T>
    {

        int Add(T p,string tableName);
        int UpdateItem(T p, string tableName, string idFieldName);
        bool DeleteItem(T p, string tableName);
        T Get(int prodid, string tableName);
    }
}
