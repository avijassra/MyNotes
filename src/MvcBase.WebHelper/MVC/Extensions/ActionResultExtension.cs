namespace MvcBase.WebHelper
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Action Result Extension class
    /// </summary>
    internal static class ActionResultExtension
    {
        /// <summary>
        /// Ases the MVC result.
        /// </summary>
        /// <param name="actionResult">The action result.</param>
        /// <returns>IMVCResult</returns>
        public static IMvcResult AsMVCResult(this ActionResult actionResult)
        {
            // actionResult null check
            if (null == actionResult)
            {
                throw new ArgumentNullException("actionResult", "actionResult cannot be a null reference (Nothing in Visual Basic)");
            }

            var mvcResult = new MvcResult();
            var properties = actionResult.GetType().GetProperties();

            if (properties != null)
            {
                mvcResult.Controller = (string)properties.Where(p => p.Name == "Controller").First().GetValue(actionResult, null);
                mvcResult.Action = (string)properties.Where(p => p.Name == "Action").First().GetValue(actionResult, null);
                mvcResult.RouteValueDictionary = (RouteValueDictionary)properties.Where(p => p.Name == "RouteValueDictionary").First().GetValue(actionResult, null);
            }

            return mvcResult;
        }
    }
}