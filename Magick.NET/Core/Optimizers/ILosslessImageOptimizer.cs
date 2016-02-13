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

using System.IO;

namespace ImageMagick.ImageOptimizers
{
  /// <summary>
  /// Interface for optimizers that support lossless compression.
  /// </summary>
  public interface ILosslessImageOptimizer
  {
    /// <summary>
    /// When set to true various compression types will be used to find the smallest file. This
    /// process will take extra time because the file has to be written multiple times.
    /// </summary>
    bool OptimalCompression
    {
      get;
      set;
    }

    /// <summary>
    /// Performs lossless compression on speified the file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The image file to optimize</param>
    void LosslessCompress(FileInfo file);

    /// <summary>
    /// Performs lossless compression on speified the file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The image file to optimize</param>
    void LosslessCompress(string fileName);
  }
}
