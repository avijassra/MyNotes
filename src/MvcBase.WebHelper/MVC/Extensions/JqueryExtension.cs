namespace MvcBase.WebHelper.Mvc.Extensions
{
    using System;
    using System.Web.Mvc.Html;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Web.Routing;
    using MvcBase.WebHelper.Extensions;

    /// <summary>
    /// Extension class for JQuery.
    /// </summary>
    public static class JqueryExtension
    {
        /// <summary>
        /// Begins the jquery form.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="actionResult">The action result.</param>
        /// <returns>The MvcForm</returns>
        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult)
        {
            // ajaxHelper null check
            if (null == ajaxHelper)
            {
                throw new ArgumentNullException("ajaxHelper", "ajaxHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var callInfo = actionResult.AsMVCResult();
            return BeginJqueryForm(ajaxHelper, formId, callInfo.Action, callInfo.Controller, null, callInfo.RouteValueDictionary, string.Empty, null /* htmlAttributes */);
        }

        /// <summary>
        /// Begins the jquery form.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="actionResult">The action result.</param>
        /// <param name="callback">The callback function name</param>
        /// <returns>The MvcForm</returns>
        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult, string callback)
        {
            // ajaxHelper null check
            if (null == ajaxHelper)
            {
                throw new ArgumentNullException("ajaxHelper", "ajaxHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var callInfo = actionResult.AsMVCResult();
            return BeginJqueryForm(ajaxHelper, formId, callInfo.Action, callInfo.Controller, callback, callInfo.RouteValueDictionary, string.Empty, null /* htmlAttributes */);
        }

        /// <summary>
        /// Begins the jquery form.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="actionResult">The action result.</param>
        /// <param name="callback">The callback function name</param>
        /// <param name="cssClassNames">The form css class name(s)</param>
        /// <returns>The MvcForm</returns>
        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, ActionResult actionResult, string callback, string cssClassNames)
        {
            // ajaxHelper null check
            if (null == ajaxHelper)
            {
                throw new ArgumentNullException("ajaxHelper", "ajaxHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var callInfo = actionResult.AsMVCResult();
            return BeginJqueryForm(ajaxHelper, callInfo.Action, callInfo.Controller, formId, callback, callInfo.RouteValueDictionary, cssClassNames, null /* htmlAttributes */);
        }

        /// <summary>
        /// Begins the jquery form.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="callback">Callback function name.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="cssClassNames">The CSS class names.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>MvcForm</returns>
        public static MvcForm BeginJqueryForm(this AjaxHelper ajaxHelper, string formId, string actionName, string controllerName, string callback, RouteValueDictionary routeValues, string cssClassNames, IDictionary<string, object> htmlAttributes)
        {
            // ajaxHelper null check
            if (null == ajaxHelper)
            {
                throw new ArgumentNullException("ajaxHelper", "ajaxHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            // get target URL
            string formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true /* includeImplicitMvcValues */);
            return FormHelper(ajaxHelper, formId, formAction, true, callback, cssClassNames, htmlAttributes);
        }

        /// <summary>
        /// Forms the helper.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="formAction">The form action.</param>
        /// <param name="isAjax">If set to <c>true</c> [is ajax].</param>
        /// <param name="callback">The callback function name.</param>
        /// <param name="cssClassNames">The CSS class names.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>MvcForm</returns>
        private static MvcForm FormHelper(this AjaxHelper ajaxHelper, string formId, string formAction, bool isAjax, string callback, string cssClassNames, IDictionary<string, object> htmlAttributes)
        {
            // ajaxHelper null check
            if (null == ajaxHelper)
            {
                throw new ArgumentNullException("ajaxHelper", "ajaxHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("action", formAction);
            tagBuilder.MergeAttribute("method", @"post");

            var metadataBuilder = new System.Text.StringBuilder();
            metadataBuilder.AppendFormat("isAjax: {0}", isAjax.ToJavascriptString());

            if (!string.IsNullOrEmpty(callback))
            {
                metadataBuilder.AppendFormat(", callback: '{0}'", callback);
            }

            var metadata = "{" + metadataBuilder.ToString() + "}";

            tagBuilder.Attributes["data-options"] = metadata;
            tagBuilder.MergeAttribute("autocomplete", @"off");
            tagBuilder.MergeAttribute("class", "jqAjaxForm " + (!string.IsNullOrEmpty(cssClassNames) ? cssClassNames : string.Empty));
            tagBuilder.GenerateId(formId);
            ajaxHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            using (var form = new MvcForm(ajaxHelper.ViewContext))
            {
                if (ajaxHelper.ViewContext.ClientValidationEnabled)
                {
                    ajaxHelper.ViewContext.FormContext.FormId = tagBuilder.Attributes["id"];
                }
                return form;
            }
        }
    }
}