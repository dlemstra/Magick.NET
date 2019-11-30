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
using System.Text;

namespace ImageMagick
{
    internal sealed class ExifWriter
    {
        private const uint _OffsetDelta = 6;

        private readonly ExifParts _allowedParts;

        public ExifWriter(ExifParts allowedParts) => _allowedParts = allowedParts;

        public byte[] Write(List<IExifValue> values)
        {
            var ifdValues = GetPartValues(values, ExifParts.IfdTags);
            var exifValues = GetPartValues(values, ExifParts.ExifTags);
            var gpsValues = GetPartValues(values, ExifParts.GpsTags);

            var exifOffset = GetOffsetValue(ifdValues, exifValues, ExifTag.SubIFDOffset);
            var gpsOffset = GetOffsetValue(ifdValues, gpsValues, ExifTag.GPSIFDOffset);

            if (ifdValues.Count == 0 && exifValues.Count == 0 && gpsValues.Count == 0)
                return null;

            var ifdLength = GetLength(ifdValues) + 4U;
            var exifLength = GetLength(exifValues);
            var gpsLength = GetLength(gpsValues);

            var ifdOffset = 10U + 4U - _OffsetDelta;
            var thumbnailOffset = ifdOffset + ifdLength + exifLength + gpsLength;

            var length = _OffsetDelta + thumbnailOffset + 2U;

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

            if (exifOffset != null)
                exifOffset.SetValue(ifdOffset + ifdLength);

            if (gpsOffset != null)
                gpsOffset.SetValue(ifdOffset + ifdLength + exifLength);

            i = Write(BitConverter.GetBytes(ifdOffset), result, i);
            i = WriteHeaders(ifdValues, result, i, 4);
            i = Write(BitConverter.GetBytes(thumbnailOffset), result, i);
            i = WriteData(ifdValues, result, i);

            if (exifValues.Count > 0)
            {
                i = WriteHeaders(exifValues, result, i);
                i = WriteData(exifValues, result, i);
            }

            if (gpsValues.Count > 0)
            {
                i = WriteHeaders(gpsValues, result, i);
                i = WriteData(gpsValues, result, i);
            }

            Write(BitConverter.GetBytes((ushort)0), result, i);

            return result;
        }

        private static bool HasValue(IExifValue exifValue)
        {
            var value = exifValue.GetValue();
            if (value is null)
                return false;

            if (exifValue.DataType == ExifDataType.String)
            {
                var stringValue = (string)value;
                return stringValue.Length > 0;
            }

            if (value is Array arrayValue)
                return arrayValue.Length > 0;

            return true;
        }

        private static IExifValue GetOffsetValue(List<IExifValue> ifdValues, List<IExifValue> values, ExifTag offset)
        {
            var index = -1;

            for (var i = 0; i < ifdValues.Count; i++)
            {
                if (ifdValues[i].Tag == offset)
                    index = i;
            }

            if (values.Count > 0)
            {
                if (index != -1)
                    return ifdValues[index];

                var result = ExifValues.Create(offset);
                ifdValues.Add(result);

                return result;
            }
            else if (index != -1)
            {
                ifdValues.RemoveAt(index);
            }

            return null;
        }

        private static uint GetLength(List<IExifValue> values)
        {
            if (values.Count == 0)
                return 0;

            uint length = 2;

            foreach (var value in values)
            {
                var valueLength = GetLength(value);

                length += 2 + 2 + 4 + 4;

                if (valueLength > 4)
                    length += valueLength;
            }

            return length;
        }

        private static uint GetLength(IExifValue value) => GetNumberOfComponents(value) * ExifDataTypes.GetSize(value.DataType);

        private static uint GetNumberOfComponents(IExifValue exifValue)
        {
            var value = exifValue.GetValue();

            if (exifValue.DataType == ExifDataType.String)
                return (uint)Encoding.UTF8.GetBytes((string)value).Length + 1;

            if (value is Array arrayValue)
                return (uint)arrayValue.Length;

            return 1;
        }

        private static int WriteHeaders(List<IExifValue> values, byte[] destination, int offset) => WriteHeaders(values, destination, offset, 0);

