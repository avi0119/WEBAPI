using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenericCRUD<T>
    {

        int Add(T p, string tableName);
        int UpdateItem(T p, string tableName, string idFieldName);
        bool DeleteItem(T p, string tableName);
        T Get<T2, T3,T4>(int prodid, string[] tableName, string[] idFieldName,  string[] idFieldName_right, dynamic param, Func<T, T2, T3,T4, T> dl2);
        T Get<T2, T3>(int prodid, string[] tableName, string[] idFieldName,  string[] idFieldName_right, dynamic param, Func<T, T2, T3, T> dl2);
        T Get<T2>(int prodid, string[] tableName, string[] idFieldName,  string[] idFieldName_right, dynamic param, Func<T, T2, T> dl2);
        T Get(int prodid, string tableName, string idFieldName, dynamic param);
        T ReturnAllOrders<T2>(int prodid, string[] tableName, string[] idFieldName,  string[] idFieldName_right, object param);



        IEnumerable<T> Get<T2, T3,T4>(string[] tableName, string[] idFieldName,  string[] idFieldName_right, Func<T, T2, T3,T4, T> dl2);
        IEnumerable<T> Get<T2, T3>( string[] tableName, string[] idFieldName,  string[] idFieldName_right, Func<T, T2, T3, T> dl2);
        IEnumerable<T> Get<T2>( string[] tableName, string[] idFieldName,  string[] idFieldName_right, Func<T, T2, T> dl2);
        IEnumerable<T>  Get( string tableName, string idFieldName);
    }
}
