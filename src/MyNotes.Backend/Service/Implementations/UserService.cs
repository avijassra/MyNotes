﻿namespace MyNotes.Backend.Service.Implementations
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

    internal class UserService : IUserService
    {
        ILog _logger;
        ISessionFactory _sessionFactory;

        public UserService(ILog logger, ISessionFactory sessionFactory)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;
        }

        public LoggedUserInfoDto Authenticate(string username, string password)
        {
            return (new UserStorageProxy(_sessionFactory)).ValidateUser(username, password);
        }
    }
}