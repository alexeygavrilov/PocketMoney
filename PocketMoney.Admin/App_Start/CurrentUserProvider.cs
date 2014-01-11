using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Admin
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public void AddCurrentUser(IUser user, bool persist = false)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.FullName(),
                Clock.UtcNow(),
                Clock.UtcNow() + FormsAuthentication.Timeout,
                persist,
                user.Id.ToBase32Url() + "|" + user.Family.Id.ToBase32Url());

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie authCookie =
                        new HttpCookie(FormsAuthentication.FormsCookieName,
                                       encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public IUser GetCurrentUser()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
                throw new InvalidDataException("Cannot found current user");

            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            string[] ids = ticket.UserData.Split(new char[1] { '|' });

            return new WrapperUser(ticket.Name,
                ids[0].FromBase32Url(),
                ids[1].FromBase32Url());
        }

        public void RemoveCurrentUser()
        {
            FormsAuthentication.SignOut();
        }

        public void SetData(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public object GetDate(string key)
        {
            return HttpContext.Current.Session[key];
        }
    }
}
