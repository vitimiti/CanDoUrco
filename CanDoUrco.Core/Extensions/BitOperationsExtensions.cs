// BitOperationsExtensions.cs - Defines extension methods for bit operations in the CanDoUrco framework.
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

using System.Numerics;

namespace CanDoUrco.Core.Extensions;

public static class BitOperationsExtensions
{
    extension(BitOperations)
    {
        public static bool TryGetBitIndex<T>(T mask, out byte index)
            where T : unmanaged, IBinaryInteger<T>
        {
            if (!T.IsPow2(mask))
            {
                index = 0;
                return false;
            }

            var tzc = T.TrailingZeroCount(mask);
            index = byte.CreateTruncating(tzc);
            return true;
        }
    }
}
