using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheOptimizeTransparencyMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() => images.OptimizeTransparency());
                }
            }

            [TestMethod]
            public void ShouldCorrectlyOptimizeTheImages()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                    var image = new MagickImage(MagickColors.Red, 11, 11);
                    using (var pixels = image.GetPixels())
                    {
                        pixels.SetPixel(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                    }

                    collection.Add(image);
                    collection.OptimizeTransparency();

                    Assert.AreEqual(11, collection[1].Width);
                    Assert.AreEqual(11, collection[1].Height);
                    Assert.AreEqual(0, collection[1].Page.X);
                    Assert.AreEqual(0, collection[1].Page.Y);
                    ColorAssert.AreEqual(MagickColors.Lime, collection[1], 5, 5);
                    ColorAssert.AreEqual(new MagickColor("#f000"), collection[1], 4, 4);
                }
            }
        }
    }
}
