//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
  internal abstract class DrawableCoordinates<TCoordinateType>
  {
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
    private void CheckCoordinates(int minCount)
    {
      if (Coordinates.Count == 0)
        throw new ArgumentException("Value cannot be empty", "coordinates");

      foreach (TCoordinateType coordinate in Coordinates)
      {
        if (coordinate == null)
          throw new ArgumentNullException("Value should not contain null values", "coordinates");
      }

      if (Coordinates.Count < minCount)
        throw new ArgumentException("Value should contain at least " + minCount + " coordinates.", "coordinates");
    }

    protected DrawableCoordinates(IEnumerable<TCoordinateType> coordinates, int minCount)
    {
      Throw.IfNull("coordinates", coordinates);

      Coordinates = new List<TCoordinateType>(coordinates);
      CheckCoordinates(minCount);
    }

    protected List<TCoordinateType> Coordinates
    {
      get;
      private set;
    }

    public IList<TCoordinateType> ToList()
    {
      return Coordinates;
    }
  }
}