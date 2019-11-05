using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EpicReader
{
    public sealed class DocumentName
        : IEquatable<DocumentName>
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

        public DocumentName(string value)
            : this(
                  new Timestamp(long.Parse(value.Substring(0, 10))),
                  new Guid(value.Substring(11, 32)),
                  new FileName(value.Substring(44, value.Length - 44)))
        {
        }

        public string FileName() => _fileName.ToString();

        public override bool Equals(object obj)
        {
            return Equals(obj as DocumentName);
        }

        public bool Equals([AllowNull] DocumentName other)
        {
            return other != null &&
                   EqualityComparer<Timestamp>.Default.Equals(_timestamp, other._timestamp) &&
                   _guid.Equals(other._guid) &&
                   EqualityComparer<FileName>.Default.Equals(_fileName, other._fileName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_timestamp, _guid, _fileName);
        }

        public override string ToString()
        {
            return $"{_timestamp.ToString()}_{_guid.ToString().Replace("-", string.Empty)}_{_fileName}";
        }
    }
}
