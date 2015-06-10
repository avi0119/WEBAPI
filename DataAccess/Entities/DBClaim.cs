using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBClaim
    {
        public int ClaimID { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }

        public DateTime StartDate { get; set; }

    }
}
