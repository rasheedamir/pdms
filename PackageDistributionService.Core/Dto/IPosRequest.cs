using System.Collections.Generic;

namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// PosRequest - Request from Pos
    /// </summary>
    public interface IPosRequest
    {
        /// <summary>
        /// CoopStoreId - unique id of the the coop store
        /// </summary>
        int CoopStoreId { get; set; }

        /// <summary>
        /// PosNumber - point of sales number
        /// </summary>
        string PosNumber { get; set; }

        /// <summary>
        /// PackageVersionId
        /// </summary>
        int PackageVersionId { get; set; }

        /// <summary>
        /// TerminalSerialNumber - a store can have multiple pos
        /// </summary>
        string TerminalSerialNumber { get; set; }

        /// <summary>
        /// IpAddress of the pos
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// HostName of the pos
        /// </summary>
        string HostName { get; set; }

        /// <summary>
        /// PosManufacturerName
        /// </summary>
        string PosManufacturerName { get; set; }

        /// <summary>
        /// PosVersion
        /// </summary>
        string PosVersion { get; set; }

        /// <summary>
        /// list of assemblies
        /// </summary>
        List<IAssemblyInfo> AssemblyInfos { get; set; }
    }
}
