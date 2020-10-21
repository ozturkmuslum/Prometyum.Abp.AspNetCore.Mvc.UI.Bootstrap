using Prometyum.Sample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Prometyum.Sample.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class SampleController : AbpController
    {
        protected SampleController()
        {
            LocalizationResource = typeof(SampleResource);
        }
    }
}