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

namespace ImageMagick
{
    internal sealed class ExifReader
    {
        private readonly Collection<ExifTagValue> _invalidTags = new Collection<ExifTagValue>();

        private EndianReader _reader;
        private bool _isLittleEndian;
        private uint _exifOffset;
        private uint _gpsOffset;
        private uint _startIndex = 0;

        private delegate TDataType ReadMethod<TDataType>();

        public uint ThumbnailLength { get; private set; }

        public uint ThumbnailOffset { get; private set; }

        public IEnumerable<ExifTagValue> InvalidTags => _invalidTags;

        public Collection<IExifValue> Read(byte[] data)
        {
            var result = new Collection<IExifValue>();

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

        private static TDataType[] ReadArray<TDataType>(uint numberOfComponents, ReadMethod<TDataType> read)
        {
            var result = new TDataType[numberOfComponents];

            for (int i = 0; i < numberOfComponents; i++)
            {
                result.SetValue(read(), i);
            }

            return result;
        }

        private static bool IsLong(IExifValue value) => value.DataType == ExifDataType.Long && !value.IsArray;

        private void AddValues(Collection<IExifValue> values, uint index)
        {
            _reader.Seek(_startIndex + index);
            var count = ReadShort();

            for (ushort i = 0; i < count; i++)
            {
                var value = CreateValue();
                if (value == null)
                    continue;

                var duplicate = false;
                foreach (var val in values)
                {
                    if (val.Tag == value.Tag)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                    continue;

                if (value.Tag == ExifTagValue.SubIFDOffset)
                {
                    if (IsLong(value))
                        _exifOffset = (uint)value.Value;
                }
                else if (value.Tag == ExifTagValue.GPSIFDOffset)
                {
                    if (IsLong(value))
                        _gpsOffset = (uint)value.Value;
                }
                else
                    values.Add(value);
            }
        }

        private IExifValue CreateValue()
        {
            if (!_reader.CanRead(12))
                return null;

            var tag = (ExifTagValue)ReadShort();
            var dataType = EnumHelper.Parse(ReadShort(), ExifDataType.Unknown);
            IExifValue value = null;

            if (dataType == ExifDataType.Unknown)
                return null;

            var numberOfComponents = ReadLong();

            if (dataType == ExifDataType.Undefined && numberOfComponents == 0)
                numberOfComponents = 4;

            var oldIndex = _reader.Index;
            var length = numberOfComponents * ExifDataTypes.GetSize(dataType);

            if (length <= 4)
            {
                value = CreateValue(tag, dataType, numberOfComponents);
            }
            else
            {
                var newIndex = _startIndex + ReadLong();

                if (_reader.Seek(newIndex))
                {
                    if (_reader.CanRead(length))
                    {
                        value = CreateValue(tag, dataType, numberOfComponents);
                    }
                }

                if (value == null)
                {
                    _invalidTags.Add(tag);
                }
            }

            _reader.Seek(oldIndex + 4);

            return value;
        }

        private IExifValue CreateValue(ExifTagValue tag, ExifDataType dataType, uint numberOfComponents)
        {
            switch (dataType)
            {
                case ExifDataType.Byte:
                case ExifDataType.Undefined:
                    if (numberOfComponents == 1)
                        return ExifByte.Create(tag, dataType, ReadByte());
                    else
                        return ExifByteArray.Create(tag, dataType, ReadArray(numberOfComponents, ReadByte));

                case ExifDataType.Double:
                    if (numberOfComponents == 1)
                        return ExifDouble.Create(tag, ReadDouble());
                    else
                        return ExifDoubleArray.Create(tag, ReadArray(numberOfComponents, ReadDouble));

                case ExifDataType.Float:

                    if (numberOfComponents == 1)
                        return ExifFloat.Create(tag, ReadFloat());
                    else
                        return ExifFloatArray.Create(tag, ReadArray(numberOfComponents, ReadFloat));

                case ExifDataType.Long:
                    if (numberOfComponents == 1)
                        return ExifLong.Create(tag, ReadLong());
                    else
                        return ExifLongArray.Create(tag, ReadArray(numberOfComponents, ReadLong));

                case ExifDataType.Rational:
                    if (numberOfComponents == 1)
                        return ExifRational.Create(tag, ReadRational());
                    else
                        return ExifRationalArray.Create(tag, ReadArray(numberOfComponents, ReadRational));

                case ExifDataType.Short:
                    if (numberOfComponents == 1)
                        return ExifShort.Create(tag, ReadShort());
                    else
                        return ExifShortArray.Create(tag, ReadArray(numberOfComponents, ReadShort));

                case ExifDataType.SignedByte:
                    if (numberOfComponents == 1)
                        return ExifSignedByte.Create(tag, ReadSignedByte());
                    else
                        return ExifSignedByteArray.Create(tag, ReadArray(numberOfComponents, ReadSignedByte));

                case ExifDataType.SignedLong:
                    if (numberOfComponents == 1)
                        return ExifSignedLong.Create(tag, ReadSignedLong());
                    else
                        return ExifSignedLongArray.Create(tag, ReadArray(numberOfComponents, ReadSignedLong));

                case ExifDataType.SignedRational:
                    if (numberOfComponents == 1)
                        return ExifSignedRational.Create(tag, ReadSignedRational());
                    else
                        return ExifSignedRationalArray.Create(tag, ReadArray(numberOfComponents, ReadSignedRational));

                case ExifDataType.SignedShort:
                    if (numberOfComponents == 1)
                        return ExifSignedShort.Create(tag, ReadSignedShort());
                    else
                        return ExifSignedShortArray.Create(tag, ReadArray(numberOfComponents, ReadSignedShort));

                case ExifDataType.String:
                    return ExifString.Create(tag, ReadString(numberOfComponents));

                default:
                    throw new NotSupportedException();
            }
        }

        private byte ReadByte() => _reader.ReadByte() ?? 0;

        private double ReadDouble() => (_isLittleEndian ? _reader.ReadDoubleLSB() : _reader.ReadDoubleMSB()) ?? 0;

        private float ReadFloat() => (_isLittleEndian ? _reader.ReadFloatLSB() : _reader.ReadFloatMSB()) ?? 0;

        private uint ReadLong() => (_isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB()) ?? 0;

        private ushort ReadShort() => (_isLittleEndian ? _reader.ReadShortLSB() : _reader.ReadShortMSB()) ?? 0;

        private string ReadString(uint length) => _isLittleEndian ? _reader.ReadString(length) : _reader.ReadString(length);

        private Rational ReadRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default;

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default;

            return new Rational(numerator.Value, denominator.Value, false);
        }

        private unsafe SignedRational ReadSignedRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default;

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default;

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
            var values = new Collection<IExifValue>();
            AddValues(values, offset);

            foreach (var value in values)
            {
                if (value.Tag == ExifTagValue.JPEGInterchangeFormat && IsLong(value))
                    ThumbnailOffset = (uint)value.Value + _startIndex;
                else if (value.Tag == ExifTagValue.JPEGInterchangeFormatLength && IsLong(value))
                    ThumbnailLength = (uint)value.Value;
            }
        }
    }
}