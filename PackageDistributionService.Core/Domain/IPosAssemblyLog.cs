using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for PosAssemblyLog entity
    /// Point of Sales Assembly log.
    /// </summary>
    public interface IPosAssemblyLog : IEntityWithTypedId<int>
    {
        /// <summary>
        /// pos_assembly_log_id : primary key
        /// </summary>
        //int PosAssemblyLogId { get; set; }

        /// <summary>
        /// PosCallLog entity to which this PosAssemblyLog belongs
        /// 1(PosCallLog)-to-M(PosAssemblyLog)
        /// </summary>
        IPosCallLog PosCallLog { get; set; }

        /// <summary>
        /// pos_call_log_id of PosCallLog entity
        /// </summary>
        int PosCallLogId { get; set; }

        /// <summary>
        /// Assembly Name
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// Assembly Version
        /// </summary>
        string AssemblyVersion { get; set; }
    }
}
