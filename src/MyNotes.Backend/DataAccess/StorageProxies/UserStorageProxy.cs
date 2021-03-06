﻿namespace MyNotes.Backend.DataAccess.StorageProxies
{
    using System;
    using AutoMapper;
    using NHibernate;
    using MyNotes.Backend.DataAccess.DomainObjects.Entities;
    using MyNotes.Backend.DataAccess.DomainObjects.Repositories;
    using MyNotes.Backend.Dtos;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using MyNotes.Backend.Setup.Helper;

    internal class UserStorageProxy
    {
        ISessionFactory _sessionFactory;

        public UserStorageProxy(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public LoggedUserInfoDto ValidateUser(string username, string password)
        {
            LoggedUserInfoDto userDto = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(x => x.Username == username && x.Password == password && !x.IsLocked);
                userDto = Mapper.Map<LoggedUserInfoDto>(user);
            }

            return userDto;
        }

        public UserDto GetSingle(Guid id)
        {
            UserDto userDto = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(id);
                userDto = Mapper.Map<UserDto>(user);
            }

            return userDto;
        }

        public IList<UserDto> GetAll()
        {
            IList<UserDto> userDtos = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var users = userRepository.FindAll().List();
                userDtos = Mapper.Map<IList<UserDto>>(users);
            }

            return userDtos;
        }

        public IList<UserDto> GetAllInGroup(Guid groupId)
        {
            IList<UserDto> userDtos = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var users = userRepository.FindAll(x => x.Group.Id ==  groupId).List();
                userDtos = Mapper.Map<IList<UserDto>>(users);
            }

            return userDtos;
        }

        public MessageResultDto AddUser(string firstname, string lastname, string nickname, string username, Guid groupId)
        {
            User user;
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Group> groupRepository = new Repository<Group>(session);
                var group = groupRepository.FindOne(x => x.Id == groupId);

                IRepository<User> userRepository = new Repository<User>(session);
                var existingUser = userRepository.FindOne(new Tuple<Expression<Func<User, object>>, string>(x => x.Username, username));

                if (null == existingUser)
                {
                    user = new User
                    {
                        FirstName = firstname,
                        LastName = lastname,
                        Nickname = nickname,
                        Username = username,
                        Password = Constants.DEFAULT_PASSWORD,
                        Group = group
                    };
                    userRepository.Add(user);
                    transaction.Commit();
                    result.SuccessMessage("User added successfully", user.Id);
                }
                else
                {
                    result.ErrorMessage("User with same username already exists");
                }
            }
            return result;
        }

        public MessageResultDto UpdateUser(Guid id, string firstname, string lastname, string nickname, string username, Guid groupId)
        {
            var result = new MessageResultDto();
            result.Message = "User updated successfully";

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(x => x.Id == id);

                if (user.Group.Id == groupId)
                {
                    IRepository<Group> groupRepository = new Repository<Group>(session);
                    var group = groupRepository.FindOne(x => x.Id == groupId);
                    user.Group = group;
                }

                user.FirstName = firstname;
                user.LastName = lastname;
                user.Nickname = nickname;
                user.Username = username;

                userRepository.Add(user);

                transaction.Commit();
            }
            return result;
        }

        public MessageResultDto DeleteUser(Guid id)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(id);
                if (user.Accounts.Count > 0)
                {
                    result.ErrorMessage("User has got accounts registered. Please remove all accounts first");
                }
                else
                {
                    userRepository.Delete(id);
                    transaction.Commit();
                    result.Message = "User deleted successfully";
                }
            }
            return result;
        }

        public MessageResultDto ResetPassword(Guid id)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(id);
                user.Password = Constants.DEFAULT_PASSWORD;
                transaction.Commit();
                result.Message = "Password has been reset successfully";
            }
            return result;
        }

        public MessageResultDto LockedStatus(Guid id, bool isLocked)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(id);
                user.IsLocked = isLocked ;
                transaction.Commit();
                result.Message = string.Format("User has been {0} successsfully", isLocked ? "locked" : "unlocked");
            }
            return result;
        }
    }
}
