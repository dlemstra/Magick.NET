// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
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
    public abstract class ImageOptimizerTestHelper
    {
        protected long AssertCompress(string fileName, bool resultIsSmaller, Func<FileInfo, bool> action)
        {
            using (var tempFile = new TemporaryFile(fileName))
            {
                var before = tempFile.Length;

                var result = action(tempFile);

                var after = tempFile.Length;

                Assert.Equal(resultIsSmaller, result);

                if (resultIsSmaller)
                    Assert.True(after < before);
                else
                    Assert.Equal(before, after);

                return after;
            }
        }

        protected long AssertCompress(string fileName, bool resultIsSmaller, Func<string, bool> action)
        {
            using (var tempFile = new TemporaryFile(fileName))
            {
                var before = tempFile.Length;

                var result = action(tempFile.FullName);

                tempFile.Refresh();
                var after = tempFile.Length;

                Assert.Equal(resultIsSmaller, result);

                if (resultIsSmaller)
                    Assert.True(after < before);
                else
                    Assert.Equal(before, after);

                return after;
            }
        }

        protected long AssertCompress(string fileName, bool resultIsSmaller, Func<Stream, bool> action)
        {
            using (FileStream fileStream = FileHelper.OpenRead(fileName))
            {
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Position = 42;
                    fileStream.CopyTo(memoryStream);
                    memoryStream.Position = 42;

                    var before = memoryStream.Length;

                    var result = action(memoryStream);

                    var after = memoryStream.Length;

                    Assert.Equal(42, memoryStream.Position);
                    Assert.Equal(resultIsSmaller, result);

                    if (resultIsSmaller)
                        Assert.True(after < before);
                    else
                        Assert.Equal(before, after);

                    return after - 42;
                }
            }
        }

        protected void AssertInvalidFileFormat(string fileName, Action<FileInfo> action)
        {
            using (var tempFile = new TemporaryFile(fileName))
            {
                Assert.Throws<MagickCorruptImageErrorException>(() => action(tempFile));
            }
        }

        protected void AssertInvalidFileFormat(string fileName, Action<string> action)
        {
            using (var tempFile = new TemporaryFile(fileName))
            {
                Assert.Throws<MagickCorruptImageErrorException>(() => action(tempFile.FullName));
            }
        }

        protected void AssertInvalidFileFormat(string fileName, Action<Stream> action)
        {
            using (var tempFile = new TemporaryFile(fileName))
            {
                using (FileStream fileStream = FileHelper.OpenRead(fileName))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        memoryStream.Position = 0;

                        Assert.Throws<MagickCorruptImageErrorException>(() => action(memoryStream));
                    }
                }
            }
        }
    }
}
