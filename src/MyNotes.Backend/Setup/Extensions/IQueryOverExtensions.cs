namespace MyNotes.Backend.Setup.Extensions
{
    using System;
    using NHibernate;
    using System.Linq.Expressions;
    using System.Collections;

    public static class IQueryOverExtensions
    {
        public static IQueryOver<TEntity, TEntity> Where<TEntity>(this IQueryOver<TEntity, TEntity> queryover, Expression<Func<TEntity, object>> expression, string value)
        {
            return queryover
                        .WhereRestrictionOn(expression)
                        .IsInsensitiveLike(value);
        }
    }
}