using Application.Common.Filter;
using Application.NetworkConfigsGenerator.Queries.NetworkConfigBasedOnTemplatesList;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.NetworkConfigsGenerator.Queries
{
    using static Testing;
    public class NetworkConfigsListTests : TestBase
    {
        [Test]
        public async Task Should_ReturnNotEmptyOrNullList()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new NetworkConfigBasedOnTemplatesListQuery(10, null, null, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<NetworkConfig>>().Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_ReturnListOfTenNetworkConfigs()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new NetworkConfigBasedOnTemplatesListQuery(10, null, null, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<NetworkConfig>>().Should().HaveCount(10 > filter.PageSize ? filter.PageSize : 10);
            for (int i = 0; i < 10; i++)
            {
                result.Result.As<List<NetworkConfig>>()[i].Should().BeOfType<NetworkConfig>();
            }
        }

        [Test]
        public async Task Should_ReturnListOfTenNetworkConfigsWithCorrectMask()
        {
            //arrange
            int requestedConfigs = 8;
            string arrangedMask = "255.255.0.0";
            int freeAddressesInSubnet = 65534 - requestedConfigs;
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new NetworkConfigBasedOnTemplatesListQuery(requestedConfigs, null, arrangedMask, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<NetworkConfig>>().Should().HaveCount(requestedConfigs > filter.PageSize ? filter.PageSize : requestedConfigs);
            for (int i = 0; i < requestedConfigs; i++)
            {
                result.Result.As<List<NetworkConfig>>()[i].subnetMask.Should().Be(arrangedMask);
                result.Result.As<List<NetworkConfig>>()[i].freeHostsNumberInSubnet.Should().Be(freeAddressesInSubnet);
            }
        }

        [Test]
        public async Task Should_ReturnListOfTenNetworkConfigsWithIpFromTemplate()
        {
            //arrange
            int requestedConfigs = 8;
            string templateIp = "192.168.3.x";
            var templateOctets = templateIp.Split('.');
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new NetworkConfigBasedOnTemplatesListQuery(requestedConfigs, templateIp, null, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<NetworkConfig>>().Should().HaveCount(requestedConfigs > filter.PageSize ? filter.PageSize : requestedConfigs);
            for (int i = 0; i < requestedConfigs; i++)
            {
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[0].Should().Be(templateOctets[0]);
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[1].Should().Be(templateOctets[1]);
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[2].Should().Be(templateOctets[2]);
            }
        }

        [Test]
        public async Task Should_ReturnListOfTenNetworkConfigsWithIpAndMaskFromTemplate()
        {
            //arrange
            int requestedConfigs = 8;
            string templateIp = "192.168.3.x";
            string arrangedMask = "255.255.255.0";
            int freeAddressesInSubnet = 254 - requestedConfigs;
            var templateOctets = templateIp.Split('.');
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new NetworkConfigBasedOnTemplatesListQuery(requestedConfigs, templateIp, arrangedMask, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<NetworkConfig>>().Should().HaveCount(requestedConfigs > filter.PageSize ? filter.PageSize : requestedConfigs);
            for (int i = 0; i < requestedConfigs; i++)
            {
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[0].Should().Be(templateOctets[0]);
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[1].Should().Be(templateOctets[1]);
                result.Result.As<List<NetworkConfig>>()[i].ipHostAddress.Split('.')[2].Should().Be(templateOctets[2]);
                result.Result.As<List<NetworkConfig>>()[i].subnetMask.Should().Be(arrangedMask);
                result.Result.As<List<NetworkConfig>>()[i].freeHostsNumberInSubnet.Should().Be(freeAddressesInSubnet);
            }
        }
    }
}
