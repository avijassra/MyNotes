namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Linq;
    using System.Collections.Generic;

    internal interface IRepository<TEntity>
    {
        /// <summary>
        /// Add entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity of type TEntity
        /// </summary>
        /// <param name="id">Id of the entity</param>
        void Delete(Guid id);

        /// <summary>
        /// Delete entity of type TEntity
        /// </summary>
        /// <param name="entity">Entity of type TEntity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Find entity of type TEntity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(Guid id);

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="entityQuery">Entity query to fetch entity</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(IEntityQuery<TEntity> entityQuery);

        /// <summary>
        /// Find all entities
        /// </summary>
        /// <returns>IQueryable of entity of type TEntity</returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>IQueryable of entity of type TEntity</returns>
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="entityListQuery">Entity list query </param>
        /// <returns>IQueryable of entity of type TEntity</returns>
        IQueryable<TEntity> FindAll(IEntityListQuery<TEntity> entityListQuery);
    }
}
