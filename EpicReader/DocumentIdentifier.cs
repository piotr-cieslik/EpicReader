using System;

namespace EpicReader
{
    public sealed class DocumentIdentifier
    {
        private readonly DateTime _timestamp;
        private readonly string _fileName;

        public DocumentIdentifier(
            DateTime timestamp,
            string fileName)
        {
            _timestamp = timestamp;
            _fileName = fileName;
        }

        public DateTime Timestamp() => _timestamp;

        public string FileName() => _fileName;

        public static DocumentIdentifier Parse(string documentIdentifier)
        {
            var ticks = long.Parse(documentIdentifier.Substring(0, 20));
            var timestamp = new DateTime(ticks);
            var fileName = documentIdentifier.Substring(20, documentIdentifier.Length - 20);
            return new DocumentIdentifier(timestamp, fileName);
        }

        public override string ToString()
        {
            return $"{_timestamp.Ticks.ToString("D20")}{_fileName}";
        }
    }
}
