namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System.Web.Mvc;
    using MvcBase.WebHelper.Mvc.Extensions;

    public class JsonResponseActionResult : ActionResult
    {
        private RefreshOptions _refreshOptions;

        internal JsonResponseActionResult(RefreshOptions refreshOptions)
        {
            _refreshOptions = refreshOptions;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var ajaxResponse = GetAjaxResponse(context, _refreshOptions.PopupViewName, _refreshOptions.PopupViewModel, _refreshOptions.ContentViewName, _refreshOptions.ContentViewModel, 
                _refreshOptions.SlidingScreenId, _refreshOptions.ResultViewName, _refreshOptions.ResultViewModel, _refreshOptions.RedirectUrl, _refreshOptions.HasError, _refreshOptions.Message);

            new JsonResult { Data = ajaxResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet }.ExecuteResult(context);
        }

        private static AjaxResponse GetAjaxResponse(ControllerContext context, string popupViewName, object popupViewModel, string contentViewName, object contentViewModel, 
            string slidingScreenId, string resultViewName, object resultViewModel, string redirectUrl, bool hasError, string message)
        {
            var response = new AjaxResponse();

            response.HasError = hasError;
            response.Message = message;
            response.SlidingScreenId = slidingScreenId;

            if (!string.IsNullOrEmpty(popupViewName))
                response.PopupView = context.RenderPartialViewToString(popupViewName, popupViewModel);

            if (!string.IsNullOrEmpty(contentViewName))
                response.ContentView = context.RenderPartialViewToString(contentViewName, contentViewModel);

            if (!string.IsNullOrEmpty(resultViewName))
                response.Result = context.RenderPartialViewToString(resultViewName, resultViewModel);
            else if(resultViewModel != null)
                response.Result = resultViewModel;

            if (!string.IsNullOrEmpty(redirectUrl))
                response.RedirectUrl = redirectUrl;

            return response;
        }
    }
}