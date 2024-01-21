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
        WritePosition(ifdOffset, stream);

        WriteHeaders(ifdValues, stream, countDelta);

        var exifValuesOffset = 0L;
        if (exifValues.Count > 0)
        {
            WriteHeader(ExifValues.Create(ExifTag.SubIFDOffset)!, stream);
            exifValuesOffset = GetOffsetPositionAndSkipData(stream);
        }

        var gpsValuesOffset = 0L;
        if (gpsValues.Count > 0)
        {
            WriteHeader(ExifValues.Create(ExifTag.GPSIFDOffset)!, stream);
            gpsValuesOffset = GetOffsetPositionAndSkipData(stream);
        }

        var thumbnailOffset = BitConverter.GetBytes(0L);
        Write(thumbnailOffset, stream);

        WriteValues(ifdValues, stream);

        var endOfLink = BitConverter.GetBytes(0L);
        if (exifValues.Count > 0)
        {
            WriteCurrentOffset(exifValuesOffset, stream);
            WriteHeaders(exifValues, stream);
            Write(endOfLink, stream);
            WriteValues(exifValues, stream);
        }

        if (gpsValues.Count > 0)
        {
            WriteCurrentOffset(gpsValuesOffset, stream);
            WriteHeaders(gpsValues, stream);
            Write(endOfLink, stream);
            WriteValues(gpsValues, stream);
        }

        return stream.ToArray();
    }

    private static void Write(byte[] bytes, Stream stream)
        => stream.Write(bytes, 0, bytes.Length);

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

    private static long GetOffsetPositionAndSkipData(Stream stream)
    {
        var position = stream.Position;
        stream.Position += 4;
        return position;
    }

    private static uint PositionToOffset(long offset)
        => (uint)offset - 6;

    private static void WriteHeaders(Collection<IExifValue> values, Stream stream)
        => WriteHeaders(values, stream, 0);

    private static void WriteHeaders(Collection<IExifValue> values, Stream stream, ushort countDelta)
    {
        var count = (ushort)(values.Count + countDelta);

        Write(BitConverter.GetBytes(count), stream);

        var dataOffset = PositionToOffset(stream.Position) + (uint)(count * HeaderSize) + 8;

        foreach (var value in values)
        {
            WriteHeader(value, stream);

            var length = GetLength(value);

            if (length <= 4)
            {
                WriteValue(value, stream);
                stream.Position += 4 - length;
            }
            else
            {
                Write(BitConverter.GetBytes(dataOffset), stream);
                dataOffset += length;
            }
        }
    }

    private static void WriteHeader(IExifValue value, Stream stream)
    {
        Write(BitConverter.GetBytes((ushort)value.Tag), stream);
        Write(BitConverter.GetBytes((ushort)value.DataType), stream);
        Write(BitConverter.GetBytes(GetNumberOfComponents(value)), stream);
    }

    private static void WriteCurrentOffset(long position, Stream stream)
    {
        var currentPosition = stream.Position;
        stream.Position = position;

        WritePosition(currentPosition, stream);

        stream.Position = currentPosition;
    }

    private static void WritePosition(long position, Stream stream)
        => Write(BitConverter.GetBytes(PositionToOffset(position)), stream);

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

    private static void WriteValues(Collection<IExifValue> values, Stream stream)
    {
        foreach (var value in values)
        {
            if (GetLength(value) > 4)
                WriteValue(value, stream);
        }
    }

    private static void WriteValue(IExifValue exifValue, Stream stream)
    {
        if (exifValue.IsArray && exifValue.DataType != ExifDataType.String)
            WriteArray(exifValue, stream);
        else
            WriteValue(exifValue.DataType, exifValue.GetValue(), stream);
    }

    private static void WriteArray(IExifValue exifValue, Stream stream)
    {
        var value = exifValue.GetValue();

        if (exifValue.DataType == ExifDataType.String)
            WriteValue(ExifDataType.String, value, stream);

        foreach (var obj in (Array)value)
            WriteValue(exifValue.DataType, obj, stream);
    }

    private static void WriteValue(ExifDataType dataType, object value, Stream stream)
    {
        switch (dataType)
        {
            case ExifDataType.String:
                Write(Encoding.UTF8.GetBytes((string)value), stream);
                stream.WriteByte(0x00);
                break;
            case ExifDataType.Byte:
            case ExifDataType.Undefined:
                stream.WriteByte((byte)value);
                break;
            case ExifDataType.Double:
                Write(BitConverter.GetBytes((double)value), stream);
                break;
            case ExifDataType.Short:
                if (value is Number shortNumber)
                    Write(BitConverter.GetBytes((ushort)shortNumber), stream);
                else
                    Write(BitConverter.GetBytes((ushort)value), stream);
                break;
            case ExifDataType.Long:
                if (value is Number longNumber)
                    Write(BitConverter.GetBytes((uint)longNumber), stream);
                else
                    Write(BitConverter.GetBytes((uint)value), stream);
                break;
            case ExifDataType.Rational:
                WriteRational((Rational)value, stream);
                break;
            case ExifDataType.SignedByte:
                stream.WriteByte(unchecked((byte)((sbyte)value)));
                break;
            case ExifDataType.SignedLong:
                Write(BitConverter.GetBytes((int)value), stream);
                break;
            case ExifDataType.SignedShort:
                Write(BitConverter.GetBytes((short)value), stream);
                break;
            case ExifDataType.SignedRational:
                WriteSignedRational((SignedRational)value, stream);
                break;
            case ExifDataType.Float:
                Write(BitConverter.GetBytes((float)value), stream);
                break;
            default:
                throw new NotSupportedException();
        }
    }

    private static void WriteRational(Rational value, Stream stream)
    {
        Write(BitConverter.GetBytes(value.Numerator), stream);
        Write(BitConverter.GetBytes(value.Denominator), stream);
    }

    private static void WriteSignedRational(SignedRational value, Stream stream)
    {
        Write(BitConverter.GetBytes(value.Numerator), stream);
        Write(BitConverter.GetBytes(value.Denominator), stream);
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
