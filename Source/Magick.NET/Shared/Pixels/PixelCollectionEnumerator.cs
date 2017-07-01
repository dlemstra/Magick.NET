// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal sealed class PixelCollectionEnumerator : IEnumerator<Pixel>
    {
        private QuantumType[] _Row;
        private PixelCollection _Collection;
        private int _Height;
        private int _X;
        private int _Y;
        private int _Width;

        public PixelCollectionEnumerator(PixelCollection collection, int width, int height)
        {
            _Collection = collection;
            _Width = width;
            _Height = height;
            Reset();
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Pixel Current
        {
            get
            {
                if (_X == -1)
                    return null;

                QuantumType[] pixel = new QuantumType[_Collection.Channels];
                Array.Copy(_Row, _X * _Collection.Channels, pixel, 0, _Collection.Channels);

                return Pixel.Create(_Collection, _X, _Y, pixel);
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++_X == _Width)
            {
                _X = 0;
                _Y++;
                SetRow();
            }

            if (_Y < _Height)
                return true;

            _X = _Width - 1;
            _Y = _Height - 1;
            return false;
        }

        public void Reset()
        {
            _X = -1;
            _Y = 0;
            SetRow();
        }

        private void SetRow()
        {
            if (_Y < _Height)
                _Row = _Collection.GetAreaUnchecked(0, _Y, _Width, 1);
        }
    }
}