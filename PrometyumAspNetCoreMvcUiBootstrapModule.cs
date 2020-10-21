using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
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
        }
    }
}
