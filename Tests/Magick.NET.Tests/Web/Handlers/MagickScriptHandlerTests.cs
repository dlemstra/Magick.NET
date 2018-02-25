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

#if !NETCORE

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using ImageMagick;
using ImageMagick.Web;
using ImageMagick.Web.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MagickScriptHandlerTests
    {
        private Encoding Encoding => System.Text.Encoding.GetEncoding(1252);

        private MagickFormatInfo JpgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Jpg);

        [TestMethod]
        public void Test_Optimize()
        {
            IScriptData scriptData = new TestScriptData()
            {
                OutputFormat = MagickFormat.Jpg,
                Script = XElement.Load(Files.Scripts.Draw).CreateNavigator(),
            };

            IImageData imageData = new FileImageData(Files.ImageMagickJPG, JpgFormatInfo);
            Test_Optimize(imageData, scriptData);

            TestStreamUrlResolver resolver = new TestStreamUrlResolver(Files.ImageMagickJPG);
            imageData = new StreamImageData(resolver, JpgFormatInfo);
            Test_Optimize(imageData, scriptData);
        }

        [TestMethod]
        public void Test_ProcessRequest()
        {
            TestScriptData scriptData = new TestScriptData()
            {
                OutputFormat = MagickFormat.Png,
                Script = XElement.Load(Files.Scripts.Resize).CreateNavigator(),
            };

            IImageData imageData = new FileImageData(Files.ImageMagickJPG, JpgFormatInfo);
            Test_ProcessRequest(imageData, scriptData);

            scriptData.OutputFormat = MagickFormat.Png;

            TestStreamUrlResolver resolver = new TestStreamUrlResolver(Files.ImageMagickJPG);
            imageData = new StreamImageData(resolver, JpgFormatInfo);
            Test_ProcessRequest(imageData, scriptData);
        }

        private void Test_ProcessRequest(IImageData imageData, TestScriptData scriptData)
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string tempDir = directory.FullName;

                string config = $@"<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}""/>";

                MagickWebSettings settings = TestSectionLoader.Load(config);

                HttpRequest request = new HttpRequest("foo", "https://bar", string.Empty);

                string outputFile = Path.Combine(tempDir, "output.png");

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                using (IMagickImage image = new MagickImage(outputFile))
                {
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                    Assert.AreEqual(62, image.Width);
                    Assert.AreEqual(59, image.Height);
                }

                Assert.AreEqual(2, tempDir.GetFiles().Count());

                File.Delete(outputFile);

                FileInfo cacheFile = tempDir.GetFiles().First();
                File.WriteAllText(cacheFile.FullName, string.Empty);

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(0, File.ReadAllBytes(outputFile).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                cacheFile.LastWriteTimeUtc = new DateTime(1979, 11, 19);

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                Assert.AreNotEqual(0, File.ReadAllBytes(cacheFile.FullName).Count());
                Assert.AreEqual(2, tempDir.GetFiles().Count());

                using (IMagickImage image = new MagickImage(outputFile))
                {
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                    Assert.AreEqual(62, image.Width);
                    Assert.AreEqual(59, image.Height);
                }

                scriptData.OutputFormat = MagickFormat.Tiff;

                outputFile = Path.Combine(tempDir, "output.tiff");

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                Assert.AreEqual(4, tempDir.GetFiles().Count());

                using (IMagickImage image = new MagickImage(outputFile))
                {
                    Assert.AreEqual(MagickFormat.Tiff, image.Format);
                    Assert.AreEqual(62, image.Width);
                    Assert.AreEqual(59, image.Height);
                }
            }
        }

        private void Test_Optimize(IImageData imageData, IScriptData scriptData)
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string tempDir = directory.FullName;

                string config = $@"
<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}"">
  <optimization enabled=""false""/>
</magick.net.web>";

                MagickWebSettings settings = TestSectionLoader.Load(config);

                HttpRequest request = new HttpRequest("foo", "https://bar", string.Empty);

                FileInfo outputFile = new FileInfo(Path.Combine(tempDir, "output.jpg"));

                using (StreamWriter writer = new StreamWriter(outputFile.FullName, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                outputFile.Refresh();
                long lengthWithoutOptimization = outputFile.Length;

                foreach (FileInfo file in tempDir.GetFiles())
                {
                    file.Delete();
                }

                config = $@"<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}""/>";
                settings = TestSectionLoader.Load(config);

                using (StreamWriter writer = new StreamWriter(outputFile.FullName, false, Encoding))
                {
                    HttpResponse response = new HttpResponse(writer);
                    HttpContext context = new HttpContext(request, response);

                    MagickScriptHandler handler = new MagickScriptHandler(settings, imageData, scriptData);
                    handler.ProcessRequest(context);
                }

                outputFile.Refresh();
                Assert.IsTrue(outputFile.Length < lengthWithoutOptimization);
            }
        }
    }
}

#endif