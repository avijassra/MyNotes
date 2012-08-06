namespace MyNotes.Backend.DataAccess.DomainObjects.StorageProxies.Queries
{
    using NHibernate;
    using NHibernate.Linq;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    

    public class AccoutListQuery : IEntityListQuery<Account>
    {
        ISession _session;

        public AccoutListQuery(ISession session) 
        {
            _session = session;
        }

        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>NhQueryable of Account</returns>
        public NhQueryable<Account> Execute()
        {
            return null;
        }
    }
}