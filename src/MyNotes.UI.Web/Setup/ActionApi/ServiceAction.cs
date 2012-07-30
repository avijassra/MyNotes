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
        private Controller _controller;

        public ServiceAction(Controller controller)
        {
            _controller = controller;
        }

        public void Initialize(Controller controller)
        {
            _controller = controller;
        }

        public IServiceSetAction Put()
        {
            _serviceSetAction = new ServiceSetAction(_controller);
            return _serviceSetAction;
        }

        public IServiceGetAction Fetch()
        {
            _serviceGetAction = new ServiceGetAction(_controller);
            return _serviceGetAction;
        }
    }
}