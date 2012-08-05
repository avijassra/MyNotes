namespace MyNotes.Backend.DataAccess.StorageProxies
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

    internal class AccountStorageProxy
    {
        ISessionFactory _sessionFactory;

        public AccountStorageProxy(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public AccountDto GetSingle(Guid id)
        {
            AccountDto accountDto = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<Account> accountRepository = new Repository<Account>(session);
                var account = accountRepository.FindOne(id);
                accountDto = Mapper.Map<AccountDto>(account);
            }

            return accountDto;
        }

        public IList<AccountDto> GetAllInGroup(Guid groupId)
        {
            IList<AccountDto> accountDtos = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                var accounts = (from user in session.QueryOver<User>()
                               where user.Group.Id == groupId
                               select user.Accounts).List();
                accountDtos = Mapper.Map<IList<AccountDto>>(accounts);
            }

            return accountDtos;
        }

        public IList<AccountDto> GetAllWithUser(Guid groupId, Guid userId)
        {
            IList<AccountDto> accountDtos = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<Account> accountRepository = new Repository<Account>(session);
                var accounts = accountRepository.FindAll(x => x.User.Id == userId).List();
                accountDtos = Mapper.Map<IList<AccountDto>>(accounts);
            }

            return accountDtos;
        }

        public MessageResultDto AddAccount(string name, Guid userId)
        {
            Account account;
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<User> userRepository = new Repository<User>(session);
                var user = userRepository.FindOne(x => x.Id == userId);

                IRepository<Account> accountRepository = new Repository<Account>(session);
                var existingAccount = accountRepository.FindOne(new Tuple<Expression<Func<Account, object>>, string>(x => x.Name, name));

                if (null == existingAccount)
                {
                    account = new Account
                    {
                        Name = name,
                        User = user
                    };
                    accountRepository.Add(account);
                    transaction.Commit();
                    result.SuccessMessage("Account added successfully", account.Id);
                }
                else
                {
                    result.ErrorMessage("Account with same name already exists");
                }
            }
            return result;
        }

        public MessageResultDto UpdateAccount(Guid id, string name, Guid userId)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Account> accountRepository = new Repository<Account>(session);
                var account = accountRepository.FindOne(x => x.Id == id);

                if (account.User.Id != userId)
                {
                    IRepository<User> userRepository = new Repository<User>(session);
                    var user = userRepository.FindOne(x => x.Id == userId);
                    account.User= user;
                }

                account.Name = name;
                accountRepository.Add(account);
                transaction.Commit();
                result.Message = "Account updated successfully";
            }
            return result;
        }

        public MessageResultDto DeleteAccount(Guid id)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Account> accountRepository = new Repository<Account>(session);
                var account = accountRepository.FindOne(id);
                if (account.Transactions.Count > 0)
                {
                    result.ErrorMessage("Account has got some transaction(s). Please remove all transactions first");
                }
                else
                {
                    accountRepository.Delete(id);
                    transaction.Commit();
                    result.Message = "Account deleted successfully";
                }
            }
            return result;
        }
    }
}