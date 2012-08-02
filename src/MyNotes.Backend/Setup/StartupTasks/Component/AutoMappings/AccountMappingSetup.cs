namespace MyNotes.Backend.Setup.StartupTasks.Component.AutoMappings
{
    using AutoMapper;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.Dtos;

    internal class AccountMappingSetup : IIncludeComponent
    {
        public void Add()
        {
            Mapper.CreateMap<Account, AccountDto>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.Id));
        }
    }
}