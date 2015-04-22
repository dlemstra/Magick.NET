//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class JpegOptimizerTests
	{
		//===========================================================================================
		private const string _Category = "JpegOptimizer";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new JpegOptimizer((FileInfo)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new JpegOptimizer((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new JpegOptimizer("");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new JpegOptimizer(Files.Missing);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_LosslessCompress()
		{
			FileInfo tempFile = new FileInfo(Files.TemporaryImageMagickJPG);
			try
			{
				JpegOptimizer optimizer = new JpegOptimizer(tempFile);

				long before = tempFile.Length;
				optimizer.LosslessCompress();

				tempFile.Refresh();
				long after = tempFile.Length;
				Assert.AreNotEqual(before, after);
			}
			finally
			{
				if (tempFile.Exists)
					tempFile.Delete();
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
