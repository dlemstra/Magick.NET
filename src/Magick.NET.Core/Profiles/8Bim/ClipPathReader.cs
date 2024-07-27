// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;
using System.Text;

namespace ImageMagick;

internal sealed class ClipPathReader
{
    private readonly uint _height;
    private readonly uint _width;
    private readonly PointD[] _first = new PointD[3];
    private readonly PointD[] _last = new PointD[3];
    private readonly StringBuilder _path = new StringBuilder();

    private int _index;
    private bool _inSubpath = false;
    private int _knotCount = 0;

    private ClipPathReader(uint width, uint height)
    {
        _width = width;
        _height = height;

        _index = 0;
    }

    public static string Read(uint width, uint height, byte[] data)
    {
        var reader = new ClipPathReader(width, height);
        return reader.Read(data);
    }

    private string Read(byte[] data)
    {
        while (_index < data.Length)
        {
            var selector = ByteConverter.ToShort(data, ref _index);
            switch (selector)
            {
                case 0:
                case 3:
                    SetKnotCount(data);
                    break;
                case 1:
                case 2:
                case 4:
                case 5:
                    AddPath(data);
                    break;
                case 6:
                case 7:
                case 8:
                default:
                    _index += 24;
                    break;
            }
        }

        return _path.ToString();
    }

    private void AddPath(byte[] data)
    {
        if (_knotCount == 0)
        {
            _index += 24;
            return;
        }

        var point = CreatePoint(data);

        if (_inSubpath == false)
        {
            _path.AppendFormat(CultureInfo.InvariantCulture, "M {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);

            for (var k = 0; k < 3; k++)
            {
                _first[k] = point[k];
                _last[k] = point[k];
            }
        }
        else
        {
            if ((_last[1].X == _last[2].X) && (_last[1].Y == _last[2].Y) && (point[0].X == point[1].X) && (point[0].Y == point[1].Y))
                _path.AppendFormat(CultureInfo.InvariantCulture, "L {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);
            else
                _path.AppendFormat(CultureInfo.InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###}\n", _last[2].X, _last[2].Y, point[0].X, point[0].Y, point[1].X, point[1].Y);

            for (var k = 0; k < 3; k++)
                _last[k] = point[k];
        }

        _inSubpath = true;
        _knotCount--;
        if (_knotCount == 0)
        {
            ClosePath();
            _inSubpath = false;
        }
    }

    private void ClosePath()
    {
        if ((_last[1].X == _last[2].X) && (_last[1].Y == _last[2].Y) && (_first[0].X == _first[1].X) && (_first[0].Y == _first[1].Y))
            _path.AppendFormat(CultureInfo.InvariantCulture, "L {0:0.###} {1:0.###} Z\n", _first[1].X, _first[1].Y);
        else
            _path.AppendFormat(CultureInfo.InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###} Z\n", _last[2].X, _last[2].Y, _first[0].X, _first[0].Y, _first[1].X, _first[1].Y);
    }

    private PointD[] CreatePoint(byte[] data)
    {
        var result = new PointD[3];

        for (var i = 0; i < 3; i++)
        {
            var yy = (uint)ByteConverter.ToUInt(data, ref _index);
            var y = (int)yy;
            if (yy > 2147483647)
                y = (int)(yy - 4294967295U - 1);

            var xx = (uint)ByteConverter.ToUInt(data, ref _index);
            var x = (int)xx;
            if (xx > 2147483647)
                x = (int)(xx - 4294967295U - 1);

            result[i] = new PointD((double)x * _width / 4096 / 4096, (double)y * _height / 4096 / 4096);
        }

        return result;
    }

    private void SetKnotCount(byte[] data)
    {
        if (_knotCount != 0)
        {
            _index += 24;
            return;
        }

        _knotCount = ByteConverter.ToShort(data, ref _index);
        _index += 22;
    }
}
