using AoC.Day02;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day02.Test
{
    public class Day02Test
    {
        private string[] sampleInput = new string[] {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };

        private Command[] sampleData = new Command[] {
            new Command(Direction.Forward, 5),
            new Command(Direction.Down, 5),
            new Command(Direction.Forward, 8),
            new Command(Direction.Up, 3),
            new Command(Direction.Down, 8),
            new Command(Direction.Forward, 2)
        };

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
            solution.Should().Be(150);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(900);
        }
    }
}
