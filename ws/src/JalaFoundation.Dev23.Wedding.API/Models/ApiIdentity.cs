using JalaFoundation.Dev23.Wedding.DAL.Models;
using System;
using System.Security.Principal;

namespace JalaFoundation.Dev23.Wedding.API.Models
{
    public class ApiIdentity : IIdentity
    {
        public Account Account { get; private set; }

        public ApiIdentity(Account account)
        {
            if (account == null) throw new ArgumentNullException("user");
            this.Account = account;
        }

        public string Name
        {
            get
            {
                return this.Account.Name;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Basic";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
    }
}