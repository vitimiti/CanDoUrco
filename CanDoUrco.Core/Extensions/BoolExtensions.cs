// BoolExtensions.cs - Provides extension methods for the boolean type in the CanDoUrco framework.
// Copyright (C) <year>  <name of author>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Buffers;
using System.Buffers.Text;

namespace CanDoUrco.Core.Extensions;

public static class BoolExtensions
{
    extension(bool value)
    {
        public bool TryFormat(
            Span<byte> destination,
            out int bytesWritten,
            ReadOnlySpan<char> format = default
        ) =>
            Utf8Formatter.TryFormat(
                value,
                destination,
                out bytesWritten,
                StandardFormat.Parse(format)
            );

        public bool TryFormat(
            Span<char> destination,
            out int charsWritten,
            ReadOnlySpan<char> format = default
        ) => TryFormat(value, destination, out charsWritten, StandardFormat.Parse(format));

        public bool TryFormat(
            Span<char> destination,
            out int charsWritten,
            StandardFormat format = default
        )
        {
            var symbol = FormattingHelpers.GetSymbolOrDefault(format, 'G');
            if (value)
            {
                if (symbol == 'G')
                {
                    if (!"True".TryCopyTo(destination))
                    {
                        goto BufferTooSmall;
                    }
                }
                else if (symbol == 'l')
                {
                    if (!"true".TryCopyTo(destination))
                    {
                        goto BufferTooSmall;
                    }
                }
                else
                {
                    goto BadFormat;
                }

                charsWritten = 4;
                return true;
            }
            else
            {
                if (symbol == 'G')
                {
                    if (!"False".TryCopyTo(destination))
                    {
                        goto BufferTooSmall;
                    }
                }
                else if (symbol == 'l')
                {
                    if (!"false".TryCopyTo(destination))
                    {
                        goto BufferTooSmall;
                    }
                }
                else
                {
                    goto BadFormat;
                }

                charsWritten = 5;
                return true;
            }

            BadFormat:
            {
                throw new FormatException("Format specifier was invalid.");
            }

            BufferTooSmall:
            {
                charsWritten = 0;
                return false;
            }
        }
    }

    static class FormattingHelpers
    {
        public static char GetSymbolOrDefault(in StandardFormat format, char defaultSymbol)
        {
            var symbol = format.Symbol;
            if (symbol == default && format.Precision == default)
            {
                symbol = defaultSymbol;
            }

            return symbol;
        }
    }
}
