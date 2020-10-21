using Microsoft.AspNetCore.Mvc;

namespace Prometyum.Sample.Web.Pages
{
    public class IndexModel : SamplePageModel
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