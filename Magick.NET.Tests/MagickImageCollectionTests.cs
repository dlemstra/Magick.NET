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
using System.Drawing;
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
				new MagickImageCollection(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImageCollection((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImageCollection((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImageCollection((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickImageCollection(Files.Missing);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Combine()
		{
			using (MagickImage rose = new MagickImage("rose:"))
			{
				using (MagickImageCollection collection = new MagickImageCollection(rose.Separate(Channels.RGB)))
				{
					Assert.AreEqual(3, collection.Count);

					MagickImage image = collection.Merge();
					Assert.AreNotEqual(rose.TotalColors, image.TotalColors);

					image = collection.Combine();
					Assert.AreEqual(rose.TotalColors, image.TotalColors);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_CopyTo()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				collection.Add(new MagickImage(Files.SnakewarePNG));
				collection.Add(new MagickImage(Files.RoseSparkleGIF));

				MagickImage[] images = new MagickImage[collection.Count];
				collection.CopyTo(images, 0);

				Assert.AreEqual(collection[0], images[0]);
				Assert.AreNotEqual(collection[0], images[1]);

				collection.CopyTo(images, 1);
				Assert.AreEqual(collection[0], images[0]);
				Assert.AreEqual(collection[0], images[1]);

				images = new MagickImage[collection.Count + 1];
				collection.CopyTo(images, 0);

				images = new MagickImage[1];
				collection.CopyTo(images, 0);

				ExceptionAssert.Throws<ArgumentNullException>(delegate()
				{
					collection.CopyTo(null, -1);
				});

				ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate()
				{
					collection.CopyTo(images, -1);
				});

			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void Test_Dispose()
		{
			MagickImage image = new MagickImage(Color.Red, 10, 10);

			MagickImageCollection collection = new MagickImageCollection();
			collection.Add(image);
			collection.Dispose();

			Assert.AreEqual(0, collection.Count);
			image.Wave();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Index()
		{
			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				for (int i = 0; i < collection.Count; i++)
				{
					collection[i].Resize(35, 23);
					Assert.AreEqual(35, collection[i].Width);

					collection[i] = collection[i];
					Assert.AreEqual(35, collection[i].Width);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Merge()
		{
			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				using (MagickImage first = collection.Merge())
				{
					Assert.AreEqual(collection[0].Width, first.Width);
					Assert.AreEqual(collection[0].Height, first.Height);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Morph()
		{
			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				Assert.AreEqual(3, collection.Count);

				using (MagickImageCollection morphed = collection.Morph(2))
				{
					Assert.AreEqual(7, morphed.Count);
				}
			}
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
				collection.Read(Files.Missing);
			});

			collection.Read(File.ReadAllBytes(Files.RoseSparkleGIF));
			Assert.AreEqual(3, collection.Count);

			using (FileStream fs = File.OpenRead(Files.RoseSparkleGIF))
			{
				collection.Read(fs);
				Assert.AreEqual(3, collection.Count);
			}

			collection.Read(Files.RoseSparkleGIF);
			Assert.AreEqual(3, collection.Count);

			collection.Dispose();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ReadSettings()
		{
			using (MagickImageCollection collection = new MagickImageCollection())
			{
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new MagickGeometry(150, 150);

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(150, collection[0].Density.Width);

				settings = new MagickReadSettings();
				settings.FrameIndex = 1;

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(1, collection.Count);

				settings = new MagickReadSettings();
				settings.FrameIndex = 1;
				settings.FrameCount = 2;

				collection.Read(Files.RoseSparkleGIF, settings);

				Assert.AreEqual(2, collection.Count);

				settings = null;
				collection.Read(Files.RoseSparkleGIF, settings);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Remove()
		{
			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				MagickImage first = collection[0];
				collection.Remove(first);

				Assert.AreEqual(2, collection.Count);
				Assert.AreEqual(-1, collection.IndexOf(first));

				first = collection[0];
				collection.RemoveAt(0);

				Assert.AreEqual(1, collection.Count);
				Assert.AreEqual(-1, collection.IndexOf(first));
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_RePage()
		{
			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				collection[0].Page = new MagickGeometry("0x0+10+10");

				Assert.AreEqual(10, collection[0].Page.X);
				Assert.AreEqual(10, collection[0].Page.Y);

				collection.RePage();

				Assert.AreEqual(0, collection[0].Page.X);
				Assert.AreEqual(0, collection[0].Page.Y);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Write()
		{
			long fileSize;
			using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
			{
				fileSize = image.FileSize;
			}

			using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
			{
				using (MemoryStream memStream = new MemoryStream())
				{
					collection.Write(memStream);

					Assert.AreEqual(fileSize, memStream.Length);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}