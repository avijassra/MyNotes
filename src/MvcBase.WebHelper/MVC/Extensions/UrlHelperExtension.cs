namespace MvcBase.WebHelper.Mvc.Extensions
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// UrlHelper Extension
    /// </summary>
    public static class UrlHelperExtension
    {
        /// <summary>
        /// URLs the with action.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="actionResult">The controller action.</param>
        /// <returns>URL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Ajax url created for page history tracking")]
        public static string UrlWithAction(this UrlHelper urlHelper, ActionResult actionResult)
        {
            // urlHelper null check
            if (null == urlHelper)
            {
                throw new ArgumentNullException("urlHelper", "urlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var callInfo = actionResult.AsMVCResult();
            return urlHelper.Action(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary);
        }

        /// <summary>
        /// URLs the with action fragment.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <returns>URL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Ajax url created for page history tracking")]
        public static string UrlWithActionFragment(this UrlHelper urlHelper, ActionResult fragmentAction)
        {
            // urlHelper null check
            if (null == urlHelper)
            {
                throw new ArgumentNullException("urlHelper", "urlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            object routeValue = new { area = string.Empty, controller = fragmentAction.AsMVCResult().Controller, action = string.Empty };
            return String.Format("{0}#!{1}", RouteTable.Routes.GetVirtualPathForArea(urlHelper.RequestContext, new RouteValueDictionary(routeValue)).VirtualPath, fragmentAction.AsMVCResult().Action);
        }
    }
}