﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Admin.Portal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
        }
    }
}
