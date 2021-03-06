﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Prometyum.Sample.Web
{
    [Dependency(ReplaceServices = true)]
    public class SampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Sample";
    }
}
