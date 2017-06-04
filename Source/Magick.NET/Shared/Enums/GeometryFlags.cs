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

namespace ImageMagick
{
  internal enum GeometryFlags
  {
    NoValue = 0,
    PercentValue = 0x1000,      /* '%'  percentage of something */
    IgnoreAspectRatio = 0x2000, /* '!'  resize no-aspect - special use flag */
    Less = 0x4000,              /* '<'  resize smaller - special use flag */
    Greater = 0x8000,           /* '>'  resize larger - spacial use flag */
    FillArea = 0x10000,         /* '^'  special handling needed */
    LimitPixels = 0x20000,      /* '@'  resize to area - special use flag */
  }
}