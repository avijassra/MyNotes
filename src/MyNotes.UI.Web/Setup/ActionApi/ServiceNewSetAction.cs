namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MvcBase.WebHelper;
    using System.Reflection;
    using System.Linq.Expressions;

    public class ServiceNewSetAction : ActionResult, IServiceNewSetAction
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

        public override void ExecuteResult(ControllerContext context)
        {
            var ajaxResponse = createAjaxResponse();

            new JsonResult { Data = ajaxResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet }.ExecuteResult(context);
        }

        protected AjaxResponse createAjaxResponse()
        {
            var refreshOptions = new RefreshOptions();
            string redirectLink = null;

            if (!_controller.ModelState.IsValid)
            {
                refreshOptions.HasError = true;
                refreshOptions.Message = "Please fix the errors";
            }
            else
            {
                //if(_expression.)
                //data = ;
            }

            //if (_hasError && _onSuccess != null)
            //{
            //    redirectLink = (_onSuccessIsFragmentAction ?
            //        _controller.Url.UrlWithActionFragment(_onSuccess) :
            //        _controller.Url.UrlWithAction(_onSuccess));
            //}
            //else if (!_hasError && _onFailure != null)
            //{
            //    redirectLink = (_onFailureIsFragmentAction ?
            //        _controller.Url.UrlWithActionFragment(_onFailure) :
            //        _controller.Url.UrlWithAction(_onFailure));
            //}

            return null;
        }
    }
}