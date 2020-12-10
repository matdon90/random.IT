using Application.Common.Filter;
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
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new GuidListQuery(10, false, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<string>>().Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_ReturnListOfTenGuids()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new GuidListQuery(10, false, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<string>>().Should().HaveCount(10 > filter.PageSize ? filter.PageSize : 10);
            for (int i = 0; i < 10; i++)
            {
                result.Result.As<List<string>>()[i].Should<string>().BeOfType<string>();
            }
        }

        [Test]
        public async Task Should_ReturnListOfTenGuidsLowercase()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new GuidListQuery(10, false, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<string>>().Should().HaveCount(10 > filter.PageSize ? filter.PageSize : 10);
            for (int i = 0; i < 10; i++)
            {
                result.Result.As<List<string>>()[i].Should<string>().BeOfType<string>();
                result.Result.As<List<string>>()[i].Should().Be(result.Result.As<List<string>>()[i].ToLower());
            }
        }

        [Test]
        public async Task Should_ReturnListOfTenGuidsUppercase()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            var query = new GuidListQuery(10, true, filter, path);

            //act
            var result = await SendAsync(query);

            //assert
            result.Result.As<List<string>>().Should().HaveCount(10 > filter.PageSize ? filter.PageSize : 10);
            for (int i = 0; i < 10; i++)
            {
                result.Result.As<List<string>>()[i].Should<string>().BeOfType<string>();
                result.Result.As<List<string>>()[i].Should().Be(result.Result.As<List<string>>()[i].ToUpper());
            }
        }

        [Test]
        public async Task Should_ReturnListWithNoRepeats()
        {
            //arrange
            var filter = new PaginationFilter();
            var path = String.Empty;
            int guidNumber = 10000;

            var allResults = new List<string>();
            //act
            for (int i = 0; i < (guidNumber / filter.PageSize); i++)
            {
                var result = await SendAsync(new GuidListQuery(guidNumber, false, new PaginationFilter(i + 1, 10), path));
                allResults.AddRange(result.Result.As<List<string>>());

                //assert
                result.Result.As<List<string>>().Should().HaveCount(guidNumber > filter.PageSize ? filter.PageSize : guidNumber);
                result.Result.As<List<string>>().Should().OnlyHaveUniqueItems();
            }

            allResults.Should().OnlyHaveUniqueItems();
        }
    }
}
