using System.Collections.Generic;
using NHibernate.Criterion;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="GroupStore" /> from DB.
    /// </summary>
    [Repository]
    public class GroupStoreDao : AbstractNHibernateDao<IGroupStore, int>, IGroupStoreDao
    {
        /// <summary>
        /// Get all by coop store id
        /// </summary>
        /// <param name="coopStoreId"></param>
        /// <returns></returns>
        public IList<IGroupStore> GetAllByCoopStoreId(int coopStoreId)
        {
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("CoopStoreId", coopStoreId),
                };
            return GetByCriteria(criterion.ToArray());
        }
    }
}
