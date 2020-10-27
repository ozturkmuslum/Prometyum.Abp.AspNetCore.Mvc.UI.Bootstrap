using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.Bundling
{
    public class PrometyumBootstrapGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            //context.Files.Add("/themes/adminlte/css/layout.css");
        }
    }
}
