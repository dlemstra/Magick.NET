// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Core.Helpers
{
    [TestClass]
    public class StreamWrapperTests
    {
        [TestMethod]
        public void Test_Dispose()
        {
            using (TestStream stream = new TestStream(true, true, true))
            {
                StreamWrapper streamWrapper = StreamWrapper.CreateForReading(stream);
                streamWrapper.Dispose();
                streamWrapper.Dispose();
            }
        }

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

            using (TestStream stream = new TestStream(true, true, true))
            {
                StreamWrapper.CreateForReading(stream);
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
                StreamWrapper.CreateForWriting(stream);
            }
        }

        [TestMethod]
        public void Test_Read()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWrapper streamWrapper = StreamWrapper.CreateForReading(memStream))
                {
                    int count = streamWrapper.Read(IntPtr.Zero, UIntPtr.Zero, IntPtr.Zero);
                    Assert.AreEqual(0, count);
                }
            }
        }

        [TestMethod]
        public void Test_ReadExceptions()
        {
            using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
            {
                using (ReadExceptionStream stream = new ReadExceptionStream(fs))
                {
                    using (IMagickImage image = new MagickImage())
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
                    using (IMagickImage image = new MagickImage())
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
        public void Test_Write()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWrapper streamWrapper = StreamWrapper.CreateForReading(memStream))
                {
                    int count = streamWrapper.Write(IntPtr.Zero, UIntPtr.Zero, IntPtr.Zero);
                    Assert.AreEqual(0, count);
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
                    using (IMagickImage image = new MagickImage("logo:"))
                    {
                        image.Write(stream);

                        Assert.AreEqual(0, memStream.Position);
                    }
                }
            }
        }
    }
}
