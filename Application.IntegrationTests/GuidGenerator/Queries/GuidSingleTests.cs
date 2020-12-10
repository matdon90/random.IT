using Application.GuidGenerator.Queries.GuidSingle;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.GuidGenerator.Queries
{
    using static Testing;

    public class GuidSingleTests : TestBase
    {
        [Test]
        public async Task Should_ReturnNotNull()
        {
            //arrange
            var query = new GuidSingleQuery(false);

            //act
            var result = await SendAsync(query);

            //assert
            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task Should_ReturnGuidType()
        {
            //arrange
            var query = new GuidSingleQuery(false);

            //act
            var result = await SendAsync(query);

            //assert
            result.Should<string>().BeOfType<string>();
        }

        [Test]
        public async Task Should_ReturnNoRepeats()
        {
            //arrange
            var guidList = new List<string>();
            var query = new GuidSingleQuery(false);
            int guidNumber = 100000;

            //act
            for (int i = 0; i < guidNumber; i++)
            {
                guidList.Add(await SendAsync(query));
            }

            //assert
            guidList.Should().HaveCount(guidNumber);
            guidList.Should().OnlyHaveUniqueItems();
        }
    }
}
