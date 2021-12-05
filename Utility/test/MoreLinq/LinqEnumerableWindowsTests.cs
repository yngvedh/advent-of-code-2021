using AoC.MoreLinq;
using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace AoC.MoreLinq.Test
{
    public class LinqEnumerableWindowsTests
    {
        [Fact]
        public void ShouldProduceEmptyEnumerableIfSourceIsEmpty()
        {
            var windows = Enumerable.Empty<int>().Windows(3);
            windows.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void ShouldProduceSingleWindowIfWindowSizeEqualsElementCount(int size)
        {
            var elements = Enumerable.Range(0, size);
            var windows = elements.Windows(size);

            windows.Should().Equal(elements);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,1)]
        [InlineData(2,2)]
        [InlineData(10,3)]
        public void ShouldProduceCorrectNumberOfWindows(int count, int windowSize)
        {
            var elements = Enumerable.Range(0, count);
            var windows = elements.Windows(windowSize);

            var expectedWindowCount = count - windowSize + 1;
            windows.Should().HaveCount(expectedWindowCount);

            var expectedWindows = Enumerable
                .Range(0, expectedWindowCount)
                .Select(_ => Enumerable.Range(_, windowSize));

            windows.Should().BeEquivalentTo(expectedWindows);
        }
    }
}
