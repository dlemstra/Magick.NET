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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ImageMagick.ImageOptimizers
{
  /// <summary>
  /// Class that can be used to optimize png files.
  /// </summary>
  public sealed class PngOptimizer : IImageOptimizer, ILosslessImageOptimizer
  {
    private static void CheckFormat(MagickImage image)
    {
      MagickFormat format = image.FormatInfo.Module;
      if (format != MagickFormat.Png)
        throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString(), null);
    }

    private static void CheckTransparency(MagickImage image)
    {
      if (!image.HasAlpha)
        return;

      if (image.IsOpaque)
        image.HasAlpha = false;
    }

    private void DoLosslessCompress(FileInfo file)
    {
      using (MagickImage image = new MagickImage(file))
      {
        CheckFormat(image);

        image.Strip();
        image.SetDefine(MagickFormat.Png, "exclude-chunks", "all");
        image.SetDefine(MagickFormat.Png, "include-chunks", "tRNS,gAMA");
        CheckTransparency(image);

        Collection<FileInfo> tempFiles = new Collection<FileInfo>();

        try
        {
          FileInfo bestFile = null;

          foreach (int quality in GetQualityList())
          {
            FileInfo tempFile = new FileInfo(Path.GetTempFileName());
            tempFiles.Add(tempFile);

            image.Quality = quality;
            image.Write(tempFile);
            tempFile.Refresh();

            if (bestFile == null || bestFile.Length > tempFile.Length)
              bestFile = tempFile;
            else
              tempFile.Delete();
          }

          if (bestFile.Length < file.Length)
            bestFile.CopyTo(file.FullName, true);
        }
        finally
        {
          foreach (FileInfo tempFile in tempFiles)
          {
            if (tempFile.Exists)
              tempFile.Delete();
          }
        }
      }
    }

    private IEnumerable<int> GetQualityList()
    {
      if (OptimalCompression)
        return new int[] { 91, 94, 95, 97 };
      else
        return new int[] { 90 };
    }

    ///<summary>
    /// Initializes a new instance of the PngOptimizer class.
    ///</summary>
    public PngOptimizer()
    {
    }

    /// <summary>
    /// When set to true various compression types will be used to find the smallest file. This
    /// process will take extra time because the file has to be written multiple times.
    /// </summary>
    public bool OptimalCompression
    {
      get;
      set;
    }

    /// <summary>
    /// The format that the optimizer supports.
    /// </summary>
    public MagickFormatInfo Format
    {
      get
      {
        return MagickNET.GetFormatInformation(MagickFormat.Png);
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
      Throw.IfNull("file", file);

      DoLosslessCompress(file);
      file.Refresh();
    }
  }
}
