using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    public class ProAutocompleteTagHelper : AbpTagHelper<ProAutocompleteTagHelper, ProAutocompleteTagHelperService>
    {
        public ModelExpression AspFor { get; set; }

        [HtmlAttributeName("asp-for-hidden")]
        public ModelExpression AspForForHidden { get; set; }

        public string Label { get; set; }

        [HtmlAttributeName("info")]
        public string InfoText { get; set; }

        [HtmlAttributeName("disabled")]
        public bool IsDisabled { get; set; } = false;

        [HtmlAttributeName("readonly")]
        public bool? IsReadonly { get; set; } = false;

        public bool AutoFocus { get; set; }
        public int MinLength { get; set; } = 3;

        public AbpFormControlSize Size { get; set; } = AbpFormControlSize.Default;

        [HtmlAttributeName("required-symbol")]
        public bool DisplayRequiredSymbol { get; set; } = true;

        public string Name { get; set; }

        public string Value { get; set; }

        public bool SuppressLabel { get; set; }

        [HtmlAttributeName("source")]
        public string Source { get; set; }
        public string NoResultsText { get; set; }
        public string OnSelectedChanged { get; set; }

        public ProAutocompleteTagHelper(ProAutocompleteTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
