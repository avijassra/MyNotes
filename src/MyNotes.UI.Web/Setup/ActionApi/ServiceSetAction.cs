namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MvcBase.WebHelper;
    using System.Reflection;

    public class ServiceSetAction : IServiceSetAction
    {
        Controller _controller;
        object _result;
        bool _hasError = false;
        string _message = null;
        ActionResult _onSuccess;
        bool _onSuccessIsFragmentAction;
        ActionResult _onFailure;
        bool _onFailureIsFragmentAction;

        public ServiceSetAction(Controller controller)
        {
            _controller = controller;
        }

        public IServiceSetAction WithCommand<TViewModel>(Func<TViewModel> serviceCommand)
        {
            if (_controller.ModelState.IsValid)
            {
                try
                {
                    _result = serviceCommand();
                }
                catch(Exception e)
                {
                    _hasError = true;
                    _message = e.Message;
                }
            }
            
            return this;
        }

        public IServiceSetAction WithCommand<TDto>(TDto arg, Action<TDto> serviceCommand)
        {
            if (_controller.ModelState.IsValid)
            {
                serviceCommand(arg);
            }
            return this;
        }

        public IServiceSetAction OnSuccess(ActionResult actionResult)
        {
            return OnSuccess(actionResult, true);
        }

        public IServiceSetAction OnSuccess(ActionResult actionResult, bool isFragmentAction)
        {
            _onSuccess = actionResult;
            _onSuccessIsFragmentAction = isFragmentAction;
            return this;
        }

        public IServiceSetAction OnFailure(ActionResult actionResult)
        {
            return OnFailure(actionResult, true);
        }

        public IServiceSetAction OnFailure(ActionResult actionResult, bool isFragmentAction)
        {
            _onFailure = actionResult;
            _onFailureIsFragmentAction = isFragmentAction;
            return this;
        }

        public JsonResponseActionResult AsJsonResult()
        {
            object data = null;
            string redirectLink = null;

            if (!_controller.ModelState.IsValid)
            {
                _hasError = true;
                _message = "Please fix the errors";
            }
            else
            {
                data = _result;
            }
            
            if (_hasError && _onSuccess != null)
            {
                redirectLink = (_onSuccessIsFragmentAction ?
                    _controller.Url.UrlWithActionFragment(_onSuccess) :
                    _controller.Url.UrlWithAction(_onSuccess));
            }
            else if(!_hasError && _onFailure!=null)
            {
                redirectLink = (_onFailureIsFragmentAction ?
                    _controller.Url.UrlWithActionFragment(_onFailure) :
                    _controller.Url.UrlWithAction(_onFailure));
            }
            
            return new JsonResponseActionResult(
                    new RefreshOptions()
                    {
                        HasError = _hasError,
                        Message = _message,
                        ResultViewModel = data,
                        RedirectUrl = redirectLink,
                    }
                );
        }
    }
}