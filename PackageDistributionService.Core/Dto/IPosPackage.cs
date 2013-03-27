using System;

namespace PackageDistributionService.Core.Dto
{
    public interface IPosPackage
    {
        /// <summary>
        /// PackageVersionId
        /// </summary>
        int PackageVersionId { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        string Filename { get; set; }

        /// <summary>
        /// Activation Time
        /// </summary>
        DateTime? ActivationTime { get; set; }

        /// <summary>
        /// Md5 checksum
        /// </summary>
        string Md5CheckSum { get; set; }

        /// <summary>
        /// Contents of the package as byte array!
        /// </summary>
        Byte[] PackageContents { get; set; }

        /// <summary>
        /// Status = 0 : Nothing New
        /// Status = 1 : New Package Included
        /// Status = 2 : Exception! Anything went wrong. Contact system admin.
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// Message to describe the status
        /// </summary>
        string Message { get; set; }

        string ToStringMini();
    }
}
