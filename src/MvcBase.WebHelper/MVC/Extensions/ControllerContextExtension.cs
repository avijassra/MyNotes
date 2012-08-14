namespace MvcBase.WebHelper
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    /// <summary>
    /// JQuery Form Extension
    /// </summary>
    public static class ControllerContextExtension
    {
        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>Rendered view</returns>
        public static string RenderPartialViewToString(this ControllerContext context, string viewName, object model)
        {
            // context null check
            if (null == context)
            {
                throw new ArgumentNullException("context", "context cannot be a null reference (Nothing in Visual Basic)");
            }

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return MvcHtmlString.Create(sw.GetStringBuilder().ToString()).ToHtmlString();
            }
        }
    }
}