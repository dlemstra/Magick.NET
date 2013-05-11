//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickNETTests
	{
		//===========================================================================================
		[TestMethod]
		public void GetFormatInfo()
		{
			foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
			{
				if (format == MagickFormat.Unknown)
					continue;

				MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
				Assert.IsNotNull(formatInfo, "Cannot find MagickFormatInfo for: " + format);
			}
		}
		//===========================================================================================
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InitializeNull()
		{
			MagickNET.Initialize(null);
		}
		//===========================================================================================
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InitializeInvalidFolder()
		{
			MagickNET.Initialize("Invalid");
		}
		//===========================================================================================
		[TestMethod]
		public void SupportedFormats()
		{
			foreach (MagickFormatInfo formatInfo in MagickNET.SupportedFormats)
			{
				Assert.AreNotEqual(MagickFormat.Unknown, formatInfo.Format, "Unknown format: " + formatInfo.Description);
			}
		}
		//===========================================================================================
		[TestMethod]
		public void Version()
		{
			Assert.IsTrue(!string.IsNullOrEmpty(MagickNET.Version));
			Assert.IsTrue(MagickNET.Version.Contains("Q16"));
			Assert.IsTrue(MagickNET.Version.Contains("v4.0"));
			Assert.IsTrue(MagickNET.Version.Contains("x86"));
		}
		//===========================================================================================
	}
	//==============================================================================================
}
