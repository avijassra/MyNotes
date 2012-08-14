namespace MyNotes.UI.Web.Setup.StartupTasks.Registration
{
    using MvcBase.WebHelper;
    using Microsoft.Practices.Unity;
    using log4net;
    using MyNotes.UI.Web.EchoServiceRef;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;

    public class ServiceDependency : IRegisterDependency
    {
        public void Inject(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IServiceAction, ServiceAction>();
            unityContainer.RegisterType<IServiceGetAction, ServiceGetAction>();
            unityContainer.RegisterType<IServiceSetAction, ServiceSetAction>();

            unityContainer.RegisterType<IServiceNewAction, ServiceNewAction>();
            unityContainer.RegisterType<IServiceNewGetAction, ServiceNewGetAction>();
            unityContainer.RegisterType<IServiceNewSetAction, ServiceNewSetAction>();

            unityContainer.RegisterInstance<IAccountService>(new AccountServiceClient());
            unityContainer.RegisterInstance<IGroupService>(new GroupServiceClient());
            unityContainer.RegisterInstance<IEchoService>(new EchoServiceClient());
            unityContainer.RegisterInstance<IUserService>(new UserServiceClient());
        }
    }
}