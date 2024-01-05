// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal sealed class ColorProfileReader
{
    private readonly ColorProfileData _data = new ColorProfileData();
    private readonly EndianReader _reader;

    private ColorProfileReader(byte[] data)
    {
        _reader = new EndianReader(data);
    }

    public static ColorProfileData Read(byte[]? data)
    {
        if (data is null)
            return new ColorProfileData();

        var reader = new ColorProfileReader(data);
        reader.ReadColorSpace();
        reader.ReadTagTable();

        return reader._data;
    }

    private static ColorSpace DetermineColorSpace(string colorSpace)
    {
        switch (colorSpace)
        {
            case "CMY":
                return ColorSpace.CMY;
            case "CMYK":
                return ColorSpace.CMYK;
            case "GRAY":
                return ColorSpace.Gray;
            case "HSL":
                return ColorSpace.HSL;
            case "HSV":
                return ColorSpace.HSV;
            case "Lab":
                return ColorSpace.Lab;
            case "Luv":
                return ColorSpace.YUV;
            case "RGB":
                return ColorSpace.sRGB;
            case "XYZ":
                return ColorSpace.XYZ;
            case "YCbr":
                return ColorSpace.YCbCr;
            default:
                throw new NotSupportedException(colorSpace);
        }
    }

    private void ReadColorSpace()
    {
        _reader.Seek(16);

        var colorSpace = _reader.ReadString(4);
        if (colorSpace is null)
            return;

        _data.ColorSpace = DetermineColorSpace(colorSpace.TrimEnd());
    }

    private void ReadTagTable()
    {
        if (!_reader.Seek(128))
            return;

        var count = _reader.ReadLong();
        for (var i = 0; i < count; i++)
        {
            var tag = _reader.ReadLong();
            switch (tag)
            {
                case 0x63707274:
                    _data.Copyright = ReadTag();
                    break;
                case 0x64657363:
                    _data.Description = ReadTag();
                    break;
                case 0x646D6E64:
                    _data.Manufacturer = ReadTag();
                    break;
                case 0x646D6464:
                    _data.Model = ReadTag();
                    break;
                default:
                    _reader.Skip(8);
                    break;
            }
        }
    }

    private string? ReadTag()
    {
        var offset = _reader.ReadLong();
        var length = _reader.ReadLong();

        if (offset is null || length is null)
            return null;

        var originalIndex = _reader.Index;

        if (!_reader.Seek(offset.Value))
            return null;

        var value = ReadTagValue(length.Value);

        _reader.Seek(originalIndex);

        return value;
    }

    private string? ReadTagValue(uint length)
        => _reader.ReadString(4) switch
        {
            "desc" => ReadTextDescriptionTypeValue(),
            "text" => ReadTextTypeValue(length),
            _ => null,
        };

    private string? ReadTextDescriptionTypeValue()
    {
        if (!_reader.Skip(4))
            return null;

        var length = _reader.ReadLong();
        if (length is null)
            return null;

        return _reader.ReadString(length.Value);
    }

    private string? ReadTextTypeValue(uint length)
    {
        if (!_reader.Skip(4))
            return null;

        return _reader.ReadString(length);
    }
}
