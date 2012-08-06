namespace MyNotes.Backend.DataAccess.DomainObjects.StorageProxies.Queries
{
    using NHibernate;
    using NHibernate.Linq;
    using System.Linq;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    using System;
    

    public class GroupNameExistsQuery : IEntityQuery<Group>
    {
        ISession _session;
        string _name;
        Guid _id;

        public GroupNameExistsQuery(ISession session, string name) 
            : this(session, name, Guid.Empty)
        {
        }

        public GroupNameExistsQuery(ISession session, string name, Guid id)
        {
            _session = session;
            _name = name.ToLower();
            _id = id;
        }

        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>Group object</returns>
        public Group Execute()
        {
            return _session.Query<Group>().Where(g => g.Id != _id && g.Name.ToLower() == _name).FirstOrDefault();
        }
    }
}