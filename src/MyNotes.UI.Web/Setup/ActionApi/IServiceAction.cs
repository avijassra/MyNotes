namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public interface IServiceAction
    {
        /// <summary>
        /// Fetch action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceGetAction</returns>
        IServiceGetAction Fetch();

        /// <summary>
        /// Put action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceSetAction</returns>
        IServiceSetAction Put();
    }
}