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

using System.IO;

namespace ImageMagick.ImageOptimizers
{
  /// <summary>
  /// Class that can be used to optimize jpeg files.
  /// </summary>
  public sealed partial class JpegOptimizer : IImageOptimizer
  {
    private static void LosslessCompress(FileInfo file, bool progressive)
    {
      FileInfo output = new FileInfo(Path.GetTempFileName());

      try
      {
        int result = NativeJpegOptimizer.Optimize(file.FullName, output.FullName, progressive);

        if (result == 1)
          throw new MagickCorruptImageErrorException("Unable to decompress the jpeg file.");

        if (result == 2)
          throw new MagickCorruptImageErrorException("Unable to compress the jpeg file.");

        if (result != 0)
          return;

        output.Refresh();
        if (output.Length < file.Length)
          output.CopyTo(file.FullName, true);

        file.Refresh();
      }
      finally
      {
        FileHelper.Delete(output);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JpegOptimizer"/> class.
    /// </summary>
    public JpegOptimizer()
    {
      Progressive = true;
    }

    /// <summary>
    /// Gets the format that the optimizer supports.
    /// </summary>
    public MagickFormatInfo Format
    {
      get
      {
        return MagickNET.GetFormatInformation(MagickFormat.Jpeg);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether various compression types will be used to find
    /// the smallest file. This process will take extra time because the file has to be written
    /// multiple times.
    /// </summary>
    public bool OptimalCompression
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a progressive jpeg file will be created.
    /// </summary>
    public bool Progressive
    {
      get;
      set;
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

      LosslessCompress(new FileInfo(fileName));
    }

    /// <summary>
    /// Performs lossless compression on speified the file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The png file to optimize</param>
    public void LosslessCompress(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      LosslessCompress(file, Progressive);
      if (OptimalCompression)
        LosslessCompress(file, !Progressive);
    }
  }
}
