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

using System;
using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class ImageOptimizerTests : IImageOptimizerTests
  {
    private void Test_LosslessCompressWithTempFile(string fileName)
    {
      string tempFile = Path.GetTempFileName();

      try
      {
        File.Copy(fileName, tempFile, true);
        Test_LosslessCompress_Smaller(tempFile);
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    protected override ILosslessImageOptimizer CreateLosslessImageOptimizer()
    {
      return new ImageOptimizer();
    }

    [TestMethod]
    public void Test_InvalidArguments()
    {
      Test_LosslessCompress_InvalidArguments();
    }

    [TestMethod]
    public void Test_IsSupported()
    {
      ImageOptimizer optimizer = new ImageOptimizer();

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        optimizer.IsSupported((FileInfo)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        optimizer.IsSupported((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        optimizer.IsSupported("");
      });

      Assert.IsTrue(optimizer.IsSupported(Files.FujiFilmFinePixS1ProGIF));
      Assert.IsTrue(optimizer.IsSupported(Files.ImageMagickJPG));
      Assert.IsTrue(optimizer.IsSupported(Files.SnakewarePNG));
      Assert.IsTrue(optimizer.IsSupported(Files.Missing));
      Assert.IsFalse(optimizer.IsSupported(Files.InvitationTif));
    }

    [TestMethod]
    public void Test_LosslessCompress()
    {
      Test_LosslessCompress_Smaller(Files.FujiFilmFinePixS1ProGIF);
      Test_LosslessCompress_Smaller(Files.ImageMagickJPG);
      Test_LosslessCompress_Smaller(Files.SnakewarePNG);
      Test_LosslessCompressWithTempFile(Files.ImageMagickJPG);
      Test_LosslessCompressWithTempFile(Files.SnakewarePNG);
    }
  }
}
