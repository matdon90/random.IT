using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;

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
        /// <param name="inputToCheck"></param>
        /// <returns></returns>
        private bool IsNumeric(string stringToCheck)
        {
            int retNum;

            bool isNum = int.TryParse(stringToCheck, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Checks if number is within a provided range.
        /// </summary>
        /// <param name="numberToCheck"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        bool IsInRange(int numberToCheck, int minValue, int maxValue)
        {
            return (numberToCheck >= minValue && numberToCheck <= maxValue);
        }

        /// <summary>
        /// Gets most optimal subnet mask based on number of hosts needed.
        /// </summary>
        /// <param name="numberOfHosts"></param>
        /// <returns></returns>
        private string GetSubnetMask(int numberOfHosts)
        {
            return numberOfHosts switch
            {
                var x when x > 8388608 => "255.0.0.0",
                var x when x > 4194304 & x <= 8388608 => "255.128.0.0",
                var x when x > 2097152 & x <= 4194304 => "255.192.0.0",
                var x when x > 1048576 & x <= 2097152 => "255.224.0.0",
                var x when x > 524288 & x <= 1048576 => "255.240.0.0",
                var x when x > 262144 & x <= 524288 => "255.248.0.0",
                var x when x > 131072 & x <= 262144 => "255.252.0.0",
                var x when x > 65536 & x <= 131072 => "255.254.0.0",
                var x when x > 32768 & x <= 65536 => "255.255.0.0",
                var x when x > 16384 & x <= 32768 => "255.255.128.0",
                var x when x > 8192 & x <= 16384 => "255.255.192.0",
                var x when x > 4096 & x <= 8192 => "255.255.224.0",
                var x when x > 2048 & x <= 4096 => "255.255.240.0",
                var x when x > 1024 & x <= 2048 => "255.255.248.0",
                var x when x > 512 & x <= 1024 => "255.255.252.0",
                var x when x > 256 & x <= 512 => "255.255.254.0",
                var x when x > 128 & x <= 256 => "255.255.255.0",
                var x when x > 64 & x <= 128 => "255.255.255.128",
                var x when x > 32 & x <= 64 => "255.255.255.192",
                var x when x > 16 & x <= 32 => "255.255.255.224",
                var x when x > 8 & x <= 16 => "255.255.255.240",
                var x when x > 4 & x <= 8 => "255.255.255.248",
                var x when x >= 1 & x <= 4 => "255.255.255.252",
                _ => "0.0.0.0"
            };
        }

        /// <summary>
        /// Gets ip based on provided IP address template.
        /// </summary>
        /// <param name="ipTemplate"></param>
        /// <returns></returns>
        private string GetIpAddress(string ipTemplate)
        {
            var ipOctetTable = new string[4];
            string ipAddress = string.Empty;

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
        /// Get subnet address based on IP address and subnet mask.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="subnetMask"></param>
        /// <returns></returns>
        private string GetSubnetAddress(string ipAddress, string subnetMask)
        {
            return string.Empty;
        }

        /// <summary>
        /// Get subnet broadcast address based on subnet mask and subnet address.
        /// </summary>
        /// <param name="subnetMask"></param>
        /// <returns></returns>
        private string GetSubnetBroadcastAddress(string subnetMask, string subnetAddress)
        {
            return string.Empty;
        }

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
