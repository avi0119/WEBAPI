using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebAPI
{
    public interface IGenericController<T0,T, T2, T3, T4>
 {



          IEnumerable<T> Get();






         T Get(int productID);



 

         bool Delete(int productID);






         T Post(T value);
         T Post(int productID, T value);






         T Put(T value);

         T Put(int productID, T value);
         
         



    }
}
