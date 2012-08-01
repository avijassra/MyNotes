namespace MyNotes.UI.Web.Setup.Attributes
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.ViewModels.Shared;

    public class AdminAuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser).IsSysAccount)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
                base.OnActionExecuting(filterContext);
        }
    }
}