//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
  ///<summary>
  /// Specifies the error metric types.
  ///</summary>
  public enum ErrorMetric
  {
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,

    /// <summary>
    /// Absolute
    /// </summary>
    Absolute,

    /// <summary>
    /// Fuzz
    /// </summary>
    Fuzz,

    /// <summary>
    /// MeanAbsolute
    /// </summary>
    MeanAbsolute,

    /// <summary>
    /// MeanErrorPerPixel
    /// </summary>
    MeanErrorPerPixel,

    /// <summary>
    /// MeanSquared
    /// </summary>
    MeanSquared,

    /// <summary>
    /// NormalizedCrossCorrelation
    /// </summary>
    NormalizedCrossCorrelation,

    /// <summary>
    /// PeakAbsolute
    /// </summary>
    PeakAbsolute,

    /// <summary>
    /// PeakSignalToNoiseRatio
    /// </summary>
    PeakSignalToNoiseRatio,

    /// <summary>
    /// PerceptualHash
    /// </summary>
    PerceptualHash,

    /// <summary>
    /// RootMeanSquared
    /// </summary>
    RootMeanSquared
  }
}