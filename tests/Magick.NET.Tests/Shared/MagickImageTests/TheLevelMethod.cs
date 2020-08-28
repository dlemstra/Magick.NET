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
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheLevelMethod
        {
            [TestMethod]
            public void ShouldUseCompositeAsDefaultChannels()
            {
                using (var first = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var second = first.Clone())
                    {
                        first.Level(new Percentage(50), new Percentage(10));
                        second.Level(new Percentage(50), new Percentage(10), Channels.Composite);

                        Assert.AreEqual(first.Signature, second.Signature);
                    }
                }
            }

            [TestMethod]
            public void ShouldUseOneAsGammaDefault()
            {
                using (var first = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var second = first.Clone())
                    {
                        first.Level(new Percentage(50), new Percentage(10));
                        second.Level(new Percentage(50), new Percentage(10), 1.0, Channels.Composite);

                        Assert.AreEqual(first.Signature, second.Signature);
                    }
                }
            }

            [TestMethod]
            public void ShouldScaleTheColors()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var second = image.Clone())
                    {
                        image.Level(new Percentage(50.0), new Percentage(10.0));

                        var fifty = (QuantumType)(Quantum.Max * 0.5);
                        var ten = (QuantumType)(Quantum.Max * 0.1);
                        second.Level(fifty, ten, Channels.Red);
                        second.Level(fifty, ten, Channels.Green | Channels.Blue);
                        second.Level(fifty, ten, Channels.Alpha);

                        Assert.AreEqual(image.Signature, second.Signature);
                    }
                }
            }
        }
    }
}
