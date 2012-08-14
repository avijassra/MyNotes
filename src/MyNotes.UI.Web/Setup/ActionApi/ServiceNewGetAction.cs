﻿namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using AutoMapper;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public class ServiceNewGetAction : IServiceNewGetAction
    {
        private Controller _controller;
        private string _slidingScreenId;
        private string _contentViewName;
        private object _contentViewModel;
        private string _popupViewName;
        private object _popupViewModel;
        private string _resultViewName;
        private object _resultViewModel;

        public ServiceNewGetAction(Controller controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithPopup(string viewName)
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
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithPopup<TViewModel>(string viewName, Func<TViewModel> serviceQuery)
        {
            return WithPopup<TViewModel, TViewModel>(viewName, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the popup container
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <typeparam name="TViewModel">Mapping Func type to object type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model of type</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithPopup<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery)
        {
            _popupViewName = viewName;

            if (serviceQuery != null)
            {
                var dto = serviceQuery();
                if (typeof(TDto) != typeof(TViewModel))
                {
                    _popupViewModel = Mapper.Map<TViewModel>(dto);
                }
                else
                {
                    _popupViewModel = dto;
                }
            }

            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithContent(string viewName)
        {
            _contentViewName = viewName;
            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithContent<TViewModel>(string viewName, Func<TViewModel> serviceQuery)
        {
            return WithContent<TViewModel, TViewModel>(viewName, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which replaces the html in the main html container
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <typeparam name="TViewModel">Mapping Func type to object type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithContent<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery)
        {
            _contentViewName = viewName;

            if (serviceQuery != null)
            {
                var dto = serviceQuery();
                if (typeof(TDto) != typeof(TViewModel))
                {
                    _contentViewModel = Mapper.Map<TViewModel>(dto);
                }
                else
                {
                    _contentViewModel = dto;
                }
            }

            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithSlidingContent(string viewName)
        {
            _slidingScreenId = getSlidingScreenId(viewName);
            return WithContent(viewName);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithSlidingContent<TViewModel>(string viewName, Func<TViewModel> serviceQuery)
        {
            return WithSlidingContent<TViewModel, TViewModel>(viewName, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render which put it in the sliding html in the main html container
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <typeparam name="TViewModel">Mapping Func type to object type</typeparam>
        /// <param name="viewName">View name</param>
        /// <param name="serviceQuery">Func to return view model for the view</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithSlidingContent<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery)
        {
            _slidingScreenId = getSlidingScreenId(viewName);
            return WithContent<TDto,TViewModel>(viewName, serviceQuery);
        }
        
        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <typeparam name="TViewModel">Mapping Func type to object type</typeparam>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithResult<TDto, TViewModel>(Func<TDto> serviceQuery)
        {
            return WithResult<TDto, TViewModel>(null, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithResult<TDto>(Func<TDto> serviceQuery)
        {
            return WithResult<TDto, TDto>(null, serviceQuery);
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <param name="viewName">View name for result</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithResult(string viewName)
        {
            _resultViewName = viewName;
            return this;
        }

        /// <summary>
        /// In this method, we can specify the view we want to render as result and we can use it on client side
        /// </summary>
        /// <typeparam name="TDto">Func return type</typeparam>
        /// <typeparam name="TViewModel">Mapping Func type to object type</typeparam>
        /// <param name="viewName">View name for result</param>
        /// <param name="serviceQuery">Func to return view model for the result</param>
        /// <returns>Object of type IServiceNewGetAction</returns>
        public IServiceNewGetAction WithResult<TDto, TViewModel>(string viewName, Func<TDto> serviceQuery)
        {
            _resultViewName = viewName;

            if (serviceQuery != null)
            {
                var dto = serviceQuery();
                if (typeof(TDto) != typeof(TViewModel))
                {
                    _resultViewModel = Mapper.Map<TViewModel>(dto);
                }
                else
                {
                    _resultViewModel = dto;
                }
            }

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
                        SlidingScreenId = _slidingScreenId,
                        PopupViewName = _popupViewName,
                        PopupViewModel = _popupViewModel,
                        ResultViewName = _resultViewName,
                        ResultViewModel = _resultViewModel,
                    }
                );
        }

        private string getSlidingScreenId(string viewName)
        {
            return viewName
                .Substring(viewName.LastIndexOf('/') + 1)
                .Replace(".cshtml","")
                .Replace("_", "jq");
        }
    }
}