//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using ImageMagick;
using ImageMagick.Web;
using ImageMagick.Web.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Magick.NET.Tests
{
  [TestClass]
  public class GzipHandlerTests
  {
    private MagickFormatInfo SvgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Svg);

    [TestMethod]
    public void Test_CanCompress()
    {
      string config = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"" enableGzip=""false""/>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      bool canCompress = GzipHandler.CanCompress(settings, SvgFormatInfo);

      Assert.IsFalse(canCompress);
    }

    [TestMethod]
    public void Test_ProcessRequest()
    {
      string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

      try
      {
        string config = $@"<magick.net.web cacheDirectory=""{tempDir}"" tempDirectory=""{tempDir}""/>";

        MagickWebSettings settings = TestSectionLoader.Load(config);

        TestUrlResolver resolver = new TestUrlResolver();
        resolver.FileName = Path.Combine(tempDir, "test.svg");

        File.Copy(Files.Logos.MagickNETSVG, resolver.FileName);

        HttpRequest request = new HttpRequest("foo", "https://bar", "");

        string outputFile = Path.Combine(tempDir, "output");

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        CollectionAssert.AreEqual(File.ReadAllBytes(resolver.FileName), File.ReadAllBytes(outputFile));
        Assert.AreEqual(2, tempDir.GetFiles().Count());

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          request.SetHeaders("Accept-Encoding", "invalid");
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        CollectionAssert.AreEqual(File.ReadAllBytes(resolver.FileName), File.ReadAllBytes(outputFile));
        Assert.AreEqual(2, tempDir.GetFiles().Count());

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          request.SetHeaders("Accept-Encoding", "gzip");
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        Assert.IsTrue(new FileInfo(outputFile).Length < new FileInfo(resolver.FileName).Length);
        Assert.AreEqual(3, tempDir.GetFiles().Count());

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          request.SetHeaders("Accept-Encoding", "deflate");
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        Assert.IsTrue(new FileInfo(outputFile).Length < new FileInfo(resolver.FileName).Length);
        Assert.AreEqual(4, tempDir.GetFiles().Count());

        File.Delete(outputFile);

        FileInfo cacheFile = tempDir.GetFiles().First();

        DateTime lastWriteTime = cacheFile.LastWriteTime;

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          request.SetHeaders("Accept-Encoding", "deflate");
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        cacheFile.Refresh();

        Assert.AreEqual(lastWriteTime, cacheFile.LastWriteTime);
        Assert.AreEqual(4, tempDir.GetFiles().Count());

        File.SetLastWriteTime(cacheFile.FullName, new DateTime(1979, 11, 19));
        lastWriteTime = cacheFile.LastWriteTime;

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
          request.SetHeaders("Accept-Encoding", "deflate");
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          GzipHandler handler = new GzipHandler(settings, resolver, SvgFormatInfo);
          handler.ProcessRequest(context);
        }

        cacheFile.Refresh();

        Assert.AreNotEqual(lastWriteTime, cacheFile.LastWriteTime);
        Assert.AreEqual(4, tempDir.GetFiles().Count());
      }
      finally
      {
        if (Directory.Exists(tempDir))
          Directory.Delete(tempDir, true);
      }
    }
  }
}
