using System.Collections.Generic;

namespace ProjectBase.Data.Dao
{
    /// <summary>
    /// Generic DAO Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity to operate with.</typeparam>
    /// <typeparam name="TId">Entity id type.</typeparam>
    public interface IDao<TEntity, in TId>
    {
        /// <summary>
        /// Finds entity with given id.
        /// </summary>
        /// <param name="id">The id to search with.</param>
        /// <returns>Found entity or null if not found.</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Returns all entities of given type.
        /// The result may be different than in database based on
        /// filters or other locked search criteria for search.
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();
    }
}
