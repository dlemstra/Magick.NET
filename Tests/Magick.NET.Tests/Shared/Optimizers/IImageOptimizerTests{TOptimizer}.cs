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
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public abstract class IImageOptimizerTests<TOptimizer>
        where TOptimizer : IImageOptimizer, new()
    {
        protected IImageOptimizer Optimizer => new TOptimizer();

        protected long AssertCompressSmaller(string fileName)
        {
            return AssertCompress(fileName, true, (FileInfo file) =>
            {
                Optimizer.Compress(file);
                Optimizer.Compress(file.FullName);
            });
        }

        protected void AssertCompressNotSmaller(string fileName)
        {
            AssertCompress(fileName, false, (FileInfo file) =>
            {
                Optimizer.Compress(file);
                Optimizer.Compress(file.FullName);
            });
        }

        protected long AssertLosslessCompressSmaller(string fileName)
        {
            return AssertCompress(fileName, true, (FileInfo file) =>
            {
                Optimizer.LosslessCompress(file);
                Optimizer.LosslessCompress(file.FullName);
            });
        }

        protected void AssertLosslessCompressNotSmaller(string fileName)
        {
            AssertCompress(fileName, false, (FileInfo file) =>
            {
                Optimizer.LosslessCompress(file);
                Optimizer.LosslessCompress(file.FullName);
            });
        }

        private long AssertCompress(string fileName, bool resultIsSmaller, Action<FileInfo> action)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                long before = tempFile.FileInfo.Length;

                action(tempFile.FileInfo);

                long after = tempFile.FileInfo.Length;

                if (resultIsSmaller)
                    Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
                else
                    Assert.AreEqual(before, after);

                return after;
            }
        }
    }
}
