using System;

namespace EpicReader
{
    public sealed class DocumentName
    {
        private readonly Timestamp _timestamp;
        private readonly Guid _guid;
        private readonly FileName _fileName;

        public DocumentName(
            Timestamp timestamp,
            Guid guid,
            FileName fileName)
        {
            _timestamp = timestamp;
            _guid = guid;
            _fileName = fileName;
        }

        public override string ToString() => $"{_timestamp.ToString()}_{_guid.ToString().Replace("-", string.Empty)}_{_fileName}";
    }
}
