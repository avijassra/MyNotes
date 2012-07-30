namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public interface IServiceAction
    {
        void Initialize(Controller controller);

        IServiceGetAction Fetch();

        IServiceSetAction Put();
    }
}