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
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public sealed class ImageProfileTests
	{
		//===========================================================================================
		private const string _Category = "ImageProfileTests";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new ImageProfile(null, Files.SnakewarePNG);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new ImageProfile("name", (byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new ImageProfile("name", (Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new ImageProfile("name", (string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new ImageProfile("name", new byte[] { });
			});
		}
		//===========================================================================================
	}
	//==============================================================================================
}
