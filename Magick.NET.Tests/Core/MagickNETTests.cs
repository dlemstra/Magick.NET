//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public partial class MagickNETTests
  {
    private const string _Category = "MagickNET";

    [TestMethod, TestCategory(_Category)]
    public void Test_Features()
    {
#if Q8 || Q16
#if DEBUG_TEST
      Assert.AreEqual("Debug Cipher DPC OpenCL ", MagickNET.Features);
#else
      Assert.AreEqual("Cipher DPC OpenCL ", MagickNET.Features);
#endif
#elif Q16HDRI
#if DEBUG_TEST
      Assert.AreEqual("Debug Cipher DPC HDRI OpenCL ", MagickNET.Features);
#else
      Assert.AreEqual("Cipher DPC HDRI OpenCL ", MagickNET.Features);
#endif
#else
#error Not implemented!
#endif
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Initialize()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        MagickNET.Initialize(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        MagickNET.Initialize("Invalid");
      });

      string path = Files.Root + @"..\Magick.NET.Native\Resources\xml";
      foreach (string fileName in Directory.GetFiles(path, "*.xml"))
      {
        string tempFile = fileName + ".tmp";

        if (File.Exists(tempFile))
          File.Delete(tempFile);

        File.Move(fileName, tempFile);

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          MagickNET.Initialize(path);
        }, "MagickNET._ImageMagickFiles does not contain: " + Path.GetFileName(fileName));

        File.Move(tempFile, fileName);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Log()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        int count = 0;
        EventHandler<LogEventArgs> logDelegate = delegate (object sender, LogEventArgs arguments)
        {
          Assert.IsNull(sender);
          Assert.IsNotNull(arguments);
          Assert.AreNotEqual(LogEvents.None, arguments.EventType);
          Assert.IsNotNull(arguments.Message);
          Assert.AreNotEqual(0, arguments.Message.Length);

          count++;
        };

        MagickNET.Log += logDelegate;

        image.Flip();
        Assert.AreEqual(0, count);

        MagickNET.SetLogEvents(LogEvents.All);

        image.Flip();
        Assert.AreNotEqual(0, count);

        MagickNET.Log -= logDelegate;
        count = 0;

        image.Flip();
        Assert.AreEqual(0, count);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_MagickFormats()
    {
      List<string> missingFormats = new List<string>();

      foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
      {
        if (format == MagickFormat.Unknown)
          continue;

        MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
        if (formatInfo == null)
          missingFormats.Add(format.ToString());
      }

      if (missingFormats.Count > 0)
        Assert.Fail("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_RandomSeed()
    {
      using (MagickImage first = new MagickImage("plasma:red", 10, 10))
      {
        using (MagickImage second = new MagickImage("plasma:red", 10, 10))
        {
          Assert.AreNotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
        }
      }

      MagickNET.SetRandomSeed(1337);

      using (MagickImage first = new MagickImage("plasma:red", 10, 10))
      {
        using (MagickImage second = new MagickImage("plasma:red", 10, 10))
        {
          Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_SupportedFormats()
    {
      foreach (MagickFormatInfo formatInfo in MagickNET.SupportedFormats)
      {
        Assert.AreNotEqual(MagickFormat.Unknown, formatInfo.Format, "Unknown format: " + formatInfo.Description + " (" + formatInfo.Module + ")");
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Version_Quantum()
    {
#if Q8
      StringAssert.Contains(MagickNET.Version, "Q8");
#elif Q16
      StringAssert.Contains(MagickNET.Version, "Q16");
#elif Q16HDRI
      StringAssert.Contains(MagickNET.Version, "Q16-HDRI");
#else
#error Not implemented!
#endif
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_SetTempDirectory()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        MagickNET.SetTempDirectory(null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        MagickNET.SetTempDirectory("Invalid");
      });

      MagickNET.SetTempDirectory(Path.GetTempPath());
    }
  }
}
