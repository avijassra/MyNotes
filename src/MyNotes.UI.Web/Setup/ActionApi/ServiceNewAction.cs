namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using Microsoft.Practices.Unity;

    public class ServiceNewAction : IServiceNewAction
    {
        Controller _controller;
        IServiceNewGetAction _serviceGetAction;
        IServiceNewSetAction _serviceSetAction;

        public ServiceNewAction(Controller controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Fetch action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceGetAction</returns>
        public IServiceNewSetAction Put()
        {
            _serviceSetAction = WebDependencyBuilder.Container.Resolve<IServiceNewSetAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", _controller)
                                   });
            return _serviceSetAction;
        }

        /// <summary>
        /// Put action api's for the server
        /// </summary>
        /// <returns>Object of type IServiceSetAction</returns>
        public IServiceNewGetAction Fetch()
        {
            _serviceGetAction = WebDependencyBuilder.Container.Resolve<IServiceNewGetAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", _controller)
                                   });
            return _serviceGetAction;
        }
    }
}