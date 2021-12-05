using AoC.AocTemplate;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.AoCTemplate.Test
{
    public class AoCTemplateTest
    {
        private string[] sampleInput = new string[0];
        private Placeholder sampleData = new Placeholder();

        [Fact]
        public void ShouldParseCorrectly()
        {
            var parsedData = Program.Parse(sampleInput);
            parsedData.Should().BeEquivalentTo(sampleData);
        }

        [Fact]
        public void ShouldFindSolutionForPart1Sample()
        {
            var solution = Program.SolvePart1(sampleData);
            solution.Should().Be(127);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(62);
        }
    }
}
