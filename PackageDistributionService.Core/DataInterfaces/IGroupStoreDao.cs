using System.Collections.Generic;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// GroupStore related DAO operations interface
    /// </summary>
    public interface IGroupStoreDao : IDao<IGroupStore, int>, ISupportsSaveDao<IGroupStore, int>, ISupportsCriteriaDao<IGroupStore>, ISupportsDeleteDao<IGroupStore>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coopStoreId"></param>
        /// <returns></returns>
        IList<IGroupStore> GetAllByCoopStoreId(int coopStoreId);
    }
}
