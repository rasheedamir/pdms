using System.ServiceModel;
using PackageDistributionService.Core.Dto;

namespace PackageDistributionService.Service.Services
{
    /// <summary>
    /// Package distribution service
    /// </summary>
    [ServiceContract]
    public interface IDistributionService
    {
        /// <summary>
        /// Get package function
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        PosPackage GetPackage(PosRequest request);
    }
}
