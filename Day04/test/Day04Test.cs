using AoC.Day04;
using FluentAssertions;
using System;
using Xunit;

namespace AoC.Day04.Test
{
    public class Day04Test
    {
        private string[] sampleInput = new []
        {
            "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19",
            " 3 15  0  2 22",
            " 9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };
        private BingoGame sampleData = new BingoGame(
            new [] {7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1},
            new [] {
                new Board(new int[][] {
                    new []{22, 13, 17, 11,  0},
                    new []{8,  2,  23,  4, 24},
                    new []{21, 9,  14, 16,  7},
                    new []{6,  10,  3, 18,  5},
                    new []{1,  12, 20, 15, 19}
                }),
                new Board(new int[][] {
                    new []{3,  15,  0,  2, 22},
                    new []{9,  18, 13, 17,  5},
                    new []{19,  8,  7, 25, 23},
                    new []{20, 11, 10, 24,  4},
                    new []{14, 21, 16, 12,  6}
                }),
                new Board(new int[][] {
                    new []{14, 21, 17, 24,  4},
                    new []{10, 16, 15,  9, 19},
                    new []{18,  8, 23, 26, 20},
                    new []{22, 11, 13,  6,  5},
                    new []{2,  0, 12,  3,  7}
                })
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
            solution.Should().Be(4512);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(1924);
        }
    }
}
