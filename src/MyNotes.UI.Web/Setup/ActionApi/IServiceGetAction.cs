namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;

    public interface IServiceGetAction
    {
        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithPopup(string viewName);

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <typeparam name="TViewModel">Func return type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithPopup<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <typeparam name="TViewModel">Func of type TViewModel</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithPopup<TViewModel>(string viewName, Func<TViewModel> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithContent(string viewName);

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithContent<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithContent<TViewModel>(string viewName, Func<TViewModel> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithSlidingContent(string viewName);

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithSlidingContent<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithSlidingContent<TViewModel>(string viewName, Func<TViewModel> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithResult<TDto, TViewModel>(Func<TDto> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithResult<TDto>(Func<TDto> serviceQuery);

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="viewName">View name for result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithResult(string viewName);

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="viewName">View name for result</param>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction WithResult<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery);

        /// <summary>
        /// Executes all the options selected in fluent api
        /// </summary>
        /// <returns>Json response action result </returns>
        JsonResponseActionResult AsJsonResult();
    }
}