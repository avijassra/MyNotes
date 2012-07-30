namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;

    public interface IServiceGetAction
    {
        IServiceGetAction WithPopup<TViewModel>(string viewName, Func<TViewModel> query = null);

        IServiceGetAction WithContent<TViewModel>(string viewName, Func<TViewModel> query = null);

        IServiceGetAction WithResult<TViewModel>(Func<TViewModel> query);

        IServiceGetAction WithResult<TViewModel>(string viewName, Func<TViewModel> query = null);

        JsonResponseActionResult Execute();
    }
}