﻿using Prometyum.Sample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Prometyum.Sample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SampleEntityFrameworkCoreDbMigrationsModule),
        typeof(SampleApplicationContractsModule)
        )]
    public class SampleDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
