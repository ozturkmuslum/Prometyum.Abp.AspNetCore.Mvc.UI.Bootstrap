﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Prometyum.Sample.EntityFrameworkCore
{
    [DependsOn(
        typeof(SampleEntityFrameworkCoreModule)
        )]
    public class SampleEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SampleMigrationsDbContext>();
        }
    }
}
