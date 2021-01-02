// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StreamWrapperTests
    {
        public class TheWriteMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenBufferIsNull()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        int count = streamWrapper.Write(IntPtr.Zero, (UIntPtr)10, IntPtr.Zero);
                        Assert.Equal(0, count);
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnZeroWhenNothingShouldBeWritten()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        byte[] buffer = new byte[255];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Write((IntPtr)p, UIntPtr.Zero, IntPtr.Zero);
                            Assert.Equal(0, count);
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldNotThrowExceptionWhenWhenStreamThrowsExceptionDuringWriting()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var stream = new WriteExceptionStream(memStream))
                    {
                        using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                        {
                            byte[] buffer = new byte[10];
                            fixed (byte* p = buffer)
                            {
                                int count = streamWrapper.Write((IntPtr)p, (UIntPtr)10, IntPtr.Zero);
                                Assert.Equal(-1, count);
                            }
                        }
                    }
                }
            }

            [Fact]
            public unsafe void ShouldReturnTheNumberOfBytesThatCouldBeWritten()
            {
                using (var stream = new MemoryStream())
                {
                    using (var streamWrapper = StreamWrapper.CreateForWriting(stream))
                    {
                        byte[] buffer = new byte[5];
                        fixed (byte* p = buffer)
                        {
                            int count = streamWrapper.Write((IntPtr)p, (UIntPtr)5, IntPtr.Zero);
                            Assert.Equal(5, count);
                        }
                    }
                }
            }
        }
    }
}
