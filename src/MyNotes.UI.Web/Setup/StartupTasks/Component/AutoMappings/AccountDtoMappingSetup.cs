namespace MyNotes.UI.Web.Setup.StartupTasks.Component.AutoMappings
{
    using AutoMapper;
    using MvcBase.WebHelper;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.ViewModels.Admin.Account;

    public class AccountDtoMappingSetup : IIncludeComponent
    {
        public void Setup()
        {
            Mapper.CreateMap<AccountDto, AccountViewModel>();

            Mapper.CreateMap<AccountDto, SaveAccountViewModel>();
        }
    }
}