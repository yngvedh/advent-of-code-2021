using AoC.Day05;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day05.Test
{
    public class Day05Test
    {
        private string[] sampleInput = new string[]
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2"
        };

        private VentMap sampleData = new VentMap(
            new Line[] {
                new Line(new Point(0,9), new Point(5,9)),
                new Line(new Point(8,0), new Point(0,8)),
                new Line(new Point(9,4), new Point(3,4)),
                new Line(new Point(2,2), new Point(2,1)),
                new Line(new Point(7,0), new Point(7,4)),
                new Line(new Point(6,4), new Point(2,0)),
                new Line(new Point(0,9), new Point(2,9)),
                new Line(new Point(3,4), new Point(1,4)),
                new Line(new Point(0,0), new Point(8,8)),
                new Line(new Point(5,5), new Point(8,2))
            }
        );

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
            solution.Should().Be(5);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(12);
        }

        [Fact]
        public void ShouldConvertSampleLine1ToCorrectPoints()
        {
            var expected = new [] { new Point(1,1), new Point(1,2), new Point(1,3) };
            var points = Program.ToPoints(new Line(new Point(1,1), new Point(1,3)));
            points.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldConvertSampleLine2ToCorrectPoints()
        {
            var expected = new [] { new Point(9,7), new Point(9,8), new Point(9,9) };
            var points = Program.ToPoints(new Line(new Point(9,7), new Point(9,9)));
            points.Should().BeEquivalentTo(expected);
        }
    }
}
