namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web.Mvc;

    public interface IServiceSetAction
    {
        /// <summary>
        /// The server post/put action with command
        /// </summary>
        /// <param name="serviceCommand">Func with return type of bool</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction WithCommand<TViewModel>(Func<TViewModel> serviceCommand);

        /// <summary>
        /// The server post/put action with command
        /// </summary>
        /// <param name="arg">Arguments for action</param>
        /// <param name="serviceCommand">Func with return type of bool</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction WithCommand<TDto>(TDto arg, Action<TDto> serviceCommand);

        /// <summary>
        /// Action on success execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction OnSuccess(ActionResult actionResult);

        /// <summary>
        /// Action on success execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <param name="isFragmentAction">True if action is of type ajax</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction OnSuccess(ActionResult actionResult, bool isFragmentAction);

        /// <summary>
        /// Action on failed execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction OnFailure(ActionResult actionResult);

        /// <summary>
        /// Action on failed execution of the command
        /// </summary>
        /// <param name="actionResult">Action result</param>
        /// <param name="isFragmentAction">True if action is of type ajax</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction OnFailure(ActionResult actionResult, bool isFragmentAction);

        /// <summary>
        /// Executes all the options selected in fluent api
        /// </summary>
        /// <returns>Json response action result </returns>
        JsonResponseActionResult Execute();
    }
}