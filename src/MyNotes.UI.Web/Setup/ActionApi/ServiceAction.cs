namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;

    public class ServiceAction : IServiceAction
    {
        private IServiceGetAction _serviceGetAction;
        private IServiceSetAction _serviceSetAction;

        /// <summary>
        /// Fetch action api's for the server
        /// </summary>
        /// <param name="controller">Controller object</param>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceSetAction Put(Controller controller)
        {
            _serviceSetAction = new ServiceSetAction(controller);
            return _serviceSetAction;
        }

        /// <summary>
        /// Put action api's for the server
        /// </summary>
        /// <param name="controller">Controller object</param>
        /// <returns>Object of type IServiceSetAction</returns>
        public IServiceGetAction Fetch(Controller controller)
        {
            _serviceGetAction = new ServiceGetAction(controller);
            return _serviceGetAction;
        }
    }
}