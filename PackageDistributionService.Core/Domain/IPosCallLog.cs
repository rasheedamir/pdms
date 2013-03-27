using System;
using System.Collections.Generic;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for PosCallLog entity
    /// Point of sales call log
    /// </summary>
    public interface IPosCallLog : IEntityWithTypedId<int>
    {
        /// <summary>
        /// pos_call_log_id : primary key
        /// </summary>
        //int PosCallLogId { get; set; }

        /// <summary>
        /// coop_store_id - unique id of the the coop store
        /// </summary>
        int CoopStoreId { get; set; }
        
        /// <summary>
        /// pos_number - point of sales number
        /// </summary>
        string PosNumber { get; set; }

        /// <summary>
        /// the time when system recieved the call from pos
        /// </summary>
        DateTime? CallTimestamp { get; set; }

        /// <summary>
        /// PackageVersion entity
        /// </summary>
        IPackageVersion PackageVersion { get; set; }

        /// <summary>
        /// package_version_id of PackageVersion entity
        /// </summary>
        int PackageVersionId { get; set; }
        
        /// <summary>
        /// terminal_serial_number - a store can have multiple pos
        /// </summary>
        string TerminalSerialNumber { get; set; }
        
        /// <summary>
        /// ip_address - of the pos
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// host_nanem - of the pos
        /// </summary>
        string HostName { get; set; }

        /// <summary>
        /// pos_manufacturer_name
        /// </summary>
        string PosManufacturerName { get; set; }

        /// <summary>
        /// pos_verion
        /// </summary>
        string PosVersion { get; set; }
        
        /// <summary>
        /// list of assemblies
        /// </summary>
        IList<IPosAssemblyLog> PosAssemblyLogs { get; set; }
    }
}
