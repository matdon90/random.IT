using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Services
{
    public class NetworkConfigGeneratorService : INetworkConfigGenerator
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Creates random IP octet.
        /// </summary>
        /// <returns></returns>
        private int CreateIpOctet()
        {
            int minOctetValue = 0;
            int maxOctetValue = 255;
            return _random.Next(minOctetValue, maxOctetValue);
        }

        /// <summary>
        /// Checks if string is a number.
        /// </summary>
        /// <param name="inputToCheck">Input to check</param>
        /// <returns></returns>
        private bool IsNumeric(string stringToCheck)
        {
            return int.TryParse(stringToCheck, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out int retNum);
        }

        /// <summary>
        /// Checks if number is within a provided range.
        /// </summary>
        /// <param name="numberToCheck">Number to check</param>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximal value</param>
        /// <returns></returns>
        bool IsInRange(int numberToCheck, int minValue, int maxValue)
        {
            return (numberToCheck >= minValue && numberToCheck <= maxValue);
        }

        /// <summary>
        /// Gets most optimal subnet mask based on number of hosts needed.
        /// </summary>
        /// <param name="numberOfHosts">Number of hosts</param>
        /// <returns></returns>
        private string GetSubnetMask(int numberOfHosts)
        {
            return numberOfHosts switch
            {
                var x when x > 8388606 => "255.0.0.0",
                var x when x > 4194302 & x <= 8388606 => "255.128.0.0",
                var x when x > 2097150 & x <= 4194302 => "255.192.0.0",
                var x when x > 1048574 & x <= 2097150 => "255.224.0.0",
                var x when x > 524286 & x <= 1048574 => "255.240.0.0",
                var x when x > 262142 & x <= 524286 => "255.248.0.0",
                var x when x > 131070 & x <= 262142 => "255.252.0.0",
                var x when x > 65534 & x <= 131070 => "255.254.0.0",
                var x when x > 32766 & x <= 65534 => "255.255.0.0",
                var x when x > 16382 & x <= 32766 => "255.255.128.0",
                var x when x > 8190 & x <= 16382 => "255.255.192.0",
                var x when x > 4094 & x <= 8190 => "255.255.224.0",
                var x when x > 2046 & x <= 4094 => "255.255.240.0",
                var x when x > 1022 & x <= 2046 => "255.255.248.0",
                var x when x > 510 & x <= 1022 => "255.255.252.0",
                var x when x > 256 & x <= 510 => "255.255.254.0",
                var x when x > 126 & x <= 254 => "255.255.255.0",
                var x when x > 62 & x <= 126 => "255.255.255.128",
                var x when x > 30 & x <= 62 => "255.255.255.192",
                var x when x > 14 & x <= 30 => "255.255.255.224",
                var x when x > 6 & x <= 14 => "255.255.255.240",
                var x when x > 2 & x <= 6 => "255.255.255.248",
                var x when x >= 1 & x <= 2 => "255.255.255.252",
                _ => "0.0.0.0"
            };
        }

        /// <summary>
        /// Get number of available hosts in subnet based on number of hosts needed.
        /// </summary>
        /// <param name="numberOfHosts">Number of hosts</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <returns></returns>
        private int GetFreeHostsNumber(int numberOfHosts, string subnetMask)
        {
            return subnetMask switch
            {
                var x when x == "255.0.0.0" => 16777214 - numberOfHosts,
                var x when x == "255.128.0.0" => 16777214 - numberOfHosts,
                var x when x == "255.192.0.0" => 4194302 - numberOfHosts,
                var x when x == "255.224.0.0" => 2097150 - numberOfHosts,
                var x when x == "255.240.0.0" => 1048574 - numberOfHosts,
                var x when x == "255.248.0.0" => 524286 - numberOfHosts,
                var x when x == "255.252.0.0" => 262142 - numberOfHosts,
                var x when x == "255.254.0.0" => 131070 - numberOfHosts,
                var x when x == "255.255.0.0" => 65534 - numberOfHosts,
                var x when x == "255.255.128.0" => 32766 - numberOfHosts,
                var x when x == "255.255.192.0" => 16382 - numberOfHosts,
                var x when x == "255.255.224.0" => 8190 - numberOfHosts,
                var x when x == "255.255.240.0" => 4094 - numberOfHosts,
                var x when x == "255.255.248.0" => 2046 - numberOfHosts,
                var x when x == "255.255.252.0" => 1022 - numberOfHosts,
                var x when x == "255.255.254.0" => 510 - numberOfHosts,
                var x when x == "255.255.255.0" => 254 - numberOfHosts,
                var x when x == "255.255.255.128" => 126 - numberOfHosts,
                var x when x == "255.255.255.192" => 62 - numberOfHosts,
                var x when x == "255.255.255.224" => 30 - numberOfHosts,
                var x when x == "255.255.255.240" => 14 - numberOfHosts,
                var x when x == "255.255.255.248" => 6 - numberOfHosts,
                var x when x == "255.255.255.252" => 2 - numberOfHosts,
                _ => -1
            };
        }


        /// <summary>
        /// Gets base IP address based on provided IP address template.
        /// </summary>
        /// <param name="ipTemplate">IP address template</param>
        /// <returns></returns>
        private string GetBaseIpAddress(string ipTemplate)
        {
            var ipOctetTable = new string[4];
            string ipAddress;
            if (ipTemplate == null)
            {
                for (int i = 0; i < ipOctetTable.Length; i++)
                {
                    ipOctetTable[i] = CreateIpOctet().ToString();
                }
                ipAddress = $"{ipOctetTable[0]}.{ipOctetTable[1]}.{ipOctetTable[2]}.{ipOctetTable[3]}";
            }
            else
            {
                ipOctetTable = ipTemplate.Split('.');
                for (int i = 0; i < ipOctetTable.Length; i++)
                {
                    if (!IsNumeric(ipOctetTable[i]))
                    {
                        ipOctetTable[i] = CreateIpOctet().ToString();
                    }
                    if (!IsInRange(int.Parse(ipOctetTable[i]), 0, 255))
                    {
                        ipOctetTable[i] = CreateIpOctet().ToString();
                    }
                }
                ipAddress = $"{ipOctetTable[0]}.{ipOctetTable[1]}.{ipOctetTable[2]}.{ipOctetTable[3]}";
            }
            return ipAddress;
        }

        /// <summary>
        /// Calculating next IP address based on provided IP.
        /// </summary>
        /// <param name="ipBaseTemplate">Base IP address</param>
        /// <returns></returns>
        private string GetNextIpAddress(string ipBaseTemplate)
        {
            var ipOctetTable = new string[4];
            string ipAddress = string.Empty;

            var ipOctetIntTable = ipBaseTemplate.Split('.').Select(o => int.Parse(o)).ToArray();
            ipOctetIntTable[3]++;
            ipOctetTable = ipOctetIntTable.Select(o => o.ToString()).ToArray();
            return $"{ipOctetTable[0]}.{ipOctetTable[1]}.{ipOctetTable[2]}.{ipOctetTable[3]}";
        }

        /// <summary>
        /// Get subnet address based on IP address and subnet mask.
        /// </summary>
        /// <param name="ipAddress">IP addres</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <returns></returns>
        private string GetSubnetAddress(string ipAddress, string subnetMask)
        {
            var subnetAddressOctetTable = new int[4];
            var ipAddressOctetTable = ipAddress.Split(".").Select(o => int.Parse(o)).ToArray();
            var subnetMaskOctetTable = subnetMask.Split(".").Select(o => int.Parse(o)).ToArray();
            for (int i = 0; i < subnetAddressOctetTable.Length; i++)
            {
                subnetAddressOctetTable[i] = ipAddressOctetTable[i] & subnetMaskOctetTable[i];
            }
            return $"{subnetAddressOctetTable[0]}.{subnetAddressOctetTable[1]}.{subnetAddressOctetTable[2]}.{subnetAddressOctetTable[3]}";
        }

        /// <summary>
        /// Get subnet broadcast address based on subnet mask and subnet address.
        /// </summary>
        /// <param name="subnetMask">Subnet mask</param>
        /// <param name="subnetAddress">Subnet address</param>
        /// <returns></returns>
        private string GetSubnetBroadcastAddress(string subnetMask, string subnetAddress)
        {
            var subnetBroadcastAddressOctetTable = new int[4];
            var subnetAddressOctetTable = subnetAddress.Split(".").Select(o => int.Parse(o)).ToArray();
            var negateSubnetMaskOctetTable = subnetMask.Split(".").Select(o => (~int.Parse(o) & 0xf)).ToArray();
            for (int i = 0; i < subnetBroadcastAddressOctetTable.Length; i++)
            {
                subnetBroadcastAddressOctetTable[i] = subnetAddressOctetTable[i] + negateSubnetMaskOctetTable[i];
            }
            return $"{subnetBroadcastAddressOctetTable[0]}.{subnetBroadcastAddressOctetTable[1]}.{subnetBroadcastAddressOctetTable[2]}.{subnetBroadcastAddressOctetTable[3]}";

        }

        /// <summary>
        /// Generates random network configs in the number defined in parameters.
        /// </summary>
        /// <param name="numberOfConfigs">Number of network configs</param>
        /// <returns></returns>
        public IEnumerable<NetworkConfig> GenerateNetworkConfigs(int numberOfConfigs)
        {
            var networkConfigList = new List<NetworkConfig>();
            var ipAddressBase = GetBaseIpAddress(null);
            var subnetMask = GetSubnetMask(numberOfConfigs);
            var subnetAddress = GetSubnetAddress(ipAddressBase, subnetMask);
            var subnetBroadcastAddress = GetSubnetBroadcastAddress(subnetMask, subnetAddress);
            var freeHostsNumber = GetFreeHostsNumber(numberOfConfigs, subnetMask);

            for (int i = 0; i < numberOfConfigs; i++)
            {
                var ipBaseTemplate = i == 0 ? subnetAddress : networkConfigList.Last().ipHostAddress;
                var ipAddress = GetNextIpAddress(ipBaseTemplate);
                var config = new NetworkConfig()
                {
                    ipHostAddress = ipAddress,
                    subnetMask = subnetMask,
                    subnetAddress = subnetAddress,
                    subnetBroadcastAddress = subnetBroadcastAddress,
                    freeHostsNumberInSubnet = freeHostsNumber
                };

                networkConfigList.Add(config);
            }
            return networkConfigList;
        }

        /// <summary>
        /// Generates random network configs with IP of based template and in the number defined in parameters.
        /// </summary>
        /// <param name="numberOfConfigs">Number of network configs</param>
        /// <param name="ipTemplate">IP address template</param>
        /// <returns></returns>
        public IEnumerable<NetworkConfig> GenerateNetworkConfigsByIpTemplate(int numberOfConfigs, string ipTemplate)
        {
            var networkConfigList = new List<NetworkConfig>();
            var ipAddressBase = GetBaseIpAddress(ipTemplate);
            var subnetMask = GetSubnetMask(numberOfConfigs);
            var subnetAddress = GetSubnetAddress(ipAddressBase, subnetMask);
            var subnetBroadcastAddress = GetSubnetBroadcastAddress(subnetMask, subnetAddress);
            var freeHostsNumber = GetFreeHostsNumber(numberOfConfigs, subnetMask);

            for (int i = 0; i < numberOfConfigs; i++)
            {
                var ipBaseTemplate = i == 0 ? subnetAddress : networkConfigList.Last().ipHostAddress;
                var ipAddress = GetNextIpAddress(ipBaseTemplate);
                var config = new NetworkConfig()
                {
                    ipHostAddress = ipAddress,
                    subnetMask = subnetMask,
                    subnetAddress = subnetAddress,
                    subnetBroadcastAddress = subnetBroadcastAddress,
                    freeHostsNumberInSubnet = freeHostsNumber
                };

                networkConfigList.Add(config);
            }
            return networkConfigList;
        }

        /// <summary>
        /// /// Generates random network configs with subnet mask and in the number defined in parameters.
        /// </summary>
        /// <param name="numberOfConfigs">Number of network configs</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <returns></returns>
        public IEnumerable<NetworkConfig> GenerateNetworkConfigsByMask(int numberOfConfigs, string subnetMask)
        {
            if (GetFreeHostsNumber(0, subnetMask) < numberOfConfigs)
            {
                subnetMask = GetSubnetMask(numberOfConfigs);
            }

            var networkConfigList = new List<NetworkConfig>();
            var ipAddressBase = GetBaseIpAddress(null);
            var subnetAddress = GetSubnetAddress(ipAddressBase, subnetMask);
            var subnetBroadcastAddress = GetSubnetBroadcastAddress(subnetMask, subnetAddress);
            var freeHostsNumber = GetFreeHostsNumber(numberOfConfigs, subnetMask);

            for (int i = 0; i < numberOfConfigs; i++)
            {
                var ipBaseTemplate = i == 0 ? subnetAddress : networkConfigList.Last().ipHostAddress;
                var ipAddress = GetNextIpAddress(ipBaseTemplate);
                var config = new NetworkConfig()
                {
                    ipHostAddress = ipAddress,
                    subnetMask = subnetMask,
                    subnetAddress = subnetAddress,
                    subnetBroadcastAddress = subnetBroadcastAddress,
                    freeHostsNumberInSubnet = freeHostsNumber
                };

                networkConfigList.Add(config);
            }
            return networkConfigList;
        }

        /// <summary>
        /// /// Generates random network configs with IP of based template, subnet mask and in the number defined in parameters.
        /// </summary>
        /// <param name="numberOfConfigs">Number of network configs</param>
        /// <param name="ipTemplate">IP address template</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <returns></returns>
        public IEnumerable<NetworkConfig> GenerateNetworkConfigsByIpAndMaskTemplate(int numberOfConfigs, string ipTemplate, string subnetMask)
        {
            if (GetFreeHostsNumber(0, subnetMask) < numberOfConfigs)
            {
                subnetMask = GetSubnetMask(numberOfConfigs);
            }

            var networkConfigList = new List<NetworkConfig>();
            var ipAddressBase = GetBaseIpAddress(ipTemplate);
            var subnetAddress = GetSubnetAddress(ipAddressBase, subnetMask);
            var subnetBroadcastAddress = GetSubnetBroadcastAddress(subnetMask, subnetAddress);
            var freeHostsNumber = GetFreeHostsNumber(numberOfConfigs, subnetMask);

            for (int i = 0; i < numberOfConfigs; i++)
            {
                var ipBaseTemplate = i == 0 ? subnetAddress : networkConfigList.Last().ipHostAddress;
                var ipAddress = GetNextIpAddress(ipBaseTemplate);
                var config = new NetworkConfig()
                {
                    ipHostAddress = ipAddress,
                    subnetMask = subnetMask,
                    subnetAddress = subnetAddress,
                    subnetBroadcastAddress = subnetBroadcastAddress,
                    freeHostsNumberInSubnet = freeHostsNumber
                };

                networkConfigList.Add(config);
            }
            return networkConfigList;
        }
    }
}
