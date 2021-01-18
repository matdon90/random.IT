using Application.Common.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Services
{
    public class NetworkConfigGeneratorService : INetworkConfigGenerator
    {
        public IEnumerable<NetworkConfig> GenerateNetworkConfigs(int numberOfConfigs)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NetworkConfig> GenerateNetworkConfigsByIpTemplate(int numberOfConfigs, string ipTemplate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NetworkConfig> GenerateNetworkConfigsByMask(int numberOfConfigs, string subnetMask)
        {
            throw new System.NotImplementedException();
        }
    }
}
