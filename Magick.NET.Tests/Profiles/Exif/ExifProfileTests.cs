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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class ExifProfileTests
	{
		//===========================================================================================
		private const string _Category = "ExifProfile";
		//===========================================================================================
		private static void TestProfile(ExifProfile profile)
		{
			Assert.IsNotNull(profile);

			Assert.AreEqual(44, profile.Values.Count());

			foreach (ExifValue value in profile.Values)
			{
				Assert.IsNotNull(value.Value);

				if (value.Tag == ExifTag.Software)
					Assert.AreEqual("Adobe Photoshop 7.0", value.ToString());

				if (value.Tag == ExifTag.XResolution)
					Assert.AreEqual(300.0, value.Value);
			}
		}
		//===========================================================================================
		private static void TestValue(ExifValue value, string expected)
		{
			Assert.IsNotNull(value);
			Assert.AreEqual(expected, value.Value);
		}
		//===========================================================================================
		private static void TestValue(ExifValue value, double expected)
		{
			Assert.IsNotNull(value);
			Assert.AreEqual(expected, value.Value);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			using (MemoryStream memStream = new MemoryStream())
			{
				using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
				{
					ExifProfile profile = image.GetExifProfile();
					Assert.IsNull(profile);

					profile = new ExifProfile();
					profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

					image.AddProfile(profile);

					profile = image.GetExifProfile();
					Assert.IsNotNull(profile);

					image.Write(memStream);
				}

				memStream.Position = 0;
				using (MagickImage image = new MagickImage(memStream))
				{
					ExifProfile profile = image.GetExifProfile();

					Assert.IsNotNull(profile);
					Assert.AreEqual(1, profile.Values.Count());

					ExifValue value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
					TestValue(value, "Dirk Lemstra");
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Infinity()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				profile.SetValue(ExifTag.ExposureBiasValue, double.PositiveInfinity);
				image.AddProfile(profile);

				profile = image.GetExifProfile();
				ExifValue value = profile.GetValue(ExifTag.ExposureBiasValue);
				Assert.IsNotNull(value);
				Assert.IsTrue(double.PositiveInfinity.Equals(value.Value));

				profile.SetValue(ExifTag.ExposureBiasValue, double.NegativeInfinity);
				image.AddProfile(profile);

				profile = image.GetExifProfile();
				value = profile.GetValue(ExifTag.ExposureBiasValue);
				Assert.IsNotNull(value);
				Assert.IsTrue(double.NegativeInfinity.Equals(value.Value));

				profile.SetValue(ExifTag.FlashEnergy, double.NegativeInfinity);
				image.AddProfile(profile);

				profile = image.GetExifProfile();
				value = profile.GetValue(ExifTag.FlashEnergy);
				Assert.IsNotNull(value);
				Assert.IsTrue(double.PositiveInfinity.Equals(value.Value));
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SetValue()
		{
			using (MemoryStream memStream = new MemoryStream())
			{
				using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
				{
					ExifProfile profile = image.GetExifProfile();
					TestProfile(profile);

					profile.SetValue(ExifTag.Software, "Magick.NET");

					ExifValue value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Software);
					TestValue(value, "Magick.NET");

					ExceptionAssert.Throws<ArgumentException>(delegate()
					{
						value.Value = 15;
					});

					profile.SetValue(ExifTag.ShutterSpeedValue, 75.55);

					value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.ShutterSpeedValue);
					TestValue(value, 75.55);

					ExceptionAssert.Throws<ArgumentException>(delegate()
					{
						value.Value = 75;
					});

					profile.SetValue(ExifTag.XResolution, 150.0);

					value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.XResolution);
					TestValue(value, 150.0);

					ExceptionAssert.Throws<ArgumentException>(delegate()
					{
						value.Value = "Magick.NET";
					});

					image.Density = new PointD(72);

					profile.SetValue(ExifTag.ReferenceBlackWhite, null);

					image.AddProfile(profile);

					image.Write(memStream);
				}

				memStream.Position = 0;
				using (MagickImage image = new MagickImage(memStream))
				{
					ExifProfile profile = image.GetExifProfile();

					Assert.IsNotNull(profile);
					Assert.AreEqual(43, profile.Values.Count());

					ExifValue value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Software);
					TestValue(value, "Magick.NET");

					value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.ShutterSpeedValue);
					TestValue(value, 75.55);

					value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.XResolution);
					TestValue(value, 72.0);

					profile.Parts = ExifParts.ExifTags;

					image.AddProfile(profile);

					memStream.Position = 0;
					image.Write(memStream);
				}

				memStream.Position = 0;
				using (MagickImage image = new MagickImage(memStream))
				{
					ExifProfile profile = image.GetExifProfile();

					Assert.IsNotNull(profile);
					Assert.AreEqual(24, profile.Values.Count());
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Thumbnail()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				Assert.IsNotNull(profile);

				using (MagickImage thumbnail = profile.CreateThumbnail())
				{
					Assert.IsNotNull(thumbnail);
					Assert.AreEqual(128, thumbnail.Width);
					Assert.AreEqual(85, thumbnail.Height);
					Assert.AreEqual(MagickFormat.Jpeg, thumbnail.Format);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Values()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				ExifProfile profile = image.GetExifProfile();
				TestProfile(profile);

				using (MagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
				{
					Assert.IsNull(emptyImage.GetExifProfile());
					emptyImage.AddProfile(profile);

					profile = emptyImage.GetExifProfile();
					TestProfile(profile);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
