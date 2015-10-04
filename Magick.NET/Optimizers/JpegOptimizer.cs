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

using System.IO;

namespace ImageMagick.ImageOptimizers
{
  /// <summary>
  /// Class that can be used to optimize jpeg files.
  /// </summary>
  public sealed class JpegOptimizer : IImageOptimizer, ILosslessImageOptimizer
  {
    private Wrapper.JpegOptimizer _Instance;

    ///<summary>
    /// Initializes a new instance of the JpegOptimizer class.
    ///</summary>
    public JpegOptimizer()
    {
      _Instance = new Wrapper.JpegOptimizer();
    }

    /// <summary>
    /// The format that the optimizer supports.
    /// </summary>
    public MagickFormatInfo Format
    {
      get
      {
        return MagickNET.GetFormatInformation(MagickFormat.Jpeg);
      }
    }

    /// <summary>
    /// When set to true various compression types will be used to find the smallest file. This
    /// process will take extra time because the file has to be written multiple times.
    /// </summary>
    public bool OptimalCompression
    {
      get
      {
        return _Instance.OptimalCompression;
      }
      set
      {
        _Instance.OptimalCompression = value;
      }
    }

    /// <summary>
    /// When set to true a progressive jpeg file will be created.
    /// </summary>
    public bool Progressive
    {
      get
      {
        return _Instance.Progressive;
      }
      set
      {
        _Instance.Progressive = value;
      }
    }

    /// <summary>
    /// Performs lossless compression on speified the file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The png file to optimize</param>
    public void LosslessCompress(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      _Instance.LosslessCompress(new FileInfo(filePath));
    }

    /// <summary>
    /// Performs lossless compression on speified the file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The png file to optimize</param>
    public void LosslessCompress(FileInfo file)
    {
      Throw.IfNull("file", file);

      _Instance.LosslessCompress(file);
    }
  }
}
