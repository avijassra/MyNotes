namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;
    using System.Linq.Expressions;

    public interface IServiceNewSetAction
    {
        /// <summary>
        /// The server post/put action with command
        /// </summary>
        /// <typeparam name="TViewModel">Func return type</typeparam>
        /// <param name="serviceCommand">Func with return type of bool</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction WithCommand<TViewModel>(Expression<Func<TViewModel>> expression);

        /// <summary>
        /// The server post/put action with command
        /// </summary>
        /// <param name="arg">Arguments for action</param>
        /// <param name="serviceCommand">Func with return type of bool</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction WithCommand<TDto>(Expression<Action<TDto>> expression);

        /// <summary>
        /// Action on success execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction OnSuccess(ActionResult actionResult);

        /// <summary>
        /// Action on success execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <param name="isFragmentAction">True if action is of type ajax</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction OnSuccess(ActionResult actionResult, bool isFragmentAction);

        /// <summary>
        /// Action on failed execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction OnFailure(ActionResult actionResult);

        /// <summary>
        /// Action on failed execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <param name="isFragmentAction">True if action is of type ajax</param>
        /// <returns>Object of type IServiceNewSetAction</returns>
        IServiceNewSetAction OnFailure(ActionResult actionResult, bool isFragmentAction);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        JsonResponseActionResult AsJsonResult();
    }
}