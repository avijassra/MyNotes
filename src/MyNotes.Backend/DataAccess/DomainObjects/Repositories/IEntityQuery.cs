namespace MyNotes.Backend.DataAccess.DomainObjects.Repositories
{
    using NHibernate;

    public interface IEntityQuery<TEntity>
    {
        /// <summary>
        /// This method executes the query
        /// </summary>
        /// <returns>Object of type TEntity</returns>
        TEntity Execute();
    }
}