// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheCloneMethod
        {
            [TestMethod]
            public void ShouldReturnEmptyCollectionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    using (IMagickImageCollection clones = images.Clone())
                    {
                        Assert.AreEqual(0, clones.Count);
                    }
                }
            }

            [TestMethod]
            public void ShouldCloneTheImagesInTheCollection()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.Add(Files.Builtin.Logo);
                    images.Add(Files.Builtin.Rose);
                    images.Add(Files.Builtin.Wizard);

                    using (IMagickImageCollection clones = images.Clone())
                    {
                        Assert.IsFalse(ReferenceEquals(images[0], clones[0]));
                        Assert.IsFalse(ReferenceEquals(images[1], clones[1]));
                        Assert.IsFalse(ReferenceEquals(images[2], clones[2]));
                    }
                }
            }
        }
    }
}
