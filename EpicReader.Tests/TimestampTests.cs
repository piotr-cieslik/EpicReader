using Xunit;

namespace EpicReader.Tests
{
    public class TimestampTests
    {
        [Theory]
        [InlineData(0, "0000000000")]
        [InlineData(1, "0000000001")]
        [InlineData(1572888075, "1572888075")]
        public void ShouldBeConvertedToFixedLengthString(long value, string expected)
        {
            Assert.Equal(
                expected,
                new Timestamp(value).ToString());
        }

        [Fact]
        public void ShouldBeEqual()
        {
            Assert.Equal(
                new Timestamp(0),
                new Timestamp(new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)));
        }
    }
}
