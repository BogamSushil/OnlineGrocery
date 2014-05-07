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
                 "~/Scripts/jquery-1.10.2.js",
                 "~/Scripts/jquery.ui.widget.js",
                 "~/Scripts/jquery.ui.core.js",
                 "~/Scripts/jquery.ui.accordion.js"
                 ));            

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
                  "~/Content/productList.css",
                  "~/Content/lhsMenu.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui/css").Include(
                "~/Content/jquery-ui/jquery.ui.all.css"));
        }       
    }
}