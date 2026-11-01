// IByteSize`2.cs - Defines the generic IByteSize<TSelf, TBackend> interface for CanDoUrco.Abstractions
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
using CanDoUrco.Core.Numerics;

namespace CanDoUrco.Core.Types;

public interface IByteSize<TSelf, TBackend> : IByteSize, IArithmeticOperators<TSelf, TSelf, TSelf>
    where TSelf : IByteSize<TSelf, TBackend>
    where TBackend : unmanaged, IUnsignedNumber<TBackend>
{
    public TBackend Value { get; }

    static abstract implicit operator TSelf(ByteSize value);

    static abstract explicit operator TSelf(TBackend value);

    static abstract explicit operator TSelf(int value);

    static abstract TSelf operator *(TSelf left, ElementCount right);

    static abstract TSelf operator *(ElementCount right, TSelf left);

    static abstract TSelf operator +(TSelf left, TBackend right);

    static abstract TSelf operator -(TSelf left, TBackend right);

    static abstract TSelf operator *(TSelf left, TBackend right);

    static abstract TSelf operator /(TSelf left, TBackend right);

    static abstract TSelf operator +(TBackend left, TSelf right);

    static abstract TSelf operator -(TBackend left, TSelf right);

    static abstract TSelf operator *(TBackend left, TSelf right);

    static abstract TSelf operator /(TBackend left, TSelf right);

    static abstract TSelf operator +(TSelf left, int right);

    static abstract TSelf operator -(TSelf left, int right);

    static abstract TSelf operator *(TSelf left, int right);

    static abstract TSelf operator /(TSelf left, int right);

    static abstract TSelf operator +(int left, TSelf right);

    static abstract TSelf operator -(int left, TSelf right);

    static abstract TSelf operator *(int left, TSelf right);

    static abstract TSelf operator /(int left, TSelf right);
}
