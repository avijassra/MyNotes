namespace MvcBase.WebHelper.Mvc.Extensions
{
    using System;
    using System.Web.Mvc;
    using System.Text;
    using System.Web.Routing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Web;
    using System.Globalization;

    public static class HtmlHelperExtension
    {
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

        public static MvcHtmlString ActionLinkWithFragment(this HtmlHelper htmlHelper, string text, ActionResult fragmentAction, string cssClass = null, string dataOptions = null)
        {
            var mvcActionResult = fragmentAction.AsMVCResult() as IMvcResult;

            if (mvcActionResult == null)
                return null;

            var options = string.Empty;

            var actionLink = string.Format("{0}#{1}",
                        RouteTable.Routes.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext,
                                                        new RouteValueDictionary(new
                                                        {
                                                            area = string.Empty,
                                                            controller = mvcActionResult.Controller,
                                                            action = string.Empty,
                                                        })).VirtualPath,
                         mvcActionResult.Action);

            if (!string.IsNullOrEmpty(dataOptions))
                options = "data-options=\"" + dataOptions.Trim() + "\"";
            
            return new MvcHtmlString(string.Format("<a id=\"{0}\" href=\"{1}\" class=\"jqAddress {2}\" {3}>{4}</a>", Guid.NewGuid(), actionLink, 
                (string.IsNullOrEmpty(cssClass) ? string.Empty : cssClass.Trim()),
                (string.IsNullOrEmpty(options) ? string.Empty : options.Trim()), text));
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