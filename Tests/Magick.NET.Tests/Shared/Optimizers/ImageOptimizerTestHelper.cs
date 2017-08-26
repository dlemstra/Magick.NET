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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public abstract class ImageOptimizerTestHelper
    {
        protected long AssertCompress(string fileName, bool resultIsSmaller, Func<FileInfo, bool> action)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                long before = tempFile.Length;

                bool result = action(tempFile);

                long after = tempFile.Length;

                Assert.AreEqual(resultIsSmaller, result);

                if (resultIsSmaller)
                    Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
                else
                    Assert.AreEqual(before, after);

                return after;
            }
        }

        protected long AssertCompress(string fileName, bool resultIsSmaller, Func<string, bool> action)
        {
            using (TemporaryFile tempFile = new TemporaryFile(fileName))
            {
                long before = tempFile.Length;

                bool result = action(tempFile.FullName);

                tempFile.Refresh();
                long after = tempFile.Length;

                Assert.AreEqual(resultIsSmaller, result);

                if (resultIsSmaller)
                    Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
                else
                    Assert.AreEqual(before, after);

                return after;
            }
        }
    }
}
