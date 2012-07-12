namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Common;
    using MvcBase.WebHelper.Mvc.Extensions;
    using MyNotes.UI.Web.AdminServiceRef;

    public class ServiceSetAction : IServiceSetAction
    {
        private Controller _controller;
        private SessionKey _sessionKey;
        private Func<MessageResultDto> _serviceCommand;
        private ActionResult _onSuccess;
        private bool _onSuccessIsFragmentAction;
        private ActionResult _onFailure;
        private bool _onFailureIsFragmentAction;

        public ServiceSetAction(Controller controller, SessionKey sessionKey)
        {
            _controller = controller;
            _sessionKey = sessionKey;
        }

        public IServiceSetAction WithCommand(Func<MessageResultDto> serviceCommand)
        {
            _serviceCommand = serviceCommand;
            return this;
        }

        public IServiceSetAction OnSuccess(ActionResult actionResult, bool isFragmentAtion = true)
        {
            _onSuccess = actionResult;
            _onSuccessIsFragmentAction = isFragmentAtion;
            return this;
        }

        public IServiceSetAction OnFailure(ActionResult actionResult, bool isFragmentAtion = true)
        {
            _onFailure = actionResult;
            _onFailureIsFragmentAction = isFragmentAtion;
            return this;
        }

        public JsonResponseActionResult Execute()
        {
            var hasError = false;
            string message = null;
            object data = null;
            var commandSuccessfull = new MessageResultDto();
            string redirectLink = null;

            if (!_controller.ModelState.IsValid)
            {
                hasError = true;
                message = "Please fix the errors";
            }
            else
            {
                commandSuccessfull = _serviceCommand();
                hasError = commandSuccessfull.HasError;
                message = commandSuccessfull.Message;
                data = commandSuccessfull.Id;
            }
            
            if (hasError && _onSuccess != null)
            {
                redirectLink = (_onSuccessIsFragmentAction ?
                    _controller.Url.UrlWithActionFragment(_onSuccess) :
                    _controller.Url.UrlWithAction(_onSuccess));
            }
            else if(!hasError && _onFailure!=null)
            {
                redirectLink = (_onFailureIsFragmentAction ?
                    _controller.Url.UrlWithActionFragment(_onFailure) :
                    _controller.Url.UrlWithAction(_onFailure));
            }
            
            return new JsonResponseActionResult(
                    new RefreshOptions()
                    {
                        HasError = hasError,
                        Message = message,
                        ResultViewModel = data,
                        RedirectUrl = redirectLink,
                    }
                );
        }
    }
}