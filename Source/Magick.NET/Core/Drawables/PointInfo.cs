//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.Collections.Generic;
using System.Diagnostics;

namespace ImageMagick
{
  internal sealed partial class PointInfoCollection
  {
    private PointInfoCollection(int count)
    {
      _NativeInstance = new NativePointInfoCollection(count);
      Count = count;
    }

    public PointInfoCollection(IList<PointD> coordinates)
      : this(coordinates.Count)
    {
      for (int i = 0; i < coordinates.Count; i++)
      {
        PointD point = coordinates[i];
        _NativeInstance.Set(i, point.X, point.Y);
      }
    }

    public int Count
    {
      get;
      private set;
    }

    public void Dispose()
    {
      Debug.Assert(_NativeInstance != null);
      _NativeInstance.Dispose();
    }
  }
}