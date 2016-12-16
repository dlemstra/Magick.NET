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
  /// Class that can be used to optimize gif files.
  /// </summary>
  public sealed class GifOptimizer : IImageOptimizer, ILosslessImageOptimizer
  {
    private static void CheckFormat(MagickImage image)
    {
      MagickFormat format = image.FormatInfo.Module;
      if (format != MagickFormat.Gif)
        throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString(), null);
    }

    private static void DoLosslessCompress(FileInfo file)
    {
      using (MagickImageCollection images = new MagickImageCollection(file))
      {
        if (images.Count == 1)
        {
          DoLosslessCompress(file, images[0]);
          return;
        }
      }
    }

    private static void DoLosslessCompress(FileInfo file, MagickImage image)
    {
      CheckFormat(image);

      image.Strip();

      FileInfo tempFile = new FileInfo(Path.GetTempFileName());
      try
      {
        image.Settings.Interlace = Interlace.NoInterlace;
        image.Write(tempFile);

        if (tempFile.Length < file.Length)
          tempFile.CopyTo(file.FullName, true);
      }
      finally
      {
        FileHelper.Delete(tempFile);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GifOptimizer"/> class.
    /// </summary>
    public GifOptimizer()
    {
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
    /// Gets the format that the optimizer supports.
    /// </summary>
    public MagickFormatInfo Format
    {
      get
      {
        return MagickNET.GetFormatInformation(MagickFormat.Gif);
      }
    }

    /// <summary>
    /// Performs lossless compression on the file. If the new file size is not smaller the file
    /// won't be overwritten.
    /// </summary>
    /// <param name="fileName">The png file to optimize</param>
    public void LosslessCompress(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      DoLosslessCompress(new FileInfo(filePath));
    }

    /// <summary>
    /// Performs lossless compression on the file. If the new file size is not smaller the file
    /// won't be overwritten.
    /// </summary>
    /// <param name="file">The png file to optimize</param>
    public void LosslessCompress(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      DoLosslessCompress(file);
      file.Refresh();
    }
  }
}
