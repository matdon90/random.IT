using Application.Common.Services;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests.Common.Services
{
    [TestFixture]
    public class NetworkConfigGeneratorServiceTests
    {
        private NetworkConfigGeneratorService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new NetworkConfigGeneratorService();
        }

        [Test]
        public void Should_ReturnOneNetworkConfig()
        {
            //arrange


            //act
            var result = _service.GenerateNetworkConfigs(1);

            //assert
            result.ToList().First().Should().BeOfType<NetworkConfig>();
        }

        [Test]
        public void Should_ReturnSelectedNetworkConfig()
        {
            //arrange
            int numberOfConfigs = 40;

            //act
            var result = _service.GenerateNetworkConfigs(numberOfConfigs);

            //assert
            result.Should().HaveCount(numberOfConfigs);
        }
    }
}
