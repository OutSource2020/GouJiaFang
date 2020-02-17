using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AccountInquiry22",
                url: "api/AccountInquiry22",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "AccountInquiry",
                }
            );

            routes.MapRoute(
                name: "AccountInquiry11",
                url: "api/AccountInquiry11",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "AccountInquiry",
                }
            );

            routes.MapRoute(
                name: "OrderInquire22",
                url: "api/OrderInquire22",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "OrderInquire",
                }
            );

            routes.MapRoute(
                name: "OrderInquire11",
                url: "api/OrderInquire11",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "OrderInquire",
                }
            );

            routes.MapRoute(
                name: "OrderCreate22",
                url: "api/OrderCreate22",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "OrderCreate",
                }
            );

            routes.MapRoute(
                name: "OrderCreate11",
                url: "api/OrderCreate11",
                defaults: new
                {
                    controller = "PPPayment",
                    action = "OrderCreate",
                }
            );

            // routes.MapRoute(
            //     name: "Default",
            //     url: "{controller}/{action}/{id}",
            //     defaults: new { action = "Index", id = UrlParameter.Optional }
            // );
        }
    }
}

