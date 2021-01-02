// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Text;

namespace ImageMagick
{
    internal sealed class EndianReader
    {
        private readonly byte[] _data;

        public EndianReader(byte[] data)
        {
            Throw.IfNullOrEmpty(nameof(data), data);

            _data = data;
            Index = 0;
        }

        public uint Index { get; private set; }

        public bool CanRead(uint length)
        {
            if (length > _data.Length)
                return false;

            return Index + length <= _data.Length;
        }

        public bool Seek(uint index)
        {
            if (index >= _data.Length)
                return false;

            Index = index;

            return true;
        }

        public bool Skip(uint value)
        {
            if (Index + value >= _data.Length)
                return false;

            Index += value;

            return true;
        }

        public byte? ReadByte()
        {
            if (Index >= _data.Length)
                return null;

            var result = _data[Index];

            Index++;

            return result;
        }

        public unsafe double? ReadDoubleLSB()
        {
            if (!CanRead(8))
                return null;

            ulong result = _data[Index];
            result |= (ulong)_data[Index + 1] << 8;
            result |= (ulong)_data[Index + 2] << 16;
            result |= (ulong)_data[Index + 3] << 24;
            result |= (ulong)_data[Index + 4] << 32;
            result |= (ulong)_data[Index + 5] << 40;
            result |= (ulong)_data[Index + 6] << 48;
            result |= (ulong)_data[Index + 7] << 56;

            Index += 8;

            return *(double*)&result;
        }

        public unsafe double? ReadDoubleMSB()
        {
            if (!CanRead(8))
                return null;

            ulong result = (ulong)_data[Index] << 56;
            result |= (ulong)_data[Index + 1] << 48;
            result |= (ulong)_data[Index + 2] << 40;
            result |= (ulong)_data[Index + 3] << 32;
            result |= (ulong)_data[Index + 4] << 24;
            result |= (ulong)_data[Index + 5] << 16;
            result |= (ulong)_data[Index + 6] << 8;
            result |= _data[Index + 7];

            Index += 8;

            return *(double*)&result;
        }

        public uint? ReadLongLSB()
        {
            if (!CanRead(4))
                return null;

            uint result = _data[Index];
            result |= (uint)_data[Index + 1] << 8;
            result |= (uint)_data[Index + 2] << 16;
            result |= (uint)_data[Index + 3] << 24;

            Index += 4;

            return result;
        }

        public uint? ReadLongMSB()
        {
            if (!CanRead(4))
                return null;

            uint result = (uint)_data[Index] << 24;
            result |= (uint)_data[Index + 1] << 16;
            result |= (uint)_data[Index + 2] << 8;
            result |= _data[Index + 3];

            Index += 4;

            return result;
        }

        public ushort? ReadShortLSB()
        {
            if (!CanRead(2))
                return null;

            ushort result = _data[Index];
            result |= (ushort)(_data[Index + 1] << 8);

            Index += 2;

            return result;
        }

        public ushort? ReadShortMSB()
        {
            if (!CanRead(2))
                return null;

            ushort result = (ushort)(_data[Index] << 8);
            result |= _data[Index + 1];

            Index += 2;

            return result;
        }

        public unsafe float? ReadFloatLSB()
        {
            uint? result = ReadLongLSB();
            if (result == null)
                return null;

            uint value = result.Value;

            return *(float*)&value;
        }

        public unsafe float? ReadFloatMSB()
        {
            uint? result = ReadLongMSB();
            if (result == null)
                return null;

            uint value = result.Value;

            return *(float*)&value;
        }

        public string ReadString(uint length)
        {
            if (length == 0)
                return string.Empty;

            if (!CanRead(length))
                return null;

            var result = Encoding.UTF8.GetString(_data, (int)Index, (int)length);
            int nullCharIndex = result.IndexOf('\0');
            if (nullCharIndex != -1)
                result = result.Substring(0, nullCharIndex);

            Index += length;

            return result;
        }
    }
}
