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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;
using System.Web;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickHandlerTests
  {
    private MagickFormatInfo PngFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Png);

    [TestMethod]
    public void Test_CacheControlMode()
    {
      string configCache = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache""/>";

      string configNoCache = @"
<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"">
  <clientCache cacheControlMode=""NoControl""/>
</magick.net.web>";

      HttpRequest request = new HttpRequest("foo", "https://bar", "");

      using (MemoryStream memStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter(memStream))
        {
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          MagickWebSettings settings = TestSectionLoader.Load(configNoCache);
          TestMagickHandler handler = new TestMagickHandler(settings, null, PngFormatInfo);
          handler.ProcessRequest(context);

          Assert.AreNotEqual(HttpCacheability.Public, response.Cache.GetCacheability());

          settings = TestSectionLoader.Load(configCache);
          handler = new TestMagickHandler(settings, null, PngFormatInfo);
          handler.ProcessRequest(context);

          Assert.AreEqual(HttpCacheability.Public, response.Cache.GetCacheability());
        }
      }
    }

    [TestMethod]
    public void Test_Version()
    {
      string config = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"" showVersion=""true""/>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      TestMagickHandler handler = new TestMagickHandler(settings, null, PngFormatInfo);

      HttpRequest request = new HttpRequest("foo", "https://bar", "");

      using (MemoryStream memStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter(memStream))
        {
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          handler.ProcessRequest(context);

          Assert.AreEqual("image/png", response.ContentType);
          Assert.IsNotNull(response.GetHeader("X-Magick"));
        }
      }
    }

    [TestMethod]
    public void Test_Write304()
    {
      string config = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache""/>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      TestMagickHandler handler = new TestMagickHandler(settings, null, PngFormatInfo);

      HttpRequest request = new HttpRequest("foo", "https://bar", "");

      using (MemoryStream memStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter(memStream))
        {
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          FileInfo file = new FileInfo(handler.FileName);
          string modifiedSince = file.LastWriteTimeUtc.AddMinutes(1).ToString("r", CultureInfo.InvariantCulture);
          request.SetHeaders("If-Modified-Since", modifiedSince);
          handler.ProcessRequest(context);

          Assert.AreEqual(200, response.StatusCode);
          Assert.AreEqual(file.LastWriteTimeUtc.ToString(), response.Cache.GetLastModified().ToString());

          request.SetHeaders("If-Modified-Since", "foobar");
          handler.ProcessRequest(context);

          Assert.AreEqual(200, response.StatusCode);

          modifiedSince = file.LastWriteTimeUtc.AddMinutes(1).ToString("r", CultureInfo.InvariantCulture) + "; foo";
          request.SetHeaders("If-Modified-Since", modifiedSince);
          handler.ProcessRequest(context);

          Assert.AreEqual(200, response.StatusCode);

          modifiedSince = file.LastWriteTimeUtc.ToString("r", CultureInfo.InvariantCulture);
          request.SetHeaders("If-Modified-Since", modifiedSince);
          handler.ProcessRequest(context);

          Assert.AreEqual(304, response.StatusCode);

          string tempFile = Path.GetTempFileName();
          try
          {
            File.Copy(handler.FileName, tempFile, true);
            File.SetLastWriteTimeUtc(tempFile, DateTime.Now.AddYears(2));

            handler.FileName = tempFile;

            request.SetHeaders("If-Modified-Since", modifiedSince);
            handler.ProcessRequest(context);

            Assert.AreEqual(304, response.StatusCode);
            Assert.AreEqual(DateTime.Now.Year, response.Cache.GetLastModified().Year);
          }
          finally
          {
            if (File.Exists(tempFile))
              File.Delete(tempFile);
          }
        }
      }
    }

    [TestMethod]
    public void Test_WriteFile()
    {
      string config = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache""/>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      TestMagickHandler handler = new TestMagickHandler(settings, null, PngFormatInfo);

      HttpRequest request = new HttpRequest("foo", "https://bar", "");

      using (MemoryStream memStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter(memStream))
        {
          HttpResponse response = new HttpResponse(writer);
          HttpContext context = new HttpContext(request, response);

          Assert.IsFalse(handler.IsReusable);

          handler.ProcessRequest(null);

          handler.FileName = null;
          handler.ProcessRequest(context);

          handler.FileName = "";
          handler.ProcessRequest(context);

          handler.FileName = "missing";
          ExceptionAssert.Throws<ArgumentNullException>(() =>
          {
            handler.ProcessRequest(context);
          });
        }
      }
    }
  }
}
