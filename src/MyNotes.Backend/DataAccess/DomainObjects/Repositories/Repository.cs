namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Linq;
    using MyNotes.Backend.Setup.Extensions;
    using System.Collections.Generic;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Add entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        public void Add(TEntity entity)
        {
            _session.Save(entity);
        }

        /// <summary>
        /// Update entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        public void Update(TEntity entity)
        {
            _session.Update(entity);
        }

        /// <summary>
        /// Delete entity of type TEntity
        /// </summary>
        /// <param name="entity">Id of type entity</param>
        public void Delete(Guid id)
        {
            _session.Delete(_session.Get<TEntity>(id));
        }

        /// <summary>
        /// Delete entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        public void Delete(TEntity entity)
        {
            _session.Delete(entity);
        }

        /// <summary>
        /// Find entity of type TEntity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(Guid id)
        {
            return _session.Get<TEntity>(id);
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(Expression<Func<TEntity, bool>> expression)
        {
            var query = _session.Query<TEntity>().Where(expression);

            return query.SingleOrDefault<TEntity>();
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="entityQuery">Entity query to fetch entity</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(IEntityQuery<TEntity> entityQuery)
        {
            return entityQuery.Execute();
        }

        /// <summary>
        /// Find all entities
        /// </summary>
        /// <returns>IQueryOver of entity of type TEntity</returns>
        public IQueryable<TEntity> FindAll()
        {
            var query = _session.Query<TEntity>();

            query.Cacheable().CacheMode(CacheMode.Normal);

            return query;
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>IQueryOver of entity of type TEntity</returns>
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            var query = _session.Query<TEntity>();

            query.Cacheable().CacheMode(CacheMode.Normal);

            return query.Where(expression);
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="entityListQuery">Entity list query </param>
        /// <returns>IQueryable of entity of type TEntity</returns>
        public IQueryable<TEntity> FindAll(IEntityListQuery<TEntity> entityListQuery)
        {
            return entityListQuery.Execute();
        }
    }
}
