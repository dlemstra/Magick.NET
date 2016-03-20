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

using System;
using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  public abstract class IImageOptimizerTests
  {
    private static FileInfo CreateTemporaryFile(string fileName)
    {
      string tempFile = GetTemporaryFileName(Path.GetExtension(fileName));
      File.Copy(fileName, tempFile, true);

      return new FileInfo(tempFile);
    }

    private void Test_LosslessCompress(string fileName, bool resultIsSmaller)
    {
      FileInfo tempFile = CreateTemporaryFile(fileName);
      try
      {
        ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
        Assert.IsNotNull(optimizer);

        long before = tempFile.Length;
        optimizer.LosslessCompress(tempFile);

        long after = tempFile.Length;

        if (resultIsSmaller)
          Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
        else
          Assert.AreEqual(before, after);
      }
      finally
      {
        FileHelper.Delete(tempFile);
      }
    }

    protected abstract ILosslessImageOptimizer CreateLosslessImageOptimizer();

    protected static string GetTemporaryFileName(string extension)
    {
      string tempFile = Path.GetTempFileName();
      File.Move(tempFile, tempFile + extension);

      return tempFile + extension;
    }

    protected void Test_LosslessCompress_Smaller(string fileName)
    {
      Test_LosslessCompress(fileName, true);
    }

    protected void Test_LosslessCompress_NotSmaller(string fileName)
    {
      Test_LosslessCompress(fileName, false);
    }

    protected void Test_LosslessCompress_InvalidFile(string fileName)
    {
      FileInfo tempFile = CreateTemporaryFile(fileName);
      try
      {
        ExceptionAssert.Throws<MagickCorruptImageErrorException>(delegate ()
        {
          ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
          Assert.IsNotNull(optimizer);

          optimizer.LosslessCompress(tempFile);
        });
      }
      finally
      {
        FileHelper.Delete(tempFile);
      }
    }

    protected void Test_LosslessCompress_InvalidArguments()
    {
      ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
      Assert.IsNotNull(optimizer);

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        optimizer.LosslessCompress((FileInfo)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        optimizer.LosslessCompress((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        optimizer.LosslessCompress("");
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        optimizer.LosslessCompress(Files.Missing);
      });
    }
  }
}
