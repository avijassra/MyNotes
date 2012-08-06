namespace MyNotes.Backend.DataAccess.DomainObjects.StorageProxies.Queries
{
    using NHibernate;
    using NHibernate.Linq;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    

    public class UserAuthenticationQuery : IEntityQuery<User>
    {
        ISession _session;

        public UserAuthenticationQuery(ISession session) 
        {
            _session = session;
        }

        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>User object</returns>
        public User Execute()
        {
            return null;
        }
    }
}