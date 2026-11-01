using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CanDoUrco.Collections.Spans;
using CanDoUrco.Core.Extensions;
using CanDoUrco.Core.Pointers;
using CanDoUrco.Core.Types;

namespace CanDoUrco.Strings.Unicode;

public static class Utf8
{
    public static unsafe int GetByteCount(Pointer<byte> bytes) =>
        MemoryMarshal.CreateReadOnlySpanFromNullTerminated(bytes).Length;

    public static unsafe ReadOnlySpan<byte> GetNullTerminatedSpan(Pointer<byte> bytes) =>
        MemoryMarshal.CreateReadOnlySpanFromNullTerminated(bytes);

    public static int EvaluateLength(ReadOnlySpan<byte> utf8Buffer)
    {
        var length = utf8Buffer.IndexOf((byte)0);
        return length != -1 ? length : utf8Buffer.Length;
    }

    public static ScratchSpan<char> GetChars(
        ReadOnlySpan<byte> value,
        Span<char> stackBuffer,
        out Span<char> chars
    )
    {
        var scratch = new ScratchSpan<char>(GetCharCount(value), stackBuffer, out chars);
        GetChars(value, chars);
        return scratch;
    }

    public static ScratchSpan<byte> GetBytes(
        ReadOnlySpan<char> value,
        Span<byte> stackBuffer,
        out Span<byte> bytes
    )
    {
        var scratch = new ScratchSpan<byte>(GetByteCount(value), stackBuffer, out bytes);
        GetBytes(value, bytes);
        return scratch;
    }

    public static int GetByteCount(char[] chars) => Encoding.UTF8.GetByteCount(chars);

    public static int GetByteCount(string str) => Encoding.UTF8.GetByteCount(str);

    public static int GetByteCount(char[] chars, int index, int count) =>
        Encoding.UTF8.GetByteCount(chars, index, count);

    public static int GetByteCount(string str, int index, int count) =>
        Encoding.UTF8.GetByteCount(str, index, count);

    public static unsafe int GetByteCount(Pointer<char> chars, int count) =>
        Encoding.UTF8.GetByteCount(chars, count);

    public static int GetByteCount(ReadOnlySpan<char> chars) => Encoding.UTF8.GetByteCount(chars);

    public static byte[] GetBytes(char[] chars) => Encoding.UTF8.GetBytes(chars);

    public static byte[] GetBytes(char[] chars, int index, int count) =>
        Encoding.UTF8.GetBytes(chars, index, count);

    public static int GetBytes(
        char[] chars,
        int charIndex,
        int charCount,
        byte[] bytes,
        int byteIndex
    ) => Encoding.UTF8.GetBytes(chars, charIndex, charCount, bytes, byteIndex);

    public static byte[] GetBytes(string str) => Encoding.UTF8.GetBytes(str);

    public static byte[] GetBytes(string str, int index, int count) =>
        Encoding.UTF8.GetBytes(str, index, count);

    public static int GetBytes(
        string str,
        int charIndex,
        int charCount,
        byte[] bytes,
        int byteIndex
    ) => Encoding.UTF8.GetBytes(str, charIndex, charCount, bytes, byteIndex);

    public static unsafe int GetBytes(
        Pointer<char> chars,
        int charCount,
        Pointer<byte> bytes,
        int byteCount
    ) => Encoding.UTF8.GetBytes(chars, charCount, bytes, byteCount);

