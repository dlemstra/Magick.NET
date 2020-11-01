// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheCloneMethod
        {
            [Fact]
            public void ShouldReturnEmptyCollectionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    using (var clones = images.Clone())
                    {
                        Assert.Empty(clones);
                    }
                }
            }

            [Fact]
            public void ShouldCloneTheImagesInTheCollection()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(Files.Builtin.Logo);
                    images.Add(Files.Builtin.Rose);
                    images.Add(Files.Builtin.Wizard);

                    using (var clones = images.Clone())
                    {
                        Assert.False(ReferenceEquals(images[0], clones[0]));
                        Assert.False(ReferenceEquals(images[1], clones[1]));
                        Assert.False(ReferenceEquals(images[2], clones[2]));
                    }
                }
            }
        }
    }
}
