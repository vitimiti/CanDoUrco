using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace CanDoUrco.Core.Extensions;

public static class EnumExtensions
{
    extension(Enum)
    {
        public static bool TryFormat<T>(
            T value,
            Span<byte> destination,
            out int bytesWritten,
            [StringSyntax(StringSyntaxAttribute.EnumFormat)] ReadOnlySpan<char> format = default
        )
            where T : unmanaged, Enum =>
            TryFormatUnconstrained(value, destination, out bytesWritten, format);

        public static bool TryFormatUnconstrained<TEnum>(
            TEnum value,
            Span<byte> destination,
            out int bytesWritten,
            [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default
        )
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException($"Type {typeof(TEnum)} is not an enum.");
            }

            Span<char> utf16 = stackalloc char[256];
            if (TryFormatUnconstrained(null, value, utf16, out var charsWritten, format))
            {
                return TryGetBytes(utf16[..charsWritten], destination, out bytesWritten);
            }

            if (destination.Length > utf16.Length)
            {
                var array16 = ArrayPool<char>.Shared.Rent(destination.Length);
                try
                {
                    utf16 = array16;
                    if (TryFormatUnconstrained(null, value, utf16, out charsWritten, format))
                    {
                        return TryGetBytes(utf16[..charsWritten], destination, out bytesWritten);
                    }
                }
                finally
                {
                    ArrayPool<char>.Shared.Return(array16);
                }
            }

            bytesWritten = 0;
            return false;
        }

        public static bool TryFormatUnconstrained<TEnum>(
            TEnum value,
            Span<char> destination,
            out int charsWritten,
            [StringSyntax(StringSyntaxAttribute.EnumFormat)] ReadOnlySpan<char> format = default
        )
            where TEnum : Enum =>
            TryFormatUnconstrained(null, value, destination, out charsWritten, format);

        private static bool TryGetBytes(
            ReadOnlySpan<char> chars,
            Span<byte> destination,
            out int bytesWritten
        )
        {
            if (Utf8.FromUtf16(chars, destination, out _, out bytesWritten) is OperationStatus.Done)
            {
                return true;
            }

            bytesWritten = 0;
            return false;
        }

        [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = nameof(TryFormatUnconstrained))]
        static extern bool TryFormatUnconstrained<TEnum>(
            Enum? _,
            TEnum value,
            Span<char> destination,
            out int charsWritten,
            [StringSyntax(StringSyntaxAttribute.EnumFormat)] ReadOnlySpan<char> format = default
        );
    }
}
