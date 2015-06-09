using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenericCRUD<T, T0>:IDisposable
    {

        int Add(T p, string tableName, bool returnint=true);
        List<T> Add(List<T> list, string tableName,string idFieldName);
        int UpdateItem(T p, string tableName, string idFieldName);
        bool DeleteItem(T p, string tableName);
        T Get<T2, T3,T4>(int prodid, Dictionary<string, object> QueryRelatedArgs, dynamic param, Func<T, T2, T3,T4, T> dl2);
        T Get<T2, T3>(int prodid, Dictionary<string, object> QueryRelatedArgs, dynamic param, Func<T, T2, T3, T> dl2);
        T Get<T2>(int prodid, Dictionary<string, object> QueryRelatedArgs, dynamic param, Func<T, T2, T> dl2);
        T Get(int prodid, string tableName, string idFieldName, dynamic param);
        T ReturnAllOrders<T2>(int prodid, Dictionary<string, object> QueryRelatedArgs, object param);



        IEnumerable<T> Get<T2, T3,T4>(Dictionary<string, object> QueryRelatedArgs, Func<T, T2, T3,T4, T> dl2);
        IEnumerable<T> Get<T2, T3>( Dictionary<string, object> QueryRelatedArgs, Func<T, T2, T3, T> dl2);
        IEnumerable<T> Get<T2>( Dictionary<string, object> QueryRelatedArgs, Func<T, T2, T> dl2);
        IEnumerable<T>  Get( string tableName, string idFieldName);
    }
}
