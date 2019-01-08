using LxcLibrary.WebBase.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mack.Club.SkyGrass.GoodNightWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            this.HandleError();
        }

        private void HandleError()
        {
            var error = Server.GetLastError();

            Response.ContentType = "application/json";

            if (error == null) { return; }

            string message = error.Message;

            if (!message.Contains("errcode"))
            {
                var ret = new RetModel<bool>(-1, message);
                message = ret.serialize();
            }

            var dic = new Dictionary<string, object>();
            dic.Add("error", error);
            dic.Add("url", Request.Url.ToString());

            Response.Write(message);

            Server.ClearError();
        }
    }
}
