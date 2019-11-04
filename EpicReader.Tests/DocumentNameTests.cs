using System;
using Xunit;

namespace EpicReader.Tests
{
    public class DocumentNameTests
    {
        [Fact]
        public void ShouldConvertToString()
        {
            var timestamp = new Timestamp(123456789);
            var guid = new Guid("a9d7a3ae-df80-4925-a542-17347df3a280");
            var fileName = new FileName("file.pdf");
            var documentName = new DocumentName(timestamp, guid, fileName);

            Assert.Equal(
                "0123456789_a9d7a3aedf804925a54217347df3a280_file.pdf",
                documentName.ToString());
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var timestamp = new Timestamp(123456789);
            var guid = new Guid("a9d7a3ae-df80-4925-a542-17347df3a280");
            var fileName = new FileName("file.pdf");
            var documentName1 = new DocumentName(timestamp, guid, fileName);
            var documentName2 = new DocumentName(timestamp, guid, fileName);

            Assert.Equal(documentName1, documentName2);
        }
    }
}
