namespace MvcBase.WebHelper
{
    using System;
    using System.Web.Mvc;
    using System.Text;
    using System.Web.Routing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    /// <summary>
    /// HtmlHelperExtension class
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Action link with fragment.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <returns>string</returns>
        public static MvcHtmlString ActionLinkWithFragment(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction)
        {
            // htmlHelper null check
            if (null == htmlHelper)
            {
                throw new ArgumentNullException("htmlHelper", "htmlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            return ActionLinkWithFragment(htmlHelper, text, fragmentAction, string.Empty);
        }

        /// <summary>
        /// Actions the link with fragment.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>string</returns>
        public static MvcHtmlString ActionLinkWithFragment(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction, string cssClass)
        {
            // htmlHelper null check
            if (null == htmlHelper)
            {
                throw new ArgumentNullException("htmlHelper", "htmlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            return fragmentActionLink(htmlHelper, text, fragmentAction, HashFormate.Standard, cssClass);
        }

        /// <summary>
        /// Actions the link with fragment.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>string</returns>
        public static MvcHtmlString ActionLinkWithFragment(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction, HashFormate hashFormate)
        {
            // htmlHelper null check
            if (null == htmlHelper)
            {
                throw new ArgumentNullException("htmlHelper", "htmlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            return fragmentActionLink(htmlHelper, text, fragmentAction, hashFormate, string.Empty);
        }

        /// <summary>
        /// Actions the link with fragment.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>string</returns>
        public static MvcHtmlString ActionLinkWithFragment(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction, HashFormate hashFormate, string cssClass)
        {
            // htmlHelper null check
            if (null == htmlHelper)
            {
                throw new ArgumentNullException("htmlHelper", "htmlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            return fragmentActionLink(htmlHelper, text, fragmentAction, hashFormate, cssClass);
        }

        /// <summary>
        /// Action link with fragment.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>string</returns>
        private static MvcHtmlString fragmentActionLink(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction, HashFormate hashFormate, string cssClass)
        {
            // htmlHelper null check
            if (null == htmlHelper)
            {
                throw new ArgumentNullException("htmlHelper", "htmlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var mvcActionResult = fragmentAction.AsMVCResult() as IMvcResult;

            if (null == mvcActionResult)
            {
                return null;
            }

            var actionLink = new UrlHelper(htmlHelper.ViewContext.RequestContext).UrlWithActionFragment(fragmentAction, hashFormate);

            return new MvcHtmlString(string.Format("<a id=\"{0}\" href=\"{1}\" class=\"jqAddress {2}\">{3}</a>",
                                                Guid.NewGuid(),
                                                actionLink,
                                                (!string.IsNullOrEmpty(cssClass) ? cssClass : string.Empty),
                                                text));
        }

        private static FieldValidationMetadata ApplyFieldValidationMetadata(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string modelName)
        {
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            FieldValidationMetadata fieldMetadata = formContext.GetValidationMetadataForField(modelName, true /* createIfNotFound */);

            // write rules to context object
            IEnumerable<ModelValidator> validators = ModelValidatorProviders.Providers.GetValidators(modelMetadata, htmlHelper.ViewContext);
            foreach (ModelClientValidationRule rule in validators.SelectMany(v => v.GetClientValidationRules()))
            {
                fieldMetadata.ValidationRules.Add(rule);
            }

            return fieldMetadata;
        }

        public static MvcHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            string validationMessage = null;
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var strExpression = ExpressionHelper.GetExpressionText(expression);
            var modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(strExpression);
            //var formContext = htmlHelper.ViewContext.GetFormContextForClientValidation();
            var formContext = (htmlHelper.ViewContext.ClientValidationEnabled) ? htmlHelper.ViewContext.FormContext : null;

            if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
            {
                return null;
            }

            var modelState = htmlHelper.ViewData.ModelState[modelName];
            var modelErrors = (modelState == null) ? null : modelState.Errors;
            var modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

            if (modelError == null && formContext == null)
            {
                return null;
            }

            TagBuilder builder = new TagBuilder("div");
            builder.MergeAttributes(htmlAttributes);
            builder.AddCssClass((modelError != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);

            if (modelError != null)
            {
                builder.SetInnerText(modelError.ErrorMessage);
            }

            if (formContext != null)
            {
                bool replaceValidationMessageContents = String.IsNullOrEmpty(validationMessage);

                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    builder.MergeAttribute("data-valmsg-for", modelName);
                    builder.MergeAttribute("data-valmsg-replace", replaceValidationMessageContents.ToString().ToLowerInvariant());
                }
                else
                {
                    FieldValidationMetadata fieldMetadata = ApplyFieldValidationMetadata(htmlHelper, modelMetadata, modelName);
                    // rules will already have been written to the metadata object
                    fieldMetadata.ReplaceValidationMessageContents = replaceValidationMessageContents; // only replace contents if no explicit message was specified

                    // client validation always requires an ID
                    builder.GenerateId(modelName + "_validationMessage");
                    fieldMetadata.ValidationMessageId = builder.Attributes["id"];
                }
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString PopoverValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string validationMessage = null;
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var strExpression = ExpressionHelper.GetExpressionText(expression);
            var modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(strExpression);
            //var formContext = htmlHelper.ViewContext.GetFormContextForClientValidation();
            var formContext = (htmlHelper.ViewContext.ClientValidationEnabled) ? htmlHelper.ViewContext.FormContext : null;

            if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
            {
                return null;
            }

            var modelState = htmlHelper.ViewData.ModelState[modelName];
            var modelErrors = (modelState == null) ? null : modelState.Errors;
            var modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);

            if (modelError == null && formContext == null)
            {
                return null;
            }

            TagBuilder builder = new TagBuilder("a");
            builder.AddCssClass("icon-question-sign jqValErr");
            builder.MergeAttribute("style", "display:none;");
            builder.MergeAttribute("rel", "popover");

            if (modelError != null)
            {
                builder.SetInnerText(modelError.ErrorMessage);
            }

            if (formContext != null)
            {
                bool replaceValidationMessageContents = String.IsNullOrEmpty(validationMessage);

                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    builder.MergeAttribute("for", modelName);
                }
                else
                {
                    FieldValidationMetadata fieldMetadata = ApplyFieldValidationMetadata(htmlHelper, modelMetadata, modelName);
                    // rules will already have been written to the metadata object
                    fieldMetadata.ReplaceValidationMessageContents = replaceValidationMessageContents; // only replace contents if no explicit message was specified

                    // client validation always requires an ID
                    builder.GenerateId(modelName + "_validationMessage");
                    fieldMetadata.ValidationMessageId = builder.Attributes["id"];
                }
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}