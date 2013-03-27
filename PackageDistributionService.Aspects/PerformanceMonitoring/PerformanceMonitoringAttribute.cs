using System;

namespace PackageDistributionService.Aspects.PerformanceMonitoring
{
    /// <summary>
    /// This attribute can be used to mark properties and methods 
    /// on which each call should be performance monitored.
    /// Perforamance means the time it took to completely run the
    /// method.
    /// </summary>
    public class PerformanceMonitoringAttribute : Attribute
    {
    }
}
