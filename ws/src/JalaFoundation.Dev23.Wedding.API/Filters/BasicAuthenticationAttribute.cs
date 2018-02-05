using JalaFoundation.Dev23.Wedding.API.Models;
using JalaFoundation.Dev23.Wedding.DAL.Repositories;
using System;
using System.Security.Principal;
using System.Text;
using System.Web;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.API.Filters
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                // in case the user is inventory manager
                if (authToken == "aW52ZW50b3J5TWFuYWdlcjppbnZlbnRvcnlNYW5hZ2Vy")
                {
                    Account accountManager = new Account()
                    {
                        Name = "Inventory Manager",
                        Token = "aW52ZW50b3J5TWFuYWdlcjppbnZlbnRvcnlNYW5hZ2Vy",
                    };

                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(accountManager), new string[] { });
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    AccountsRepository accountsRepository = new AccountsRepository();

                    var account = accountsRepository.isValid(username, password);

                    if (account != null)
                    {
                        HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(account), new string[] { });
                        base.OnActionExecuting(actionContext);
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
            }
        }
    }
}