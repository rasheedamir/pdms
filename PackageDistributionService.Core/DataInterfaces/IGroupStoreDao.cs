using System.Collections.Generic;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// GroupStore related DAO operations interface
    /// </summary>
    public interface IGroupStoreDao : IDao<GroupStore, int>, ISupportsSaveDao<GroupStore, int>, ISupportsCriteriaDao<GroupStore>, ISupportsDeleteDao<GroupStore>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coopStoreId"></param>
        /// <returns></returns>
        IList<GroupStore> GetAllByCoopStoreId(int coopStoreId);
    }
}
