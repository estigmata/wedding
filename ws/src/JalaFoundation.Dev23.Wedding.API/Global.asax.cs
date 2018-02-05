using System.Web.Http;
using JalaFoundation.Dev23.Wedding.API.Configuration;

namespace JalaFoundation.Dev23.Wedding.API
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WeddingWebApiConfig.Register);
        }
    }
}