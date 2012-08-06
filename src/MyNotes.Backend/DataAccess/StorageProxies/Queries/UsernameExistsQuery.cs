namespace MyNotes.Backend.DataAccess.DomainObjects.StorageProxies.Queries
{
    using NHibernate;
    using NHibernate.Linq;
    using System.Linq;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    using System;
    

    public class UsernameExistsQuery : IEntityQuery<User>
    {
        ISession _session;
        string _username;
        Guid _id;

        public UsernameExistsQuery(ISession session, string username)
            : this(session, username, Guid.Empty)
        {
        }

        public UsernameExistsQuery(ISession session, string username, Guid id)
        {
            _session = session;
            _username = username.ToLower();
            _id = id;
        }

        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>User object</returns>
        public User Execute()
        {
            return _session.Query<User>().Where(u => u.Id != _id && u.Username.ToLower() == _username).FirstOrDefault();
        }
    }
}