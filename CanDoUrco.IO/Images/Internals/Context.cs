// A set of utilities to handle IO for Can Do Urco.
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

namespace CanDoUrco.IO.Images.Internals;

internal sealed class Context
{
    public uint ImgX { get; set; }
    public uint ImgY { get; set; }
    public int ImgN { get; set; }
    public int ImgOutN { get; set; }
    public IOCallbacks IO { get; set; } = new();
    public object? IOUserData { get; set; }
    public int ReadFromCallbacks { get; set; }
    public int BufLen { get; set; }
    public byte[] BufferStart { get; } = new byte[Constants.BufferStartLength];
    public int BufferStartPosition { get; set; }
    public int CallbackAlreadyRead { get; set; }
    public byte[]? ImgBuffer { get; set; }
    public int ImgBufferPosition { get; set; }
    public byte[]? ImgBufferEnd { get; set; }
    public int ImgBufferOriginalPosition { get; set; }
    public byte[]? ImgBufferOriginal { get; set; }
    public int ImgBufferOriginalEndPosition { get; set; }
    public byte[]? ImgBufferOriginalEnd { get; set; }
    public int ImgBufferEndPosition { get; set; }
}
