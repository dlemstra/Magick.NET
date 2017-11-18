using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.NativeDebug
{
    [TestClass]
    public class NativeDebugTests
    {
        [TestMethod]
        public void TestNative()
        {
            MagickReadSettings settings = new MagickReadSettings();
            settings.Density = new Density(96);
            settings.Format = MagickFormat.Png;

            using (MagickImageCollection images = new MagickImageCollection())
            {
                var bytes = System.IO.File.ReadAllBytes(@"i:\1.pdf");
                images.Read(bytes, settings);
            }
        }
    }
}
