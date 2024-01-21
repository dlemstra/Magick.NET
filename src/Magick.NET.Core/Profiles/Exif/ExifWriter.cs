// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ImageMagick;

internal sealed class ExifWriter
{
    private const int HeaderSize = 2 + 2 + 4 + 4;

    private readonly ExifParts _allowedParts;

    public ExifWriter(ExifParts allowedParts)
        => _allowedParts = allowedParts;

    public byte[]? Write(Collection<IExifValue> values)
    {
        var ifdValues = GetPartValues(values, ExifParts.IfdTags);
        var exifValues = GetPartValues(values, ExifParts.ExifTags);
        var gpsValues = GetPartValues(values, ExifParts.GpsTags);

        RemoveOffsetValues(ifdValues, ExifTag.SubIFDOffset, ExifTag.GPSIFDOffset);

        if (ifdValues.Count == 0 && exifValues.Count == 0 && gpsValues.Count == 0)
            return null;

        using var stream = new MemoryStream();
        stream.WriteByte((byte)'E');
        stream.WriteByte((byte)'x');
        stream.WriteByte((byte)'i');
        stream.WriteByte((byte)'f');
        stream.WriteByte(0x00);
        stream.WriteByte(0x00);

        if (BitConverter.IsLittleEndian)
        {
            stream.WriteByte((byte)'I');
            stream.WriteByte((byte)'I');
            stream.WriteByte(0x2A);
            stream.WriteByte(0x00);
        }
        else
        {
            stream.WriteByte((byte)'M');
            stream.WriteByte((byte)'M');
            stream.WriteByte(0x00);
            stream.WriteByte(0x2A);
        }

        ushort countDelta = 0;
        if (exifValues.Count > 0)
            countDelta++;
        if (gpsValues.Count > 0)
            countDelta++;

        var ifdOffset = stream.Position + 4;
        WritePosition(stream, ifdOffset);

        WriteHeaders(stream, ifdValues, countDelta);

        var exifValuesOffset = 0L;
        if (exifValues.Count > 0)
        {
            WriteHeader(stream, ExifValues.Create(ExifTag.SubIFDOffset)!);
            exifValuesOffset = GetOffsetPositionAndSkipData(stream);
        }

        var gpsValuesOffset = 0L;
        if (gpsValues.Count > 0)
        {
            WriteHeader(stream, ExifValues.Create(ExifTag.GPSIFDOffset)!);
            gpsValuesOffset = GetOffsetPositionAndSkipData(stream);
        }

        var thumbnailOffset = BitConverter.GetBytes(0L);
        stream.WriteBytes(thumbnailOffset);

        WriteValues(stream, ifdValues);

        var endOfLink = BitConverter.GetBytes(0L);
        if (exifValues.Count > 0)
        {
            WriteCurrentOffset(stream, exifValuesOffset);
            WriteHeaders(stream, exifValues);
            stream.WriteBytes(endOfLink);
            WriteValues(stream, exifValues);
        }

        if (gpsValues.Count > 0)
        {
            WriteCurrentOffset(stream, gpsValuesOffset);
            WriteHeaders(stream, gpsValues);
            stream.WriteBytes(endOfLink);
            WriteValues(stream, gpsValues);
        }

        return stream.ToArray();
    }

    private static void RemoveOffsetValues(Collection<IExifValue> ifdValues, params ExifTag[] offsetTags)
    {
        for (var i = ifdValues.Count - 1; i >= 0; i--)
        {
            foreach (var offsetTag in offsetTags)
            {
                if (ifdValues[i].Tag == offsetTag)
                    ifdValues.RemoveAt(i);
            }
        }
    }

    private static long GetOffsetPositionAndSkipData(MemoryStream stream)
    {
        var position = stream.Position;
        stream.Position += 4;
        return position;
    }

    private static uint PositionToOffset(long offset)
        => (uint)offset - 6;

    private static void WriteHeaders(MemoryStream stream, Collection<IExifValue> values)
        => WriteHeaders(stream, values, 0);

    private static void WriteHeaders(MemoryStream stream, Collection<IExifValue> values, ushort countDelta)
    {
        var count = (ushort)(values.Count + countDelta);

        stream.WriteBytes(BitConverter.GetBytes(count));

        var dataOffset = PositionToOffset(stream.Position) + (uint)(count * HeaderSize) + 8;

        foreach (var value in values)
        {
            WriteHeader(stream, value);

            var length = GetLength(value);

            if (length <= 4)
            {
                WriteValue(stream, value);
                stream.Position += 4 - length;
            }
            else
            {
                stream.WriteBytes(BitConverter.GetBytes(dataOffset));
                dataOffset += length;
            }
        }
    }