        private static int WriteHeaders(List<IExifValue> values, byte[] destination, int offset, uint dataOffset)
        {
            offset = Write(BitConverter.GetBytes((ushort)values.Count), destination, offset);

            dataOffset += (uint)(offset + (values.Count * (2 + 2 + 4 + 4))) - _OffsetDelta;

            foreach (var value in values)
            {
                offset = Write(BitConverter.GetBytes((ushort)value.Tag), destination, offset);
                offset = Write(BitConverter.GetBytes((ushort)value.DataType), destination, offset);
                offset = Write(BitConverter.GetBytes(GetNumberOfComponents(value)), destination, offset);

                var length = GetLength(value);

                if (length <= 4)
                {
                    WriteValue(value, destination, offset);
                }
                else
                {
                    Write(BitConverter.GetBytes(dataOffset), destination, offset);
                    dataOffset += length;
                }

                offset += 4;
            }

            return offset;
        }

        private static int WriteData(List<IExifValue> values, byte[] destination, int offset)
        {
            foreach (var value in values)
            {
                if (GetLength(value) > 4)
                    offset = WriteValue(value, destination, offset);
            }

            return offset;
        }

        private static int Write(byte[] source, byte[] destination, int offset)
        {
            Buffer.BlockCopy(source, 0, destination, offset, source.Length);

            return offset + source.Length;
        }

        private static int WriteValue(IExifValue exifValue, byte[] destination, int offset)
        {
            if (exifValue.IsArray && exifValue.DataType != ExifDataType.String)
                return WriteArray(exifValue, destination, offset);
            else
                return WriteValue(exifValue.DataType, exifValue.GetValue(), destination, offset);
        }

        private static int WriteArray(IExifValue exifValue, byte[] destination, int offset)
        {
            var value = exifValue.GetValue();

            if (exifValue.DataType == ExifDataType.String)
                return WriteValue(ExifDataType.String, value, destination, offset);

            int newOffset = offset;
            foreach (object obj in (Array)value)
                newOffset = WriteValue(exifValue.DataType, obj, destination, newOffset);

            return newOffset;
        }

        private static int WriteValue(ExifDataType dataType, object value, byte[] destination, int offset)
        {
            switch (dataType)
            {
                case ExifDataType.String:
                    offset = Write(Encoding.UTF8.GetBytes((string)value), destination, offset);
                    destination[offset] = 0;
                    return offset + 1;
                case ExifDataType.Byte:
                case ExifDataType.Undefined:
                    destination[offset] = (byte)value;
                    return offset + 1;
                case ExifDataType.Double:
                    return Write(BitConverter.GetBytes((double)value), destination, offset);
                case ExifDataType.Short:
                    if (value is Number shortNumber)
                        return Write(BitConverter.GetBytes((ushort)shortNumber), destination, offset);
                    else
                        return Write(BitConverter.GetBytes((ushort)value), destination, offset);
                case ExifDataType.Long:
                    if (value is Number longNumber)
                        return Write(BitConverter.GetBytes((uint)longNumber), destination, offset);
                    else
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

        private static int WriteRational(Rational value, byte[] destination, int offset)
        {
            offset = Write(BitConverter.GetBytes(value.Numerator), destination, offset);
            offset = Write(BitConverter.GetBytes(value.Denominator), destination, offset);

            return offset;
        }

        private static int WriteSignedRational(SignedRational value, byte[] destination, int offset)
        {
            offset = Write(BitConverter.GetBytes(value.Numerator), destination, offset);
            offset = Write(BitConverter.GetBytes(value.Denominator), destination, offset);

            return offset;
        }

        private List<IExifValue> GetPartValues(List<IExifValue> values, ExifParts part)
        {
            var result = new List<IExifValue>();

            if (!EnumHelper.HasFlag(_allowedParts, part))
                return result;

            foreach (var value in values)
            {
                if (!HasValue(value))
                    continue;

                if (ExifTags.GetPart(value.Tag) == part)
                    result.Add(value);
            }

            return result;
        }
    }
}