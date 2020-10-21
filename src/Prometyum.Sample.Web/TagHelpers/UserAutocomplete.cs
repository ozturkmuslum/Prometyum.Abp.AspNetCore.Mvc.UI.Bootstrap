using Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Prometyum.Sample.Web.TagHelpers
{
    public class UserAutocompleteTagHelper:ProAutocompleteTagHelper
    {
        public UserAutocompleteTagHelper(ProAutocompleteTagHelperService tagHelperService) 
            : base(tagHelperService)
        {
            Url = "/user/search";
        }
    }
}
