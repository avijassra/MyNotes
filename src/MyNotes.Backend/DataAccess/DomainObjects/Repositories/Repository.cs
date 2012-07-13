namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using System;
    using NHibernate;
    using System.Linq.Expressions;
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
            var query = _session.QueryOver<TEntity>().Where(expression);

            return query.SingleOrDefault<TEntity>();
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression)
        {
            return FindOne(new List<Tuple<Expression<Func<TEntity, object>>, string>> { caseSensitiveExpression });
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="caseSensitiveExpression">List of tuple case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions)
        {
            var query = _session.QueryOver<TEntity>();

            foreach(var caseSensitiveExpression in caseSensitiveExpressions)
            {
                if (null != query)
                    query = query.Where(caseSensitiveExpression.Item1, caseSensitiveExpression.Item2);
                else
                    break;
            }

            if (null != query)
                return query.Take(1).SingleOrDefault<TEntity>();
            else
                return null;
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(Expression<Func<TEntity, bool>> expression, Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression)
        {
            return FindOne(expression, new List<Tuple<Expression<Func<TEntity, object>>, string>> { caseSensitiveExpression });
        }

        /// <summary>
        /// Find entity of type TEntity by expresion
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">List of case sensitive expresion</param>
        /// <returns>Entity of type TEntity</returns>
        public TEntity FindOne(Expression<Func<TEntity, bool>> expression, List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions)
        {
            var query = _session.QueryOver<TEntity>().Where(expression);

            foreach (var caseSensitiveExpression in caseSensitiveExpressions)
            {
                if (null != query)
                    query = query.Where(caseSensitiveExpression.Item1, caseSensitiveExpression.Item2);
                else
                    break;
            }

            if (null != query)
                return query.Take(1).SingleOrDefault<TEntity>();
            else
                return null;
        }

        /// <summary>
        /// Find all entities
        /// </summary>
        /// <returns>IQueryOver of entity of type TEntity</returns>
        public IQueryOver<TEntity> FindAll()
        {
            var query = _session.QueryOver<TEntity>();

            query.Cacheable().CacheMode(CacheMode.Normal);

            return query;
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <returns>IQueryOver of entity of type TEntity</returns>
        public IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            var query = _session.QueryOver<TEntity>();

            query.Cacheable().CacheMode(CacheMode.Normal);

            return query.Where(expression);
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        public IQueryOver<TEntity> FindAll(Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression)
        {
            return FindAll(new List<Tuple<Expression<Func<TEntity, object>>, string>> { caseSensitiveExpression });
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="caseSensitiveExpression">List of tuple case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        public IQueryOver<TEntity> FindAll(List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions)
        {
            var query = _session.QueryOver<TEntity>();

            foreach (var caseSensitiveExpression in caseSensitiveExpressions)
            {
                if (null != query)
                    query = query.Where(caseSensitiveExpression.Item1, caseSensitiveExpression.Item2);
                else
                    break;
            }

            return query;
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">Case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        public IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, Tuple<Expression<Func<TEntity, object>>, string> caseSensitiveExpression)
        {
            return FindAll(expression, new List<Tuple<Expression<Func<TEntity, object>>, string>> { caseSensitiveExpression });
        }

        /// <summary>
        /// Find all entities with filter
        /// </summary>
        /// <param name="expression">Linq expression</param>
        /// <param name="caseSensitiveExpression">List of case sensitive expresion</param>
        /// <returns>IQueryOver of entities of type TEntity</returns>
        public IQueryOver<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, List<Tuple<Expression<Func<TEntity, object>>, string>> caseSensitiveExpressions)
        {
            var query = _session.QueryOver<TEntity>().Where(expression);

            foreach (var caseSensitiveExpression in caseSensitiveExpressions)
            {
                if (null != query)
                    query = query.Where(caseSensitiveExpression.Item1, caseSensitiveExpression.Item2);
                else
                    break;
            }

            return query;
        }
    }
}
