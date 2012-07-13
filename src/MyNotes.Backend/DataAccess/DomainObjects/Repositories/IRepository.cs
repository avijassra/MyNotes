namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using System;
    using NHibernate;
    using System.Linq.Expressions;
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
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression);

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="caseSensitiveExpression">List of tuple case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions);

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> expression, Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression);

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">List of case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> expression, List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions);

        /// <summary>
        /// Find all entities
        /// </summary>
        /// <returns>IQueryOver of entity of type TEntity</returns>
        IQueryOver<TEntity> FindAll();

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        IQueryOver<TEntity> FindAll(Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression);

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="caseSensitiveExpression">List of tuple case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        IQueryOver<TEntity> FindAll(List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions);

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression);

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">List of case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions);
    }
}
