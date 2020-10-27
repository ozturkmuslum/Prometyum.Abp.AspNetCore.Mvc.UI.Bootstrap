using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.Bundling
{
    public class PrometyumBootstrapGlobalScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/libs/bootstrap-autocomplete/bootstrap-autocomplete.js");
            //context.Files.Add("/js/scripts.js");
        }
    }
}
