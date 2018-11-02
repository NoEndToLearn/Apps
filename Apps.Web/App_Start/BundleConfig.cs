using System.Web;
using System.Web.Optimization;

namespace Apps.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
      "~/Scripts/home.js"));

            //bundles.Add(new ScriptBundle("~/bundles/common").Include(
            //      "~/Scripts/common.js"));

            //easyui
            bundles.Add(new StyleBundle("~/Content/themes/blue/css").Include("~/Content/themes/blue/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/themes/gray/css").Include("~/Content/themes/gray/easyui.css"));
            bundles.Add(new StyleBundle("~/Content/themes/metro/css").Include("~/Content/themes/metro/easyui.css"));
            //bundles.Add(new StyleBundle("~/Content/themes/icon/css").Include("~/Content/themes/color.css", "~/Content/themes/icon.css"));

            bundles.Add(
                new StyleBundle("~/Bundles/App/vendor/css")
                    .Include("~/Content/AdminLTE-master/dist/css/skins/_all-skins.css", new CssRewriteUrlTransform())
                    .Include("~/Content/AdminLTE-master/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/AdminLTE-master/bower_components/font-awesome/css/font-awesome.min.css")
                    .Include("~/Content/AdminLTE-master/bower_components/Ionicons/css/ionicons.min.css")
                    .Include("~/Content/AdminLTE-master/dist/css/AdminLTE.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/AdminLTE-master/bower_components/morris.js/morris.css", new CssRewriteUrlTransform())
                     .Include("~/Content/AdminLTE-master/bower_components/jvectormap/jquery-jvectormap.css")
                         .Include("~/Content/AdminLTE-master/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css")
                         .Include("~/Content/AdminLTE-master/bower_components/bootstrap-daterangepicker/daterangepicker.css")
                         .Include("~/Content/AdminLTE-master/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")

                );
            bundles.Add(
                            new ScriptBundle("~/Bundles/App/vendor/js")
                                .Include(
                                    "~/Content/AdminLTE-master/bower_components/jquery/dist/jquery.min.js",
                                    "~/Content/AdminLTE-master/bower_components/jquery-ui/jquery-ui.min.js",

                                    "~/Content/AdminLTE-master/bower_components/bootstrap/dist/js/bootstrap.min.js",

                                    "~/Content/AdminLTE-master/bower_components/raphael/raphael.min.js",
                                    "~/Content/AdminLTE-master/bower_components/morris.js/morris.min.js",

                                    "~/Content/AdminLTE-master/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",

                                    "~/Content/AdminLTE-master/bower_components/jquery-knob/dist/jquery.knob.min.js",
                                    "~/Content/AdminLTE-master/bower_components/moment/min/moment.min.js",
                                    "~/Content/AdminLTE-master/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                                    "~/Content/AdminLTE-master/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                                    "~/Content/AdminLTE-master/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                                    "~/Content/AdminLTE-master/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",

                                    "~/Content/AdminLTE-master/bower_components/fastclick/lib/fastclick.js",
                                    "~/Content/AdminLTE-master/dist/js/adminlte.min.js",
                                    "~/Content/AdminLTE-master/dist/js/pages/dashboard.js",
                                    "~/Content/AdminLTE-master/angular-ui-router.min.js"
                                )
                            );
            //bundles.Add(new ScriptBundle("~/bundles/easyuiplus").Include(
            //            "~/Scripts/jquery.easyui.plus.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate.unobtrusive.plus.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
"~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}
