using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using PackageDistributionService.Core.Dto;
using PackageDistributionService.Service.Services;

namespace PackageDistributionService.Client
{
    public class DistributionServiceClient
    {
        private readonly IDistributionService _client;

        /// <summary>
        /// 
        /// </summary>
        private DistributionServiceClient()
        {
            var factory = new ChannelFactory<IDistributionService>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost/DistributionService/DistributionService.svc"));
            _client = factory.CreateChannel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private LspPackage GetPackage(LspRequest request)
        {
            return _client.GetPackage(request);
        }

        public static void Main(string[] args)
        {
            try
            {
                var client = new DistributionServiceClient();
                var request = new LspRequest
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
                var assemblyInfos = new List<AssemblyInfo>
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
                Console.WriteLine(exception.InnerException.Message);
                Console.ReadLine();
            }
        }
    }
}
