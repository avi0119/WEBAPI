using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Common;

namespace ModelObjects
{
    public class CartObject
    {
        public IEnumerable<ProductEntry> products { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string zip  { get; set; }
        public string state { get; set; }
        public string country { get; set; }


        public List<OrderDetail> EntityOrederDetails(int orderid)
        {
            List<OrderDetail> ret = new List<OrderDetail>();




            foreach (ProductEntry pe in products)
            {
                OrderDetail od = new OrderDetail();
                od.OrderID = orderid;
                od.Quantity = pe.count;
                od.ProductID = pe.ProductID;
                od.UnitPrice = pe.UnitPrice;
                ret.Add(od);

            }

            return ret;
        }
        public Order EntityOrder( DateTime orddate)
        {
            
            Order o= new Order();
            //o.CustomerID = customerID;
            o.OrderDate = orddate;
            o.RequiredDate = o.ShippedDate = orddate; ;
            o.ShipAddress = this.street;
            o.ShipName = this.name;
            o.ShipCountry = this.country;
          
           

            return o;
        }
      
    }
}
