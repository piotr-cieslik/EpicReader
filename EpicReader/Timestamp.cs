using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EpicReader
{
    public sealed class Timestamp 
        : IEquatable<Timestamp>
    {
        private readonly long _value;

        public Timestamp(DateTime value)
            : this(((DateTimeOffset)value).ToUnixTimeSeconds())
        {
        }

        public Timestamp(long value) => _value = value;

        public override bool Equals(object obj) => Equals(obj as Timestamp);

        public bool Equals([AllowNull] Timestamp other) => other != null && _value == other._value;

        public override int GetHashCode() => HashCode.Combine(_value);

        public override string ToString() => _value.ToString("D10");

        public static bool operator ==(Timestamp left, Timestamp right) => EqualityComparer<Timestamp>.Default.Equals(left, right);

        public static bool operator !=(Timestamp left, Timestamp right) => !(left == right);
    }
}
