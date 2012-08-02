namespace MyNotes.Backend.Service.Implementations
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using log4net;
    using AutoMapper;
    using NHibernate;
    using Microsoft.Practices.Unity;
    using MyNotes.Backend.Setup.StartupTasks;
    using MyNotes.Backend.Service.Contracts;
    using MyNotes.Backend.Dtos;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    using MyNotes.Backend.DataAccess.StorageProxies;
    using System.Collections.Generic;

    internal class GroupService : IGroupService
    {
        ILog _logger;
        ISessionFactory _sessionFactory;

        public GroupService(ILog logger, ISessionFactory sessionFactory)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;
        }

        public GroupDto GetSingleGroup(Guid id)
        {
            return (new GroupStorageProxy(_sessionFactory)).GetSingle(id);
        }
        
        public IList<GroupDto> GetAllGroups()
        {
            return (new GroupStorageProxy(_sessionFactory)).GetAll();
        }

        public MessageResultDto AddGroup(string name)
        {
            return (new GroupStorageProxy(_sessionFactory)).AddGroup(name);
        }

        public MessageResultDto UpdateGroup(Guid id, string name)
        {
            return (new GroupStorageProxy(_sessionFactory)).UpdateGroup(id, name);
        }

        public MessageResultDto DeleteGroup(Guid id)
        {
            return (new GroupStorageProxy(_sessionFactory)).DeleteGroup(id);
        }
    }
}