// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.ObjectModel;

namespace ImageMagick;

internal sealed class ExifReader
{
    private readonly ExifData _data = new ExifData();
    private readonly EndianReader _reader;

    private uint _exifOffset;
    private uint _gpsOffset;
    private uint _startIndex;

    private ExifReader(byte[] data)
    {
        _reader = new EndianReader(data);
    }

    private delegate TDataType ReadMethod<TDataType>();

    public static ExifData Read(byte[] data)
    {
        if (data is null || data.Length == 0)
            return new ExifData();

        var reader = new ExifReader(data);
        reader.Read();

        return reader._data;
    }

    private static TDataType[] ReadArray<TDataType>(uint numberOfComponents, ReadMethod<TDataType> read)
    {
        var result = new TDataType[numberOfComponents];

        for (var i = 0; i < numberOfComponents; i++)
        {
            result.SetValue(read(), i);
        }

        return result;
    }

    private void Read()
    {
        // Exif
        if (_reader.ReadLong() == 0x45786966)
        {
            if (_reader.ReadShort() != 0)
                return;

            _startIndex = 6;
        }

        // II
        _reader.IsLittleEndian = _reader.ReadShort() == 0x4949;

        if (ReadShort() != 0x002A)
            return;

        var ifdOffset = ReadLong();
        AddValues(_data.Values, ifdOffset);

        var thumbnailOffset = ReadLong();
        if (thumbnailOffset != 0)
            ReadThumbnail(thumbnailOffset);

        if (_exifOffset != 0)
            AddValues(_data.Values, _exifOffset);

        if (_gpsOffset != 0)
            AddValues(_data.Values, _gpsOffset);
    }

    private void AddValues(Collection<IExifValue> values, uint index)
    {
        _reader.Seek(_startIndex + index);
        var count = ReadShort();

        for (ushort i = 0; i < count; i++)
        {
            var value = CreateValue();
            if (value is null)
                continue;

            var duplicate = false;
            foreach (var val in values)
            {
                if (val == value)
                {
                    duplicate = true;
                    break;
                }
            }

            if (duplicate)
                continue;

            if (value == ExifTag.SubIFDOffset)
                _exifOffset = ((ExifLong)value).Value;
            else if (value == ExifTag.GPSIFDOffset)
                _gpsOffset = ((ExifLong)value).Value;
            else
                values.Add(value);
        }
    }

    private ExifValue? CreateValue()
    {
        if (!_reader.CanRead(12))
            return null;

        var tag = (ExifTagValue)ReadShort();
        var dataType = EnumHelper.Parse(ReadShort(), ExifDataType.Unknown);
        ExifValue? value = null;

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

            if (_reader.Seek(newIndex) && _reader.CanRead(length))
                value = CreateValue(tag, dataType, numberOfComponents);
        }

        if (value is null)
            _data.InvalidTags.Add(new UnkownExifTag(tag));

        _reader.Seek(oldIndex + 4);

        return value;
    }

    private ExifValue? CreateValue(ExifTagValue tag, ExifDataType dataType, uint numberOfComponents)
    {
        if (!_reader.CanRead(numberOfComponents))
            return null;

        var exifValue = ExifValues.Create(tag);
        if (exifValue is null)
            exifValue = ExifValues.Create(tag, dataType, numberOfComponents);

        if (exifValue is null)
            return null;

        var value = ReadValue(dataType, numberOfComponents);
        if (!exifValue.SetValue(value))
            return null;

        return exifValue;
    }

    private object ReadValue(ExifDataType dataType, uint numberOfComponents)
    {
        switch (dataType)
        {
            case ExifDataType.Byte:
            case ExifDataType.Undefined:
                if (numberOfComponents == 1)
                    return ReadByte();
                else
                    return ReadArray(numberOfComponents, ReadByte);

            case ExifDataType.Double:
                if (numberOfComponents == 1)
                    return ReadDouble();
                else
                    return ReadArray(numberOfComponents, ReadDouble);

            case ExifDataType.Float:

                if (numberOfComponents == 1)
                    return ReadFloat();
                else
                    return ReadArray(numberOfComponents, ReadFloat);

            case ExifDataType.Long:
                if (numberOfComponents == 1)
                    return ReadLong();
                else
                    return ReadArray(numberOfComponents, ReadLong);

            case ExifDataType.Rational:
                if (numberOfComponents == 1)
                    return ReadRational();
                else
                    return ReadArray(numberOfComponents, ReadRational);

            case ExifDataType.Short:
                if (numberOfComponents == 1)
                    return ReadShort();
                else
                    return ReadArray(numberOfComponents, ReadShort);

            case ExifDataType.SignedByte:
                if (numberOfComponents == 1)
                    return ReadSignedByte();
                else
                    return ReadArray(numberOfComponents, ReadSignedByte);

            case ExifDataType.SignedLong:
                if (numberOfComponents == 1)
                    return ReadSignedLong();
                else
                    return ReadArray(numberOfComponents, ReadSignedLong);

            case ExifDataType.SignedRational:
                if (numberOfComponents == 1)
                    return ReadSignedRational();
                else
                    return ReadArray(numberOfComponents, ReadSignedRational);

            case ExifDataType.SignedShort:
                if (numberOfComponents == 1)
                    return ReadSignedShort();
                else
                    return ReadArray(numberOfComponents, ReadSignedShort);

            case ExifDataType.String:
                return ReadString(numberOfComponents);

            default:
                throw new NotSupportedException();
        }
    }

    private byte ReadByte()
        => _reader.ReadByte() ?? 0;

    private double ReadDouble()
        => _reader.ReadDouble() ?? 0;

    private float ReadFloat()
        => _reader.ReadFloat() ?? 0;

    private uint ReadLong()
        => _reader.ReadLong() ?? 0;

    private ushort ReadShort()
        => _reader.ReadShort() ?? 0;

    private string ReadString(uint length)
        => _reader.ReadString(length) ?? string.Empty;

    private Rational ReadRational()
    {
        var numerator = _reader.ReadLong();
        if (numerator is null)
            return default;

        var denominator = _reader.ReadLong();
        if (denominator is null)
            return default;

        return new Rational(numerator.Value, denominator.Value, false);
    }

    private unsafe SignedRational ReadSignedRational()
    {
        var numerator = _reader.ReadLong();
        if (numerator is null)
            return default;

        var denominator = _reader.ReadLong();
        if (denominator is null)
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
            if (value.Equals(ExifTag.JPEGInterchangeFormat))
                _data.ThumbnailOffset = ((ExifLong)value).Value + _startIndex;
            else if (value.Equals(ExifTag.JPEGInterchangeFormatLength))
                _data.ThumbnailLength = ((ExifLong)value).Value;
        }
    }
}
