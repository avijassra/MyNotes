namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using NHibernate;
    using NHibernate.Linq;
    using System.Collections.Generic;

    public interface IEntityListQuery<TEntity>
    {
        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>NhQueryable of TEntity type</returns>
        NhQueryable<TEntity> Execute();
    }
}