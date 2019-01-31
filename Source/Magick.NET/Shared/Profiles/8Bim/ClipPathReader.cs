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

using System.Globalization;
using System.Text;

namespace ImageMagick
{
    internal sealed class ClipPathReader
    {
        private readonly int _height;
        private readonly int _width;

        private PointD[] _first;
        private int _index;
        private bool _inSubpath;
        private StringBuilder _path;
        private int _knotCount;
        private PointD[] _last;

        public ClipPathReader(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public string Read(byte[] data, int offset, int length)
        {
            Reset(offset);

            while (_index < offset + length)
            {
                short selector = ByteConverter.ToShort(data, ref _index);
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

            PointD[] point = CreatePoint(data);

            if (_inSubpath == false)
            {
                _path.AppendFormat(CultureInfo.InvariantCulture, "M {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);

                for (int k = 0; k < 3; k++)
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

                for (int k = 0; k < 3; k++)
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
            PointD[] result = new PointD[3];

            for (int i = 0; i < 3; i++)
            {
                uint yy = (uint)ByteConverter.ToUInt(data, ref _index);
                int y = (int)yy;
                if (yy > 2147483647)
                    y = (int)(yy - 4294967295U - 1);

                uint xx = (uint)ByteConverter.ToUInt(data, ref _index);
                int x = (int)xx;
                if (xx > 2147483647)
                    x = (int)(xx - 4294967295U - 1);

                result[i] = new PointD((double)x * _width / 4096 / 4096, (double)y * _height / 4096 / 4096);
            }

            return result;
        }

        private void Reset(int offset)
        {
            _index = offset;
            _knotCount = 0;
            _inSubpath = false;
            _path = new StringBuilder();
            _first = new PointD[3];
            _last = new PointD[3];
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
}