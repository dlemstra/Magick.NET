// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    internal sealed partial class PointInfoCollection : INativeInstance
    {
        public PointInfoCollection(IList<PointD> coordinates)
          : this(coordinates.Count)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                var point = coordinates[i];
                _nativeInstance.Set(i, point.X, point.Y);
            }
        }

        private PointInfoCollection(int count)
        {
            _nativeInstance = new NativePointInfoCollection(count);
            Count = count;
        }

        public int Count { get; private set; }

        IntPtr INativeInstance.Instance => _nativeInstance.Instance;

        public void Dispose()
        {
            DebugThrow.IfNull(_nativeInstance);
            _nativeInstance.Dispose();
        }

        public double GetX(int index) => _nativeInstance.GetX(index);

        public double GetY(int index) => _nativeInstance.GetY(index);
    }
}