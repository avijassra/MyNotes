namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MvcBase.WebHelper;
    using System.Reflection;
    using System.Linq.Expressions;

    public class ServiceNewSetAction : IServiceNewSetAction
    {
        Controller _controller;
        Expression _expression;
        ActionResult _onSuccess;
        bool _onSuccessIsFragmentLink;
        ActionResult _onFailure;
        bool _onFailureIsFragmentLink;

        public ServiceNewSetAction(Controller controller)
        {
            _controller = controller;
        }

        public IServiceNewSetAction WithCommand<TViewModel>(Expression<Func<TViewModel>> expression)
        {
            _expression = expression;
            return this;
        }

        public IServiceNewSetAction WithCommand<TDto>(Expression<Action<TDto>> expression)
        {
            _expression = expression;
            return this;
        }

        public IServiceNewSetAction OnSuccess(ActionResult actionResult)
        {
            return OnSuccess(actionResult, true);
        }

        public IServiceNewSetAction OnSuccess(ActionResult actionResult, bool isFragmentAction)
        {
            _onSuccess = actionResult;
            _onSuccessIsFragmentLink = isFragmentAction;
            return this;
        }

        public IServiceNewSetAction OnFailure(ActionResult actionResult)
        {
            return OnFailure(actionResult, true);
        }

        public IServiceNewSetAction OnFailure(ActionResult actionResult, bool isFragmentAction)
        {
            _onFailure = actionResult;
            _onFailureIsFragmentLink = isFragmentAction;
            return this;
        }

        public JsonResponseActionResult AsJsonResult()
        {
            var refreshOptions = new RefreshOptions();

            if (!_controller.ModelState.IsValid)
            {
                refreshOptions.HasError = true;
                refreshOptions.Message = "Please fix the errors";
            }
            else
            {
                //Func<int> method = _expression.ToLambda().CastTo<int>();
                //refreshOptions.ResultViewModel = method();
            }

            if (!refreshOptions.HasError && _onSuccess != null)
            {
                refreshOptions.RedirectUrl = (_onSuccessIsFragmentLink ?
                    _controller.Url.UrlWithActionFragment(_onSuccess) :
                    _controller.Url.UrlWithAction(_onSuccess));
            }
            else if (refreshOptions.HasError && _onFailure != null)
            {
                refreshOptions.RedirectUrl = (_onFailureIsFragmentLink ?
                    _controller.Url.UrlWithActionFragment(_onFailure) :
                    _controller.Url.UrlWithAction(_onFailure));
            }

            return new JsonResponseActionResult(refreshOptions);
        }
    }
}