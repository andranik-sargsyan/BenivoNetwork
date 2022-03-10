using BenivoNetwork.BLL.Configuration;
using BenivoNetwork.Common.Models;
using BenivoNetwork.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace BenivoNetwork
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorConfig.Register(GlobalConfiguration.Configuration);
            AutoMapperConfig.Register();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            {
                return;
            }

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }

            _accountModel = JsonConvert.DeserializeObject<AccountModel>(authTicket.UserData);
            if (Context.User != null)
            {
                Context.User = new GenericPrincipal(Context.User.Identity, new[] { _accountModel.Role.ToString() });
            }
        }
        
        protected void Application_PostAuthenticateRequest()
        {
            if (Request.IsAuthenticated)
            {
                var identity = ClaimsPrincipal.Current.Identities.First();

                identity.AddClaims(new List<Claim>
                {
                    new Claim("ID", _accountModel.ID.ToString()),
                    new Claim("FirstName", _accountModel.FirstName),
                    new Claim("LastName", _accountModel.LastName),
                    new Claim("UserName", _accountModel.UserName),
                    new Claim("Email", _accountModel.Email),
                    new Claim("ImageURL", _accountModel.ImageURL ?? string.Empty),
                    new Claim("Role", _accountModel.Role.ToString())
                });
            }
        }

        private AccountModel _accountModel;
    }
}
