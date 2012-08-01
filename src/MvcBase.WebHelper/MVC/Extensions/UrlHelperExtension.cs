namespace MvcBase.WebHelper.Mvc.Extensions
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    // <summary>
    /// UrlHelperExtension
    /// </summary>
    public static class UrlHelperExtension
    {
        /// <summary>
        /// URLs the with action.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="controllerAction">The controller action.</param>
        /// <returns>URL</returns>
        public static string UrlWithAction(this UrlHelper urlHelper, ActionResult controllerAction)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            var callInfo = controllerAction.AsMVCResult();

            return urlHelper.Action(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary);
        }

        /// <summary>
        /// URLs the with action fragment.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <returns>URL</returns>
        public static string UrlWithActionFragment(this UrlHelper urlHelper, ActionResult fragmentAction)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            object routeValue = new { area = string.Empty, controller = fragmentAction.AsMVCResult().Controller, action = string.Empty };

            return String.Format("{0}#!{1}", RouteTable.Routes.GetVirtualPathForArea(urlHelper.RequestContext, new RouteValueDictionary(routeValue)).VirtualPath, fragmentAction.AsMVCResult().Action);
        }
    }
}