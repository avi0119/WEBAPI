using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Double Discount { get; set; }

        // reference 
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
