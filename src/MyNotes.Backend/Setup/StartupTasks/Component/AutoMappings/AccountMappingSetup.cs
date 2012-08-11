namespace MyNotes.Backend.Setup.StartupTasks.Component.AutoMappings
{
    using System.Linq;
    using AutoMapper;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.Dtos;

    internal class AccountMappingSetup : IIncludeComponent
    {
        public void Add()
        {
            Mapper.CreateMap<Account, AccountDto>()
                .ForMember(d => d.Balance, o => o.MapFrom(s => s.Transactions.Sum(t => t.Balance)))
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.Id))
                .ForMember(d => d.UserNickname, o => o.MapFrom(s => s.User.Nickname));
        }
    }
}