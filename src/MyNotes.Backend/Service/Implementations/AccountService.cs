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

    internal class AccountService : IAccountService
    {
        ILog _logger;
        ISessionFactory _sessionFactory;

        public AccountService(ILog logger, ISessionFactory sessionFactory)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;
        }

        public AccountDto GetSingleAccount(Guid id)
        {
            return (new AccountStorageProxy(_sessionFactory)).GetSingle(id);
        }

        public IList<AccountDto> GetAllAccountsInGroup(Guid groupId)
        {
            return (new AccountStorageProxy(_sessionFactory)).GetAllInGroup(groupId);
        }

        public IList<AccountDto> GetAllUserAccounts(Guid groupId, Guid userId)
        {
            return (new AccountStorageProxy(_sessionFactory)).GetAllWithUser(groupId, userId);
        }

        public MessageResultDto AddAccount(string name, Guid userId)
        {
            return (new AccountStorageProxy(_sessionFactory)).AddAccount(name, userId);
        }

        public MessageResultDto UpdateAccount(Guid id, string name, Guid userId)
        {
            return (new AccountStorageProxy(_sessionFactory)).UpdateAccount(id, name, userId);
        }

        public MessageResultDto DeleteAccount(Guid id)
        {
            return (new AccountStorageProxy(_sessionFactory)).DeleteAccount(id);
        }
    }
}