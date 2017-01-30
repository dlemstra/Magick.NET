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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Magick.NET.Tests.Core.Helpers
{
  [TestClass]
  public class StreamWrapperTests
  {
    [TestMethod]
    public void Test_Exceptions()
    {
      using (TestStream stream = new TestStream(false, true, true))
      {
        ExceptionAssert.ThrowsArgumentException(() =>
        {
          StreamWrapper.CreateForReading(stream);
        }, "stream", "readable");
      }

      using (TestStream stream = new TestStream(true, false, true))
      {
        ExceptionAssert.ThrowsArgumentException(() =>
        {
          StreamWrapper.CreateForWriting(stream);
        }, "stream", "writeable");
      }

      using (TestStream stream = new TestStream(false, true, true))
      {
        ExceptionAssert.ThrowsArgumentException(() =>
        {
          StreamWrapper.CreateForWriting(stream);
        }, "stream", "readable");
      }
    }

    [TestMethod]
    public void Test_ReadExceptions()
    {
      using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
      {
        using (ReadExceptionStream stream = new ReadExceptionStream(fs))
        {
          using (MagickImage image = new MagickImage())
          {
            ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
            {
              image.Read(stream);
            });
          }
        }
      }
    }

    [TestMethod]
    public void Test_SeekExceptions()
    {
      using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
      {
        using (SeekExceptionStream stream = new SeekExceptionStream(fs))
        {
          using (MagickImage image = new MagickImage())
          {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
            {
              image.Read(stream);
            });
          }
        }
      }
    }

    [TestMethod]
    public void Test_WriteExceptions()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        using (WriteExceptionStream stream = new WriteExceptionStream(memStream))
        {
          using (MagickImage image = new MagickImage("logo:"))
          {
            image.Write(stream);

            Assert.AreEqual(0, memStream.Position);
          }
        }
      }
    }
  }
}
