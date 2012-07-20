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

    internal class GroupStorageProxy
    {
        ISessionFactory _sessionFactory;

        public GroupStorageProxy(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IList<GroupDto> GetAll()
        {
            IList<GroupDto> groupDtos = null;

            using (ISession session = _sessionFactory.OpenSession())
            {
                IRepository<Group> groupRepository = new Repository<Group>(session);
                var groups = groupRepository.FindAll().List();
                groupDtos = Mapper.Map<IList<GroupDto>>(groups);
            }

            return groupDtos;
        }

        public MessageResultDto AddGroup(string name)
        {
            Group group = null;
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Group> groupRepository = new Repository<Group>(session);
                var existingGroup = groupRepository.FindOne(new Tuple<Expression<Func<Group, object>>, string>(x => x.Name, name));

                if (null == existingGroup)
                {
                    group = new Group { Name = name };
                    groupRepository.Add(group);
                    transaction.Commit();
                    result.SuccessMessage("Group added successfully", group.Id);
                }
                else
                {
                    result.ErrorMessage("Group with same name already exisits");
                }
            }
            return result;
        }

        public MessageResultDto UpdateGroup(Guid id, string name)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Group> groupRepository = new Repository<Group>(session);
                var existingGroup = groupRepository.FindOne(x => x.Id != id, new Tuple<Expression<Func<Group, object>>, string>(x => x.Name, name));


                if (null == existingGroup)
                {
                    var group = groupRepository.FindOne(x => x.Id == id);
                    group.Name = name;
                    groupRepository.Update(group);
                    transaction.Commit();
                    result.SuccessMessage("Group updated successfully", group.Id);
                }
                else
                {
                    result.ErrorMessage("Group with same name already exisits");
                }
            }
            return result;
        }

        public MessageResultDto DeleteGroup(Guid id)
        {
            var result = new MessageResultDto();

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                IRepository<Group> groupRepository = new Repository<Group>(session);
                var group = groupRepository.FindOne(id);
                if (group.Users.Count > 0)
                {
                    result.ErrorMessage("Group has got users registered. Please remove the user(s) from the group first.");
                }
                else
                {
                    groupRepository.Delete(id);
                    transaction.Commit();
                    result.Message = "Group deleted successfully";
                }
            }
            return result;
        }
    }
}
