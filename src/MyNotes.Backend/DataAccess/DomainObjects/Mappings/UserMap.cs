namespace MyNotes.Backend.DataAccess.DomainObjects.Mappings
{
    using FluentNHibernate.Mapping;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;

    public class UserMap : EntityBaseMap<User>
    {
        public UserMap()
        {
            Map(x => x.FirstName)
                .Not.Nullable();
            Map(x => x.LastName)
                .Not.Nullable();
            Map(x => x.Nickname);
            Map(x => x.Username)
                .Not.Nullable();
            Map(x => x.Password)
                .Not.Nullable();
            Map(x => x.IsLocked)
                .Not.Nullable();
            Map(x => x.FirstTimeReset)
                .Not.Nullable();
            References(x => x.Group)
                .Not.Nullable();
            HasMany<Account>(x => x.Accounts)
                .Inverse()
                .AsBag()
                .Cascade.SaveUpdate();
        }
    }
}
