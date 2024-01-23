// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.ObjectModel;
using System.Text;

namespace ImageMagick;

internal sealed class EightBimReader
{
    private readonly byte[] _data;
    private readonly Collection<IEightBimValue> _values = new();

    public EightBimReader(byte[] data)
        => _data = data;

    public static Collection<IEightBimValue> Read(byte[]? data)
    {
        if (data is null || data.Length == 0)
            return new();

        var reader = new EightBimReader(data);
        reader.Read();

        return reader._values;
    }

    private void Read()
    {
        var i = 0;
        while (i < _data.Length)
        {
            if (_data[i++] != '8')
                continue;
            if (_data[i++] != 'B')
                continue;
            if (_data[i++] != 'I')
                continue;
            if (_data[i++] != 'M')
                continue;

            if (i + 7 > _data.Length)
                return;

            var id = ByteConverter.ToShort(_data, ref i);

            string? name = null;
            int length = _data[i++];
            if (length != 0)
            {
                if (id > 1999 && id < 2998 && i + length < _data.Length)
                    name = Encoding.ASCII.GetString(_data, i, length);

                i += length;
            }

            if ((length + 1 & 0x01) != 0)
                i++;

            length = ByteConverter.ToUInt(_data, ref i);
            if (i + length > _data.Length)
                return;

            if (length < 0)
                return;

            if (length != 0)
            {
                var value = new byte[length];
                Array.Copy(_data, i, value, 0, length);
                _values.Add(new EightBimValue(id, name, value));
            }

            i += length;
            if ((length & 0x01) != 0)
                i++;
        }
    }
}
