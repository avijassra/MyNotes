namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using AutoMapper;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public class ServiceGetAction : IServiceGetAction
    {
        private Controller _controller;
        private string _contentViewName;
        private object _contentViewModel;
        private string _popupViewName;
        private object _popupViewModel;
        private string _resultViewName;
        private object _resultViewModel;

        public ServiceGetAction(Controller controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithPopup(string viewName)
        {
            _popupViewName = viewName;
            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <typeparam name="TViewModel">Func of type TViewModel</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithPopup<TViewModel>(string viewName, Func<TViewModel> serviceQuery)
        {
            _popupViewName = viewName;

            if (serviceQuery != null)
                _popupViewModel = Mapper.Map<TViewModel>(serviceQuery());

            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithContent(string viewName)
        {
            _contentViewName = viewName;
            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithContent<TViewModel>(string viewName, Func<TViewModel> serviceQuery)
        {
            _contentViewName = viewName;

            if (serviceQuery != null)
                _contentViewModel = Mapper.Map<TViewModel>(serviceQuery());

            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithResult<TViewModel>(Func<TViewModel> serviceQuery)
        {
            return WithResult<TViewModel>(null, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="viewName">View name for result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithResult(string viewName)
        {
            _resultViewName = viewName;
            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="viewName">View name for result</param>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceGetAction WithResult<TViewModel>(string viewName, Func<TViewModel> serviceQuery = null)
        {
            _resultViewName = viewName;

            if (serviceQuery != null)
                _resultViewModel = Mapper.Map<TViewModel>(serviceQuery());

            return this;
        }

        /// <summary>
        /// Executes all the options selected in fluent api
        /// </summary>
        /// <returns>Json response action result </returns>
        public JsonResponseActionResult Execute()
        {
            return new JsonResponseActionResult(
                    new RefreshOptions()
                    {
                        ContentViewName = _contentViewName,
                        ContentViewModel = _contentViewModel,
                        PopupViewName = _popupViewName,
                        PopupViewModel = _popupViewModel,
                        ResultViewName = _resultViewName,
                        ResultViewModel = _resultViewModel,
                    }
                );
        }
    }
}