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

namespace ImageMagick
{
  /// <summary>
  /// Class that contains setting for the connected components operation.
  /// </summary>
  public sealed class ConnectedComponentsSettings
  {
    /// <summary>
    /// Eliminate small objects by merging them with their larger neighbors.
    /// </summary>
    public double? AreaThreshold
    {
      get;
      set;
    }

    /// <summary>
    /// How many neighbors to visit, choose from 4 or 8.
    /// </summary>
    public int Connectivity
    {
      get;
      set;
    }

    /// <summary>
    /// Replace the object color in the labeled image with the mean-color from the source image.
    /// </summary>
    public bool MeanColor
    {
      get;
      set;
    }
  }
}
