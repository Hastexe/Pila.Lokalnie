using System.Web;
using System.Web.Optimization;

namespace MajsterFinale
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/CSS/Bootstrap/bootstrap.min.css",
                      "~/Content/CSS/Bootstrap/bootstrap-grid.min.css",
                      "~/Content/CSS/Bootstrap/bootstrap-reboot.min.css",
                      "~/Content/CSS/PagedList.css",
                      "~/Content/CSS/main_adv.css",
                      "~/Content/CSS/main_advdetails.css",
                      "~/Content/CSS/main_bg.css",
                      "~/Content/CSS/main_css.css",
                      "~/Content/CSS/main_nav.css",
                      "~/Content/CSS/main_reglog.css",
                      "~/Content/CSS/main_adv.css"
                      ));
        }
    }
}
