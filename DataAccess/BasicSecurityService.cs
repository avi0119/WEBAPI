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
            switch (username)
            {
                case "avisemah@gmail.com":
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.Manager));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
                    break;
                case "jbob":
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
                    break;
                case "jdoe":
                    identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
                    break;
                default:
                    return null;
            }
            return new ClaimsPrincipal(identity);
        }
        public virtual User GetUser(string username,string pw)
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
