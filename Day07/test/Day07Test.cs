using AoC.Day07;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day07.Test
{
    public class Day07Test
    {
        private string[] sampleInput = new string[] {
            "16,1,2,0,4,2,7,1,2,14"
        };

        private Crabs sampleData = new Crabs(new int[] {
            16,1,2,0,4,2,7,1,2,14
        });

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
            solution.Should().Be(37);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(168);
        }
    }
}
