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
    public abstract class ImageOptimizerTestHelper<TOptimizer> : ImageOptimizerTestHelper
        where TOptimizer : IImageOptimizer, new()
    {
        protected IImageOptimizer Optimizer => new TOptimizer();

        protected long AssertCompressSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, true, (FileInfo file) =>
            {
                Optimizer.Compress(file);
            });

            long lengthB = AssertCompress(fileName, true, (string file) =>
            {
                Optimizer.Compress(file);
            });

            Assert.AreEqual(lengthA, lengthB, 1);
            return lengthA;
        }

        protected void AssertCompressNotSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, false, (FileInfo file) =>
            {
                Optimizer.Compress(file);
            });

            long lengthB = AssertCompress(fileName, false, (string file) =>
            {
                Optimizer.Compress(file);
            });

            Assert.AreEqual(lengthA, lengthB);
        }

        protected void AssertCompressTwice(string fileName)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                Optimizer.Compress(tempFile);

                long after1 = tempFile.Length;

                Optimizer.Compress(tempFile);

                long after2 = tempFile.Length;

                Assert.AreEqual(after1, after2, 1);
            }
        }

        protected long AssertLosslessCompressSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, true, (FileInfo file) =>
            {
                Optimizer.LosslessCompress(file);
            });

            long lengthB = AssertCompress(fileName, true, (string file) =>
            {
                Optimizer.LosslessCompress(file);
            });

            Assert.AreEqual(lengthA, lengthB, 1);
            return lengthA;
        }

        protected void AssertLosslessCompressNotSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, false, (FileInfo file) =>
            {
                Optimizer.LosslessCompress(file);
            });

            long lengthB = AssertCompress(fileName, false, (string file) =>
            {
                Optimizer.LosslessCompress(file);
            });

            Assert.AreEqual(lengthA, lengthB);
        }

        protected void AssertLosslessCompressTwice(string fileName)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                Optimizer.LosslessCompress(tempFile);

                long after1 = tempFile.Length;

                Optimizer.LosslessCompress(tempFile);

                long after2 = tempFile.Length;

                Assert.AreEqual(after1, after2, 1);
            }
        }
    }
}
