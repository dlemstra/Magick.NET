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

#if !NETCOREAPP1_1

using System;
using System.IO;
using System.Linq;
using System.Web;
using ImageMagick;
using ImageMagick.Web;
using ImageMagick.Web.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class GzipHandlerTests
    {
        private MagickFormatInfo SvgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Svg);

        [TestMethod]
        public void Test_ProcessRequest()
        {
            IImageData imageData = new FileImageData(Files.Logos.MagickNETSVG, SvgFormatInfo);
            Test_ProcessRequest(imageData);

            TestStreamUrlResolver resolver = new TestStreamUrlResolver(Files.Logos.MagickNETSVG);
            imageData = new StreamImageData(resolver, SvgFormatInfo);
            Test_ProcessRequest(imageData);
        }

        private void Test_ProcessRequest(IImageData imageData)
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string tempDir = directory.FullName;

                string config = $@"<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}""/>";

                MagickWebSettings settings = TestSectionLoader.Load(config);

                HttpRequest request = new HttpRequest("foo", "https://bar", string.Empty);

                string outputFile = Path.Combine(tempDir, "output");

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(0, new FileInfo(outputFile).Length);
                Assert.AreEqual(1, tempDir.GetFiles().Count());

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    request.SetHeaders("Accept-Encoding", "invalid");
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(0, new FileInfo(outputFile).Length);
                Assert.AreEqual(1, tempDir.GetFiles().Count());

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    request.SetHeaders("Accept-Encoding", "gzip");
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                byte[] imageBytes = imageData.GetBytes();
                Assert.IsTrue(new FileInfo(outputFile).Length < imageBytes.Length);
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                File.Delete(outputFile);

                FileInfo cacheFile = tempDir.GetFiles().First();
                File.WriteAllText(cacheFile.FullName, string.Empty);

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    request.SetHeaders("Accept-Encoding", "gzip");
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(0, File.ReadAllBytes(outputFile).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                cacheFile.LastWriteTimeUtc = new DateTime(1979, 11, 19);

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    request.SetHeaders("Accept-Encoding", "gzip");
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.AreNotEqual(0, File.ReadAllBytes(cacheFile.FullName).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    request.SetHeaders("Accept-Encoding", "deflate");
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    GzipHandler handler = new GzipHandler(settings, imageData);
                    handler.ProcessRequest(context);
                }

                Assert.IsTrue(new FileInfo(outputFile).Length < imageBytes.Length);
                Assert.AreEqual(3, tempDir.GetFiles().Count());
            }
        }
    }
}

#endif