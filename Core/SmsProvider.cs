using System;
using System.Collections.Generic;

namespace SS.SMS.Core
{
    public class SmsProvider : IEquatable<SmsProvider>, IComparable<SmsProvider>
    {
        public static readonly SmsProvider AliYun = new SmsProvider("AliYun");

        public static readonly SmsProvider YunPian = new SmsProvider("YunPian");

        private SmsProvider(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as SmsProvider);
        }

        public static bool operator ==(SmsProvider a, SmsProvider b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(SmsProvider a, SmsProvider b)
        {
            return !(a == b);
        }

        public bool Equals(SmsProvider other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(SmsProvider other)
        {
            if (other == null)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return StringComparer.OrdinalIgnoreCase.Compare(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}