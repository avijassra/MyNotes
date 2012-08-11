namespace MyNotes.Backend.DataAccess.DomainObjects.StorageProxies.Queries
{
    using NHibernate;
    using NHibernate.Linq;
    using System.Linq;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    using System;
    

    public class AccountNameExistsQuery : IEntityQuery<Account>
    {
        ISession _session;
        Guid _userId;
        string _name;
        Guid _id;

        public AccountNameExistsQuery(ISession session, Guid userId, string name) 
            : this(session, userId, name, Guid.Empty)
        {
        }

        public AccountNameExistsQuery(ISession session, Guid userId, string name, Guid id)
        {
            _session = session;
            _userId = userId;
            _name = name.ToLower();
            _id = id;
        }

        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>Account object</returns>
        public Account Execute()
        {
            var accounts =  _session.Query<Account>().Where(a => a.User.Id == _userId && a.Name.ToLower() == _name);
            if (Guid.Empty != _id)
            {
                accounts = accounts.Where(a => a.Id != _id);
            }

            return accounts.FirstOrDefault();
        }
    }
}