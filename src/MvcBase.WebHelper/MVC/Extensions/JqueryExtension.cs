﻿namespace MvcBase.WebHelper.Mvc.Extensions
{
    using System.Web.Mvc.Html;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Web.Routing;
    using MvcBase.WebHelper.Extensions;

    public static class JqueryExtension
    {
        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult)
        {
            var callInfo = actionResult.AsMVCResult();

            return BeginJqueryForm(ajaxHelper, formId, callInfo.Action, callInfo.Controller, null, callInfo.RouteValueDictionary, "", null /* htmlAttributes */);
        }

        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult, string callback)
        {
            var callInfo = actionResult.AsMVCResult();

            return BeginJqueryForm(ajaxHelper, formId, callInfo.Action, callInfo.Controller, callback, callInfo.RouteValueDictionary, "", null /* htmlAttributes */);
        }

        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult, string callback, string cssClassNames)
        {
            var callInfo = actionResult.AsMVCResult();

            return BeginJqueryForm(ajaxHelper, callInfo.Action, callInfo.Controller, formId, callback, callInfo.RouteValueDictionary, cssClassNames, null /* htmlAttributes */);
        }

        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, string actionName, string controllerName, string callback, RouteValueDictionary routeValues, string cssClassNames, IDictionary<string, object> htmlAttributes)
        {
            // get target URL
            string formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true /* includeImplicitMvcValues */);
            return FormHelper(ajaxHelper, formId, formAction, true, callback, cssClassNames, htmlAttributes);
        }

        private static MvcForm FormHelper(this AjaxHelper ajaxHelper, string formId, string formAction, bool isAjax, string callback, string cssClassNames, 
            IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("form");

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("action", formAction);
            tagBuilder.MergeAttribute("method", @"post");

            var metadataBuilder = new System.Text.StringBuilder();
            metadataBuilder.AppendFormat("isAjax: {0}", isAjax.ToJavascriptString());
            
            if(!string.IsNullOrEmpty(callback))
                metadataBuilder.AppendFormat(", callback: '{0}'", callback);

            var metadata = "{" + metadataBuilder.ToString() + "}";

            tagBuilder.Attributes["data-options"] = metadata;
            tagBuilder.MergeAttribute("autocomplete", @"off");
            tagBuilder.MergeAttribute("class", "jqAjaxForm " + (!string.IsNullOrEmpty(cssClassNames) ? cssClassNames : string.Empty));
            tagBuilder.GenerateId(formId);
            ajaxHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            var form = new MvcForm(ajaxHelper.ViewContext);
            if (ajaxHelper.ViewContext.ClientValidationEnabled)
                ajaxHelper.ViewContext.FormContext.FormId = tagBuilder.Attributes["id"];

            return form;
        }
    }
}