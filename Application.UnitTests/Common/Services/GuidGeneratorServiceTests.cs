using Application.Common.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests.Common.Services
{
    [TestFixture]
    public class GuidGeneratorServiceTests
    {
        private GuidGeneratorService service;
        readonly DateTime date = new DateTime(2020, 08, 18, 8, 53, 00);

        [SetUp]
        public void SetUp()
        {
            service = new GuidGeneratorService();
        }

        [Test]
        public void Should_ReturnOneGuid()
        {
            //arrange


            //act
            var result = service.GuidGenerateSingle(date);

            //assert
            result.Should<Guid>().BeOfType<Guid>();
        }

        [Test]
        public void Should_Version1Have1OnPosition14()
        {
            //arrange


            //act
            var result = service.GuidGenerateSingle(date).ToString().ToCharArray();

            //assert
            result[14].Should().Be('1');
        }

        [Test]
        public void Should_Version1Have89abOnPosition19()
        {
            //arrange


            //act
            var result = service.GuidGenerateSingle(date).ToString().ToCharArray();

            //assert
            result[19].ToString().Should().MatchRegex("[89ab]");
        }

        [Test]
        public void Should_ReturnListOfGuids()
        {
            //arrange
            int numberOfGuids = 10000;

            //act
            var result = service.GuidGenerateMultiple(numberOfGuids);

            //assert
            result.Should().HaveCount(numberOfGuids);
            result.Should<Guid>().BeOfType<List<Guid>>();
        }

        [Test]
        public void Should_Version4Have4OnPosition14()
        {
            //arrange
            int numberOfGuids = 10000;

            //act
            var result = service.GuidGenerateMultiple(numberOfGuids).ToList();

            //assert
            for (int i = 0; i < numberOfGuids; i++)
            {
                result[i].ToString().ToCharArray()[14].Should().Be('4');
            }
        }

        [Test]
        public void Should_Version4Have89abOnPosition19()
        {
            //arrange
            int numberOfGuids = 10000;

            //act
            var result = service.GuidGenerateMultiple(numberOfGuids).ToList();

            //assert
            for (int i = 0; i < result.Count; i++)
            {
                result[i].ToString().ToCharArray()[19].ToString().Should().MatchRegex("[89ab]");
            }
        }
    }
}
