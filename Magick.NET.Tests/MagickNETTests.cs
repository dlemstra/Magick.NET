//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
	//==============================================================================================
	[TestClass]
	public class MagickNETTests
	{
		//===========================================================================================
		private const string _Category = "MagickNET";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_GetFormatInfo()
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
				Assert.Fail("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats));
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Features()
		{
#if Q8
			Assert.AreEqual("DPC Modules OpenCL OpenMP", MagickNET.Features);
#elif Q16
			Assert.AreEqual("DPC Modules OpenCL OpenMP", MagickNET.Features);
#elif Q16HDRI
			Assert.AreEqual("DPC Modules HDRI OpenCL OpenMP", MagickNET.Features);
#else
#error Not implemented!
#endif
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Initialize()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickNET.Initialize(null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickNET.Initialize("Invalid");
			});

			string path = Path.GetDirectoryName(GetType().Assembly.Location) + @"..\..\..\..\..\Magick.NET\Resources\xml";
			foreach (string fileName in Directory.GetFiles(path, "*.xml"))
			{
				string tempFile = fileName + ".tmp";

				if (File.Exists(tempFile))
					File.Delete(tempFile);

				File.Move(fileName, tempFile);

				ExceptionAssert.Throws<ArgumentException>(delegate()
				{
					MagickNET.Initialize(path);
				}, "MagickNET._ImageMagickFiles does not contain: " + Path.GetFileName(fileName));

				File.Move(tempFile, fileName);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Log()
		{
			using (MagickImage image = new MagickImage(Files.SnakewarePNG))
			{
				int count = 0;
				EventHandler<LogEventArgs> logDelegate = delegate(object sender, LogEventArgs arguments)
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
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_OpenCL()
		{
			Assert.AreEqual(MagickNET.UseOpenCL, true);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SupportedFormats()
		{
			foreach (MagickFormatInfo formatInfo in MagickNET.SupportedFormats)
			{
				Assert.AreNotEqual(MagickFormat.Unknown, formatInfo.Format, "Unknown format: " + formatInfo.Description);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Version()
		{
			StringAssert.Contains(MagickNET.Version, "x86");

#if NET20
			StringAssert.Contains(MagickNET.Version, "net20");
#else
			StringAssert.Contains(MagickNET.Version, "net40-client");
#endif

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
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SetTempDirectory()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickNET.SetTempDirectory(null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickNET.SetTempDirectory("Invalid");
			});

			MagickNET.SetTempDirectory(Path.GetTempPath());
		}
		//===========================================================================================
	}
	//==============================================================================================
}
