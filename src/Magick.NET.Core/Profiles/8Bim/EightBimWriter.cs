// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageMagick;

internal sealed class EightBimWriter
{
    public static byte[] Write(Collection<IEightBimValue> values)
    {
        if (values.Count == 0)
            return Array.Empty<byte>();

        using var stream = new MemoryStream();
        foreach (var value in values)
        {
            stream.WriteByte((byte)'8');
            stream.WriteByte((byte)'B');
            stream.WriteByte((byte)'I');
            stream.WriteByte((byte)'M');

            stream.WriteBytes(BitConverter.GetBytes(value.Id).Reverse().ToArray());

            var length = 0;
            byte[]? bytes = null;

            if (value.Id > 1999 && value.Id < 2998 && value.Name is not null && value.Name.Length < byte.MaxValue)
            {
                bytes = Encoding.ASCII.GetBytes(value.Name);
                length = bytes.Length;
            }

            stream.WriteByte((byte)length);
            if (bytes is not null)
                stream.WriteBytes(bytes);

            if ((length + 1 & 0x01) != 0)
                stream.WriteByte(0);

            bytes = value.ToByteArray();
            stream.WriteBytes(BitConverter.GetBytes((uint)bytes.Length).Reverse().ToArray());
            if (bytes.Length > 0)
                stream.WriteBytes(bytes);

            if ((bytes.Length & 0x01) != 0)
                stream.WriteByte(0);
        }

        return stream.ToArray();
    }
}
