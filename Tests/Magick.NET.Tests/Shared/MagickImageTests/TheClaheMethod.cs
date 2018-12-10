using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheClaheMethod
        {
            [TestMethod]
            public void ShouldChangeTheImage()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    using (IMagickImage result = image.Clone())
                    {
                        result.Clahe(10, 20, 30, 1.5);
#if Q8
                        Assert.AreEqual(0.03, image.Compare(result, ErrorMetric.RootMeanSquared), 0.01);
#elif Q16 || Q16HDRI
                        Assert.AreEqual(0.08, image.Compare(result, ErrorMetric.RootMeanSquared), 0.01);
#else
#error Not implemented!
#endif
                    }
                }
            }
        }
    }
}
