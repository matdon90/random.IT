using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface INetworkConfigGenerator
    {
        IEnumerable<NetworkConfig> GenerateNetworkConfigs(int numberOfConfigs);
        IEnumerable<NetworkConfig> GenerateNetworkConfigsByIpTemplate(int numberOfConfigs, string ipTemplate);
        IEnumerable<NetworkConfig> GenerateNetworkConfigsByMask(int numberOfConfigs, string subnetMask);
        IEnumerable<NetworkConfig> GenerateNetworkConfigsByIpAndMaskTemplate(int numberOfConfigs, string ipTemplate, string subnetMask);
    }
}
