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
	public class MagickNETTests : InitializeTests
	{
		//===========================================================================================
		private const string _Category = "MagickNET";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_GetFormatInfo()
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
		[TestMethod, TestCategory(_Category)]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Test_InitializeNull()
		{
			MagickNET.Initialize(null);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		[ExpectedException(typeof(ArgumentException))]
		public void Test_InitializeInvalidFolder()
		{
			MagickNET.Initialize("Invalid");
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
#else
#error Not implemented!
#endif
		}
		//===========================================================================================
	}
	//==============================================================================================
}
