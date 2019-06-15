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
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    internal sealed class ExifReader
    {
        private readonly Collection<ExifTag> _invalidTags = new Collection<ExifTag>();

        private EndianReader _reader;
        private bool _isLittleEndian;
        private uint _exifOffset;
        private uint _gpsOffset;
        private uint _startIndex = 0;

        private delegate TDataType ReadMethod<TDataType>();

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

        public IEnumerable<ExifTag> InvalidTags
        {
            get
            {
                return _invalidTags;
            }
        }

        public Collection<ExifValue> Read(byte[] data)
        {
            var result = new Collection<ExifValue>();

            if (data == null || data.Length == 0)
                return result;

            _reader = new EndianReader(data);

            if (_reader.ReadString(4) == "Exif")
            {
                if (_reader.ReadShortMSB() != 0)
                    return result;

                _startIndex = 6;
            }

            _isLittleEndian = _reader.ReadString(2) == "II";

            if (ReadShort() != 0x002A)
                return result;

            var ifdOffset = ReadLong();
            AddValues(result, ifdOffset);

            var thumbnailOffset = ReadLong();
            ReadThumbnail(thumbnailOffset);

            if (_exifOffset != 0)
                AddValues(result, _exifOffset);

            if (_gpsOffset != 0)
                AddValues(result, _gpsOffset);

            return result;
        }

        private static TDataType[] ToArray<TDataType>(ExifDataType dataType, uint length, ReadMethod<TDataType> read)
        {
            var dataTypeSize = (int)ExifDataTypes.GetSize(dataType);
            var arrayLength = (int)length / dataTypeSize;

            var result = new TDataType[arrayLength];

            for (int i = 0; i < arrayLength; i++)
            {
                result.SetValue(read(), i);
            }

            return result;
        }

        private void AddValues(Collection<ExifValue> values, uint index)
        {
            _reader.Seek(_startIndex + index);
            var count = ReadShort();

            for (ushort i = 0; i < count; i++)
            {
                var value = CreateValue();
                if (value == null)
                    continue;

                var duplicate = false;
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
                        _exifOffset = (uint)value.Value;
                }
                else if (value.Tag == ExifTag.GPSIFDOffset)
                {
                    if (value.DataType == ExifDataType.Long)
                        _gpsOffset = (uint)value.Value;
                }
                else
                    values.Add(value);
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        private object ConvertValue(ExifDataType dataType, uint length, uint numberOfComponents)
        {
            if (length == 0)
                return null;

            switch (dataType)
            {
                case ExifDataType.Unknown:
                    return null;
                case ExifDataType.Ascii:
                    return ReadString(length);
                case ExifDataType.Undefined:
                case ExifDataType.Byte:
                    if (numberOfComponents == 1)
                        return ReadByte();
                    else
                        return ToArray(dataType, length, ReadByte);
                case ExifDataType.Double:
                    if (numberOfComponents == 1)
                        return ReadDouble();
                    else
                        return ToArray(dataType, length, ReadDouble);
                case ExifDataType.Long:
                    if (numberOfComponents == 1)
                        return ReadLong();
                    else
                        return ToArray(dataType, length, ReadLong);
                case ExifDataType.Rational:
                    if (numberOfComponents == 1)
                        return ReadRational();
                    else
                        return ToArray(dataType, length, ReadRational);
                case ExifDataType.Short:
                    if (numberOfComponents == 1)
                        return ReadShort();
                    else
                        return ToArray(dataType, length, ReadShort);
                case ExifDataType.SignedByte:
                    if (numberOfComponents == 1)
                        return ReadSignedByte();
                    else
                        return ToArray(dataType, length, ReadSignedByte);
                case ExifDataType.SignedLong:
                    if (numberOfComponents == 1)
                        return ReadSignedLong();
                    else
                        return ToArray(dataType, length, ReadSignedLong);
                case ExifDataType.SignedRational:
                    if (numberOfComponents == 1)
                        return ReadSignedRational();
                    else
                        return ToArray(dataType, length, ReadSignedRational);
                case ExifDataType.SignedShort:
                    if (numberOfComponents == 1)
                        return ReadSignedShort();
                    else
                        return ToArray(dataType, length, ReadSignedShort);
                case ExifDataType.Float:
                    if (numberOfComponents == 1)
                        return ReadSingle();
                    else
                        return ToArray(dataType, length, ReadSingle);
                default:
                    throw new NotSupportedException();
            }
        }

        private ExifValue CreateValue()
        {
            if (!_reader.CanRead(12))
                return null;

            var tag = (ExifTag)ReadShort();
            var dataType = EnumHelper.Parse(ReadShort(), ExifDataType.Unknown);
            object value = null;

            if (dataType == ExifDataType.Unknown)
                return new ExifValue(tag, dataType, value, false);

            var numberOfComponents = ReadLong();

            if (dataType == ExifDataType.Undefined && numberOfComponents == 0)
                numberOfComponents = 4;

            var oldIndex = _reader.Index;
            var length = numberOfComponents * ExifDataTypes.GetSize(dataType);

            if (length <= 4)
            {
                value = ConvertValue(dataType, length, numberOfComponents);
            }
            else
            {
                var newIndex = _startIndex + ReadLong();

                if (_reader.Seek(newIndex))
                {
                    if (_reader.CanRead(length))
                    {
                        value = ConvertValue(dataType, length, numberOfComponents);
                    }
                }

                if (value == null)
                {
                    _invalidTags.Add(tag);
                    _reader.Seek(oldIndex + 4);
                    return null;
                }
            }

            _reader.Seek(oldIndex + 4);

            var isArray = value != null && numberOfComponents != 1;
            return new ExifValue(tag, dataType, value, isArray);
        }

        private byte ReadByte() => _reader.ReadByte() ?? 0;

        private double ReadDouble() => (_isLittleEndian ? _reader.ReadDoubleLSB() : _reader.ReadDoubleMSB()) ?? 0;

        private uint ReadLong() => (_isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB()) ?? 0;

        private ushort ReadShort() => (_isLittleEndian ? _reader.ReadShortLSB() : _reader.ReadShortMSB()) ?? 0;

        private float ReadSingle() => (_isLittleEndian ? _reader.ReadSingleLSB() : _reader.ReadSingleMSB()) ?? 0;

        private string ReadString(uint length) => _isLittleEndian ? _reader.ReadString(length) : _reader.ReadString(length);

        private Rational ReadRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default(Rational);

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default(Rational);

            return new Rational(numerator.Value, denominator.Value, false);
        }

        private unsafe SignedRational ReadSignedRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default(SignedRational);

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default(SignedRational);

            var num = numerator.Value;
            var dem = denominator.Value;

            return new SignedRational(*(int*)&num, *(int*)&dem, false);
        }

        private unsafe sbyte ReadSignedByte()
        {
            var result = ReadByte();
            return *(sbyte*)&result;
        }

        private unsafe int ReadSignedLong()
        {
            var result = ReadLong();
            return *(int*)&result;
        }

        private unsafe short ReadSignedShort()
        {
            var result = ReadShort();
            return *(short*)&result;
        }

        private void ReadThumbnail(uint offset)
        {
            var values = new Collection<ExifValue>();
            AddValues(values, offset);

            foreach (ExifValue value in values)
            {
                if (value.Tag == ExifTag.JPEGInterchangeFormat && (value.DataType == ExifDataType.Long))
                    ThumbnailOffset = (uint)value.Value + _startIndex;
                else if (value.Tag == ExifTag.JPEGInterchangeFormatLength && value.DataType == ExifDataType.Long)
                    ThumbnailLength = (uint)value.Value;
            }
        }
    }
}