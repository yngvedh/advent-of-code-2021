using AoC.MoreLinq;
using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace AoC.MoreLinq.Test
{
    public class LinqEnumerableHeadsTests
    {
        [Fact]
        public void ShouldProduceEmptyEnumerableIfSourceIsEmpty()
        {
            var windows = Enumerable.Empty<int>().Heads();
            windows.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void ShouldProduceHeadsCorrectly(int count)
        {
            var elements = Enumerable.Range(0, count);
            var windows = elements.Heads();

            windows.Should().HaveCount(count);

            var expectedWindows = Enumerable
                .Range(1, count)
                .Select(_ => Enumerable.Range(0, _));

            windows.Should().BeEquivalentTo(expectedWindows);
        }
    }
}
