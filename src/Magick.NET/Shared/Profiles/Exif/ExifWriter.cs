// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ImageMagick
{
    internal sealed class ExifWriter
    {
        private const int _StartIndex = 6;

        private readonly ExifParts _allowedParts;
        private readonly Collection<ExifValue> _values;
        private readonly Collection<int> _ifdIndexes;
        private readonly Collection<int> _exifIndexes;
        private readonly Collection<int> _gPSIndexes;

        private Collection<int> _dataOffsets;

        public ExifWriter(Collection<ExifValue> values, ExifParts allowedParts)
        {
            _values = values;
            _allowedParts = allowedParts;

            _ifdIndexes = GetIndexes(ExifParts.IfdTags, ExifTags.Ifd);
            _exifIndexes = GetIndexes(ExifParts.ExifTags, ExifTags.Exif);
            _gPSIndexes = GetIndexes(ExifParts.GPSTags, ExifTags.GPS);
        }

        public byte[] GetData()
        {
            uint length = 0;
            int exifIndex = -1;
            int gpsIndex = -1;

            if (_exifIndexes.Count > 0)
                exifIndex = GetIndex(_ifdIndexes, ExifTag.SubIFDOffset);

            if (_gPSIndexes.Count > 0)
                gpsIndex = GetIndex(_ifdIndexes, ExifTag.GPSIFDOffset);

            var ifdLength = 2 + GetLength(_ifdIndexes) + 4;
            var exifLength = GetLength(_exifIndexes);
            var gpsLength = GetLength(_gPSIndexes);

            if (exifLength > 0)
                exifLength += 2;

            if (gpsLength > 0)
                gpsLength += 2;

            length = ifdLength + exifLength + gpsLength;

            if (length == 6)
                return null;

            length += 10 + 4 + 2;

            var result = new byte[length];
            result[0] = (byte)'E';
            result[1] = (byte)'x';
            result[2] = (byte)'i';
            result[3] = (byte)'f';
            result[4] = 0x00;
            result[5] = 0x00;

            if (BitConverter.IsLittleEndian)
            {
                result[6] = (byte)'I';
                result[7] = (byte)'I';
            }
            else
            {
                result[6] = (byte)'M';
                result[7] = (byte)'M';
            }

            result[8] = 0x2A;
            result[9] = 0x00;

            var i = 10;
            var ifdOffset = ((uint)i - _StartIndex) + 4;
            var thumbnailOffset = ifdOffset + ifdLength + exifLength + gpsLength;

            if (exifLength > 0)
                _values[exifIndex].Value = ifdOffset + ifdLength;

            if (gpsLength > 0)
                _values[gpsIndex].Value = ifdOffset + ifdLength + exifLength;

            i = Write(BitConverter.GetBytes(ifdOffset), result, i);
            i = WriteHeaders(_ifdIndexes, result, i);
            i = Write(BitConverter.GetBytes(thumbnailOffset), result, i);
            i = WriteData(_ifdIndexes, result, i);

            if (exifLength > 0)
            {
                i = WriteHeaders(_exifIndexes, result, i);
                i = WriteData(_exifIndexes, result, i);
            }

            if (gpsLength > 0)
            {
                i = WriteHeaders(_gPSIndexes, result, i);
                i = WriteData(_gPSIndexes, result, i);
            }

            Write(BitConverter.GetBytes((ushort)0), result, i);

            return result;
        }

        private static int Write(byte[] source, byte[] destination, int offset)
        {
            Buffer.BlockCopy(source, 0, destination, offset, source.Length);

            return offset + source.Length;
        }

        private static int WriteArray(ExifValue value, byte[] destination, int offset)
        {
            if (value.DataType == ExifDataType.String)
                return WriteValue(ExifDataType.String, value.Value, destination, offset);

            int newOffset = offset;
            foreach (object obj in (Array)value.Value)
                newOffset = WriteValue(value.DataType, obj, destination, newOffset);

            return newOffset;
        }

        private static int WriteRational(Rational value, byte[] destination, int offset)
        {
            Write(BitConverter.GetBytes(value.Numerator), destination, offset);
            Write(BitConverter.GetBytes(value.Denominator), destination, offset + 4);

            return offset + 8;
        }

        private static int WriteSignedRational(SignedRational value, byte[] destination, int offset)
        {
            Write(BitConverter.GetBytes(value.Numerator), destination, offset);
            Write(BitConverter.GetBytes(value.Denominator), destination, offset + 4);

            return offset + 8;
        }

        private static int WriteValue(ExifDataType dataType, object value, byte[] destination, int offset)
        {
            switch (dataType)
            {
                case ExifDataType.String:
                    return Write(Encoding.UTF8.GetBytes((string)value), destination, offset);
                case ExifDataType.Byte:
                case ExifDataType.Undefined:
                    destination[offset] = (byte)value;
                    return offset + 1;
                case ExifDataType.Double:
                    return Write(BitConverter.GetBytes((double)value), destination, offset);
                case ExifDataType.Short:
                    return Write(BitConverter.GetBytes((ushort)value), destination, offset);
                case ExifDataType.Long:
                    return Write(BitConverter.GetBytes((uint)value), destination, offset);
                case ExifDataType.Rational:
                    return WriteRational((Rational)value, destination, offset);
                case ExifDataType.SignedByte:
                    destination[offset] = unchecked((byte)((sbyte)value));
                    return offset + 1;
                case ExifDataType.SignedLong:
                    return Write(BitConverter.GetBytes((int)value), destination, offset);
                case ExifDataType.SignedShort:
                    return Write(BitConverter.GetBytes((short)value), destination, offset);
                case ExifDataType.SignedRational:
                    return WriteSignedRational((SignedRational)value, destination, offset);
                case ExifDataType.Float:
                    return Write(BitConverter.GetBytes((float)value), destination, offset);
                default:
                    throw new NotSupportedException();
            }
        }

        private static int WriteValue(ExifValue value, byte[] destination, int offset)
        {
            if (value.IsArray && value.DataType != ExifDataType.String)
                return WriteArray(value, destination, offset);
            else
                return WriteValue(value.DataType, value.Value, destination, offset);
        }

        private int WriteData(Collection<int> indexes, byte[] destination, int offset)
        {
            if (_dataOffsets.Count == 0)
                return offset;

            var newOffset = offset;

            var i = 0;
            foreach (int index in indexes)
            {
                ExifValue value = _values[index];
                if (value.Length > 4)
                {
                    Write(BitConverter.GetBytes(newOffset - _StartIndex), destination, _dataOffsets[i++]);
                    newOffset = WriteValue(value, destination, newOffset);
                }
            }

            return newOffset;
        }

        private int GetIndex(Collection<int> indexes, ExifTag tag)
        {
            foreach (int index in indexes)
            {
                if (_values[index].Tag == tag)
                    return index;
            }

            int newIndex = _values.Count;
            indexes.Add(newIndex);
            _values.Add(ExifValue.Create(tag, null));
            return newIndex;
        }

        private Collection<int> GetIndexes(ExifParts part, ExifTag[] tags)
        {
            if (!EnumHelper.HasFlag(_allowedParts, part))
                return new Collection<int>();

            var result = new Collection<int>();
            for (int i = 0; i < _values.Count; i++)
            {
                var value = _values[i];

                if (!value.HasValue)
                    continue;

                int index = Array.IndexOf(tags, value.Tag);
                if (index > -1)
                    result.Add(i);
            }

            return result;
        }

        private uint GetLength(IEnumerable<int> indexes)
        {
            uint length = 0;

            foreach (int index in indexes)
            {
                var valueLength = (uint)_values[index].Length;

                if (valueLength > 4)
                    length += 12 + valueLength;
                else
                    length += 12;
            }

            return length;
        }

        private int WriteHeaders(Collection<int> indexes, byte[] destination, int offset)
        {
            _dataOffsets = new Collection<int>();

            var newOffset = Write(BitConverter.GetBytes((ushort)indexes.Count), destination, offset);

            if (indexes.Count == 0)
                return newOffset;

            foreach (int index in indexes)
            {
                var value = _values[index];
                newOffset = Write(BitConverter.GetBytes((ushort)value.Tag), destination, newOffset);
                newOffset = Write(BitConverter.GetBytes((ushort)value.DataType), destination, newOffset);
                newOffset = Write(BitConverter.GetBytes((uint)value.NumberOfComponents), destination, newOffset);

                if (value.Length > 4)
                    _dataOffsets.Add(newOffset);
                else
                    WriteValue(value, destination, newOffset);

                newOffset += 4;
            }

            return newOffset;
        }
    }
}