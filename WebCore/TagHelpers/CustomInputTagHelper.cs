using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCore.Services.Share.Languages;

namespace WebCore.TagHelpers
{
    [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
    public class CustomInputTagHelper : TagHelper
    {
        private readonly ILanguageProviderService languageProviderService;
        private const string ForAttributeName = "asp-for";
        private const string FormatAttributeName = "asp-format";

        // Mapping from datatype names and data annotation hints to values for the <input/> element's "type" attribute.
        private static readonly Dictionary<string, string> _defaultInputTypes =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "HiddenInput", InputType.Hidden.ToString().ToLowerInvariant() },
                { "Password", InputType.Password.ToString().ToLowerInvariant() },
                { "Text", InputType.Text.ToString().ToLowerInvariant() },
                { "PhoneNumber", "tel" },
                { "Url", "url" },
                { "EmailAddress", "email" },
                { "Date", "datetime" },
                { "DateTime", "datetime" },
                { "DateTime-local", "datetime" },
                { "Time", "datetime" },
                { "Submit", "submit" },
                { nameof(Byte), "number" },
                { nameof(SByte), "number" },
                { nameof(Int16), "number" },
                { nameof(UInt16), "number" },
                { nameof(Int32), "number" },
                { nameof(UInt32), "number" },
                { nameof(Int64), "number" },
                { nameof(UInt64), "number" },
                { nameof(Single), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(Double), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(Boolean), InputType.CheckBox.ToString().ToLowerInvariant() },
                { nameof(Decimal), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(String), InputType.Text.ToString().ToLowerInvariant() },
                { nameof(IFormFile), "file" },
                { TemplateRenderer.IEnumerableOfIFormFileName, "file" },
            };

