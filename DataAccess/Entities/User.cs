using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class User
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        [Encrypted]
        public string username { get; set; }
        [Encrypted]
        public string password { get; set; }
        public int UserID { get; set; }

        public IEnumerable<DBClaim> DBClaims { get; set; }
    }
}
