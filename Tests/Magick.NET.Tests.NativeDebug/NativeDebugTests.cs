using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Magick.NET.Tests.NativeDebug
{
    [TestClass]
    public class NativeDebugTests
    {
        [TestMethod]
        public void TestNative()
        {
            using (var image = new MagickImage(@"i:\tiff\layers\new\layer3.tif[0]"))
            {
                //Assert.AreEqual("1", image.ProfileNames.Skip(1).First());

                var bytes = image.GetProfile("tiff:37724").ToByteArray();
                File.WriteAllBytes(@"i:\tiff\layers\new\profile2.txt", bytes);
            }
        }
    }
}
