using System;
using System.Web;
using System.Web.Optimization;

namespace EazyWizy.WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        { 
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/kendo.web.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Site").Include(
                    "~/Scripts/Site/*.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                    "~/Content/kendo/kendo.common.min.css",
                    "~/Content/kendo/kendo.default.min.css",
                    "~/Content/kendo/kendo.rtl.min.css"));

            bundles.Add(new StyleBundle("~/Content/Site/css").Include(
                   "~/Content/Site.css",
                  "~/Content/EazyWizy.css",
                  "~/Content/Header.css",
                  "~/Content/lhsMenu.css"));
        }       
    }
}