    private static void WriteHeader(MemoryStream stream, IExifValue value)
    {
        stream.WriteBytes(BitConverter.GetBytes((ushort)value.Tag));
        stream.WriteBytes(BitConverter.GetBytes((ushort)value.DataType));
        stream.WriteBytes(BitConverter.GetBytes(GetNumberOfComponents(value)));
    }

    private static void WriteCurrentOffset(MemoryStream stream, long position)
    {
        var currentPosition = stream.Position;
        stream.Position = position;

        WritePosition(stream, currentPosition);

        stream.Position = currentPosition;
    }

    private static void WritePosition(MemoryStream stream, long position)
        => stream.WriteBytes(BitConverter.GetBytes(PositionToOffset(position)));

    private static uint GetLength(IExifValue value)
        => GetNumberOfComponents(value) * ExifDataTypes.GetSize(value.DataType);

    private static uint GetNumberOfComponents(IExifValue exifValue)
    {
        var value = exifValue.GetValue();

        if (exifValue.DataType == ExifDataType.String)
            return (uint)Encoding.UTF8.GetBytes((string)value).Length + 1;

        if (value is Array arrayValue)
            return (uint)arrayValue.Length;

        return 1;
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

    private static void WriteValues(MemoryStream stream, Collection<IExifValue> values)
    {
        foreach (var value in values)
        {
            if (GetLength(value) > 4)
                WriteValue(stream, value);
        }
    }

    private static void WriteValue(MemoryStream stream, IExifValue exifValue)
    {
        if (exifValue.IsArray && exifValue.DataType != ExifDataType.String)
            WriteArray(stream, exifValue);
        else
            WriteValue(stream, exifValue.DataType, exifValue.GetValue());
    }

    private static void WriteArray(MemoryStream stream, IExifValue exifValue)
    {
        var value = exifValue.GetValue();

        if (exifValue.DataType == ExifDataType.String)
            WriteValue(stream, ExifDataType.String, value);

        foreach (var obj in (Array)value)
            WriteValue(stream, exifValue.DataType, obj);
    }

    private static void WriteValue(MemoryStream stream, ExifDataType dataType, object value)
    {
        switch (dataType)
        {
            case ExifDataType.String:
                stream.WriteBytes(Encoding.UTF8.GetBytes((string)value));
                stream.WriteByte(0x00);
                break;
            case ExifDataType.Byte:
            case ExifDataType.Undefined:
                stream.WriteByte((byte)value);
                break;
            case ExifDataType.Double:
                stream.WriteBytes(BitConverter.GetBytes((double)value));
                break;
            case ExifDataType.Short:
                if (value is Number shortNumber)
                    stream.WriteBytes(BitConverter.GetBytes((ushort)shortNumber));
                else
                    stream.WriteBytes(BitConverter.GetBytes((ushort)value));
                break;
            case ExifDataType.Long:
                if (value is Number longNumber)
                    stream.WriteBytes(BitConverter.GetBytes((uint)longNumber));
                else
                    stream.WriteBytes(BitConverter.GetBytes((uint)value));
                break;
            case ExifDataType.Rational:
                WriteRational(stream, (Rational)value);
                break;
            case ExifDataType.SignedByte:
                stream.WriteByte(unchecked((byte)((sbyte)value)));
                break;
            case ExifDataType.SignedLong:
                stream.WriteBytes(BitConverter.GetBytes((int)value));
                break;
            case ExifDataType.SignedShort:
                stream.WriteBytes(BitConverter.GetBytes((short)value));
                break;
            case ExifDataType.SignedRational:
                WriteSignedRational(stream, (SignedRational)value);
                break;
            case ExifDataType.Float:
                stream.WriteBytes(BitConverter.GetBytes((float)value));
                break;
            default:
                throw new NotSupportedException();
        }
    }

    private static void WriteRational(MemoryStream stream, Rational value)
    {
        stream.WriteBytes(BitConverter.GetBytes(value.Numerator));
        stream.WriteBytes(BitConverter.GetBytes(value.Denominator));
    }

    private static void WriteSignedRational(MemoryStream stream, SignedRational value)
    {
        stream.WriteBytes(BitConverter.GetBytes(value.Numerator));
        stream.WriteBytes(BitConverter.GetBytes(value.Denominator));
    }

    private Collection<IExifValue> GetPartValues(Collection<IExifValue> values, ExifParts part)
    {
        var result = new Collection<IExifValue>();

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
