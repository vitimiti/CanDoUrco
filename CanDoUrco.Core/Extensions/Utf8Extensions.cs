using System.Text.Unicode;

namespace CanDoUrco.Core.Extensions;

public static class Utf8Extensions
{
    extension(Utf8)
    {
        public static bool TryFormat<T>(
            T value,
            Span<byte> destination,
            out int bytesWritten,
            string? format = null,
            IFormatProvider? provider = null
        )
        {
            var handler = new Utf8.TryWriteInterpolatedStringHandler(
                literalLength: 0,
                formattedCount: 1,
                destination,
                provider,
                out _
            );
            if (format is null)
            {
                handler.AppendFormatted(value);
            }
            else
            {
                handler.AppendFormatted(value, format);
            }

            return Utf8.TryWrite([], ref handler, out bytesWritten);
        }
    }
}
