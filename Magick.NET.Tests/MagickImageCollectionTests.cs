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
	public class MagickImageCollectionTests
	{
		//===========================================================================================
		private const string _Category = "MagickImageCollection";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickImageCollection collection = new MagickImageCollection(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageCollection collection = new MagickImageCollection((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageCollection collection = new MagickImageCollection((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				MagickImageCollection collection = new MagickImageCollection((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				MagickImageCollection collection = new MagickImageCollection(Settings.ImageDir);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void Test_Dispose()
		{
			MagickImageCollection collection = new MagickImageCollection();
			collection.Dispose();
			Assert.AreEqual(0, collection.Count);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Read()
		{
			MagickImageCollection collection = new MagickImageCollection();

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				collection.Read(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				collection.Read((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				collection.Read((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				collection.Read((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				collection.Read(Settings.ImageDir);
			});

			collection.Read(Settings.ImageDir + "RoseSparkle.gif");
			Assert.AreEqual(3, collection.Count);

			collection.Read(Settings.ImageDir + "RoseSparkle.gif");
			Assert.AreEqual(3, collection.Count);

			collection.Dispose();
		}
	}
	//==============================================================================================
}