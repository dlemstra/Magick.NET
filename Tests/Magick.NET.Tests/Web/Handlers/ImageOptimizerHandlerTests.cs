//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

#if !NETCOREAPP1_1

using ImageMagick;
using ImageMagick.Web;
using ImageMagick.Web.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ImageOptimizerHandlerTests
    {
        private MagickFormatInfo JpgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Jpg);
        private Encoding Encoding = System.Text.Encoding.GetEncoding(1252);

        private void Test_ProcessRequest(IImageData imageData)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            try
            {
                string config = $@"<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}""/>";

                MagickWebSettings settings = TestSectionLoader.Load(config);

                HttpRequest request = new HttpRequest("foo", "https://bar", "");

                string outputFile = Path.Combine(tempDir, "output.jpg");

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    ImageOptimizerHandler handler = new ImageOptimizerHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                byte[] imageBytes = imageData.GetBytes();
                Assert.IsTrue(new FileInfo(outputFile).Length < imageBytes.Length);
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                File.Delete(outputFile);

                FileInfo cacheFile = tempDir.GetFiles().First();
                File.WriteAllText(cacheFile.FullName, "");

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    ImageOptimizerHandler handler = new ImageOptimizerHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(0, File.ReadAllBytes(outputFile).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                cacheFile.LastWriteTimeUtc = new DateTime(1979, 11, 19);

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    ImageOptimizerHandler handler = new ImageOptimizerHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreNotEqual(0, File.ReadAllBytes(outputFile).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());
            }
            finally
            {
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
        }

        [TestMethod]
        public void Test_ProcessRequest()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpg");
            try
            {
                using (IMagickImage image = new MagickImage("logo:"))
                {
                    image.Write(tempFile);
                }

                File.SetLastWriteTimeUtc(tempFile, new DateTime(2001, 1, 1));

                IImageData imageData = new FileImageData(tempFile, JpgFormatInfo);
                Test_ProcessRequest(imageData);

                File.SetLastWriteTimeUtc(tempFile, new DateTime(2001, 1, 1));

                TestStreamUrlResolver resolver = new TestStreamUrlResolver(tempFile);
                imageData = new StreamImageData(resolver, JpgFormatInfo);
                Test_ProcessRequest(imageData);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}

#endif