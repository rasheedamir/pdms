using System.ServiceModel;
using PackageDistributionService.Core.Dto;

namespace PackageDistributionService.Service.Services
{
    /// <summary>
    /// Distribution service
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
        LspPackage GetPackage(LspRequest request);

        /// <summary>
        /// Get file function.
        /// Returns a file matching the fileName in the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        FilePackage GetFile(FileRequest request);
    }
}
