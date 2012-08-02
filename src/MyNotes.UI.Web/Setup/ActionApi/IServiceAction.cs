namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public interface IServiceAction
    {
        /// <summary>
        /// Fetch action api's for the server
        /// </summary>
        /// <param name="controller">Controller object</param>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction Fetch(Controller controller);

        /// <summary>
        /// Put action api's for the server
        /// </summary>
        /// <param name="controller">Controller object</param>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction Put(Controller controller);
    }
}