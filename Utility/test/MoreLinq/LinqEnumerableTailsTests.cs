using AoC.MoreLinq;
using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace AoC.MoreLinq.Test
{
    public class LinqEnumerableTailsTests
    {
        [Fact]
        public void ShouldProduceEmptyEnumerableIfSourceIsEmpty()
        {
            var windows = Enumerable.Empty<int>().Tails();
            windows.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void ShouldProduceTailsCorrectly(int count)
        {
            var elements = Enumerable.Range(0, count);
            var windows = elements.Tails();

            windows.Should().HaveCount(count);

            var expectedWindows = Enumerable
                .Range(0, count)
                .Select(_ => Enumerable.Range(_, count - _));

            windows.Should().BeEquivalentTo(expectedWindows);
        }
    }
}
