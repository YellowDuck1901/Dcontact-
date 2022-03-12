using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dcontact
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "home",
                defaults: new { controller = "Home", action = "Home" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "admin"}
            );

            routes.MapRoute(
                name: "Confirm",
                url: "confirm",
                defaults: new { controller = "Account", action = "Comfirm" }
            );

            routes.MapRoute(
                name: "Create New Password",
                url: "createnewpass",
                defaults: new { controller = "Account", action = "CreateNewPassword" }
            );

            routes.MapRoute(
                name: "Log in",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );
            routes.MapRoute(
                name: "Recover Password",
                url: "recoverpass",
                defaults: new { controller = "Account", action = "RecoverPassword" }
            );
            routes.MapRoute(
                name: "Sign up",
                url: "signup",
                defaults: new { controller = "Account", action = "Sigup" }
            );
            routes.MapRoute(
              name: "Change Email",
              url: "changeemail",
              defaults: new { controller = "Account", action = "changeEmail" }
          );
            routes.MapRoute(
                name: "Dashboard",
                url: "dashboard",
                defaults: new { controller = "DcontactAndDcrad", action = "dashboard" }
            );
            routes.MapRoute(
                name: "Create D-Card",
                url: "createdcard",
                defaults: new { controller = "DcontactAndDcrad", action = "createDCard" }
            );
            routes.MapRoute(
                name: "Edit D-Contact",
                url: "edit",
                defaults: new { controller = "DcontactAndDcrad", action = "editDContact" }
            );
            routes.MapRoute(
                name: "Order D-Card",
                url: "order",
                defaults: new { controller = "DcontactAndDcrad", action = "oder_dcard" }
            );

            routes.MapRoute(
                name: "Error",
                url: "error",
                defaults: new { controller = "Shared", action = "Error" }
            );
            routes.MapRoute(
                name: "Link Dcontact",
                url: "{username}",
                defaults: new { controller = "LinkContact", action = "LinkContact" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
