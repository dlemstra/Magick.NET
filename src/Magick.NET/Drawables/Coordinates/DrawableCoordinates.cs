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
    internal abstract class DrawableCoordinates<TCoordinateType>
    {
        protected DrawableCoordinates(IEnumerable<TCoordinateType> coordinates, int minCount)
        {
            Throw.IfNull(nameof(coordinates), coordinates);

            CheckCoordinates(new List<TCoordinateType>(coordinates), minCount);
        }

        protected List<TCoordinateType> Coordinates { get; private set; }

        public IList<TCoordinateType> ToList() => Coordinates;

        private void CheckCoordinates(List<TCoordinateType> coordinates, int minCount)
        {
            if (coordinates.Count == 0)
                throw new ArgumentException("Value cannot be empty", nameof(coordinates));

            foreach (TCoordinateType coordinate in coordinates)
            {
                if (coordinate == null)
                    throw new ArgumentNullException(nameof(coordinates), "Value should not contain null values");
            }

            if (coordinates.Count < minCount)
                throw new ArgumentException("Value should contain at least " + minCount + " coordinates.", nameof(coordinates));

            Coordinates = coordinates;
        }
    }
}