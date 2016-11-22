using System.Web;

namespace Walltage.Service.Wrappers
{
    public class SessionWrapper : ISessionWrapper
    {
        private const string UserIdKey = "UserId";
        private const string UserNameKey = "UserName";
        private const string UserEmailKey = "UserEmail";
        private readonly HttpContextBase _context;

        public SessionWrapper(HttpContextBase context)
        {
            _context = context;
        }

        public int UserId
        {
            get
            {
                if (!CheckSession()) return 0;
                var userId = _context.Session[UserIdKey];
                if (userId == null) return 0;
                return (int)userId;
            }
            set
            {
                if (!CheckSession()) return;
                _context.Session[UserIdKey] = value;
            }
        }

        public string UserName
        {
            get
            {
                if (!CheckSession()) return "";
                var userName = _context.Session[UserNameKey];
                if (userName == null) return "";
                return userName.ToString();
            }
            set
            {
                if (!CheckSession()) return;
                _context.Session[UserNameKey] = value;
            }
        }

        public string UserEmail
        {
            get
            {
                if (!CheckSession()) return "";
                var userEmail = _context.Session[UserEmailKey];
                if (userEmail == null) return "";
                return userEmail.ToString();
            }
            set
            {
                if (!CheckSession()) return;
                _context.Session[UserEmailKey] = value;
            }
        }

        public void StartSession(int userId, string userName, string userEmail)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
        }

        public void EndSession()
        {
            _context.Session.Abandon();
        }

        private bool CheckSession()
        {
            if (_context == null || _context.Session == null) return false;
            return true;
        }
    }
}
