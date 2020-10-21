using Prometyum.Sample.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Prometyum.Sample
{
    [DependsOn(
        typeof(SampleEntityFrameworkCoreTestModule)
        )]
    public class SampleDomainTestModule : AbpModule
    {

    }
}