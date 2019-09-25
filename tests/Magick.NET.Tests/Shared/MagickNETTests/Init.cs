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

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        private static void AssertConfigFiles(string path)
        {
            Assert.IsTrue(File.Exists(Path.Combine(path, "colors.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "configure.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "delegates.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "english.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "locale.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "log.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "policy.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "thresholds.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "type.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "type-ghostscript.xml")));
        }
    }
}