    public static int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes) =>
        Encoding.UTF8.GetBytes(chars, bytes);

    public static bool TryGetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
    {
        if (FromUtf16(chars, bytes, out _, out bytesWritten) is OperationStatus.Done)
        {
            return true;
        }

        bytesWritten = 0;
        return false;
    }

    public static int GetCharCount(byte[] bytes) => Encoding.UTF8.GetCharCount(bytes);

    public static int GetCharCount(byte[] bytes, int index, int count) =>
        Encoding.UTF8.GetCharCount(bytes, index, count);

    public static unsafe int GetCharCount(Pointer<byte> bytes, int count) =>
        Encoding.UTF8.GetCharCount(bytes, count);

    public static int GetCharCount(ReadOnlySpan<byte> bytes) => Encoding.UTF8.GetCharCount(bytes);

    public static char[] GetChars(byte[] bytes) => Encoding.UTF8.GetChars(bytes);

    public static char[] GetChars(byte[] bytes, int index, int count) =>
        Encoding.UTF8.GetChars(bytes, index, count);

    public static int GetChars(
        byte[] bytes,
        int byteIndex,
        int byteCount,
        char[] chars,
        int charIndex
    ) => Encoding.UTF8.GetChars(bytes, byteIndex, byteCount, chars, charIndex);

    public static unsafe int GetChars(
        Pointer<byte> bytes,
        int byteCount,
        Pointer<char> chars,
        int charCount
    ) => Encoding.UTF8.GetChars(bytes, byteCount, chars, charCount);

    public static int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars) =>
        Encoding.UTF8.GetChars(bytes, chars);

    public static bool TryGetChars(ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten)
    {
        if (ToUtf16(bytes, chars, out _, out charsWritten) is OperationStatus.Done)
        {
            return true;
        }

        charsWritten = 0;
        return false;
    }

    public static unsafe string GetString(Pointer<byte> bytes, int byteCount) =>
        Encoding.UTF8.GetString(bytes, byteCount);

    public static unsafe string GetString(Pointer<byte> bytes) =>
        Encoding.UTF8.GetString(bytes, GetByteCount(bytes));

    public static string GetString(ReadOnlySpan<byte> bytes) => Encoding.UTF8.GetString(bytes);

    public static int GetMaxByteCount(int charCount) => Encoding.UTF8.GetMaxByteCount(charCount);

    public static int GetMaxCharCount(int byteCount) => Encoding.UTF8.GetMaxCharCount(byteCount);

    public static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

    public static string GetString(byte[] bytes, int index, int count) =>
        Encoding.UTF8.GetString(bytes, index, count);

    public static OperationStatus FromUtf16(
        ReadOnlySpan<char> source,
        Span<byte> destination,
        out int charsRead,
        out int bytesWritten,
        bool replaceInvalidSequences = true,
        bool isFinalBlock = true
    ) =>
        System.Text.Unicode.Utf8.FromUtf16(
            source,
            destination,
            out charsRead,
            out bytesWritten,
            replaceInvalidSequences,
            isFinalBlock
        );

    public static OperationStatus ToUtf16(
        ReadOnlySpan<byte> source,
        Span<char> destination,
        out int bytesRead,
        out int charsWritten,
        bool replaceInvalidSequences = true,
        bool isFinalBlock = true
    ) =>
        System.Text.Unicode.Utf8.ToUtf16(
            source,
            destination,
            out bytesRead,
            out charsWritten,
            replaceInvalidSequences,
            isFinalBlock
        );

    public static bool TryWrite(
        Span<byte> destination,
        [InterpolatedStringHandlerArgument(nameof(destination))]
            ref System.Text.Unicode.Utf8.TryWriteInterpolatedStringHandler handler,
        out int bytesWritten
    ) => System.Text.Unicode.Utf8.TryWrite(destination, ref handler, out bytesWritten);

    public static bool TryWrite(
        Span<byte> destination,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument(nameof(destination), nameof(provider))]
            ref System.Text.Unicode.Utf8.TryWriteInterpolatedStringHandler handler,
        out int bytesWritten
    ) => System.Text.Unicode.Utf8.TryWrite(destination, provider, ref handler, out bytesWritten);

    public static bool IsValid(ReadOnlySpan<byte> value) => System.Text.Unicode.Utf8.IsValid(value);

    public static bool TryFormat<T>(
        T value,
        Span<byte> destination,
        out int elementsWritten,
        string? format = null,
        IFormatProvider? provider = null
    )
    {
        if (
            TryFormatICustomFormatter(value, destination, out elementsWritten, format, provider) is
            { } result
        )
        {
            return result;
        }

        if (!typeof(T).IsValueType && value is null)
        {
            elementsWritten = 0;
            return true;
        }

        if (typeof(T) == typeof(string))
        {
            return TryAppend(
                Unsafe.As<T, string>(ref value).AsSpan(),
                destination,
                out elementsWritten
            );
        }

        if (typeof(T).IsEnum)
        {
            return Enum.TryFormatUnconstrained(value, destination, out elementsWritten, format);
        }

        if (typeof(T) == typeof(bool))
        {
            return Unsafe
                .As<T, bool>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(char))
        {
            return Unsafe
                .As<T, char>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(byte))
        {
            return Unsafe
                .As<T, byte>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(sbyte))
        {
            return Unsafe
                .As<T, sbyte>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(short))
        {
            return Unsafe
                .As<T, short>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(ushort))
        {
            return Unsafe
                .As<T, ushort>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(int))
        {
            return Unsafe
                .As<T, int>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(uint))
        {
            return Unsafe
                .As<T, uint>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(long))
        {
            return Unsafe
                .As<T, long>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(ulong))
        {
            return Unsafe
                .As<T, ulong>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(nint))
        {
            return Unsafe
                .As<T, nint>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(nuint))
        {
            return Unsafe
                .As<T, nuint>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(Int128))
        {
            return Unsafe
                .As<T, Int128>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(UInt128))
        {
            return Unsafe
                .As<T, UInt128>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(Half))
        {
            return Unsafe
                .As<T, Half>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(float))
        {
            return Unsafe
                .As<T, float>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(double))
        {
            return Unsafe
                .As<T, double>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(decimal))
        {
            return Unsafe
                .As<T, decimal>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(DateTime))
        {
            return Unsafe
                .As<T, DateTime>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(DateTimeOffset))
        {
            return Unsafe
                .As<T, DateTimeOffset>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(TimeSpan))
        {
            return Unsafe
                .As<T, TimeSpan>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(DateOnly))
        {
            return Unsafe
                .As<T, DateOnly>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(TimeOnly))
        {
            return Unsafe
                .As<T, TimeOnly>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(Guid))
        {
            return Unsafe
                .As<T, Guid>(ref value)
                .TryFormat(destination, out elementsWritten, format);
        }
        if (typeof(T) == typeof(Bool8))
        {
            return Unsafe
                .As<T, Bool8>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }
        if (typeof(T) == typeof(Bool32))
        {
            return Unsafe
                .As<T, Bool32>(ref value)
                .TryFormat(destination, out elementsWritten, format, provider);
        }

        if (value is IUtf8SpanFormattable utf8SpanFormattable)
        {
            return utf8SpanFormattable.TryFormat(
                destination,
                out elementsWritten,
                format,
                provider
            );
        }

        if (value is ISpanFormattable spanFormattable)
        {
            return TryFormatISpanFormattable(
                spanFormattable,
                destination,
                out elementsWritten,
                format,
                provider
            );
        }

        var s = value is IFormattable formattable
            ? formattable.ToString(format, provider)
            : value!.ToString();

        return TryAppend(s.AsSpan(), destination, out elementsWritten);
    }

    private static StandardFormat ParseBoolFormat(ReadOnlySpan<char> format) =>
        StandardFormat.TryParse(format, out var result) && result.Symbol is '\0' or 'G' or 'l'
            ? result
            : default;

    private static bool? TryFormatICustomFormatter<T>(
        T value,
        Span<byte> destination,
        out int elementsWritten,
        string? format,
        IFormatProvider? provider
    )
    {
        if (
            provider is null
            || provider.GetType() == typeof(CultureInfo)
            || provider.GetFormat(typeof(ICustomFormatter)) is not { } formatterObj
        )
        {
            elementsWritten = 0;
            return null;
        }

        var formatter = (ICustomFormatter)formatterObj;
        if (formatter.Format(format, value, provider) is { } customFormatted)
        {
            return TryAppend(customFormatted.AsSpan(), destination, out elementsWritten);
        }

        elementsWritten = 0;
        return null;
    }

    private static bool TryFormatISpanFormattable<T>(
        T value,
        Span<byte> destination,
        out int elementsWritten,
        string? format,
        IFormatProvider? provider
    )
    {
        if (value is not ISpanFormattable)
        {
            throw new ArgumentException(
                $"Value must implement {nameof(ISpanFormattable)}.",
                nameof(value)
            );
        }

        Span<char> utf16 = stackalloc char[256];
        return ((ISpanFormattable)value).TryFormat(utf16, out var charsWritten, format, provider)
            ? TryAppend(utf16[..charsWritten], destination, out elementsWritten)
            : GrowAndAppendFormatted(
                value,
                utf16.Length,
                destination,
                out elementsWritten,
                format,
                provider
            );

        static bool GrowAndAppendFormatted(
            T value,
            int length,
            Span<byte> destination,
            out int elementsWritten,
            ReadOnlySpan<char> format,
            IFormatProvider? provider
        )
        {
            if (value is not ISpanFormattable)
            {
                throw new ArgumentException(
                    $"Value must implement {nameof(ISpanFormattable)}.",
                    nameof(value)
                );
            }

            while (true)
            {
                var newLength = length * 2;
                if ((uint)newLength >= Array.MaxLength)
                {
                    newLength = length == Array.MaxLength ? Array.MaxLength + 1 : Array.MaxLength;
                }

                length = newLength;
                var array = ArrayPool<char>.Shared.Rent(length);
                try
                {
                    if (
                        ((ISpanFormattable)value).TryFormat(
                            array,
                            out var charsWritten,
                            format,
                            provider
                        )
                    )
                    {
                        return TryAppend(
                            array.AsSpan(0, charsWritten),
                            destination,
                            out elementsWritten
                        );
                    }
                }
                finally
                {
                    ArrayPool<char>.Shared.Return(array);
                }
            }
        }
    }

    private static bool TryAppend(
        scoped ReadOnlySpan<char> value,
        Span<byte> destination,
        out int elementsWritten
    ) => TryGetBytes(value, destination, out elementsWritten);

    public static bool TryFormat(
        this bool value,
        Span<byte> destination,
        out int elementsWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null
    ) => Utf8Formatter.TryFormat(value, destination, out elementsWritten, ParseBoolFormat(format));

    public static bool TryFormat(
        this char value,
        Span<byte> destination,
        out int elementsWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null
    ) => TryGetBytes([value], destination, out elementsWritten);
}
