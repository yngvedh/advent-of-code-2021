using AoC.Day06;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day06.Test
{
    public class Day06Test
    {
        private string[] sampleInput = new string[] {"3,4,3,1,2"};
        private Fishes sampleData = new Fishes(new long[] {0, 1, 1, 2, 1, 0, 0, 0, 0});

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
            solution.Should().Be(5934);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(26984457539L);
        }
    }
}
