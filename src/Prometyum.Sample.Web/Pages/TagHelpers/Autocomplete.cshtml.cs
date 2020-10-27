using Microsoft.AspNetCore.Mvc;

namespace Prometyum.Sample.Web.Pages.TagHelpers
{
    public class AutocompleteModel : SamplePageModel
    {
        [BindProperty]
        public string Text { get; set; }

        [BindProperty]
        public int Value { get; set; }
        public void OnGet()
        {
            
        }
    }
}