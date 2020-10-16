using Application.GuidGenerator.Queries.GuidList;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.GuidGenerator.Queries
{
    using static Testing;

    public class GuidListTests : TestBase
    {
        [Test]
        public async Task Should_ReturnNotEmptyOrNullList()
        {
            //arrange
            var query = new GuidListQuery(10);

            //act
            var result = await SendAsync(query);

            //assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_ReturnListOfTenGuids()
        {
            //arrange
            var query = new GuidListQuery(10);

            //act
            var result = await SendAsync(query);

            //assert
            result.Should().HaveCount(10);
            for (int i = 0; i < 10; i++)
            {
                result[i].Should<Guid>().BeOfType<Guid>();
            }
        }

        [Test]
        public async Task Should_ReturnListWithNoRepeats()
        {
            //arrange
            int guidNumber = 100000;
            var query = new GuidListQuery(guidNumber);


            //act
            var result = await SendAsync(query);

            //assert
            result.Should().HaveCount(guidNumber);
            result.Should().OnlyHaveUniqueItems();
        }
    }
}
