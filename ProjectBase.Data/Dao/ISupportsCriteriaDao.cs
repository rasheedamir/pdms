using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ProjectBase.Data.Dao
{
    public interface ISupportsCriteriaDao<TEntity>
    {
        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like GetAll()
        /// </summary>
        List<TEntity> GetByCriteria(params ICriterion[] criterion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        List<TEntity> GetByExample(TEntity exampleInstance, params string[] propertiesToExclude);

        /// <summary>
        /// Looks for a single instance using the example provided.
        /// </summary>
        /// <exception cref="NonUniqueResultException" />
        TEntity GetUniqueByExample(TEntity exampleInstance, params string[] propertiesToExclude);
    }
}
