using Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap
{
    [DependsOn(typeof(AbpAspNetCoreMvcUiModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiBootstrapModule))]
    public class PrometyumAspNetCoreMvcUiBootstrapModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PrometyumAspNetCoreMvcUiBootstrapModule>("Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap");
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(PrometyumBootstrapBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddContributors(typeof(PrometyumBootstrapGlobalStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(PrometyumBootstrapBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddContributors(typeof(PrometyumBootstrapGlobalScriptContributor));
                    });
            });
        }
    }
}
