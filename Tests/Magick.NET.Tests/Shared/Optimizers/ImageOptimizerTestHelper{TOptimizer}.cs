﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
                return Optimizer.Compress(file);
            });

            long lengthB = AssertCompress(fileName, true, (string file) =>
            {
                return Optimizer.Compress(file);
            });

            long lengthC = AssertCompress(fileName, true, (Stream stream) =>
            {
                return Optimizer.Compress(stream);
            });

            Assert.AreEqual(lengthA, lengthB, 1);
            Assert.AreEqual(lengthB, lengthC, 1);
            return lengthA;
        }

        protected void AssertCompressNotSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, false, (FileInfo file) =>
            {
                return Optimizer.Compress(file);
            });

            long lengthB = AssertCompress(fileName, false, (string file) =>
            {
                return Optimizer.Compress(file);
            });

            long lengthC = AssertCompress(fileName, false, (Stream stream) =>
            {
                return Optimizer.Compress(stream);
            });

            Assert.AreEqual(lengthA, lengthB);
            Assert.AreEqual(lengthB, lengthC);
        }

        protected void AssertCompressTwice(string fileName)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                bool compressed1 = Optimizer.Compress(tempFile);

                long after1 = tempFile.Length;

                bool compressed2 = Optimizer.Compress(tempFile);

                long after2 = tempFile.Length;

                Assert.AreEqual(after1, after2, 1);
                Assert.IsTrue(compressed1);
                Assert.IsFalse(compressed2);
            }
        }

        protected long AssertLosslessCompressSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, true, (FileInfo file) =>
            {
                return Optimizer.LosslessCompress(file);
            });

            long lengthB = AssertCompress(fileName, true, (string file) =>
            {
                return Optimizer.LosslessCompress(file);
            });

            long lengthC = AssertCompress(fileName, true, (Stream stream) =>
            {
                return Optimizer.LosslessCompress(stream);
            });

            Assert.AreEqual(lengthA, lengthB, 1);
            Assert.AreEqual(lengthB, lengthC, 1);
            return lengthA;
        }

        protected void AssertLosslessCompressNotSmaller(string fileName)
        {
            long lengthA = AssertCompress(fileName, false, (FileInfo file) =>
            {
                return Optimizer.LosslessCompress(file);
            });

            long lengthB = AssertCompress(fileName, false, (string file) =>
            {
                return Optimizer.LosslessCompress(file);
            });

            long lengthC = AssertCompress(fileName, false, (Stream stream) =>
            {
                return Optimizer.LosslessCompress(stream);
            });

            Assert.AreEqual(lengthA, lengthB);
            Assert.AreEqual(lengthB, lengthC);
        }

        protected void AssertLosslessCompressTwice(string fileName)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                bool compressed1 = Optimizer.LosslessCompress(tempFile);

                long after1 = tempFile.Length;

                bool compressed2 = Optimizer.LosslessCompress(tempFile);

                long after2 = tempFile.Length;

                Assert.AreEqual(after1, after2, 1);
                Assert.IsTrue(compressed1);
                Assert.IsFalse(compressed2);
            }
        }
    }
}
