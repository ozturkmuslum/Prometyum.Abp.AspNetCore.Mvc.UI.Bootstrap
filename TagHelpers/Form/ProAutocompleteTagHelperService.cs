using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Prometyum.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    public class ProAutocompleteTagHelperService : AbpTagHelperService<ProAutocompleteTagHelper>
    {
        private readonly IHtmlGenerator _generator;
        private readonly HtmlEncoder _encoder;
        private readonly IAbpTagHelperLocalizer _tagHelperLocalizer;

        //private const string InputTypeName = "hidden";

        public ProAutocompleteTagHelperService(IHtmlGenerator generator, HtmlEncoder encoder, IAbpTagHelperLocalizer tagHelperLocalizer)
        {
            _generator = generator;
            _encoder = encoder;
            _tagHelperLocalizer = tagHelperLocalizer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var innerHtml = await GetFormInputGroupAsHtmlAsync(context, output);

            var order = TagHelper.AspFor.ModelExplorer.GetDisplayOrder();

            AddGroupToFormGroupContents(
                context,
                TagHelper.AspFor.Name,
                SurroundInnerHtmlAndGet(context, output, innerHtml),
                order,
                out var suppress
            );

            if (suppress)
            {
                output.SuppressOutput();
            }
            else
            {
                output.TagMode = TagMode.StartTagAndEndTag;
                output.TagName = "div";
                LeaveOnlyGroupAttributes(context, output);
                output.Content.SetHtmlContent(output.Content.GetContent() + innerHtml);
            }
        }

        protected virtual async Task<string> GetFormInputGroupAsHtmlAsync(TagHelperContext context, TagHelperOutput output)
        { 
            var inputHiddenTag = await GetInputHiddenTagHelperOutputAsync(context, output);
            var inputTag = await GetInputTagHelperOutputAsync(context, output,inputHiddenTag);
           

            var inputHtml = inputTag.Render(_encoder);
            var inputHiddenHtml = inputHiddenTag.Render(_encoder);
            var label = await GetLabelAsHtmlAsync(context, output, inputTag);
            var info = GetInfoAsHtml(context, output, inputTag);
            var validation = await GetValidationAsHtmlAsync(context, output, inputTag);

            return GetContent(context, output, label, inputHtml + inputHiddenHtml, validation, info);
        }

        protected virtual async Task<string> GetValidationAsHtmlAsync(TagHelperContext context, TagHelperOutput output, TagHelperOutput inputTag)
        {
            if (IsOutputHidden(inputTag))
            {
                return "";
            }

            var validationMessageTagHelper = new ValidationMessageTagHelper(_generator)
            {
                For = TagHelper.AspFor,
                ViewContext = TagHelper.ViewContext
            };

            var attributeList = new TagHelperAttributeList { { "class", "text-danger" } };

            return await validationMessageTagHelper.RenderAsync(attributeList, context, _encoder, "span", TagMode.StartTagAndEndTag);
        }

        protected virtual string GetContent(TagHelperContext context, TagHelperOutput output, string label, string inputHtml, string validation, string infoHtml)
        {
            var innerContent = label + inputHtml;

            return innerContent + infoHtml + validation;
        }

        protected virtual string SurroundInnerHtmlAndGet(TagHelperContext context, TagHelperOutput output, string innerHtml)
        {
            return "<div class=\"form-group\">" +
                   Environment.NewLine + innerHtml + Environment.NewLine +
                   "</div>";
        }

        protected virtual TagHelper GetInputTagHelper(TagHelperContext context, TagHelperOutput output,string inputTypeName)
        {
            var inputTagHelper = new InputTagHelper(_generator)
            {
                For = TagHelper.AspFor,
                InputTypeName = inputTypeName,
                ViewContext = TagHelper.ViewContext
            };


            if (!TagHelper.Name.IsNullOrEmpty())
            {
                inputTagHelper.Name = TagHelper.Name;
            }

            if (!TagHelper.Value.IsNullOrEmpty())
            {
                inputTagHelper.Value = TagHelper.Value;
            }

            return inputTagHelper;
        }
  protected virtual TagHelper GetInputHiddenTagHelper(TagHelperContext context, TagHelperOutput output,string inputTypeName)
        {
            var inputTagHelper = new InputTagHelper(_generator)
            {
                For = TagHelper.AspForForHidden,
                InputTypeName = inputTypeName,
                ViewContext = TagHelper.ViewContext
            };


            if (!TagHelper.Name.IsNullOrEmpty())
            {
                inputTagHelper.Name = TagHelper.Name;
            }

            if (!TagHelper.Value.IsNullOrEmpty())
            {
                inputTagHelper.Value = TagHelper.Value;
            }

            return inputTagHelper;
        }

        protected virtual async Task<TagHelperOutput> GetInputTagHelperOutputAsync(TagHelperContext context, TagHelperOutput output,TagHelperOutput hiddenOutPut)
        {
            string inputTypeName = "text";
            var tagHelper = GetInputTagHelper(context, output,inputTypeName);

            var inputTagHelperOutput = await tagHelper.ProcessAndGetOutputAsync(
                GetInputAttributes(context, output,inputTypeName),
                context,
                "input"
            );

            AddDisabledAttribute(inputTagHelperOutput);
            AddAutoFocusAttribute(inputTagHelperOutput);
            //var isCheckbox = IsInputCheckbox(context, output, inputTagHelperOutput.Attributes);
            AddFormControlClass(context, output, inputTagHelperOutput);
            AddReadOnlyAttribute(inputTagHelperOutput);
            AddPlaceholderAttribute(inputTagHelperOutput);
            AddInfoTextId(inputTagHelperOutput);

            AddProAutoCompleteClass(context, output, inputTagHelperOutput);
            AddAutoCompleteOffAttribute(inputTagHelperOutput);
            AddDataUrlAttribute(inputTagHelperOutput,TagHelper.Url);
            AddIdOfHiddenAttribute(inputTagHelperOutput,hiddenOutPut);

         return inputTagHelperOutput;
        }

        protected virtual async Task<TagHelperOutput> GetInputHiddenTagHelperOutputAsync(TagHelperContext context, TagHelperOutput output)
        {
            var inputTypeName = "hidden";

            var tagHelper = GetInputHiddenTagHelper(context, output,inputTypeName);

            var inputTagHelperOutput = await tagHelper.ProcessAndGetOutputAsync(
                GetInputAttributes(context, output,inputTypeName),
                context,
                "input"
            );

            return inputTagHelperOutput;
        }

        private void AddFormControlClass(TagHelperContext context, TagHelperOutput output, TagHelperOutput inputTagHelperOutput)
        {
            var className = "form-control";

            inputTagHelperOutput.Attributes.AddClass(className + " " + GetSize(context, output));
        }

        private void AddProAutoCompleteClass(TagHelperContext context, TagHelperOutput output, TagHelperOutput inputTagHelperOutput)
        {
            var className = "pro-autocomplete";

            inputTagHelperOutput.Attributes.AddClass(className + " " + GetSize(context, output));
        }

        protected virtual void AddAutoFocusAttribute(TagHelperOutput inputTagHelperOutput)
        {
            if (TagHelper.AutoFocus && !inputTagHelperOutput.Attributes.ContainsName("data-auto-focus"))
            {
                inputTagHelperOutput.Attributes.Add("data-auto-focus", "true");
            }
        }

        protected virtual void AddDisabledAttribute(TagHelperOutput inputTagHelperOutput)
        {
            if (inputTagHelperOutput.Attributes.ContainsName("disabled") == false &&
                     (TagHelper.IsDisabled || TagHelper.AspFor.ModelExplorer.GetAttribute<DisabledInput>() != null))
            {
                inputTagHelperOutput.Attributes.Add("disabled", "");
            }
        }

        protected virtual void AddReadOnlyAttribute(TagHelperOutput inputTagHelperOutput)
        {
            if (inputTagHelperOutput.Attributes.ContainsName("readonly") == false &&
                    (TagHelper.IsReadonly != false || TagHelper.AspFor.ModelExplorer.GetAttribute<ReadOnlyInput>() != null))
            {
                inputTagHelperOutput.Attributes.Add("readonly", "");
            }
        }

        protected virtual void AddAutoCompleteOffAttribute(TagHelperOutput inputTagHelperOutput)
        {
            if (inputTagHelperOutput.Attributes.ContainsName("autocomplete") == false)
            {
                inputTagHelperOutput.Attributes.Add("autocomplete", "off");
            }
        }

        protected virtual void AddDataUrlAttribute(TagHelperOutput inputTagHelperOutput,string url)
        {
            if (inputTagHelperOutput.Attributes.ContainsName("data-url") == false)
            {
                inputTagHelperOutput.Attributes.Add("data-url", url);
            }
        }

        protected virtual void AddIdOfHiddenAttribute(TagHelperOutput inputTagHelperOutput,TagHelperOutput hiddenTagHelperOutput)
        {
            // get id of hidden field
            var idOfHiddenField = hiddenTagHelperOutput.Attributes.Single(o => o.Name == "id").Value;

            if (inputTagHelperOutput.Attributes.ContainsName("data-hidden-id") == false)
            {
                inputTagHelperOutput.Attributes.Add("data-hidden-id", $"#{idOfHiddenField}");
            }
        }

        protected virtual void AddPlaceholderAttribute(TagHelperOutput inputTagHelperOutput)
        {
            if (inputTagHelperOutput.Attributes.ContainsName("placeholder"))
            {
                return;
            }

            var attribute = TagHelper.AspFor.ModelExplorer.GetAttribute<Placeholder>();

            if (attribute != null)
            {
                var placeholderLocalized = _tagHelperLocalizer.GetLocalizedText(attribute.Value, TagHelper.AspFor.ModelExplorer);

                inputTagHelperOutput.Attributes.Add("placeholder", placeholderLocalized);
            }
        }

        protected virtual void AddInfoTextId(TagHelperOutput inputTagHelperOutput)
        {
            if (TagHelper.AspFor.ModelExplorer.GetAttribute<InputInfoText>() == null)
            {
                return;
            }

            var idAttr = inputTagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "id");

            if (idAttr == null)
            {
                return;
            }

            var infoText = _tagHelperLocalizer.GetLocalizedText(idAttr.Value + "InfoText", TagHelper.AspFor.ModelExplorer);

            inputTagHelperOutput.Attributes.Add("aria-describedby", infoText);
        }

        protected virtual bool IsInputCheckbox(TagHelperContext context, TagHelperOutput output, TagHelperAttributeList attributes)
        {
            return attributes.Any(a => a.Value != null && a.Name == "type" && a.Value.ToString() == "checkbox");
        }

        protected virtual async Task<string> GetLabelAsHtmlAsync(TagHelperContext context, TagHelperOutput output, TagHelperOutput inputTag)
        {
            if (IsOutputHidden(inputTag) || TagHelper.SuppressLabel)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(TagHelper.Label))
            {
                return await GetLabelAsHtmlUsingTagHelperAsync(context, output) + GetRequiredSymbol(context, output);
            }

            var label = new TagBuilder("label");
            label.Attributes.Add("for", GetIdAttributeValue(inputTag));
            label.InnerHtml.Append(TagHelper.Label);

            return label.ToHtmlString();
        }

        protected virtual string GetRequiredSymbol(TagHelperContext context, TagHelperOutput output)
        {
            if (!TagHelper.DisplayRequiredSymbol)
            {
                return "";
            }

            return TagHelper.AspFor.ModelExplorer.GetAttribute<RequiredAttribute>() != null ? "<span> * </span>" : "";
        }

        protected virtual string GetInfoAsHtml(TagHelperContext context, TagHelperOutput output, TagHelperOutput inputTag)
        {
            if (IsOutputHidden(inputTag))
            {
                return "";
            }

            var text = "";

            if (!string.IsNullOrEmpty(TagHelper.InfoText))
            {
                text = TagHelper.InfoText;
            }
            else
            {
                var infoAttribute = TagHelper.AspFor.ModelExplorer.GetAttribute<InputInfoText>();
                if (infoAttribute != null)
                {
                    text = infoAttribute.Text;
                }
                else
                {
                    return "";
                }
            }

            var idAttr = inputTag.Attributes.FirstOrDefault(a => a.Name == "id");
            var localizedText = _tagHelperLocalizer.GetLocalizedText(text, TagHelper.AspFor.ModelExplorer);

            var small = new TagBuilder("small");
            small.Attributes.Add("id", idAttr?.Value?.ToString() + "InfoText");
            small.AddCssClass("form-text text-muted");

            return small.ToHtmlString();
        }

        protected virtual async Task<string> GetLabelAsHtmlUsingTagHelperAsync(TagHelperContext context, TagHelperOutput output)
        {
            var labelTagHelper = new LabelTagHelper(_generator)
            {
                For = TagHelper.AspFor,
                ViewContext = TagHelper.ViewContext
            };

            var attributeList = new TagHelperAttributeList();

            return await labelTagHelper.RenderAsync(attributeList, context, _encoder, "label", TagMode.StartTagAndEndTag);
        }


        protected virtual TagHelperAttributeList GetInputAttributes(TagHelperContext context, TagHelperOutput output,string inputTypeName)
        {
            var groupPrefix = "group-";

            var tagHelperAttributes = output.Attributes.Where(a => !a.Name.StartsWith(groupPrefix)).ToList();

            var attrList = new TagHelperAttributeList();

            foreach (var tagHelperAttribute in tagHelperAttributes)
            {
                attrList.Add(tagHelperAttribute);
            }

            if (!inputTypeName.IsNullOrEmpty() && !attrList.ContainsName("type"))
            {
                attrList.Add("type", inputTypeName);
            }

            if (!TagHelper.Name.IsNullOrEmpty() && !attrList.ContainsName("name"))
            {
                attrList.Add("name", TagHelper.Name);
            }

            if (!TagHelper.Value.IsNullOrEmpty() && !attrList.ContainsName("value"))
            {
                attrList.Add("value", TagHelper.Value);
            }

            return attrList;
        }

        protected virtual void LeaveOnlyGroupAttributes(TagHelperContext context, TagHelperOutput output)
        {
            var groupPrefix = "group-";
            var tagHelperAttributes = output.Attributes.Where(a => a.Name.StartsWith(groupPrefix)).ToList();

            output.Attributes.Clear();

            foreach (var tagHelperAttribute in tagHelperAttributes)
            {
                var nameWithoutPrefix = tagHelperAttribute.Name.Substring(groupPrefix.Length);
                var newAttritube = new TagHelperAttribute(nameWithoutPrefix, tagHelperAttribute.Value);
                output.Attributes.Add(newAttritube);
            }
        }

        protected virtual string GetSize(TagHelperContext context, TagHelperOutput output)
        {
            var attribute = TagHelper.AspFor.ModelExplorer.GetAttribute<FormControlSize>();

            if (attribute != null)
            {
                TagHelper.Size = attribute.Size;
            }

            switch (TagHelper.Size)
            {
                case AbpFormControlSize.Small:
                    return "form-control-sm";
                case AbpFormControlSize.Medium:
                    return "form-control-md";
                case AbpFormControlSize.Large:
                    return "form-control-lg";
            }

            return "";
        }

        protected virtual bool IsOutputHidden(TagHelperOutput inputTag)
        {
            return inputTag.Attributes.Any(a => a.Name.ToLowerInvariant() == "type" && a.Value.ToString().ToLowerInvariant() == "hidden");
        }

        protected virtual string GetIdAttributeValue(TagHelperOutput inputTag)
        {
            var idAttr = inputTag.Attributes.FirstOrDefault(a => a.Name == "id");

            return idAttr != null ? idAttr.Value.ToString() : string.Empty;
        }

        protected virtual string GetIdAttributeAsString(TagHelperOutput inputTag)
        {
            var id = GetIdAttributeValue(inputTag);

            return !string.IsNullOrWhiteSpace(id) ? "for=\"" + id + "\"" : string.Empty;
        }

        protected virtual void AddGroupToFormGroupContents(TagHelperContext context, string propertyName, string html, int order, out bool suppress)
        {
            var list = context.GetValue<List<FormGroupItem>>(FormGroupContents) ?? new List<FormGroupItem>();
            suppress = list == null;

            if (list != null && !list.Any(igc => igc.HtmlContent.Contains("id=\"" + propertyName.Replace('.', '_') + "\"")))
            {
                list.Add(new FormGroupItem
                {
                    HtmlContent = html,
                    Order = order,
                    PropertyName = propertyName
                });
            }
        }
    }
}
