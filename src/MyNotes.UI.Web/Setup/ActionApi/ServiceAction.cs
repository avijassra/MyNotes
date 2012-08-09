namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using Microsoft.Practices.Unity;

    public class ServiceAction : IServiceAction
    {
        Controller _controller;
        IServiceGetAction _serviceGetAction;
        IServiceSetAction _serviceSetAction;

        public ServiceAction(Controller controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Fetch action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceSetAction Put()
        {
            _serviceSetAction = WebDependencyBuilder.Container.Resolve<IServiceSetAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", _controller)
                                   });
            return _serviceSetAction;
        }

        /// <summary>
        /// Put action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceSetAction</returns>
        public IServiceGetAction Fetch()
        {
            _serviceGetAction = WebDependencyBuilder.Container.Resolve<IServiceGetAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", _controller)
                                   });
            return _serviceGetAction;
        }
    }
}