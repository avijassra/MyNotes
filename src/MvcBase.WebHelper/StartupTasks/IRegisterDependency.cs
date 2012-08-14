namespace MvcBase.WebHelper
{
    using Microsoft.Practices.Unity;

    public interface IRegisterDependency
    {
        void Inject(IUnityContainer unityContainer);
    }
}
