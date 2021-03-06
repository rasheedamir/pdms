﻿using System;
using System.IO;
using PackageDistributionService.Client2._0.WcfDistributionService;

namespace PackageDistributionService.Client2._0
{
    public class DistributionServiceClient
    {
        public static void Main(string[] args)
        {
            try
            {
                var client = new DistributionService();
                var request = new PosRequest
                {
                    CoopStoreId = 111,
                    HostName = "Host A",
                    IpAddress = "122.111.111.333",
                    PackageVersionId = 23,
                    PosManufacturerName = "Ericsson",
                    PosNumber = "123412341234",
                    PosVersion = "1.2.0",
                    TerminalSerialNumber = "111222333444555"
                };
                var assemblyInfos = new[]
                {
                    new AssemblyInfo {AssemblyName = "Hibernate Assembly 1", AssemblyVersion = "1.0.0.1"},
                    new AssemblyInfo {AssemblyName = "Spring Assembly 2", AssemblyVersion = "2.0.0.1"},
                    new AssemblyInfo{ AssemblyName = "NUnit Assembly", AssemblyVersion = "6.7.8" }
                };
                request.AssemblyInfos = assemblyInfos;
                var package = client.GetPackage(request);

                Console.WriteLine("Status: " + package.Status);
                Console.WriteLine("Message: " + package.Message);
                Console.WriteLine("PackageVersionId: " + package.PackageVersionId);
                Console.WriteLine("Md5CheckSum: " + package.Md5CheckSum);
                File.WriteAllBytes("C:\\RecievedPackage.7z", package.PackageContents);
                Console.WriteLine("Congratulations! All went well...");

                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}
