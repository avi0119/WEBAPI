using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class IenumerableOrderDetail:IEnumerable<OrderDetail>
    {
        List<OrderDetail> od = new List<OrderDetail>();
        public List<OrderDetail> list
        {
            set
            {
                od = value;
            }
        }
        public IenumerableOrderDetail()
        {

        }
        public IEnumerator<OrderDetail> GetEnumerator()
        {
           return  od.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return  ((System.Collections.IEnumerable)od).GetEnumerator();
        }
    }
}
