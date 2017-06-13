//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
    internal sealed class ClipPathReader
    {
        private PointD[] _First;
        private int _Index;
        private int _Height;
        private bool _InSubpath;
        private StringBuilder _Path;
        private int _KnotCount;
        private PointD[] _Last;
        private int _Width;

        private void AddPath(byte[] data)
        {
            if (_KnotCount == 0)
            {
                _Index += 24;
                return;
            }

            PointD[] point = CreatePoint(data);

            if (_InSubpath == false)
            {
                _Path.AppendFormat(CultureInfo.InvariantCulture, "M {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);

                for (int k = 0; k < 3; k++)
                {
                    _First[k] = point[k];
                    _Last[k] = point[k];
                }
            }
            else
            {
                if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y) && (point[0].X == point[1].X) && (point[0].Y == point[1].Y))
                    _Path.AppendFormat(CultureInfo.InvariantCulture, "L {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);
                else
                    _Path.AppendFormat(CultureInfo.InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###}\n", _Last[2].X, _Last[2].Y, point[0].X, point[0].Y, point[1].X, point[1].Y);

                for (int k = 0; k < 3; k++)
                    _Last[k] = point[k];
            }

            _InSubpath = true;
            _KnotCount--;
            if (_KnotCount == 0)
            {
                ClosePath();
                _InSubpath = false;
            }
        }

        private void ClosePath()
        {
            if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y) && (_First[0].X == _First[1].X) && (_First[0].Y == _First[1].Y))
                _Path.AppendFormat(CultureInfo.InvariantCulture, "L {0:0.###} {1:0.###} Z\n", _First[1].X, _First[1].Y);
            else
                _Path.AppendFormat(CultureInfo.InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###} Z\n", _Last[2].X, _Last[2].Y, _First[0].X, _First[0].Y, _First[1].X, _First[1].Y);
        }

        private PointD[] CreatePoint(byte[] data)
        {
            PointD[] result = new PointD[3];

            for (int i = 0; i < 3; i++)
            {
                uint yy = (uint)ByteConverter.ToUInt(data, ref _Index);
                int y = (int)yy;
                if (yy > 2147483647)
                    y = (int)(yy - 4294967295U - 1);

                uint xx = (uint)ByteConverter.ToUInt(data, ref _Index);
                int x = (int)xx;
                if (xx > 2147483647)
                    x = (int)(xx - 4294967295U - 1);

                result[i] = new PointD((double)x * _Width / 4096 / 4096, (double)y * _Height / 4096 / 4096);
            }

            return result;
        }

        private void Reset(int offset)
        {
            _Index = offset;
            _KnotCount = 0;
            _InSubpath = false;
            _Path = new StringBuilder();
            _First = new PointD[3];
            _Last = new PointD[3];
        }

        private void SetKnotCount(byte[] data)
        {
            if (_KnotCount != 0)
            {
                _Index += 24;
                return;
            }

            _KnotCount = ByteConverter.ToShort(data, ref _Index);
            _Index += 22;
        }

        public ClipPathReader(int width, int height)
        {
            _Width = width;
            _Height = height;
        }

        public string Read(byte[] data, int offset, int length)
        {
            Reset(offset);

            while (_Index < offset + length)
            {
                short selector = ByteConverter.ToShort(data, ref _Index);
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
                        _Index += 24;
                        break;
                }
            }

            return _Path.ToString();
        }
    }
}