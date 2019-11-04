using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EpicReader
{
    public sealed class FileName : IEquatable<FileName>
    {
        private readonly string _value;

        public FileName(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("File name cannot be empty.");
            }

            if (value.Length > 200)
            {
                throw new ArgumentException("File name cannot be greater than 200 characters.");
            }

            _value = value;
        }

        public override bool Equals(object obj) => Equals(obj as FileName);

        public bool Equals([AllowNull] FileName other) => other != null && _value == other._value;

        public override int GetHashCode() => HashCode.Combine(_value);

        public override string ToString() => _value;

        public static bool operator ==(FileName left, FileName right) => EqualityComparer<FileName>.Default.Equals(left, right);

        public static bool operator !=(FileName left, FileName right) => !(left == right);
    }
}