        // Mapping from <input/> element's type to RFC 3339 date and time formats.
        private static readonly Dictionary<string, string> _rfc3339Formats =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "date", "{0:yyyy-MM-dd}" },
                { "datetime", "{0:yyyy-MM-ddTHH:mm:ss.fffK}" },
                { "datetime-local", "{0:yyyy-MM-ddTHH:mm:ss.fff}" },
                { "time", "{0:HH:mm:ss.fff}" },
            };

        /// <summary>
        /// Creates a new <see cref="InputTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public CustomInputTagHelper(IHtmlGenerator generator, ILanguageProviderService languageProviderService)
        {
            Generator = generator;
            this.languageProviderService = languageProviderService;
        }

        /// <inheritdoc />
        public override int Order => -1000;

        public bool LanguageSupport { get; set; }

        protected IHtmlGenerator Generator { get; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// The format string (see https://msdn.microsoft.com/en-us/library/txafckwd.aspx) used to format the
        /// <see cref="For"/> result. Sets the generated "value" attribute to that formatted string.
        /// </summary>
        /// <remarks>
        /// Not used if the provided (see <see cref="InputTypeName"/>) or calculated "type" attribute value is
        /// <c>checkbox</c>, <c>password</c>, or <c>radio</c>. That is, <see cref="Format"/> is used when calling
        /// <see cref="IHtmlGenerator.GenerateTextBox"/>.
        /// </remarks>
        [HtmlAttributeName(FormatAttributeName)]
        public string Format { get; set; }

        /// <summary>
        /// The type of the &lt;input&gt; element.
        /// </summary>
        /// <remarks>
        /// Passed through to the generated HTML in all cases. Also used to determine the <see cref="IHtmlGenerator"/>
        /// helper to call and the default <see cref="Format"/> value. A default <see cref="Format"/> is not calculated
        /// if the provided (see <see cref="InputTypeName"/>) or calculated "type" attribute value is <c>checkbox</c>,
        /// <c>hidden</c>, <c>password</c>, or <c>radio</c>.
        /// </remarks>
        [HtmlAttributeName("type")]
        public string InputTypeName { get; set; }

        [HtmlAttributeName("checked")]
        public bool InputChecked { get; set; }

        /// <summary>
        /// The value of the &lt;input&gt; element.
        /// </summary>
        /// <remarks>
        /// Passed through to the generated HTML in all cases. Also used to determine the generated "checked" attribute
        /// if <see cref="InputTypeName"/> is "radio". Must not be <c>null</c> in that case.
        /// </remarks>
        public string Value { get; set; }

        /// <inheritdoc />
        /// <remarks>Does nothing if <see cref="For"/> is <c>null</c>.</remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <see cref="Format"/> is non-<c>null</c> but <see cref="For"/> is <c>null</c>.
        /// </exception>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            TagHelperAttribute[] validations = output.Attributes.Where(x => x.Name.Contains("data-val-") && x.Name.Count(c => c == '-') == 2).ToArray();
            if (LanguageSupport && validations.Any())
            {
                foreach (TagHelperAttribute validation in validations)
                {
                    output.Attributes.RemoveAll(validation.Name);
                    string message = validation.Value.ToString();
                    output.Attributes.Add(validation.Name, languageProviderService.GetlangByKey(message));
                }
            }


            // Pass through attributes that are also well-known HTML attributes. Must be done prior to any copying
            // from a TagBuilder.
            if (InputTypeName != null)
            {
                output.CopyHtmlAttribute("type", context);
            }

            if (InputChecked)
            {
                output.Attributes.Add("checked", "checked");
            }

            if (Value != null)
            {
                output.CopyHtmlAttribute(nameof(Value), context);
            }

            // Note null or empty For.Name is allowed because TemplateInfo.HtmlFieldPrefix may be sufficient.
            // IHtmlGenerator will enforce name requirements.

            if (For == null)
            {
                return;
            }

            ModelMetadata metadata = For.Metadata;
            ModelExplorer modelExplorer = For.ModelExplorer;
            if (metadata == null)
            {
                return;
            }

            string inputType;
            string inputTypeHint;
            if (string.IsNullOrEmpty(InputTypeName))
            {
                // Note GetInputType never returns null.
                inputType = GetInputType(modelExplorer, out inputTypeHint);
            }
            else
            {
                inputType = InputTypeName.ToLowerInvariant();
                inputTypeHint = null;
            }

            // inputType may be more specific than default the generator chooses below.
            output.Attributes.SetAttribute("type", inputType);


            TagBuilder tagBuilder;
            switch (inputType)
            {
                case "hidden":
                    tagBuilder = GenerateHidden(modelExplorer);
                    break;

                case "checkbox":
                    GenerateCheckBox(modelExplorer, output);
                    return;

                case "password":
                    tagBuilder = Generator.GeneratePassword(
                        ViewContext,
                        modelExplorer,
                        For.Name,
                        value: null,
                        htmlAttributes: null);
                    break;

                case "radio":
                    tagBuilder = GenerateRadio(modelExplorer);
                    break;

                default:
                    tagBuilder = GenerateTextBox(modelExplorer, inputTypeHint, inputType);
                    break;
            }

            if (tagBuilder != null)
            {
                // This TagBuilder contains the one <input/> element of interest.
                output.MergeAttributes(tagBuilder);
                if (tagBuilder.HasInnerHtml)
                {
                    // Since this is not the "checkbox" special-case, no guarantee that output is a self-closing
                    // element. A later tag helper targeting this element may change output.TagMode.
                    output.Content.AppendHtml(tagBuilder.InnerHtml);
                }
            }
        }

        /// <summary>
        /// Gets an &lt;input&gt; element's "type" attribute value based on the given <paramref name="modelExplorer"/>
        /// or <see cref="InputType"/>.
        /// </summary>
        /// <param name="modelExplorer">The <see cref="ModelExplorer"/> to use.</param>
        /// <param name="inputTypeHint">When this method returns, contains the string, often the name of a
        /// <see cref="ModelMetadata.ModelType"/> base class, used to determine this method's return value.</param>
        /// <returns>An &lt;input&gt; element's "type" attribute value.</returns>
        protected string GetInputType(ModelExplorer modelExplorer, out string inputTypeHint)
        {
            foreach (string hint in GetInputTypeHints(modelExplorer))
            {
                if (_defaultInputTypes.TryGetValue(hint, out string inputType))
                {
                    inputTypeHint = hint;
                    return inputType;
                }
            }

            inputTypeHint = InputType.Text.ToString().ToLowerInvariant();
            return inputTypeHint;
        }

        private void GenerateCheckBox(ModelExplorer modelExplorer, TagHelperOutput output)
        {
            if (typeof(bool) != modelExplorer.ModelType)
            {
                return;
            }

            // Prepare to move attributes from current element to <input type="checkbox"/> generated just below.
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // Perf: Avoid allocating enumerator
            // Construct attributes correctly (first attribute wins).
            for (int i = 0; i < output.Attributes.Count; i++)
            {
                TagHelperAttribute attribute = output.Attributes[i];
                if (!htmlAttributes.ContainsKey(attribute.Name))
                {
                    htmlAttributes.Add(attribute.Name, attribute.Value);
                }
            }

            TagBuilder checkBoxTag = Generator.GenerateCheckBox(
                ViewContext,
                modelExplorer,
                For.Name,
                isChecked: null,
                htmlAttributes: htmlAttributes);
            if (checkBoxTag != null)
            {
                // Do not generate current element's attributes or tags. Instead put both <input type="checkbox"/> and
                // <input type="hidden"/> into the output's Content.
                output.Attributes.Clear();
                output.TagName = null;

                TagRenderMode renderingMode =
                    output.TagMode == TagMode.SelfClosing ? TagRenderMode.SelfClosing : TagRenderMode.StartTag;
                checkBoxTag.TagRenderMode = renderingMode;
                output.Content.AppendHtml(checkBoxTag);

                TagBuilder hiddenForCheckboxTag = Generator.GenerateHiddenForCheckbox(ViewContext, modelExplorer, For.Name);
                if (hiddenForCheckboxTag != null)
                {
                    hiddenForCheckboxTag.TagRenderMode = renderingMode;

                    if (ViewContext.FormContext.CanRenderAtEndOfForm)
                    {
                        ViewContext.FormContext.EndOfFormContent.Add(hiddenForCheckboxTag);
                    }
                    else
                    {
                        output.Content.AppendHtml(hiddenForCheckboxTag);
                    }
                }
            }
        }

        private TagBuilder GenerateRadio(ModelExplorer modelExplorer)
        {
            // Note empty string is allowed.
            if (Value == null)
            {
                return null;
            }

            return Generator.GenerateRadioButton(
                ViewContext,
                modelExplorer,
                For.Name,
                Value,
                isChecked: null,
                htmlAttributes: null);
        }

        private TagBuilder GenerateTextBox(ModelExplorer modelExplorer, string inputTypeHint, string inputType)
        {
            string format = Format;
            if (string.IsNullOrEmpty(format))
            {
                format = GetFormat(modelExplorer, inputTypeHint, inputType);
            }

            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>
            {
                { "type", inputType }
            };

            if (string.Equals(inputType, "file") && string.Equals(inputTypeHint, TemplateRenderer.IEnumerableOfIFormFileName))
            {
                htmlAttributes["multiple"] = "multiple";
            }

            return Generator.GenerateTextBox(
                ViewContext,
                modelExplorer,
                For.Name,
                value: modelExplorer.Model,
                format: format,
                htmlAttributes: htmlAttributes);
        }

        // Imitate Generator.GenerateHidden() using Generator.GenerateTextBox(). This adds support for asp-format that
        // is not available in Generator.GenerateHidden().
        private TagBuilder GenerateHidden(ModelExplorer modelExplorer)
        {
            object value = For.Model;
            byte[] byteArrayValue = value as byte[];
            if (byteArrayValue != null)
            {
                value = Convert.ToBase64String(byteArrayValue);
            }

            // In DefaultHtmlGenerator(), GenerateTextBox() calls GenerateInput() _almost_ identically to how
            // GenerateHidden() does and the main switch inside GenerateInput() handles InputType.Text and
            // InputType.Hidden identically. No behavior differences at all when a type HTML attribute already exists.
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>
            {
                { "type", "hidden" }
            };

            return Generator.GenerateTextBox(
                ViewContext,
                modelExplorer,
                For.Name,
                value: value,
                format: Format,
                htmlAttributes: htmlAttributes);
        }

        // Get a fall-back format based on the metadata.
        private string GetFormat(ModelExplorer modelExplorer, string inputTypeHint, string inputType)
        {
            string format;
            if (string.Equals("decimal", inputTypeHint, StringComparison.OrdinalIgnoreCase) &&
                string.Equals("text", inputType, StringComparison.Ordinal) &&
                string.IsNullOrEmpty(modelExplorer.Metadata.EditFormatString))
            {
                // Decimal data is edited using an <input type="text"/> element, with a reasonable format.
                // EditFormatString has precedence over this fall-back format.
                format = "{0:0.00}";
            }
            else if (_rfc3339Formats.TryGetValue(inputType, out string rfc3339Format) &&
                ViewContext.Html5DateRenderingMode == Html5DateRenderingMode.Rfc3339 &&
                !modelExplorer.Metadata.HasNonDefaultEditFormat &&
                (typeof(DateTime) == modelExplorer.Metadata.UnderlyingOrModelType || typeof(DateTimeOffset) == modelExplorer.Metadata.UnderlyingOrModelType))
            {
                // Rfc3339 mode _may_ override EditFormatString in a limited number of cases e.g. EditFormatString
                // must be a default format (i.e. came from a built-in [DataType] attribute).
                format = rfc3339Format;
            }
            else
            {
                // Otherwise use EditFormatString, if any.
                format = modelExplorer.Metadata.EditFormatString;
            }

            return format;
        }

        // A variant of TemplateRenderer.GetViewNames(). Main change relates to bool? handling.
        private static IEnumerable<string> GetInputTypeHints(ModelExplorer modelExplorer)
        {
            if (!string.IsNullOrEmpty(modelExplorer.Metadata.TemplateHint))
            {
                yield return modelExplorer.Metadata.TemplateHint;
            }

            if (!string.IsNullOrEmpty(modelExplorer.Metadata.DataTypeName))
            {
                yield return modelExplorer.Metadata.DataTypeName;
            }

            // In most cases, we don't want to search for Nullable<T>. We want to search for T, which should handle
            // both T and Nullable<T>. However we special-case bool? to avoid turning an <input/> into a <select/>.
            Type fieldType = modelExplorer.ModelType;
            if (typeof(bool?) != fieldType)
            {
                fieldType = modelExplorer.Metadata.UnderlyingOrModelType;
            }

            foreach (string typeName in TemplateRenderer.GetTypeNames(modelExplorer.Metadata, fieldType))
            {
                yield return typeName;
            }
        }
    }
}
