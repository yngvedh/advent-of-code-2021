using AoC.Day03;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day03.Test
{
    public class Day03Test
    {
        private string[] sampleInput = new string[]
        {
        //   F8421   
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        private Reading sampleData = new (new int[] {
            0b00100,
            0b11110,
            0b10110,
            0b10111,
            0b10101,
            0b01111,
            0b00111,
            0b11100,
            0b10000,
            0b11001,
            0b00010,
            0b01010,
        }, 5);

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
            solution.Should().Be(198);
        }

        [Fact]
        public void ShouldFindCorrectOxygenRating()
        {
            var rating = Program.OxygenRating(sampleData);
            rating.Should().Be(23);
        }


        [Fact]
        public void ShouldFindCorrectScrubberRating()
        {
            var rating = Program.ScrubberRating(sampleData);
            rating.Should().Be(10);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(230);
        }
    }
}
