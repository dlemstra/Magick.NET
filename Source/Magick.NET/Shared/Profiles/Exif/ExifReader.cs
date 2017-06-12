//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImageMagick
{
    internal sealed class ExifReader
    {
        private delegate TDataType ConverterMethod<TDataType>(byte[] data);

        private byte[] _Data;
        private Collection<ExifTag> _InvalidTags = new Collection<ExifTag>();
        private uint _Index;
        private bool _IsLittleEndian;
        private uint _ExifOffset;
        private uint _GPSOffset;
        private uint _StartIndex;

        private int RemainingLength
        {
            get
            {
                if (_Index >= _Data.Length)
                    return 0;

                return _Data.Length - (int)_Index;
            }
        }

        private void AddValues(Collection<ExifValue> values, uint index)
        {
            _Index = _StartIndex + index;
            ushort count = GetShort();

            for (ushort i = 0; i < count; i++)
            {
                ExifValue value = CreateValue();
                if (value == null)
                    continue;

                bool duplicate = false;
                foreach (ExifValue val in values)
                {
                    if (val.Tag == value.Tag)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                    continue;

                if (value.Tag == ExifTag.SubIFDOffset)
                {
                    if (value.DataType == ExifDataType.Long)
                        _ExifOffset = (uint)value.Value;
                }
                else if (value.Tag == ExifTag.GPSIFDOffset)
                {
                    if (value.DataType == ExifDataType.Long)
                        _GPSOffset = (uint)value.Value;
                }
                else
                    values.Add(value);
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        private object ConvertValue(ExifDataType dataType, byte[] data, uint numberOfComponents)
        {
            if (data == null || data.Length == 0)
                return null;

            switch (dataType)
            {
                case ExifDataType.Unknown:
                    return null;
                case ExifDataType.Ascii:
                    return ToString(data);
                case ExifDataType.Byte:
                    if (numberOfComponents == 1)
                        return ToByte(data);
                    else
                        return data;
                case ExifDataType.DoubleFloat:
                    if (numberOfComponents == 1)
                        return ToDouble(data);
                    else
                        return ToArray(dataType, data, ToDouble);
                case ExifDataType.Long:
                    if (numberOfComponents == 1)
                        return ToLong(data);
                    else
                        return ToArray(dataType, data, ToLong);
                case ExifDataType.Rational:
                    if (numberOfComponents == 1)
                        return ToRational(data);
                    else
                        return ToArray(dataType, data, ToRational);
                case ExifDataType.Short:
                    if (numberOfComponents == 1)
                        return ToShort(data);
                    else
                        return ToArray(dataType, data, ToShort);
                case ExifDataType.SignedByte:
                    if (numberOfComponents == 1)
                        return ToSignedByte(data);
                    else
                        return ToArray(dataType, data, ToSignedByte);
                case ExifDataType.SignedLong:
                    if (numberOfComponents == 1)
                        return ToSignedLong(data);
                    else
                        return ToArray(dataType, data, ToSignedLong);
                case ExifDataType.SignedRational:
                    if (numberOfComponents == 1)
                        return ToSignedRational(data);
                    else
                        return ToArray(dataType, data, ToSignedRational);
                case ExifDataType.SignedShort:
                    if (numberOfComponents == 1)
                        return ToSignedShort(data);
                    else
                        return ToArray(dataType, data, ToSignedShort);
                case ExifDataType.SingleFloat:
                    if (numberOfComponents == 1)
                        return ToSingle(data);
                    else
                        return ToArray(dataType, data, ToSingle);
                case ExifDataType.Undefined:
                    if (numberOfComponents == 1)
                        return ToByte(data);
                    else
                        return data;
                default:
                    throw new NotSupportedException();
            }
        }

        private ExifValue CreateValue()
        {
            if (RemainingLength < 12)
                return null;

            ExifTag tag = (ExifTag)GetShort();
            ExifDataType dataType = EnumHelper.Parse(GetShort(), ExifDataType.Unknown);
            object value = null;

            if (dataType == ExifDataType.Unknown)
                return new ExifValue(tag, dataType, value, false);

            uint numberOfComponents = GetLong();

            if (dataType == ExifDataType.Undefined && numberOfComponents == 0)
                numberOfComponents = 4;

            uint size = numberOfComponents * ExifValue.GetSize(dataType);
            byte[] data = GetBytes(4);

            if (size > 4)
            {
                uint oldIndex = _Index;
                _Index = ToLong(data) + _StartIndex;

                if (RemainingLength < size)
                {
                    _InvalidTags.Add(tag);
                    _Index = oldIndex;
                    return null;
                }

                value = ConvertValue(dataType, GetBytes(size), numberOfComponents);
                _Index = oldIndex;
            }
            else
            {
                value = ConvertValue(dataType, data, numberOfComponents);
            }

            bool isArray = value != null && numberOfComponents > 1;
            return new ExifValue(tag, dataType, value, isArray);
        }

        private byte[] GetBytes(uint length)
        {
            if (_Index + length > (uint)_Data.Length)
                return null;

            byte[] data = new byte[length];
            Array.Copy(_Data, (int)_Index, data, 0, (int)length);
            _Index += length;

            return data;
        }

        private uint GetLong()
        {
            return ToLong(GetBytes(4));
        }

        private ushort GetShort()
        {
            return ToShort(GetBytes(2));
        }

        private string GetString(uint length)
        {
            byte[] data = GetBytes(length);
            if (data == null || data.Length == 0)
                return null;

            return ToString(data);
        }

        private void GetThumbnail(uint offset)
        {
            Collection<ExifValue> values = new Collection<ExifValue>();
            AddValues(values, offset);

            foreach (ExifValue value in values)
            {
                if (value.Tag == ExifTag.JPEGInterchangeFormat && (value.DataType == ExifDataType.Long))
                    ThumbnailOffset = (uint)value.Value + _StartIndex;
                else if (value.Tag == ExifTag.JPEGInterchangeFormatLength && value.DataType == ExifDataType.Long)
                    ThumbnailLength = (uint)value.Value;
            }
        }

        private static TDataType[] ToArray<TDataType>(ExifDataType dataType, Byte[] data, ConverterMethod<TDataType> converter)
        {
            int dataTypeSize = (int)ExifValue.GetSize(dataType);
            int length = data.Length / dataTypeSize;

            TDataType[] result = new TDataType[length];
            byte[] buffer = new byte[dataTypeSize];

            for (int i = 0; i < length; i++)
            {
                Array.Copy(data, i * dataTypeSize, buffer, 0, dataTypeSize);

                result.SetValue(converter(buffer), i);
            }

            return result;
        }

        private static byte ToByte(byte[] data)
        {
            return data[0];
        }

        private double ToDouble(byte[] data)
        {
            if (!ValidateArray(data, 8))
                return default(double);

            return BitConverter.ToDouble(data, 0);
        }

        private uint ToLong(byte[] data)
        {
            if (!ValidateArray(data, 4))
                return default(uint);

            return BitConverter.ToUInt32(data, 0);
        }

        private ushort ToShort(byte[] data)
        {
            if (!ValidateArray(data, 2))
                return default(ushort);

            return BitConverter.ToUInt16(data, 0);
        }

        private float ToSingle(byte[] data)
        {
            if (!ValidateArray(data, 4))
                return default(float);

            return BitConverter.ToSingle(data, 0);
        }

        private static string ToString(byte[] data)
        {
            string result = Encoding.UTF8.GetString(data, 0, data.Length);
            int nullCharIndex = result.IndexOf('\0');
            if (nullCharIndex != -1)
                result = result.Substring(0, nullCharIndex);

            return result;
        }

        private Rational ToRational(byte[] data)
        {
            if (!ValidateArray(data, 8, 4))
                return default(Rational);

            uint numerator = BitConverter.ToUInt32(data, 0);
            uint denominator = BitConverter.ToUInt32(data, 4);

            return new Rational(numerator, denominator, false);
        }

        private sbyte ToSignedByte(byte[] data)
        {
            return unchecked((sbyte)data[0]);
        }

        private int ToSignedLong(byte[] data)
        {
            if (!ValidateArray(data, 4))
                return default(int);

            return BitConverter.ToInt32(data, 0);
        }

        private SignedRational ToSignedRational(byte[] data)
        {
            if (!ValidateArray(data, 8, 4))
                return default(SignedRational);

            int numerator = BitConverter.ToInt32(data, 0);
            int denominator = BitConverter.ToInt32(data, 4);

            return new SignedRational(numerator, denominator, false);
        }

        private short ToSignedShort(byte[] data)
        {
            if (!ValidateArray(data, 2))
                return default(short);

            return BitConverter.ToInt16(data, 0);
        }

        private bool ValidateArray(byte[] data, int size)
        {
            return ValidateArray(data, size, size);
        }

        private bool ValidateArray(byte[] data, int size, int stepSize)
        {
            if (data == null || data.Length < size)
                return false;

            if (_IsLittleEndian == BitConverter.IsLittleEndian)
                return true;

            for (int i = 0; i < data.Length; i += stepSize)
            {
                Array.Reverse(data, i, stepSize);
            }

            return true;
        }

        public uint ThumbnailLength
        {
            get;
            private set;
        }

        public uint ThumbnailOffset
        {
            get;
            private set;
        }

        public Collection<ExifValue> Read(byte[] data)
        {
            Collection<ExifValue> result = new Collection<ExifValue>();

            _Data = data;

            if (GetString(4) == "Exif")
            {
                if (GetShort() != 0)
                    return result;

                _StartIndex = 6;
            }
            else
            {
                _Index = 0;
            }

            _IsLittleEndian = GetString(2) == "II";

            if (GetShort() != 0x002A)
                return result;

            uint ifdOffset = GetLong();
            AddValues(result, ifdOffset);

            uint thumbnailOffset = GetLong();
            GetThumbnail(thumbnailOffset);

            if (_ExifOffset != 0)
                AddValues(result, _ExifOffset);

            if (_GPSOffset != 0)
                AddValues(result, _GPSOffset);

            return result;
        }

        public IEnumerable<ExifTag> InvalidTags
        {
            get
            {
                return _InvalidTags;
            }
        }
    }
}