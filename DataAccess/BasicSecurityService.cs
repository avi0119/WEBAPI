using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;
using System.Security.Principal;
using System.Web;
using System.Security.Claims;

namespace DataAccess
{
    public class BasicSecurityService : IBasicSecurityService,IDisposable
    {
        IGenericCRUD<User, int> _useraccessor;
        //private readonly ILog _log;
        public BasicSecurityService(IGenericCRUD<User,int> useraccessor) 
        {
            //_log = logManager.GetLog(typeof(BasicSecurityService));
            _useraccessor = useraccessor;
        }
        public virtual ITrans Session
        {
            get { return WebContainerManager.Get<ITrans>(); }
        }
        public bool SetPrincipal(string username, string password)
        {
            var user = GetUser(username, password);
            IPrincipal principal = null;
            if (user == null || (principal = GetPrincipal(user)) == null)
            {
                //_log.DebugFormat("System could not validate user {0}", username);
                return false;
            }
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
            return true;
        }

        public virtual IPrincipal GetPrincipal(User user)
        {
            var identity = new GenericIdentity(user.username, Constants.SchemeTypes.Basic);
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.firstname));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.lastname));
            var username = user.username.ToLowerInvariant();
            if (!(user.DBClaims == null)) {
                foreach (DBClaim c in user.DBClaims)
                {
                    string currClaim = c.Description;
                    identity.AddClaim(new Claim(ClaimTypes.Role, currClaim));
                }
            }

            //switch (username)
            //{
            //    case "avisemah@gmail.com":
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.Manager));
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
            //        break;
            //    case "jbob":
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
            //        break;
            //    case "jdoe":
            //        identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
            //        break;
            //    default:
            //        return null;
            //}
            return new ClaimsPrincipal(identity);
        }
        public User GetUser(string username, string pw)
        {
            username = username.ToLowerInvariant();
            string sql = string.Format("SELECT p.*,c.*  FROM [users] p inner join [viewUserClaimsAndDescription] c on p.UserID=c.UserID   where p.username=@username and p.password=@password");
            
            //dynamic param = new { username = username };
            //var user = _useraccessor.Get(0, "Users", "username", param);
            string splitOn = "UserID";
            string PrimaryKey="UserID";
            string propertyname2="DBClaims";
            string encryptedusername = EncryptionOfDatabaseStoreValues.EncryptThisString (username);
            ;
            string encryptedpw = EncryptionOfDatabaseStoreValues.EncryptThisString(pw);
            dynamic param = new { username = encryptedusername, password = encryptedpw };
            var user = _useraccessor.ReturnRecordAndItsChildren_givenSQL<DBClaim>(sql, splitOn, PrimaryKey, propertyname2, param);
            //_useraccessor.Dispose();
            //return  Session.QueryOver<User>().Where(x => x.Username == username).SingleOrDefault();
            return user;
        
        
        }
        public virtual User GetUser_old(string username,string pw)
        {
            username = username.ToLowerInvariant();
            dynamic param = new { username = username };
            var user=_useraccessor.Get(0, "Users", "username", param);
            //_useraccessor.Dispose();
            //return  Session.QueryOver<User>().Where(x => x.Username == username).SingleOrDefault();
            return user;
        }

        public void Dispose()
        {
           
        }
    }

}
