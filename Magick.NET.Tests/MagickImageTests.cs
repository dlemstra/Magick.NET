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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickImageTests
	{
		//===========================================================================================
		private const string _Category = "MagickImage";
		//===========================================================================================
		private static void Test_Clone(MagickImage first, MagickImage second)
		{
			Assert.AreEqual(first, second);
			second.Format = MagickFormat.Jp2;
			Assert.AreEqual(first.Format, MagickFormat.Png);
			Assert.AreEqual(second.Format, MagickFormat.Jp2);
			second.Dispose();
			Assert.AreEqual(first.Format, MagickFormat.Png);
		}
		//===========================================================================================
		private static void Test_ToBitmap(MagickImage image, ImageFormat format)
		{
			using (Bitmap bmp = image.ToBitmap(format))
			{
				Assert.AreEqual(format, bmp.RawFormat);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Artifact()
		{
			using (MagickImage image = new MagickImage(Files.SnakewarePNG))
			{
				Assert.IsNull(image.GetArtifact("test"));

				image.SetArtifact("test", "");
				Assert.AreEqual(null, image.GetArtifact("test"));

				image.SetArtifact("test", "123");
				Assert.AreEqual("123", image.GetArtifact("test"));
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Attribute()
		{
			using (MagickImage image = new MagickImage(Files.SnakewarePNG))
			{
				Assert.IsNull(image.GetAttribute("test"));

				image.SetAttribute("test", "");
				Assert.AreEqual(null, image.GetAttribute("test"));

				image.SetAttribute("test", "123");
				Assert.AreEqual("123", image.GetAttribute("test"));
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_BitDepth()
		{
			using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
			{
				Assert.AreEqual(8, image.BitDepth());

				image.Threshold(0.5);
				Assert.AreEqual(1, image.BitDepth());
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Clip()
		{
			using (MagickImage image = new MagickImage(Files.InvitationTif))
			{
				image.Clip("Pad A", false);
				Assert.IsNotNull(image.ClipMask);

				using (PixelCollection pixels = image.ClipMask.GetReadOnlyPixels())
				{
					Pixel pixelA = pixels.GetPixel(0, 0);
					Pixel pixelB = pixels.GetPixel(pixels.Width - 1, pixels.Height - 1);
					for (int i = 0; i < 3; i++)
					{
						Assert.AreEqual(0, pixelA.GetChannel(i));
						Assert.AreEqual(0, pixelB.GetChannel(i));
					}
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Clone()
		{
			MagickImage first = new MagickImage(Files.SnakewarePNG);
			MagickImage second = first.Clone();

			Test_Clone(first, second);

			second = new MagickImage(first);
			Test_Clone(first, second);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickImage(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImage((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImage((Bitmap)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImage((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				new MagickImage((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickImage(Files.Missing);
			});

			using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
			{
				using (MagickImage image = new MagickImage(bitmap))
				{
					Assert.AreEqual(MagickFormat.Png, image.Format);
				}
			}

			using (Bitmap bitmap = new Bitmap(100, 100, PixelFormat.Format24bppRgb))
			{
				using (MagickImage image = new MagickImage(bitmap))
				{
					Assert.AreEqual(MagickFormat.Bmp, image.Format);
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Compare()
		{
			MagickImage first = new MagickImage(Files.SnakewarePNG);
			MagickImage second = first.Clone();

			MagickErrorInfo same = first.Compare(second);
			Assert.IsNotNull(same);
			Assert.AreEqual(0, same.MeanErrorPerPixel);

			double distortion = first.Compare(second, Metric.AbsoluteError);
			Assert.AreEqual(0, distortion);

			first.Threshold(0.5);
			MagickErrorInfo different = first.Compare(second);
			Assert.IsNotNull(different);
			Assert.AreNotEqual(0, different.MeanErrorPerPixel);

			distortion = first.Compare(second, Metric.AbsoluteError);
			Assert.AreNotEqual(0, distortion);

			MagickImage difference = new MagickImage();
			distortion = first.Compare(second, Metric.RootMeanSquaredError, difference);
			Assert.AreNotEqual(0, distortion);
			Assert.AreNotEqual(first, difference);
			Assert.AreNotEqual(second, difference);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void Test_Dispose()
		{
			MagickImage image = new MagickImage();
			image.Dispose();
			image.Verbose = true;
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Extend()
		{
			using (MagickImage image = new MagickImage())
			{
				image.Read(Files.RedPNG);
				image.Resize(new MagickGeometry(100, 100));
				Assert.AreEqual(100, image.Width);
				Assert.AreEqual(33, image.Height);

				image.Extent(100, 100, Gravity.Center);
				Assert.AreEqual(100, image.Width);
				Assert.AreEqual(100, image.Height);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_FillColor()
		{
			using (MagickImage image = new MagickImage(MagickColor.Transparent, 100, 100))
			{
				image.FillColor = null;

				MagickColor colorA;
				image.FillColor = Color.Red;
				image.Read("caption:Magick.NET");
				using (PixelCollection pixels = image.GetReadOnlyPixels())
				{
					Pixel pixel = pixels.GetPixel(69, 6);
					colorA = pixel.ToColor();
				}

				MagickColor colorB;
				image.FillColor = Color.Yellow;
				image.Read("caption:Magick.NET");
				using (PixelCollection pixels = image.GetReadOnlyPixels())
				{
					Pixel pixel = pixels.GetPixel(69, 6);
					colorB = pixel.ToColor();
				}

				ColorAssert.AreNotEqual(colorA, colorB);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_FormatExpression()
		{
			using (MagickImage image = new MagickImage(Files.RedPNG))
			{
				ExceptionAssert.Throws<ArgumentNullException>(delegate()
				{
					image.FormatExpression(null);
				});

				Assert.AreEqual("FOO", image.FormatExpression("FOO"));
				Assert.AreEqual(null, image.FormatExpression("%FOO"));
				Assert.AreEqual("fd8c44fe1b88ad5e28e26bfd11da116a48bceca4be53cba317c93406e1a85f06", image.FormatExpression("%#"));
			}

			using (MagickImage image = new MagickImage(Files.InvitationTif))
			{
				Assert.AreEqual("sRGB IEC61966-2.1", image.FormatExpression("%[profile:icc]"));
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_FormatInfo()
		{
			MagickImage image = new MagickImage(Files.SnakewarePNG);
			MagickFormatInfo info = image.FormatInfo;

			Assert.IsNotNull(info);
			Assert.AreEqual(MagickFormat.Png, info.Format);
			Assert.AreEqual("image/png", info.MimeType);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Histogram()
		{
			MagickImage image = new MagickImage(Files.RedPNG);
			Dictionary<MagickColor, int> histogram = image.Histogram();

			Assert.IsNotNull(histogram);
			Assert.AreEqual(3, histogram.Count);

			MagickColor red = new MagickColor(Quantum.Max, 0, 0);
			MagickColor alphaRed = new MagickColor(Quantum.Max, 0, 0, 0);
			MagickColor halfAlphaRed = new MagickColor("#FF000080");

			foreach (MagickColor color in histogram.Keys)
			{
				if (color == red)
					Assert.AreEqual(50000, histogram[color]);
				else if (color == alphaRed)
					Assert.AreEqual(30000, histogram[color]);
				else if (color == halfAlphaRed)
					Assert.AreEqual(40000, histogram[color]);
				else
					Assert.Fail("Invalid color: " + color.ToString());
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IComparable()
		{
			MagickImage first = new MagickImage(Color.Red, 10, 5);

			Assert.AreEqual(0, first.CompareTo(first));
			Assert.AreEqual(1, first.CompareTo(null));
			Assert.IsFalse(first < null);
			Assert.IsFalse(first <= null);
			Assert.IsTrue(first > null);
			Assert.IsTrue(first >= null);
			Assert.IsTrue(null < first);
			Assert.IsTrue(null <= first);
			Assert.IsFalse(null > first);
			Assert.IsFalse(null >= first);

			MagickImage second = new MagickImage(Color.Green, 5, 5);

			Assert.AreEqual(1, first.CompareTo(second));
			Assert.IsFalse(first < second);
			Assert.IsFalse(first <= second);
			Assert.IsTrue(first > second);
			Assert.IsTrue(first >= second);

			second = new MagickImage(Color.Red, 5, 10);

			Assert.AreEqual(0, first.CompareTo(second));
			Assert.IsFalse(first == second);
			Assert.IsFalse(first < second);
			Assert.IsTrue(first <= second);
			Assert.IsFalse(first > second);
			Assert.IsTrue(first >= second);

			first.Dispose();
			second.Dispose();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			MagickImage first = new MagickImage(Color.Red, 10, 10);

			Assert.IsFalse(first == null);
			Assert.IsFalse(first.Equals(null));
			Assert.IsTrue(first.Equals(first));
			Assert.IsTrue(first.Equals((object)first));

			MagickImage second = new MagickImage(Color.Red, 10, 10);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));

			second = new MagickImage(Color.Green, 10, 10);

			Assert.IsTrue(first != second);
			Assert.IsFalse(first.Equals(second));

			first.Dispose();
			second.Dispose();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_LiquidRescale()
		{
			using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
			{
				MagickGeometry geometry = new MagickGeometry(128, 64);
				geometry.IgnoreAspectRatio = true;

				image.LiquidRescale(geometry);
				Assert.AreEqual(128, image.Width);
				Assert.AreEqual(64, image.Height);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ProfileNames()
		{
			using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
			{
				IEnumerable<string> names = image.ProfileNames;
				Assert.IsNotNull(names);
				Assert.AreEqual(5, names.Count());
				Assert.AreEqual("8bim,exif,icc,iptc,xmp", string.Join(",", from name in names
																							  orderby name
																							  select name));
			}

			using (MagickImage image = new MagickImage(Files.RedPNG))
			{
				IEnumerable<string> names = image.ProfileNames;
				Assert.IsNotNull(names);
				Assert.AreEqual(0, names.Count());
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Read()
		{
			MagickImage image = new MagickImage();

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				image.Read(new byte[0]);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((byte[])null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((Bitmap)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				image.Read((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				image.Read(Files.Missing);
			});

			image.Read(File.ReadAllBytes(Files.SnakewarePNG));

			using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
			{
				image.Read(bitmap);
				Assert.AreEqual(MagickFormat.Png, image.Format);
			}

			using (Bitmap bitmap = new Bitmap(100, 100, PixelFormat.Format24bppRgb))
			{
				image.Read(bitmap);
				Assert.AreEqual(MagickFormat.Bmp, image.Format);
			}

			using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
			{
				image.Read(fs);
			}

			image.Read(Files.SnakewarePNG);

			image.Read("rose:");

			image.Read(Files.RoseSparkleGIF);
			Assert.AreEqual("RöseSparkle.gif", Path.GetFileName(image.FileName));

			image.Dispose();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ReadWarning()
		{
			using (MagickImage image = new MagickImage(Files.EightBimTIF))
			{
				Assert.IsNotNull(image.ReadWarning);
			}

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				Assert.IsNull(image.ReadWarning);
			}

			using (MagickImage image = new MagickImage())
			{
				MagickWarningException exception = image.Read(Files.EightBimTIF);
				Assert.IsNotNull(exception);
				Assert.IsNotNull(image.ReadWarning);
			}

			using (MagickImage image = new MagickImage())
			{
				MagickWarningException exception = image.Read(Files.ImageMagickJPG);
				Assert.IsNull(exception);
				Assert.IsNull(image.ReadWarning);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Resize()
		{
			using (MagickImage image = new MagickImage())
			{
				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry(64, 64));
				Assert.AreEqual(64, image.Width);
				Assert.AreEqual(64, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(2.0);
				Assert.AreEqual(256, image.Width);
				Assert.AreEqual(256, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(32, 32);
				Assert.AreEqual(32, image.Width);
				Assert.AreEqual(32, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry("5x10!"));
				Assert.AreEqual(5, image.Width);
				Assert.AreEqual(10, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry("32x32<"));
				Assert.AreEqual(128, image.Width);
				Assert.AreEqual(128, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry("256x256<"));
				Assert.AreEqual(256, image.Width);
				Assert.AreEqual(256, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry("32x32>"));
				Assert.AreEqual(32, image.Width);
				Assert.AreEqual(32, image.Height);

				image.Read(Files.MagickNETIconPNG);
				image.Resize(new MagickGeometry("256x256>"));
				Assert.AreEqual(128, image.Width);
				Assert.AreEqual(128, image.Height);

				image.Read(Files.SnakewarePNG);
				image.Resize(new MagickGeometry("4096@"));
				Assert.IsTrue((image.Width * image.Height) < 4096);

				Percentage percentage = new Percentage(-0.5);
				ExceptionAssert.Throws<ArgumentException>(delegate()
				{
					image.Resize(percentage);
				});
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Separate()
		{
			using (MagickImage rose = new MagickImage("rose:"))
			{
				int i = 0;
				foreach (MagickImage image in rose.Separate())
				{
					i++;
					image.Dispose();
				}

				Assert.AreEqual(4, i);

				i = 0;
				foreach (MagickImage image in rose.Separate(Channels.RGB))
				{
					i++;
					image.Dispose();
				}

				Assert.AreEqual(3, i);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_SubImageSearch()
		{
			using (MagickImageCollection images = new MagickImageCollection())
			{
				images.Add(new MagickImage(Color.Green, 2, 2));
				images.Add(new MagickImage(Color.Red, 2, 2));

				using (MagickImage combined = images.AppendHorizontally())
				{
					using (MagickSearchResult searchResult = combined.SubImageSearch(new MagickImage(Color.Red, 0, 0), Metric.RootMeanSquaredError))
					{
						Assert.IsNotNull(searchResult);
						Assert.IsNotNull(searchResult.SimilarityImage);
						Assert.IsNotNull(searchResult.BestMatch);
						Assert.AreEqual(0.0, searchResult.SimilarityMetric);
						Assert.AreEqual(2, searchResult.BestMatch.X);
						Assert.AreEqual(0, searchResult.BestMatch.Y);
					}
				}
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_ToBitmap()
		{
			using (MagickImage image = new MagickImage(Color.Red, 10, 10))
			{
				ExceptionAssert.Throws<NotSupportedException>(delegate()
				{
					image.ToBitmap(ImageFormat.Exif);
				});

				Bitmap bmp = image.ToBitmap();
				Assert.AreEqual(ImageFormat.Bmp, bmp.RawFormat);
				ColorAssert.AreEqual(Color.Red, bmp.GetPixel(0, 0));
				bmp.Dispose();

				Test_ToBitmap(image, ImageFormat.Bmp);
				Test_ToBitmap(image, ImageFormat.Gif);
				Test_ToBitmap(image, ImageFormat.Icon);
				Test_ToBitmap(image, ImageFormat.Jpeg);
				Test_ToBitmap(image, ImageFormat.Png);
				Test_ToBitmap(image, ImageFormat.Tiff);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Thumbnail()
		{
			using (MagickImage image = new MagickImage(Files.SnakewarePNG))
			{
				image.Thumbnail(100, 100);
				Assert.AreEqual(image.Width, 100);
				Assert.AreEqual(image.Height, 58);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Warning()
		{
			int count = 0;
			EventHandler<WarningEventArgs> warningDelegate = delegate(object sender, WarningEventArgs arguments)
			{
				Assert.IsNotNull(sender);
				Assert.IsNotNull(arguments);
				Assert.IsNotNull(arguments.Message);
				Assert.IsNotNull(arguments.Exception);
				Assert.AreNotEqual("", arguments.Message);

				count++;
			};

			using (MagickImage image = new MagickImage())
			{
				image.Warning += warningDelegate;
				MagickWarningException exception = image.Read(Files.EightBimTIF);

				Assert.IsNotNull(exception);
				Assert.AreNotEqual(0, count);

				int expectedCount = count;
				image.Warning -= warningDelegate;
				exception = image.Read(Files.EightBimTIF);

				Assert.IsNotNull(exception);
				Assert.AreEqual(expectedCount, count);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Write()
		{
			using (MagickImage image = new MagickImage(Files.SnakewarePNG))
			{
				using (MemoryStream memStream = new MemoryStream())
				{
					image.Write(memStream);

					Assert.AreEqual(image.FileSize, memStream.Length);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
