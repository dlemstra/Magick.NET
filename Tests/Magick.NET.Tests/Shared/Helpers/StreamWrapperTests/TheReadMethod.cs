// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        [TestClass]
        public class TheReadMethod
        {
            [TestMethod]
            public void ShouldReturnZeroWhenBufferIsNull()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        int count = streamWrapper.Read(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                        Assert.AreEqual(0, count);
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldReturnZeroWhenNothingShouldBeRead()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        byte[] buffer = new byte[255];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Read((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                            Assert.AreEqual(0, count);
                        }
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringReading()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new ReadExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                        {
                            byte[] buffer = new byte[10];
                            fixed (byte* p = buffer)
                            {
                                int count = streamWrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                                Assert.AreEqual(-1, count);
                            }
                        }
                    }
                }
            }

            [TestMethod]
            public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeRead()
            {
                using (var stream = new MemoryStream(new byte[5]))
                {
                    using (var streamWrapper = StreamWrapper.CreateForReading(stream))
                    {
                        byte[] buffer = new byte[10];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Read((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                            Assert.AreEqual(5, count);
                        }
                    }
                }
            }
        }
    }
}
