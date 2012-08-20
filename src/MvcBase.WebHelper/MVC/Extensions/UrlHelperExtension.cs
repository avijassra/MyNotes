namespace MvcBase.WebHelper
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Linq;
    using System.Collections.Generic;

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
            return UrlWithActionFragment(urlHelper, fragmentAction, HashFormate.Standard);
        }

        /// <summary>
        /// URLs the with action fragment.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="fragmentAction">The fragment action.</param>
        /// <returns>URL</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Ajax url created for page history tracking")]
        public static string UrlWithActionFragment(this UrlHelper urlHelper, ActionResult fragmentAction, HashFormate hashFormate)
        {
            // urlHelper null check
            if (null == urlHelper)
            {
                throw new ArgumentNullException("urlHelper", "urlHelper cannot be a null reference (Nothing in Visual Basic)");
            }

            var hashStringFormat = string.Empty;
            var mvcResult = fragmentAction.AsMVCResult();
            var area = mvcResult.RouteValueDictionary.Where(x => x.Key == "Area").FirstOrDefault().Value.ToString();
            var queryString = (from routeVals in mvcResult.RouteValueDictionary
                               where routeVals.Key != "Area" && routeVals.Key != "Controller" && routeVals.Key != "Action"
                               select new KeyValuePair<string, object>(routeVals.Key, routeVals.Value)).AsQueryString();

            if (HashFormate.NoAction == hashFormate)
            {
                hashStringFormat = "{0}#!{2}";
            }
            else if (HashFormate.ActionAfterId == hashFormate)
            {
                hashStringFormat = "{0}#!{2}{1}";
            }
            else
            {
                hashStringFormat = "{0}#!{1}{2}";
            }

            object routeValue = new { area = area, controller = mvcResult.Controller, action = string.Empty };
            return String.Format(hashStringFormat, RouteTable.Routes.GetVirtualPathForArea(urlHelper.RequestContext, new RouteValueDictionary(routeValue)).VirtualPath, mvcResult.Action, queryString);
        }
    }
}