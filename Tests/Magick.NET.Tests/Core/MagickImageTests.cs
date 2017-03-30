//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Linq;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
  [TestClass]
  public partial class MagickImageTests
  {
    private MagickImage CreatePallete()
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Add(new MagickImage(MagickColors.Red, 1, 1));
        images.Add(new MagickImage(MagickColors.Blue, 1, 1));
        images.Add(new MagickImage(MagickColors.Green, 1, 1));

        return images.AppendHorizontally();
      }
    }

    private static void ShouldNotRaiseWarning(object sender, WarningEventArgs arguments)
    {
      Assert.Fail(arguments.Message);
    }

    private static void ShouldRaiseWarning(object sender, WarningEventArgs arguments)
    {
      Assert.IsNotNull(arguments.Message);
    }

    private static void Test_Chromaticity(double expectedX, double expectedY, double expectedZ, PrimaryInfo info)
    {
      Assert.AreEqual(expectedX, info.X, 0.001, "X is not equal.");
      Assert.AreEqual(expectedY, info.Y, 0.001, "Y is not equal.");
      Assert.AreEqual(expectedZ, info.Z, 0.001, "Z is not equal.");
    }

    private static void Test_Clip(bool inside, QuantumType value)
    {
      using (MagickImage image = new MagickImage(Files.InvitationTif))
      {
        image.Alpha(AlphaOption.Transparent);
        image.Clip("Pad A", inside);
        image.Alpha(AlphaOption.Opaque);

        using (MagickImage mask = image.WriteMask)
        {
          Assert.IsNotNull(mask);
          Assert.AreEqual(false, mask.HasAlpha);

          using (PixelCollection pixels = mask.GetPixels())
          {
            MagickColor pixelA = pixels.GetPixel(0, 0).ToColor();
            MagickColor pixelB = pixels.GetPixel(mask.Width - 1, mask.Height - 1).ToColor();

            Assert.AreEqual(pixelA, pixelB);
            Assert.AreEqual(value, pixelA.R);
            Assert.AreEqual(value, pixelA.G);
            Assert.AreEqual(value, pixelA.B);

            MagickColor pixelC = pixels.GetPixel(mask.Width / 2, mask.Height / 2).ToColor();
            Assert.AreEqual(Quantum.Max - value, pixelC.R);
            Assert.AreEqual(Quantum.Max - value, pixelC.G);
            Assert.AreEqual(Quantum.Max - value, pixelC.B);
          }
        }
      }
    }

    private static void Test_Clone(MagickImage first, MagickImage second)
    {
      Assert.AreEqual(first, second);
      second.Format = MagickFormat.Jp2;
      Assert.AreEqual(first.Format, MagickFormat.Png);
      Assert.AreEqual(second.Format, MagickFormat.Jp2);
      second.Dispose();
      Assert.AreEqual(first.Format, MagickFormat.Png);
    }

    private static void Test_Clone_Area(MagickImage area, MagickImage part)
    {
      Assert.AreEqual(area.Width, part.Width);
      Assert.AreEqual(area.Height, part.Height);

      Assert.AreEqual(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
    }

    private void Test_Component(MagickImage image, ConnectedComponent component, int x, int y, int width, int height)
    {
      int delta = 2;

      Assert.AreEqual(x, component.X, delta);
      Assert.AreEqual(y, component.Y, delta);
      Assert.AreEqual(width, component.Width, delta);
      Assert.AreEqual(height, component.Height, delta);

      using (MagickImage area = image.Clone())
      {
        area.Crop(component.ToGeometry(10));
        Assert.AreEqual(width + 20, area.Width, delta);
        Assert.AreEqual(height + 20, area.Height, delta);
      }
    }

    private static void Test_Ping(MagickImage image)
    {
      ExceptionAssert.Throws<InvalidOperationException>(delegate ()
      {
        image.GetPixels();
      });

      ImageProfile profile = image.Get8BimProfile();
      Assert.IsNotNull(profile);
    }

    private static void Test_Separate_Composite(MagickImage image, ColorSpace colorSpace, byte value)
    {
      Assert.AreEqual(colorSpace, image.ColorSpace);

      using (PixelCollection pixels = image.GetPixels())
      {
        Pixel pixel = pixels.GetPixel(340, 260);
        ColorAssert.AreEqual(MagickColor.FromRgb(value, value, value), pixel.ToColor());
      }
    }

    [TestMethod]
    public void Test_AdaptiveBlur()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.AdaptiveBlur(10, 5);

#if Q8 || Q16
        ColorAssert.AreEqual(new MagickColor("#a868dfa7f8d7fe76"), image, 56, 68);
#elif Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#a8a8dfdff8f8"), image, 56, 68);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_AdaptiveResize()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.AdaptiveResize(100, 80);
        Assert.AreEqual(100, image.Width);
        Assert.AreEqual(80, image.Height);
        ColorAssert.AreEqual(new MagickColor("#347bbd"), image, 34, 46);
        ColorAssert.AreEqual(new MagickColor("#a8dff8"), image, 46, 46);
      }
    }

    [TestMethod]
    public void Test_AdaptiveSharpen()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.AdaptiveSharpen(10, 10);
#if Q8 || Q16
        ColorAssert.AreEqual(new MagickColor("#a985e09ff96a"), image, 56, 68);
#elif Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#a8a8dfdff8f8"), image, 56, 68);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_AdaptiveThreshold()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.AdaptiveThreshold(10, 10);
        ColorAssert.AreEqual(MagickColors.White, image, 50, 75);
      }
    }

    [TestMethod]
    public void Test_AddNoise()
    {
      MagickNET.SetRandomSeed(1337);

      using (MagickImage first = new MagickImage(Files.Builtin.Logo))
      {
        first.AddNoise(NoiseType.Laplacian);
        ColorAssert.AreNotEqual(MagickColors.White, first, 46, 62);

        using (MagickImage second = new MagickImage(Files.Builtin.Logo))
        {
          second.AddNoise(NoiseType.Laplacian, 2.0);
          ColorAssert.AreNotEqual(MagickColors.White, first, 46, 62);
          Assert.AreNotEqual(first, second);
        }
      }

      MagickNET.SetRandomSeed(-1);
    }

    [TestMethod]
    public void Test_AddProfile()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        ColorProfile profile = image.GetColorProfile();
        Assert.IsNull(profile);

        image.AddProfile(ColorProfile.SRGB);
        profile = image.GetColorProfile();
        Assert.IsNotNull(profile);
        Assert.AreEqual(3144, profile.ToByteArray().Length);

        image.AddProfile(ColorProfile.AppleRGB, false);
        profile = image.GetColorProfile();
        Assert.IsNotNull(profile);
        Assert.AreEqual(3144, profile.ToByteArray().Length);

        image.AddProfile(ColorProfile.AppleRGB);
        profile = image.GetColorProfile();
        Assert.IsNotNull(profile);
        Assert.AreEqual(552, profile.ToByteArray().Length);
      }
    }

    [TestMethod]
    public void Test_AffineTransform()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        DrawableAffine affineMatrix = new DrawableAffine(1, 0.5, 0, 0, 0, 0);
        image.AffineTransform(affineMatrix);
        Assert.AreEqual(482, image.Width);
        Assert.AreEqual(322, image.Height);
      }
    }

    [TestMethod]
    public void Test_Alpha()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        Assert.AreEqual(image.HasAlpha, false);

        image.Alpha(AlphaOption.Transparent);

        Assert.AreEqual(image.HasAlpha, true);
        ColorAssert.AreEqual(MagickColors.Transparent, image, 0, 0);

        image.BackgroundColor = new MagickColor("red");
        image.Alpha(AlphaOption.Background);
        image.Alpha(AlphaOption.Off);

        Assert.AreEqual(image.HasAlpha, false);
        ColorAssert.AreEqual(new MagickColor(Quantum.Max, 0, 0), image, 0, 0);
      }
    }

    [TestMethod]
    public void Test_AnimationDelay()
    {
      using (MagickImage image = new MagickImage())
      {
        image.AnimationDelay = 60;
        Assert.AreEqual(60, image.AnimationDelay);

        image.AnimationDelay = -1;
        Assert.AreEqual(60, image.AnimationDelay);

        image.AnimationDelay = 0;
        Assert.AreEqual(0, image.AnimationDelay);
      }
    }

    [TestMethod]
    public void Test_AnimationIterations()
    {
      using (MagickImage image = new MagickImage())
      {
        image.AnimationIterations = 60;
        Assert.AreEqual(60, image.AnimationIterations);

        image.AnimationIterations = -1;
        Assert.AreEqual(60, image.AnimationIterations);

        image.AnimationIterations = 0;
        Assert.AreEqual(0, image.AnimationIterations);
      }
    }

    [TestMethod]
    public void Test_Annotate()
    {
      using (MagickImage image = new MagickImage(MagickColors.Thistle, 200, 50))
      {
        image.Settings.FontPointsize = 20;
        image.Settings.FillColor = MagickColors.Purple;
        image.Settings.StrokeColor = MagickColors.Purple;
        image.Annotate("Magick.NET", Gravity.East);

        ColorAssert.AreEqual(MagickColors.Purple, image, 197, 17);
        ColorAssert.AreEqual(MagickColors.Thistle, image, 174, 17);
      }

      using (MagickImage image = new MagickImage(MagickColors.GhostWhite, 200, 200))
      {
        image.Settings.FontPointsize = 30;
        image.Settings.FillColor = MagickColors.Orange;
        image.Settings.StrokeColor = MagickColors.Orange;
        image.Annotate("Magick.NET", new MagickGeometry(75, 125, 0, 0), Gravity.Undefined, 45);

        ColorAssert.AreEqual(MagickColors.GhostWhite, image, 104, 83);
        ColorAssert.AreEqual(MagickColors.Orange, image, 118, 70);
      }
    }

    [TestMethod]
    public void Test_Artifact()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.GetArtifact("");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.GetArtifact(null);
        });

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.SetArtifact("", "test");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.SetArtifact(null, "test");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.SetArtifact("test", null);
        });

        Assert.IsNull(image.GetArtifact("test"));

        image.SetArtifact("test", "");
        Assert.AreEqual("", image.GetArtifact("test"));

        image.SetArtifact("test", "123");
        Assert.AreEqual("123", image.GetArtifact("test"));

        image.SetAttribute("foo", "bar");

        IEnumerable<string> names = image.ArtifactNames;
        Assert.AreEqual(1, names.Count());
        Assert.AreEqual("test", string.Join(",", (from name in names
                                                  orderby name
                                                  select name).ToArray()));
      }
    }

    [TestMethod]
    public void Test_Attribute()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.GetAttribute("");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.GetAttribute(null);
        });

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.SetAttribute("", "test");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.SetAttribute(null, "test");
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.SetAttribute("test", null);
        });

        Assert.IsNull(image.GetAttribute("test"));

        IEnumerable<string> names = image.AttributeNames;
        Assert.AreEqual(4, names.Count());

        image.SetAttribute("test", "");
        Assert.AreEqual("", image.GetAttribute("test"));

        image.SetAttribute("test", "123");
        Assert.AreEqual("123", image.GetAttribute("test"));

        image.SetArtifact("foo", "bar");

        names = image.AttributeNames;
        Assert.AreEqual(5, names.Count());
        Assert.AreEqual("date:create,date:modify,jpeg:colorspace,jpeg:sampling-factor,test", string.Join(",", (from name in names
                                                                                                               orderby name
                                                                                                               select name).ToArray()));
      }
    }

    [TestMethod]
    public void Test_AutoGamma()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.AutoGamma();

        ColorAssert.AreEqual(new MagickColor("#00000003017E"), image, 496, 429);
      }
    }

    [TestMethod]
    public void Test_AutoOrient()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
      {
        Assert.AreEqual(600, image.Width);
        Assert.AreEqual(400, image.Height);
        Assert.AreEqual(OrientationType.TopLeft, image.Orientation);

        ExifProfile profile = image.GetExifProfile();
        profile.SetValue(ExifTag.Orientation, (ushort)6);
        image.AddProfile(profile);

        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream);

          memStream.Position = 0;
          image.Read(memStream);

          Assert.AreEqual(600, image.Width);
          Assert.AreEqual(400, image.Height);
          Assert.AreEqual(OrientationType.RightTop, image.Orientation);

          image.AutoOrient();

          Assert.AreEqual(400, image.Width);
          Assert.AreEqual(600, image.Height);
          Assert.AreEqual(OrientationType.TopLeft, image.Orientation);
        }
      }
    }

    [TestMethod]
    public void Test_BlackPointCompensation()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
      {
        Assert.AreEqual(false, image.BlackPointCompensation);
        image.RenderingIntent = RenderingIntent.Relative;

        image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
        ColorAssert.AreEqual(new MagickColor("#da478d06323d"), image, 130, 100);

        image.Read(Files.FujiFilmFinePixS1ProPNG);

        Assert.AreEqual(false, image.BlackPointCompensation);
        image.RenderingIntent = RenderingIntent.Relative;
        image.BlackPointCompensation = true;

        image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);

        ColorAssert.AreEqual(new MagickColor("#cd0a844e3209"), image, 130, 100);
      }
    }

    [TestMethod]
    public void Test_BlackThreshold()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.BlackThreshold(new Percentage(90));
        ColorAssert.AreEqual(MagickColors.Black, image, 43, 74);
        ColorAssert.AreEqual(new MagickColor("#0000f8"), image, 60, 74);
      }
    }

    [TestMethod]
    public void Test_BackgroundColor()
    {
      using (MagickImage image = new MagickImage("xc:red", 1, 1))
      {
        ColorAssert.AreEqual(new MagickColor("White"), image.BackgroundColor);
      }

      MagickColor red = new MagickColor("Red");

      using (MagickImage image = new MagickImage(red, 1, 1))
      {
        ColorAssert.AreEqual(red, image.BackgroundColor);

        image.Read(new MagickColor("Purple"), 1, 1);

        ColorAssert.AreEqual(red, image.BackgroundColor);
      }
    }

    [TestMethod]
    public void Test_BitDepth()
    {
      using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
      {
        Assert.AreEqual(8, image.BitDepth());

        image.Threshold((Percentage)50);
        Assert.AreEqual(1, image.BitDepth());
      }
    }

    [TestMethod]
    public void Test_BlueShift()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        ColorAssert.AreNotEqual(MagickColors.White, image, 180, 80);

        image.BlueShift(2);

#if Q16HDRI
        ColorAssert.AreNotEqual(MagickColors.White, image, 180, 80);
        image.Clamp();
#endif

        ColorAssert.AreEqual(MagickColors.White, image, 180, 80);

#if Q8 || Q16
        ColorAssert.AreEqual(new MagickColor("#ac2cb333c848"), image, 350, 265);
#elif Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#ac2cb333c848"), image, 350, 265);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_BrightnessContrast()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        ColorAssert.AreNotEqual(MagickColors.White, image, 340, 295);
        image.BrightnessContrast(new Percentage(50), new Percentage(50));
        image.Clamp();
        ColorAssert.AreEqual(MagickColors.White, image, 340, 295);
      }
    }

    [TestMethod]
    public void Test_CannyEdge_HoughLine()
    {
      using (MagickImage image = new MagickImage(Files.ConnectedComponentsPNG))
      {
        image.Threshold(new Percentage(50));

        ColorAssert.AreEqual(MagickColors.Black, image, 150, 365);
        image.Negate();
        ColorAssert.AreEqual(MagickColors.White, image, 150, 365);

        image.CannyEdge();
        ColorAssert.AreEqual(MagickColors.Black, image, 150, 365);

        image.Crop(260, 180, 215, 200);

        image.Settings.FillColor = MagickColors.Red;
        image.Settings.StrokeColor = MagickColors.Red;

        image.HoughLine();
        ColorAssert.AreEqual(MagickColors.Red, image, 105, 25);
      }
    }

    [TestMethod]
    public void Test_Charcoal()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Charcoal();
        ColorAssert.AreEqual(MagickColors.White, image, 424, 412);
      }
    }

    [TestMethod]
    public void Test_Chop()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        image.Chop(new MagickGeometry(new Percentage(50), new Percentage(50)));
        Assert.AreEqual(240, image.Width);
        Assert.AreEqual(320, image.Height);
      }
    }

    [TestMethod]
    public void Test_Channels()
    {
      PixelChannel[] rgb = new PixelChannel[]
      {
        PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue
      };

      PixelChannel[] rgba = new PixelChannel[]
      {
        PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue, PixelChannel.Alpha
      };

      PixelChannel[] gray = new PixelChannel[]
      {
        PixelChannel.Gray
      };

      PixelChannel[] grayAlpha = new PixelChannel[]
      {
        PixelChannel.Gray, PixelChannel.Alpha
      };

      PixelChannel[] cmyk = new PixelChannel[]
      {
        PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black
      };

      PixelChannel[] cmyka = new PixelChannel[]
      {
        PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black, PixelChannel.Alpha
      };

      using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
      {
        CollectionAssert.AreEqual(rgba, image.Channels.ToArray());

        image.Alpha(AlphaOption.Off);

        CollectionAssert.AreEqual(rgb, image.Channels.ToArray());
      }

      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        CollectionAssert.AreEqual(grayAlpha, image.Channels.ToArray());

        using (MagickImage redChannel = image.Separate(Channels.Red).First())
        {
          CollectionAssert.AreEqual(gray, redChannel.Channels.ToArray());

          redChannel.Alpha(AlphaOption.On);

          CollectionAssert.AreEqual(grayAlpha, redChannel.Channels.ToArray());
        }
      }

      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        image.ColorSpace = ColorSpace.CMYK;

        CollectionAssert.AreEqual(cmyka, image.Channels.ToArray());

        image.Alpha(AlphaOption.Off);

        CollectionAssert.AreEqual(cmyk, image.Channels.ToArray());
      }
    }

    [TestMethod]
    public void Test_Chromaticity()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        PrimaryInfo info = new PrimaryInfo(0.5, 1.0, 1.5);

        Test_Chromaticity(0.15, 0.06, 0, image.ChromaBluePrimary);
        image.ChromaBluePrimary = info;
        Test_Chromaticity(0.5, 1.0, 1.5, image.ChromaBluePrimary);

        Test_Chromaticity(0.3, 0.6, 0, image.ChromaGreenPrimary);
        image.ChromaGreenPrimary = info;
        Test_Chromaticity(0.5, 1.0, 1.5, image.ChromaGreenPrimary);

        Test_Chromaticity(0.64, 0.33, 0, image.ChromaRedPrimary);
        image.ChromaRedPrimary = info;
        Test_Chromaticity(0.5, 1.0, 1.5, image.ChromaRedPrimary);

        Test_Chromaticity(0.3127, 0.329, 0, image.ChromaWhitePoint);
        image.ChromaWhitePoint = info;
        Test_Chromaticity(0.5, 1.0, 1.5, image.ChromaWhitePoint);
      }
    }

    [TestMethod]
    public void Test_ClassType()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        Assert.AreEqual(ClassType.Direct, image.ClassType);

        image.ClassType = ClassType.Pseudo;
        Assert.AreEqual(ClassType.Pseudo, image.ClassType);

        image.ClassType = ClassType.Direct;
        Assert.AreEqual(ClassType.Direct, image.ClassType);
      }
    }

    [TestMethod]
    public void Test_Clip()
    {
      Test_Clip(false, 0);
      Test_Clip(true, Quantum.Max);
    }

    [TestMethod]
    public void Test_Clone()
    {
      using (MagickImage first = new MagickImage(Files.SnakewarePNG))
      {
        using (MagickImage second = first.Clone())
        {
          Test_Clone(first, second);
        }

        using (MagickImage second = new MagickImage(first))
        {
          Test_Clone(first, second);
        }
      }
    }

    [TestMethod]
    public void Test_Clone_Area()
    {
      using (MagickImage icon = new MagickImage(Files.MagickNETIconPNG))
      {
        using (MagickImage area = icon.Clone())
        {
          area.Crop(64, 64, Gravity.Southeast);
          area.RePage();
          Assert.AreEqual(64, area.Width);
          Assert.AreEqual(64, area.Height);

          area.Crop(64, 32, Gravity.North);

          Assert.AreEqual(64, area.Width);
          Assert.AreEqual(32, area.Height);

          using (MagickImage part = icon.Clone(new MagickGeometry(64, 64, 64, 32)))
          {
            Test_Clone_Area(area, part);
          }

          using (MagickImage part = icon.Clone(64, 64, 64, 32))
          {
            Test_Clone_Area(area, part);
          }
        }

        using (MagickImage area = icon.Clone())
        {
          area.Crop(32, 64, Gravity.Northwest);

          Assert.AreEqual(32, area.Width);
          Assert.AreEqual(64, area.Height);

          using (MagickImage part = icon.Clone(32, 64))
          {
            Test_Clone_Area(area, part);
          }
        }

        using (MagickImage area = icon.Clone(4, 2))
        {
          Assert.AreEqual(4, area.Width);
          Assert.AreEqual(2, area.Height);

          Assert.AreEqual(64, area.ToByteArray(MagickFormat.Rgba).Length);
        }
      }
    }

    [TestMethod]
    public void Test_Clut()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage clut = CreatePallete())
        {
          image.Clut(clut, PixelInterpolateMethod.Catrom);
          ColorAssert.AreEqual(MagickColors.Green, image, 400, 300);
        }
      }
    }

    [TestMethod]
    public void Test_Colorize()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        image.Colorize(MagickColors.Purple, new Percentage(50));

        ColorAssert.AreEqual(new MagickColor("#c0408000c040"), image, 45, 75);
      }
    }

    [TestMethod]
    public void Test_ColorAlpha()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        MagickColor purple = new MagickColor("purple");

        image.ColorAlpha(purple);

        ColorAssert.AreNotEqual(purple, image, 45, 75);
        ColorAssert.AreEqual(purple, image, 100, 60);
      }
    }

    [TestMethod]
    public void Test_ColorMap()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.IsNull(image.GetColormap(0));
      }

      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProGIF))
      {
        ColorAssert.AreEqual(new MagickColor("#040d14"), image.GetColormap(0));
        image.SetColormap(0, MagickColors.Fuchsia);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image.GetColormap(0));

        image.SetColormap(65536, MagickColors.Fuchsia);
        Assert.IsNull(image.GetColormap(65536));
      }
    }

    [TestMethod]
    public void Test_ColorMatrix()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Rose))
      {
        MagickColorMatrix matrix = new MagickColorMatrix(3, 0, 0, 1, 0, 1, 0, 1, 0, 0);

        image.ColorMatrix(matrix);

        ColorAssert.AreEqual(MagickColor.FromRgb(58, 31, 255), image, 39, 25);
      }
    }

    [TestMethod]
    public void Test_ColorType()
    {
      using (MagickImage image = new MagickImage(Files.WireframeTIF))
      {
        Assert.AreEqual(ColorType.TrueColor, image.ColorType);
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream);
          memStream.Position = 0;
          using (MagickImage result = new MagickImage(memStream))
          {
            Assert.AreEqual(ColorType.Grayscale, result.ColorType);
          }
        }
      }

      using (MagickImage image = new MagickImage(Files.WireframeTIF))
      {
        Assert.AreEqual(ColorType.TrueColor, image.ColorType);
        image.PreserveColorType();
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Format = MagickFormat.Psd;
          image.Write(memStream);
          memStream.Position = 0;
          using (MagickImage result = new MagickImage(memStream))
          {
            Assert.AreEqual(ColorType.TrueColor, result.ColorType);
          }
        }
      }
    }

    [TestMethod]
    public void Test_Compare()
    {
      MagickImage first = new MagickImage(Files.ImageMagickJPG);

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        first.Compare(null);
      });

      MagickImage second = first.Clone();

      MagickErrorInfo same = first.Compare(second);
      Assert.IsNotNull(same);
      Assert.AreEqual(0, same.MeanErrorPerPixel);

      double distortion = first.Compare(second, ErrorMetric.Absolute);
      Assert.AreEqual(0, distortion);

      first.Threshold(new Percentage(50));
      MagickErrorInfo different = first.Compare(second);
      Assert.IsNotNull(different);
      Assert.AreNotEqual(0, different.MeanErrorPerPixel);

      distortion = first.Compare(second, ErrorMetric.Absolute);
      Assert.AreNotEqual(0, distortion);

      MagickImage difference = new MagickImage();
      distortion = first.Compare(second, ErrorMetric.RootMeanSquared, difference);
      Assert.AreNotEqual(0, distortion);
      Assert.AreNotEqual(first, difference);
      Assert.AreNotEqual(second, difference);

      second.Dispose();

      first.Opaque(MagickColors.Black, MagickColors.Green);
      first.Opaque(MagickColors.White, MagickColors.Green);

      second = first.Clone();
      second.FloodFill(MagickColors.Gray, 0, 0);

      distortion = first.Compare(second, ErrorMetric.Absolute, Channels.Green);
      Assert.AreEqual(0, distortion);

      distortion = first.Compare(second, ErrorMetric.Absolute, Channels.Red);
      Assert.AreNotEqual(0, distortion);
    }

    [TestMethod]
    public void Test_Composite_Blur()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
        {
          image.Warning += ShouldNotRaiseWarning;
          image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");
        }
      }
    }

    [TestMethod]
    public void Test_Composite_ChangeMask()
    {
      using (MagickImage background = new MagickImage("xc:red", 100, 100))
      {
        background.BackgroundColor = MagickColors.White;
        background.Extent(200, 100);

        IDrawable[] drawables = new IDrawable[]
        {
          new DrawableFontPointSize(50),
          new DrawableText(135, 70, "X")
        };

        using (MagickImage image = background.Clone())
        {
          image.Draw(drawables);
          image.Composite(background, Gravity.Center, CompositeOperator.ChangeMask);

          using (MagickImage result = new MagickImage(MagickColors.Transparent, 200, 100))
          {
            result.Draw(drawables);
            Assert.AreEqual(0.0634, result.Compare(image, ErrorMetric.RootMeanSquared), 0.001);
          }
        }
      }
    }

    [TestMethod]
    public void Test_Composite_Copy()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
        {
          image.Composite(yellow, new PointD(50, 50), CompositeOperator.Copy);

          ColorAssert.AreEqual(MagickColors.White, image, 49, 49);
          ColorAssert.AreEqual(MagickColors.Yellow, image, 50, 50);
          ColorAssert.AreEqual(MagickColors.Yellow, image, 149, 149);
          ColorAssert.AreEqual(MagickColors.White, image, 150, 150);

          image.Composite(yellow, 100, 100, CompositeOperator.Copy);

          ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
          ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
          ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
        }
      }
    }

    [TestMethod]
    public void Test_Composite_Gravity()
    {
      MagickColor backgroundColor = MagickColors.LightBlue;
      MagickColor overlayColor = MagickColors.YellowGreen;

      using (MagickImage background = new MagickImage(backgroundColor, 100, 100))
      {
        using (MagickImage overlay = new MagickImage(overlayColor, 50, 50))
        {
          background.Composite(overlay, Gravity.West, CompositeOperator.Over);

          ColorAssert.AreEqual(backgroundColor, background, 0, 0);
          ColorAssert.AreEqual(overlayColor, background, 0, 25);
          ColorAssert.AreEqual(backgroundColor, background, 0, 75);

          ColorAssert.AreEqual(backgroundColor, background, 49, 0);
          ColorAssert.AreEqual(overlayColor, background, 49, 25);
          ColorAssert.AreEqual(backgroundColor, background, 49, 75);

          ColorAssert.AreEqual(backgroundColor, background, 50, 0);
          ColorAssert.AreEqual(backgroundColor, background, 50, 25);
          ColorAssert.AreEqual(backgroundColor, background, 50, 75);

          ColorAssert.AreEqual(backgroundColor, background, 99, 0);
          ColorAssert.AreEqual(backgroundColor, background, 99, 25);
          ColorAssert.AreEqual(backgroundColor, background, 99, 75);
        }
      }

      using (MagickImage background = new MagickImage(backgroundColor, 100, 100))
      {
        using (MagickImage overlay = new MagickImage(overlayColor, 50, 50))
        {
          background.Composite(overlay, Gravity.East, CompositeOperator.Over);

          ColorAssert.AreEqual(backgroundColor, background, 0, 0);
          ColorAssert.AreEqual(backgroundColor, background, 0, 50);
          ColorAssert.AreEqual(backgroundColor, background, 0, 75);

          ColorAssert.AreEqual(backgroundColor, background, 49, 0);
          ColorAssert.AreEqual(backgroundColor, background, 49, 25);
          ColorAssert.AreEqual(backgroundColor, background, 49, 75);

          ColorAssert.AreEqual(backgroundColor, background, 50, 0);
          ColorAssert.AreEqual(overlayColor, background, 50, 25);
          ColorAssert.AreEqual(backgroundColor, background, 50, 75);

          ColorAssert.AreEqual(backgroundColor, background, 99, 0);
          ColorAssert.AreEqual(overlayColor, background, 99, 25);
          ColorAssert.AreEqual(backgroundColor, background, 99, 75);
        }
      }
    }

    [TestMethod]
    public void Test_ConnectedComponents()
    {
      using (MagickImage image = new MagickImage(Files.ConnectedComponentsPNG))
      {
        using (MagickImage temp = image.Clone())
        {
          temp.Blur(0, 10);
          temp.Threshold((Percentage)50);

          ConnectedComponent[] components = temp.ConnectedComponents(4).OrderBy(c => c.X).ToArray();
          Assert.AreEqual(7, components.Length);
          Assert.IsNull(temp.GetArtifact("connected-components:area-threshold"));
          Assert.IsNull(temp.GetArtifact("connected-components:mean-color"));

          Test_Component(image, components[1], 94, 297, 128, 151);
          Test_Component(image, components[2], 99, 554, 128, 150);
          Test_Component(image, components[3], 267, 432, 89, 139);
          Test_Component(image, components[4], 301, 202, 148, 143);
          Test_Component(image, components[5], 341, 622, 136, 150);
          Test_Component(image, components[6], 434, 411, 88, 139);
        }

#if !Q8
        using (MagickImage temp = image.Clone())
        {
          ConnectedComponentsSettings settings = new ConnectedComponentsSettings()
          {
            Connectivity = 4,
            MeanColor = true,
            AreaThreshold = 400
          };

          ConnectedComponent[] components = temp.ConnectedComponents(settings).OrderBy(c => c.X).ToArray();
          Assert.AreEqual(13, components.Length);
          Assert.IsNotNull(temp.GetArtifact("connected-components:area-threshold"));
          Assert.IsNotNull(temp.GetArtifact("connected-components:mean-color"));

          Test_Component(image, components[1], 90, 293, 139, 162);
          Test_Component(image, components[2], 96, 550, 138, 162);
          Test_Component(image, components[3], 213, 633, 1, 2);
          Test_Component(image, components[4], 215, 637, 3, 1);
          Test_Component(image, components[5], 217, 641, 3, 1);
          Test_Component(image, components[6], 219, 645, 3, 1);
          Test_Component(image, components[7], 221, 647, 3, 1);
          Test_Component(image, components[8], 268, 433, 89, 139);
          Test_Component(image, components[9], 298, 198, 155, 151);
          Test_Component(image, components[10], 337, 618, 148, 158);
          Test_Component(image, components[11], 410, 247, 2, 1);
          Test_Component(image, components[12], 434, 411, 88, 140);
        }
#endif
      }
    }

    [TestMethod]
    public void Test_Constructor()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImage(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImage((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImage((FileInfo)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImage((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImage((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImage(Files.Missing);
      });
    }

    [TestMethod]
    public void Test_Contrast()
    {
      using (MagickImage first = new MagickImage(Files.Builtin.Wizard))
      {
        first.Contrast(true);
        first.Contrast(false);

        using (MagickImage second = new MagickImage(Files.Builtin.Wizard))
        {
          Assert.AreEqual(0.003, 0.0001, first.Compare(second, ErrorMetric.RootMeanSquared));
        }
      }
    }

    [TestMethod]
    public void Test_ContrastStretch()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        image.ContrastStretch(new Percentage(50), new Percentage(80));
        image.Alpha(AlphaOption.Opaque);

        ColorAssert.AreEqual(MagickColors.Black, image, 160, 300);
        ColorAssert.AreEqual(MagickColors.Red, image, 325, 175);
      }
    }

    [TestMethod]
    public void Test_Convolve()
    {
      using (MagickImage image = new MagickImage("xc:", 1, 1))
      {
        image.BorderColor = MagickColors.Black;
        image.Border(5);

        Assert.AreEqual(11, image.Width);
        Assert.AreEqual(11, image.Height);

        ConvolveMatrix matrix = new ConvolveMatrix(3, 0, 0.5, 0, 0.5, 1, 0.5, 0, 0.5, 0);
        image.Convolve(matrix);

        MagickColor gray = new MagickColor("#800080008000");
        ColorAssert.AreEqual(MagickColors.Black, image, 4, 4);
        ColorAssert.AreEqual(gray, image, 5, 4);
        ColorAssert.AreEqual(MagickColors.Black, image, 6, 4);
        ColorAssert.AreEqual(gray, image, 4, 5);
        ColorAssert.AreEqual(MagickColors.White, image, 5, 5);
        ColorAssert.AreEqual(gray, image, 6, 5);
        ColorAssert.AreEqual(MagickColors.Black, image, 4, 6);
        ColorAssert.AreEqual(gray, image, 5, 6);
        ColorAssert.AreEqual(MagickColors.Black, image, 6, 6);
      }
    }

    [TestMethod]
    public void Test_CopyPixels()
    {
      using (MagickImage source = new MagickImage(MagickColors.White, 100, 100))
      {
        using (MagickImage destination = new MagickImage(MagickColors.Black, 50, 50))
        {
          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null, Channels.Red);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(source, null);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(source, null, Channels.Green);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(source, null, 0, 0);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(source, null, 0, 0, Channels.Green);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null, new MagickGeometry(10, 10));
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null, new MagickGeometry(10, 10), Channels.Black);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null, new MagickGeometry(10, 10), 0, 0);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            destination.CopyPixels(null, new MagickGeometry(10, 10), 0, 0, Channels.Black);
          });

          ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
          {
            destination.CopyPixels(source, new MagickGeometry(51, 50), new PointD(0, 0));
          });

          ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
          {
            destination.CopyPixels(source, new MagickGeometry(50, 51), new PointD(0, 0));
          });

          ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
          {
            destination.CopyPixels(source, new MagickGeometry(50, 50), 1, 0);
          });

          ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
          {
            destination.CopyPixels(source, new MagickGeometry(50, 50), new PointD(0, 1));
          });

          destination.CopyPixels(source, new MagickGeometry(25, 25), 25, 25);

          ColorAssert.AreEqual(MagickColors.Black, destination, 0, 0);
          ColorAssert.AreEqual(MagickColors.Black, destination, 24, 24);
          ColorAssert.AreEqual(MagickColors.White, destination, 25, 25);
          ColorAssert.AreEqual(MagickColors.White, destination, 49, 49);

          destination.CopyPixels(source, new MagickGeometry(25, 25), 0, 25, Channels.Green);

          ColorAssert.AreEqual(MagickColors.Black, destination, 0, 0);
          ColorAssert.AreEqual(MagickColors.Black, destination, 24, 24);
          ColorAssert.AreEqual(MagickColors.White, destination, 25, 25);
          ColorAssert.AreEqual(MagickColors.White, destination, 49, 49);
          ColorAssert.AreEqual(MagickColors.Lime, destination, 0, 25);
          ColorAssert.AreEqual(MagickColors.Lime, destination, 24, 49);
        }
      }
    }

    [TestMethod]
    public void Test_CropToTiles()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        MagickImage[] tiles = image.CropToTiles(48, 48).ToArray();
        Assert.AreEqual(140, tiles.Length);

        for (int i = 0; i < tiles.Length; i++)
        {
          MagickImage tile = tiles[i];

          Assert.AreEqual(48, tile.Height);

          if (i == 13 || (i - 13) % 14 == 0)
            Assert.AreEqual(16, tile.Width, i.ToString());
          else
            Assert.AreEqual(48, tile.Width, i.ToString());

          tile.Dispose();
        }
      }
    }

    [TestMethod]
    public void Test_CycleColormap()
    {
      using (MagickImage first = new MagickImage(Files.Builtin.Logo))
      {
        Assert.AreEqual(256, first.ColormapSize);

        using (MagickImage second = first.Clone())
        {
          second.CycleColormap(128);
          Assert.AreNotEqual(first, second);

          second.CycleColormap(128);
          Assert.AreEqual(first, second);

          second.CycleColormap(256);
          Assert.AreEqual(first, second);

          second.CycleColormap(512);
          Assert.AreEqual(first, second);
        }
      }
    }

    [TestMethod]
    public void Test_Define()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        string option = "optimize-coding";

        image.Settings.SetDefine(MagickFormat.Jpg, option, true);
        Assert.AreEqual("true", image.Settings.GetDefine(MagickFormat.Jpg, option));
        Assert.AreEqual("true", image.Settings.GetDefine(MagickFormat.Jpeg, option));

        image.Settings.RemoveDefine(MagickFormat.Jpeg, option);
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpg, option));

        image.Settings.SetDefine(MagickFormat.Jpeg, option, "test");
        Assert.AreEqual("test", image.Settings.GetDefine(MagickFormat.Jpg, option));
        Assert.AreEqual("test", image.Settings.GetDefine(MagickFormat.Jpeg, option));

        image.Settings.RemoveDefine(MagickFormat.Jpg, option);
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, option));

        image.Settings.SetDefine("profile:skip", "ICC");
        Assert.AreEqual("ICC", image.Settings.GetDefine("profile:skip"));
      }
    }

    [TestMethod]
    public void Test_Density()
    {
      using (MagickImage image = new MagickImage(Files.EightBimTIF))
      {
        Assert.AreEqual(72, image.Density.X);
        Assert.AreEqual(72, image.Density.Y);
        Assert.AreEqual(DensityUnit.PixelsPerInch, image.Density.Units);
      }
    }

    [TestMethod]
    public void Test_Deskew()
    {
      using (MagickImage image = new MagickImage(Files.LetterJPG))
      {
        image.ColorType = ColorType.Bilevel;

        ColorAssert.AreEqual(MagickColors.White, image, 471, 92);

        image.Deskew(new Percentage(10));

        ColorAssert.AreEqual(new MagickColor("#007400740074"), image, 471, 92);
      }
    }

    [TestMethod]
    public void Test_Despeckle()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        MagickColor color = new MagickColor("#d1d1d1d1d1d1");
        ColorAssert.AreNotEqual(color, image, 130, 123);

        image.Despeckle();
        image.Despeckle();
        image.Despeckle();

        ColorAssert.AreEqual(color, image, 130, 123);
      }
    }

    [TestMethod]
    public void Test_DetermineColorType()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        Assert.AreEqual(ColorType.TrueColorAlpha, image.ColorType);

        ColorType colorType = image.DetermineColorType();
        Assert.AreEqual(ColorType.GrayscaleAlpha, colorType);
      }
    }

    [TestMethod]
    public void Test_Dispose()
    {
      MagickImage image = new MagickImage();
      image.Dispose();

      ExceptionAssert.Throws<ObjectDisposedException>(delegate ()
      {
        image.HasAlpha = true;
      });
    }

    [TestMethod]
    public void Test_Distort()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Distort(DistortMethod.Perspective, null);
        });

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.Distort(DistortMethod.Perspective, new double[] { });
        });

        image.BackgroundColor = MagickColors.Cornsilk;
        image.VirtualPixelMethod = VirtualPixelMethod.Background;
        image.Distort(DistortMethod.Perspective, new double[] { 0, 0, 0, 0, 0, 90, 0, 90, 90, 0, 90, 25, 90, 90, 90, 65 });
        image.Clamp();

        ColorAssert.AreEqual(new MagickColor("#0000"), image, 1, 64);
        ColorAssert.AreEqual(MagickColors.Cornsilk, image, 104, 50);
        ColorAssert.AreEqual(new MagickColor("#aa67e157f9da"), image, 66, 62);
      }
    }

    [TestMethod]
    public void Test_Drawable()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        MagickColor yellow = MagickColors.Yellow;
        image.Draw(new DrawableFillColor(yellow), new DrawableRectangle(0, 0, 10, 10));
        ColorAssert.AreEqual(yellow, image, 5, 5);
      }
    }

    [TestMethod]
    public void Test_Encipher_Decipher()
    {
      using (MagickImage original = new MagickImage(Files.SnakewarePNG))
      {
        using (MagickImage enciphered = original.Clone())
        {
          enciphered.Encipher("All your base are belong to us");
          Assert.AreNotEqual(original, enciphered);

          using (MagickImage deciphered = enciphered.Clone())
          {
            deciphered.Decipher("What you say!!");
            Assert.AreNotEqual(enciphered, deciphered);
            Assert.AreNotEqual(original, deciphered);
          }

          using (MagickImage deciphered = enciphered.Clone())
          {
            deciphered.Decipher("All your base are belong to us");
            Assert.AreNotEqual(enciphered, deciphered);
            Assert.AreEqual(original, deciphered);
          }
        }
      }
    }

    [TestMethod]
    public void Test_Edge()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        ColorAssert.AreNotEqual(MagickColors.Black, image, 400, 295);
        ColorAssert.AreNotEqual(MagickColors.Blue, image, 455, 126);

        image.Edge(2);
        image.Clamp();

        ColorAssert.AreEqual(MagickColors.Black, image, 400, 295);
        ColorAssert.AreEqual(MagickColors.Blue, image, 455, 126);
      }
    }

    [TestMethod]
    public void Test_Emboss()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        image.Emboss(4, 2);

#if Q8
        ColorAssert.AreEqual(new MagickColor("#ff5e43"), image, 325, 175);
        ColorAssert.AreEqual(new MagickColor("#4344ff"), image, 99, 270);
#elif Q16
        ColorAssert.AreEqual(new MagickColor("#ffff597e4397"), image, 325, 175);
        ColorAssert.AreEqual(new MagickColor("#431f43f0ffff"), image, 99, 270);
#elif Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#ffff59624391"), image, 325, 175);
        ColorAssert.AreEqual(new MagickColor("#431843e8ffff"), image, 99, 270);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Enhance()
    {
      using (MagickImage enhanced = new MagickImage(Files.NoisePNG))
      {
        enhanced.Enhance();

        using (MagickImage original = new MagickImage(Files.NoisePNG))
        {
          Assert.AreEqual(0.0115, enhanced.Compare(original, ErrorMetric.RootMeanSquared), 0.0003);
        }
      }
    }

    [TestMethod]
    public void Test_Equalize()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        image.Equalize();

        ColorAssert.AreEqual(MagickColors.White, image, 105, 25);
        ColorAssert.AreEqual(new MagickColor("#0000"), image, 105, 60);
      }
    }

    [TestMethod]
    public void Test_Evaluate()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, null);
        });

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { });
        });

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Evaluate(Channels.Red, null, EvaluateOperator.Set, 0.0);
        });

        image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { 5.0 });

        ColorAssert.AreEqual(new MagickColor("#9068ffffffff"), image, 100, 295);

        image.Evaluate(Channels.Red, new MagickGeometry(0, 0, 100, 295), EvaluateOperator.Set, 0);

        ColorAssert.AreEqual(new MagickColor("#0ff"), image, 99, 195);
        ColorAssert.AreEqual(new MagickColor("#9068ffffffff"), image, 100, 295);

        image.Evaluate(Channels.Green, EvaluateOperator.Set, 0);

        ColorAssert.AreEqual(new MagickColor("#00f"), image, 99, 195);
        ColorAssert.AreEqual(new MagickColor("#90680000ffff"), image, 100, 295);
      }
    }

    [TestMethod]
    public void Test_Extent()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Read(Files.RedPNG);
        image.Resize(new MagickGeometry(100, 100));
        Assert.AreEqual(100, image.Width);
        Assert.AreEqual(33, image.Height);

        image.BackgroundColor = MagickColors.Transparent;
        image.Extent(100, 100, Gravity.Center);
        Assert.AreEqual(100, image.Width);
        Assert.AreEqual(100, image.Height);

        ColorAssert.AreEqual(MagickColors.Transparent, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.Red, image, 15, 50);
        ColorAssert.AreEqual(new MagickColor(0, 0, 0, 0), image, 35, 35);
      }
    }

    [TestMethod]
    public void Test_FlipFlop()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        collection.Add(new MagickImage(MagickColors.DodgerBlue, 10, 10));
        collection.Add(new MagickImage(MagickColors.Firebrick, 10, 10));

        using (MagickImage image = collection.AppendVertically())
        {
          ColorAssert.AreEqual(MagickColors.DodgerBlue, image, 5, 0);
          ColorAssert.AreEqual(MagickColors.Firebrick, image, 5, 10);

          image.Flip();

          ColorAssert.AreEqual(MagickColors.Firebrick, image, 5, 0);
          ColorAssert.AreEqual(MagickColors.DodgerBlue, image, 5, 10);
        }

        using (MagickImage image = collection.AppendHorizontally())
        {
          ColorAssert.AreEqual(MagickColors.DodgerBlue, image, 0, 5);
          ColorAssert.AreEqual(MagickColors.Firebrick, image, 10, 5);

          image.Flop();

          ColorAssert.AreEqual(MagickColors.Firebrick, image, 0, 5);
          ColorAssert.AreEqual(MagickColors.DodgerBlue, image, 10, 5);
        }
      }
    }

    [TestMethod]
    public void Test_FontTypeMetrics()
    {
      using (MagickImage image = new MagickImage(MagickColors.Transparent, 100, 100))
      {
        image.Settings.Font = "Arial";
        image.Settings.FontPointsize = 15;
        TypeMetric typeMetric = image.FontTypeMetrics("Magick.NET");
        Assert.IsNotNull(typeMetric);
        Assert.AreEqual(14, typeMetric.Ascent);
        Assert.AreEqual(-3, typeMetric.Descent);
        Assert.AreEqual(30, typeMetric.MaxHorizontalAdvance);
        Assert.AreEqual(17, typeMetric.TextHeight);
        Assert.AreEqual(82, typeMetric.TextWidth);
        Assert.AreEqual(-4.5625, typeMetric.UnderlinePosition);
        Assert.AreEqual(2.34375, typeMetric.UnderlineThickness);

        image.Settings.FontPointsize = 150;
        typeMetric = image.FontTypeMetrics("Magick.NET");
        Assert.IsNotNull(typeMetric);
        Assert.AreEqual(136, typeMetric.Ascent);
        Assert.AreEqual(-32, typeMetric.Descent);
        Assert.AreEqual(300, typeMetric.MaxHorizontalAdvance);
        Assert.AreEqual(168, typeMetric.TextHeight);
        Assert.AreEqual(813, typeMetric.TextWidth);
        Assert.AreEqual(-4.5625, typeMetric.UnderlinePosition);
        Assert.AreEqual(2.34375, typeMetric.UnderlineThickness);
      }
    }

    [TestMethod]
    public void Test_FormatExpression()
    {
      using (MagickImage image = new MagickImage(Files.RedPNG))
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.FormatExpression(null);
        });

        Assert.AreEqual("FOO", image.FormatExpression("FOO"));
        Assert.AreEqual("OO", image.FormatExpression("%EOO"));
        image.Warning += ShouldRaiseWarning;
        Assert.AreEqual("OO", image.FormatExpression("%EOO"));
        image.Warning -= ShouldRaiseWarning;

        Assert.AreEqual("a48a7f2fdc26e9ccf75b0c85a254c958f004cc182d0ca8c3060c1df734645367", image.FormatExpression("%#"));
      }

      using (MagickImage image = new MagickImage(Files.InvitationTif))
      {
        Assert.AreEqual("sRGB IEC61966-2.1", image.FormatExpression("%[profile:icc]"));
      }
    }

    [TestMethod]
    public void Test_FormatInfo()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        MagickFormatInfo info = image.FormatInfo;

        Assert.IsNotNull(info);
        Assert.AreEqual(MagickFormat.Png, info.Format);
        Assert.AreEqual("image/png", info.MimeType);
      }
    }

    [TestMethod]
    public void Test_Frame()
    {
      int frameSize = 100;

      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        int expectedWidth = frameSize + image.Width + frameSize;
        int expectedHeight = frameSize + image.Height + frameSize;

        image.Frame(frameSize, frameSize);
        Assert.AreEqual(expectedWidth, image.Width);
        Assert.AreEqual(expectedHeight, image.Height);
      }

      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        int expectedWidth = frameSize + image.Width + frameSize;
        int expectedHeight = frameSize + image.Height + frameSize;

        image.Frame(frameSize, frameSize, 6, 6);
        Assert.AreEqual(expectedWidth, image.Width);
        Assert.AreEqual(expectedHeight, image.Height);
      }

      ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
      {
        using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
        {
          image.Frame(6, 6, frameSize, frameSize);
        }
      });
    }

    [TestMethod]
    public void Test_Fx()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Fx(null);
        });

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.Fx("");
        });

        ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
        {
          image.Fx("foobar");
        });

        image.Fx("b");

        ColorAssert.AreEqual(MagickColors.Black, image, 183, 83);
        ColorAssert.AreEqual(MagickColors.White, image, 140, 400);

        image.Fx("1/2", Channels.Green);

        ColorAssert.AreEqual(new MagickColor("#000080000000"), image, 183, 83);
        ColorAssert.AreEqual(new MagickColor("#ffff8000ffff"), image, 140, 400);

        image.Fx("1/4", Channels.Alpha);

        ColorAssert.AreEqual(new MagickColor("#000080000000"), image, 183, 83);
        ColorAssert.AreEqual(new MagickColor("#ffff8000ffff"), image, 140, 400);

        image.HasAlpha = true;
        image.Fx("1/4", Channels.Alpha);

        ColorAssert.AreEqual(new MagickColor("#0000800000004000"), image, 183, 83);
        ColorAssert.AreEqual(new MagickColor("#ffff8000ffff4000"), image, 140, 400);
      }
    }

    [TestMethod]
    public void Test_GammaCorrect()
    {
      MagickImage first = new MagickImage(Files.InvitationTif);
      first.GammaCorrect(2.0);

      MagickImage second = new MagickImage(Files.InvitationTif);
      second.GammaCorrect(2.0, Channels.Red);

      Assert.AreNotEqual(first, second);

      first.Dispose();
      second.Dispose();
    }

    [TestMethod]
    public void Test_GaussianBlur()
    {
      using (MagickImage gaussian = new MagickImage(Files.Builtin.Wizard))
      {
        gaussian.GaussianBlur(5.5, 10.2);

        using (MagickImage blur = new MagickImage(Files.Builtin.Wizard))
        {
          blur.Blur(5.5, 10.2);

          double distortion = blur.Compare(gaussian, ErrorMetric.RootMeanSquared);
#if Q8
          Assert.AreEqual(0.00066, distortion, 0.00001);
#elif Q16
          Assert.AreEqual(0.0000033, distortion, 0.0000001);
#elif Q16HDRI
          Assert.AreEqual(0.0000011, distortion, 0.0000001);
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_Grayscale()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Grayscale(PixelIntensityMethod.Brightness);
        Assert.AreEqual(1, image.ChannelCount);
        Assert.AreEqual(PixelChannel.Red, image.Channels.First());

        ColorAssert.AreEqual(MagickColors.White, image, 220, 45);
        ColorAssert.AreEqual(new MagickColor("#929292"), image, 386, 379);
        ColorAssert.AreEqual(new MagickColor("#f5f5f5"), image, 405, 158);
      }
    }

    [TestMethod]
    public void Test_HaldClut()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        using (MagickImage clut = CreatePallete())
        {
          image.HaldClut(clut);

          ColorAssert.AreEqual(new MagickColor("#052467fc2bb4"), image, 228, 276);
          ColorAssert.AreEqual(new MagickColor("#144f623a2801"), image, 295, 270);
        }
      }
    }

    [TestMethod]
    public void Test_HasClippingPath()
    {
      using (MagickImage noPath = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.IsFalse(noPath.HasClippingPath);
      }

      using (MagickImage hasPath = new MagickImage(Files.InvitationTif))
      {
        Assert.IsTrue(hasPath.HasClippingPath);
      }
    }

    [TestMethod]
    public void Test_Histogram()
    {
      MagickImage image = new MagickImage();
      Dictionary<MagickColor, int> histogram = image.Histogram();
      Assert.IsNotNull(histogram);
      Assert.AreEqual(0, histogram.Count);

      image = new MagickImage(Files.RedPNG);
      histogram = image.Histogram();

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

      image.Dispose();
    }

    [TestMethod]
    public void Test_IComparable()
    {
      MagickImage first = new MagickImage(MagickColors.Red, 10, 5);

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

      MagickImage second = new MagickImage(MagickColors.Green, 5, 5);

      Assert.AreEqual(1, first.CompareTo(second));
      Assert.IsFalse(first < second);
      Assert.IsFalse(first <= second);
      Assert.IsTrue(first > second);
      Assert.IsTrue(first >= second);

      second = new MagickImage(MagickColors.Red, 5, 10);

      Assert.AreEqual(0, first.CompareTo(second));
      Assert.IsFalse(first == second);
      Assert.IsFalse(first < second);
      Assert.IsTrue(first <= second);
      Assert.IsFalse(first > second);
      Assert.IsTrue(first >= second);

      first.Dispose();
      second.Dispose();
    }

    [TestMethod]
    public void Test_IEquatable()
    {
      MagickImage first = new MagickImage(MagickColors.Red, 10, 10);

      Assert.IsFalse(first == null);
      Assert.IsFalse(first.Equals(null));
      Assert.IsTrue(first.Equals(first));
      Assert.IsTrue(first.Equals((object)first));

      MagickImage second = new MagickImage(MagickColors.Red, 10, 10);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));

      second = new MagickImage(MagickColors.Green, 10, 10);

      Assert.IsTrue(first != second);
      Assert.IsFalse(first.Equals(second));

      first.Dispose();
      second.Dispose();

      first = null;
      Assert.IsTrue(first == null);
      Assert.IsFalse(first != null);
    }

    [TestMethod]
    public void Test_Implode()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        ColorAssert.AreEqual(new MagickColor("#00000000"), image, 69, 45);

        image.Implode(0.5, PixelInterpolateMethod.Blend);

        ColorAssert.AreEqual(new MagickColor("#a8dff8"), image, 69, 45);

        image.Implode(-0.5, PixelInterpolateMethod.Background);

        ColorAssert.AreEqual(new MagickColor("#00000000"), image, 69, 45);
      }
    }

    [TestMethod]
    public void Test_Interlace()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.AreEqual(Interlace.NoInterlace, image.Interlace);

        image.Interlace = Interlace.Png;

        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream);
          memStream.Position = 0;
          using (MagickImage result = new MagickImage(memStream))
          {
            Assert.AreEqual(Interlace.Png, result.Interlace);
          }
        }
      }
    }

    [TestMethod]
    public void Test_IsOpaque()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.IsFalse(image.IsOpaque);
        image.ColorAlpha(MagickColors.Purple);
        Assert.IsTrue(image.IsOpaque);
      }

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        Assert.IsTrue(image.IsOpaque);
        image.Opaque(MagickColors.White, MagickColors.Transparent);
        Assert.IsFalse(image.IsOpaque);
      }
    }

    [TestMethod]
    public void Test_Kuwahara()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.Kuwahara(13.4, 2.5);
        image.ColorType = ColorType.Bilevel;

        ColorAssert.AreEqual(MagickColors.White, image, 216, 120);
        ColorAssert.AreEqual(MagickColors.Black, image, 39, 138);
      }
    }

    [TestMethod]
    public void Test_Level()
    {
      using (MagickImage first = new MagickImage(Files.MagickNETIconPNG))
      {
        first.Level(new Percentage(50.0), new Percentage(10.0));

        using (MagickImage second = new MagickImage(Files.MagickNETIconPNG))
        {
          Assert.AreNotEqual(first, second);
          Assert.AreNotEqual(first.Signature, second.Signature);

          QuantumType fifty = (QuantumType)(Quantum.Max * 0.5);
          QuantumType ten = (QuantumType)(Quantum.Max * 0.1);
          second.Level(fifty, ten, Channels.Red);
          second.Level(fifty, ten, Channels.Green | Channels.Blue);
          second.Level(fifty, ten, Channels.Alpha);

          Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

          Assert.AreEqual(first, second);
          Assert.AreEqual(first.Signature, second.Signature);
        }
      }

      using (MagickImage first = new MagickImage(Files.MagickNETIconPNG))
      {
        first.InverseLevel(new Percentage(50.0), new Percentage(10.0));

        using (MagickImage second = new MagickImage(Files.MagickNETIconPNG))
        {
          Assert.AreNotEqual(first, second);
          Assert.AreNotEqual(first.Signature, second.Signature);

          QuantumType fifty = (QuantumType)(Quantum.Max * 0.5);
          QuantumType ten = (QuantumType)(Quantum.Max * 0.1);
          second.InverseLevel(fifty, ten, Channels.Red);
          second.InverseLevel(fifty, ten, Channels.Green | Channels.Blue);
          second.InverseLevel(fifty, ten, Channels.Alpha);

          Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

          Assert.AreEqual(first, second);
          Assert.AreEqual(first.Signature, second.Signature);
        }
      }
    }

    [TestMethod]
    public void Test_LevelColors()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod);
        ColorAssert.AreEqual(new MagickColor("#ffffbed24bc3fffa"), image, 42, 75);
        ColorAssert.AreEqual(new MagickColor("#ffffffff0809"), image, 62, 75);
      }

      using (MagickImage first = new MagickImage(Files.MagickNETIconPNG))
      {
        first.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
        first.InverseLevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
        first.Alpha(AlphaOption.Background);

        using (MagickImage second = new MagickImage(Files.MagickNETIconPNG))
        {
          second.Alpha(AlphaOption.Background);
#if Q8
          Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
#elif Q16 || Q16HDRI
          Assert.AreEqual(0.0, 0.00000001, first.Compare(second, ErrorMetric.RootMeanSquared));
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_LinearStretch()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Scale(100, 100);

        image.LinearStretch((Percentage)1, (Percentage)1);
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Format = MagickFormat.Histogram;
          image.Write(memStream);
          memStream.Position = 0;

          using (MagickImage histogram = new MagickImage(memStream))
          {
#if Q8
            ColorAssert.AreEqual(MagickColors.Red, histogram, 65, 12);
            ColorAssert.AreEqual(MagickColors.Lime, histogram, 95, 122);
            ColorAssert.AreEqual(MagickColors.Blue, histogram, 205, 81);
#elif Q16 || Q16HDRI
            ColorAssert.AreEqual(MagickColors.Red, histogram, 34, 182);
            ColorAssert.AreEqual(MagickColors.Lime, histogram, 122, 193);
            ColorAssert.AreEqual(MagickColors.Blue, histogram, 210, 194);
#else
#error Not implemented!
#endif
          }
        }

        image.LinearStretch((Percentage)10, (Percentage)90);
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Format = MagickFormat.Histogram;
          image.Write(memStream);
          memStream.Position = 0;

          using (MagickImage histogram = new MagickImage(memStream))
          {
#if Q8
            ColorAssert.AreEqual(MagickColors.Red, histogram, 102, 183);
            ColorAssert.AreEqual(MagickColors.Lime, histogram, 147, 188);
            ColorAssert.AreEqual(MagickColors.Blue, histogram, 195, 190);
#elif Q16
            ColorAssert.AreEqual(MagickColors.Red, histogram, 221, 182);
            ColorAssert.AreEqual(MagickColors.Lime, histogram, 11, 184);
            ColorAssert.AreEqual(MagickColors.Blue, histogram, 45, 194);
#elif Q16HDRI
            ColorAssert.AreEqual(MagickColors.Red, histogram, 221, 182);
            ColorAssert.AreEqual(MagickColors.Lime, histogram, 12, 184);
            ColorAssert.AreEqual(MagickColors.Blue, histogram, 45, 194);
#else
#error Not implemented!
#endif
          }
        }
      }
    }

    [TestMethod]
    public void Test_LocalContrast()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.LocalContrast(5.0, (Percentage)75);
        image.Clamp();

        ColorAssert.AreEqual(MagickColors.Black, image, 81, 28);
        ColorAssert.AreEqual(MagickColors.Black, image, 245, 181);
        ColorAssert.AreEqual(MagickColors.White, image, 200, 135);
        ColorAssert.AreEqual(MagickColors.White, image, 200, 135);
      }
    }

    [TestMethod]
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

    [TestMethod]
    public void Test_Magnify()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.Magnify();
        Assert.AreEqual(image.Width, 256);
        Assert.AreEqual(image.Height, 256);
      }
    }

    [TestMethod]
    public void Test_Map()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage colors = CreatePallete())
        {
          image.Map(colors);

          ColorAssert.AreEqual(MagickColors.Blue, image, 0, 0);
          ColorAssert.AreEqual(MagickColors.Green, image, 455, 396);
          ColorAssert.AreEqual(MagickColors.Red, image, 505, 451);
        }
      }

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        List<MagickColor> colors = new List<MagickColor>();
        colors.Add(MagickColors.Gold);
        colors.Add(MagickColors.Lime);
        colors.Add(MagickColors.Fuchsia);

        image.Map(colors);

        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.Lime, image, 455, 396);
        ColorAssert.AreEqual(MagickColors.Gold, image, 505, 451);
      }
    }

    [TestMethod]
    public void Test_MatteColor()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.MatteColor = MagickColors.PaleGoldenrod;
        image.Frame();

        ColorAssert.AreEqual(MagickColors.PaleGoldenrod, image, 10, 10);
        ColorAssert.AreEqual(MagickColors.PaleGoldenrod, image, 680, 520);
      }
    }

    [TestMethod]
    public void Test_Minify()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.Minify();
        Assert.AreEqual(image.Width, 64);
        Assert.AreEqual(image.Height, 64);
      }
    }

    [TestMethod]
    public void Test_Modulate()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.Modulate(new Percentage(70), new Percentage(30));

#if Q8
        ColorAssert.AreEqual(new MagickColor("#743e3e"), image, 25, 70);
        ColorAssert.AreEqual(new MagickColor("#0b0b0b"), image, 25, 40);
        ColorAssert.AreEqual(new MagickColor("#1f3a1f"), image, 75, 70);
        ColorAssert.AreEqual(new MagickColor("#5a5a5a"), image, 75, 40);
        ColorAssert.AreEqual(new MagickColor("#3e3e74"), image, 125, 70);
        ColorAssert.AreEqual(new MagickColor("#a8a8a8"), image, 125, 40);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#72803da83da8", "#747a3eb83eb8")), image, 25, 70);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#0b2d0b2d0b2d", "#0b5f0b5f0b5f")), image, 25, 40);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#1ef3397a1ef3", "#1f7c3a781f7c")), image, 75, 70);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#592d592d592d", "#5ab75ab75ab7")), image, 75, 40);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#3da83da87280", "#3eb83eb8747a")), image, 125, 70);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#a5aea5aea5ae", "#a88ba88ba88b")), image, 125, 40);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Morphology()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        ExceptionAssert.Throws<MagickOptionErrorException>(delegate ()
        {
          image.Morphology(MorphologyMethod.Smooth, "Magick");
        });

        image.Morphology(MorphologyMethod.Dilate, Kernel.Square, "1");

        image.Morphology(MorphologyMethod.Convolve, "3: 0.3,0.6,0.3 0.6,1.0,0.6 0.3,0.6,0.3");

        MorphologySettings settings = new MorphologySettings();
        settings.Method = MorphologyMethod.Convolve;
        settings.ConvolveBias = new Percentage(50);
        settings.Kernel = Kernel.DoG;
        settings.KernelArguments = "0x2";

        image.Read(Files.Builtin.Logo);

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Morphology(null);
        });

        image.Morphology(settings);

        QuantumType half = (QuantumType)((Quantum.Max / 2.0) + 0.5);
        ColorAssert.AreEqual(new MagickColor(half, half, half), image, 120, 160);
      }
    }

    [TestMethod]
    public void Test_MotionBlur()
    {
      using (MagickImage motionBlurred = new MagickImage(Files.Builtin.Logo))
      {
        motionBlurred.MotionBlur(4.0, 5.4, 10.6);

        using (MagickImage original = new MagickImage(Files.Builtin.Logo))
        {
          Assert.AreEqual(0.11019, motionBlurred.Compare(original, ErrorMetric.RootMeanSquared), 0.00001);
        }
      }
    }

    [TestMethod]
    public void Test_Normalize()
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Add(new MagickImage("gradient:gray70-gray30", 100, 100));
        images.Add(new MagickImage("gradient:blue-navy", 50, 100));

        using (MagickImage colorRange = images.AppendHorizontally())
        {
          ColorAssert.AreEqual(new MagickColor("gray70"), colorRange, 0, 0);
          ColorAssert.AreEqual(new MagickColor("blue"), colorRange, 101, 0);

          ColorAssert.AreEqual(new MagickColor("gray30"), colorRange, 0, 99);
          ColorAssert.AreEqual(new MagickColor("navy"), colorRange, 101, 99);

          colorRange.Normalize();

          ColorAssert.AreEqual(new MagickColor("white"), colorRange, 0, 0);
          ColorAssert.AreEqual(new MagickColor("blue"), colorRange, 101, 0);

#if Q8
          ColorAssert.AreEqual(new MagickColor("gray40"), colorRange, 0, 99);
          ColorAssert.AreEqual(new MagickColor("#0000b3"), colorRange, 101, 99);
#elif Q16 || Q16HDRI
          ColorAssert.AreEqual(new MagickColor("#662e662e662e"), colorRange, 0, 99);
          ColorAssert.AreEqual(new MagickColor("#00000000b317"), colorRange, 101, 99);
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_OilPaint()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.OilPaint(2, 5);
        ColorAssert.AreEqual(new MagickColor("#6a7e85"), image, 180, 98);
      }
    }

    [TestMethod]
    public void Test_OrderedDither()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.OrderedDither("h4x4a");

        ColorAssert.AreEqual(MagickColors.Yellow, image, 299, 212);
        ColorAssert.AreEqual(MagickColors.Red, image, 314, 228);
        ColorAssert.AreEqual(MagickColors.Black, image, 448, 159);
      }
    }

    [TestMethod]
    public void Test_Opaque()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);

        image.Opaque(MagickColors.Red, MagickColors.Yellow);
        ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);

        image.InverseOpaque(MagickColors.Yellow, MagickColors.Red);
        ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);

        image.InverseOpaque(MagickColors.Red, MagickColors.Red);
        ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
      }
    }

    [TestMethod]
    public void Test_Perceptible()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Perceptible(Quantum.Max * 0.4);

        ColorAssert.AreEqual(new MagickColor("#f79868"), image, 300, 210);
        ColorAssert.AreEqual(new MagickColor("#666692"), image, 410, 405);
      }
    }

    [TestMethod]
    public void Test_Ping()
    {
      MagickImage image = new MagickImage();

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        image.Ping(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Ping((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Ping((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Ping((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        image.Ping(Files.Missing);
      });

      image.Ping(Files.FujiFilmFinePixS1ProJPG);
      Test_Ping(image);
      Assert.AreEqual(600, image.Width);
      Assert.AreEqual(400, image.Height);

      image.Ping(new FileInfo(Files.FujiFilmFinePixS1ProJPG));
      Test_Ping(image);
      Assert.AreEqual(600, image.Width);
      Assert.AreEqual(400, image.Height);

      image.Ping(File.ReadAllBytes(Files.FujiFilmFinePixS1ProJPG));
      Test_Ping(image);
      Assert.AreEqual(600, image.Width);
      Assert.AreEqual(400, image.Height);

      image.Read(Files.SnakewarePNG);
      Assert.AreEqual(286, image.Width);
      Assert.AreEqual(67, image.Height);
      using (PixelCollection pixels = image.GetPixels())
      {
        Assert.AreEqual(38324, pixels.ToArray().Length);
      }

      image.Dispose();
    }

    [TestMethod]
    public void Test_Polaroid()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.BorderColor = MagickColors.Red;
        image.BackgroundColor = MagickColors.Fuchsia;
        image.Settings.FontPointsize = 20;
        image.Polaroid("Magick.NET", 10, PixelInterpolateMethod.Bilinear);
        image.Clamp();

        ColorAssert.AreEqual(MagickColors.Black, image, 103, 162);
        ColorAssert.AreEqual(MagickColors.Red, image, 99, 97);
#if Q8
        ColorAssert.AreEqual(new MagickColor("#ff00ffbc"), image, 146, 194);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#ffff0000ffffbbe5"), image, 146, 194);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Posterize()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Posterize(5);

#if Q8
        ColorAssert.AreEqual(new MagickColor("#3f7fbf"), image, 300, 150);
        ColorAssert.AreEqual(new MagickColor("#3f3f7f"), image, 495, 270);
        ColorAssert.AreEqual(new MagickColor("#3f3f3f"), image, 445, 255);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#3fff7fffbfff"), image, 300, 150);
        ColorAssert.AreEqual(new MagickColor("#3fff3fff7fff"), image, 495, 270);
        ColorAssert.AreEqual(new MagickColor("#3fff3fff3fff"), image, 445, 255);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Profile()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        ImageProfile profile = image.GetIptcProfile();
        Assert.IsNotNull(profile);
        image.RemoveProfile(profile.Name);
        profile = image.GetIptcProfile();
        Assert.IsNull(profile);

        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream);
          memStream.Position = 0;

          using (MagickImage newImage = new MagickImage(memStream))
          {
            profile = newImage.GetIptcProfile();
            Assert.IsNull(profile);
          }
        }
      }
    }

    [TestMethod]
    public void Test_ProfileNames()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        IEnumerable<string> names = image.ProfileNames;
        Assert.IsNotNull(names);
        Assert.AreEqual(5, names.Count());
        Assert.AreEqual("8bim,exif,icc,iptc,xmp", string.Join(",", (from name in names
                                                                    orderby name
                                                                    select name).ToArray()));
      }

      using (MagickImage image = new MagickImage(Files.RedPNG))
      {
        IEnumerable<string> names = image.ProfileNames;
        Assert.IsNotNull(names);
        Assert.AreEqual(0, names.Count());
      }
    }

    [TestMethod]
    public void Test_Progress()
    {
      Percentage progress = new Percentage(0);
      bool cancel = false;
      EventHandler<ProgressEventArgs> progressEvent = delegate (object sender, ProgressEventArgs arguments)
      {
        Assert.IsNotNull(sender);
        Assert.IsNotNull(arguments);
        Assert.IsNotNull(arguments.Origin);
        Assert.AreEqual(false, arguments.Cancel);

        progress = arguments.Progress;
        if (cancel)
          arguments.Cancel = true;
      };

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Progress += progressEvent;

        image.Flip();
        Assert.AreEqual(100, (int)progress);
      }

      cancel = true;

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Progress += progressEvent;

        image.Flip();
        Assert.IsTrue(progress <= (Percentage)1);
      }
    }

    [TestMethod]
    public void Test_Quantize()
    {
      QuantizeSettings settings = new QuantizeSettings();
      settings.Colors = 8;

      Assert.AreEqual(DitherMethod.Riemersma, settings.DitherMethod);
      settings.DitherMethod = null;
      Assert.AreEqual(null, settings.DitherMethod);
      settings.DitherMethod = DitherMethod.No;
      Assert.AreEqual(DitherMethod.No, settings.DitherMethod);
      settings.MeasureErrors = true;
      Assert.AreEqual(true, settings.MeasureErrors);

      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        MagickErrorInfo errorInfo = image.Quantize(settings);
#if Q8
        Assert.AreEqual(6.975, errorInfo.MeanErrorPerPixel, 0.001);
#elif Q16 || Q16HDRI
        Assert.AreEqual(1803.2, errorInfo.MeanErrorPerPixel, 0.1);
#else
#error Not implemented!
#endif
        Assert.AreEqual(0.352, errorInfo.NormalizedMaximumError, 0.002);
        Assert.AreEqual(0.001, errorInfo.NormalizedMeanError, 0.001);
      }
    }

    [TestMethod]
    public void Test_RandomThreshold()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.RandomThreshold((QuantumType)(Quantum.Max / 4), (QuantumType)(Quantum.Max / 2));

        ColorAssert.AreEqual(MagickColors.Black, image, 52, 52);
        ColorAssert.AreEqual(MagickColors.White, image, 75, 52);
        ColorAssert.AreEqual(MagickColors.Red, image, 31, 90);
        ColorAssert.AreEqual(MagickColors.Lime, image, 69, 90);
        ColorAssert.AreEqual(MagickColors.Blue, image, 120, 90);
      }
    }

    [TestMethod]
    public void Test_Raise_Lower()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Raise(30);

        ColorAssert.AreEqual(new MagickColor("#6ee29508b532"), image, 29, 30);
        ColorAssert.AreEqual(new MagickColor("#2f2054867aac"), image, 570, 265);
      }

      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Lower(30);

        ColorAssert.AreEqual(new MagickColor("#2da153c773f1"), image, 29, 30);
        ColorAssert.AreEqual(new MagickColor("#706195c7bbed"), image, 570, 265);
      }
    }

    [TestMethod]
    public void Test_Read()
    {
      MagickImage image = new MagickImage();

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        image.Read(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Read((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Read((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        image.Read((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        image.Read(Files.Missing);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        image.Read("png:" + Files.Missing);
      });

      image.Read(File.ReadAllBytes(Files.SnakewarePNG));
      Assert.AreEqual(286, image.Width);
      Assert.AreEqual(67, image.Height);

      using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
      {
        image.Read(fs);
        Assert.AreEqual(286, image.Width);
        Assert.AreEqual(67, image.Height);
        Assert.AreEqual(MagickFormat.Png, image.Format);
      }

      image.Read(Files.SnakewarePNG);
      Assert.AreEqual(286, image.Width);
      Assert.AreEqual(67, image.Height);
      Assert.AreEqual(MagickFormat.Png, image.Format);

      image.Read(Files.Builtin.Rose);
      Assert.AreEqual(70, image.Width);
      Assert.AreEqual(46, image.Height);
      Assert.AreEqual(MagickFormat.Ppm, image.Format);

      image.Read(Files.RoseSparkleGIF);
      Assert.AreEqual("RöseSparkle.gif", Path.GetFileName(image.FileName));
      Assert.AreEqual(70, image.Width);
      Assert.AreEqual(46, image.Height);
      Assert.AreEqual(MagickFormat.Gif, image.Format);

      image.Read("png:" + Files.SnakewarePNG);
      Assert.AreEqual(286, image.Width);
      Assert.AreEqual(67, image.Height);
      Assert.AreEqual(MagickFormat.Png, image.Format);

      MagickColor red = new MagickColor("red");

      image.Read(red, 50, 50);
      Assert.AreEqual(50, image.Width);
      Assert.AreEqual(50, image.Height);
      ColorAssert.AreEqual(red, image, 10, 10);

      image.Read("xc:red", 50, 50);
      Assert.AreEqual(50, image.Width);
      Assert.AreEqual(50, image.Height);
      ColorAssert.AreEqual(red, image, 5, 5);

      red = new MagickColor("cmyk(0%,100%,100%,0)");

      image.Read(red, 50, 50);
      Assert.AreEqual(50, image.Width);
      Assert.AreEqual(50, image.Height);
      Assert.AreEqual(ColorSpace.CMYK, image.ColorSpace);
      image.Clamp();
      ColorAssert.AreEqual(red, image, 10, 10);

      using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
      {
        image.Read(fs);

        fs.Position = 0;
        using (PartialStream partialStream = new PartialStream(fs, true))
        {
          using (MagickImage testImage = new MagickImage())
          {
            testImage.Read(partialStream);

            Assert.AreEqual(image.Width, testImage.Width);
            Assert.AreEqual(image.Height, testImage.Height);
            Assert.AreEqual(image.Format, testImage.Format);
            Assert.AreEqual(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
          }
        }

        fs.Position = 0;
        using (PartialStream partialStream = new PartialStream(fs, false))
        {
          using (MagickImage testImage = new MagickImage())
          {
            testImage.Read(partialStream);

            Assert.AreEqual(image.Width, testImage.Width);
            Assert.AreEqual(image.Height, testImage.Height);
            Assert.AreEqual(image.Format, testImage.Format);
            Assert.AreEqual(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
          }
        }
      }

      using (FileStream fs = File.OpenRead(Files.Logos.MagickNETSVG))
      {
        byte[] buffer = new byte[fs.Length + 1];
        fs.Read(buffer, 0, (int)fs.Length);

        using (MemoryStream memStream = new MemoryStream(buffer, 0, (int)fs.Length))
        {
          image.Read(memStream, new MagickReadSettings()
          {
            Density = new Density(72)
          });

          ColorAssert.AreEqual(new MagickColor("#231f20"), image, 129, 101);
        }
      }

      image.Dispose();

      ExceptionAssert.Throws<ObjectDisposedException>(delegate ()
      {
        image.BackgroundColor = MagickColors.PaleGreen;
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        using (TestStream testStream = new TestStream(false, true, true))
        {
          new MagickImage(testStream);
        }
      });
    }

    [TestMethod]
    public void Test_RegionMask()
    {
      using (MagickImage red = new MagickImage("xc:red", 100, 100))
      {
        using (MagickImage green = new MagickImage("xc:green", 100, 100))
        {
          green.RegionMask(new MagickGeometry(10, 10, 50, 50));

          green.Composite(red, CompositeOperator.SrcOver);

          ColorAssert.AreEqual(MagickColors.Green, green, 0, 0);
          ColorAssert.AreEqual(MagickColors.Red, green, 10, 10);
          ColorAssert.AreEqual(MagickColors.Green, green, 60, 60);

          green.RemoveRegionMask();

          green.Composite(red, CompositeOperator.SrcOver);

          ColorAssert.AreEqual(MagickColors.Red, green, 0, 0);
          ColorAssert.AreEqual(MagickColors.Red, green, 10, 10);
          ColorAssert.AreEqual(MagickColors.Red, green, 60, 60);
        }
      }
    }

    [TestMethod]
    public void Test_Resample()
    {
      using (MagickImage image = new MagickImage("xc:red", 100, 100))
      {
        image.Resample(new PointD(300));

        Assert.AreEqual(300, image.Density.X);
        Assert.AreEqual(300, image.Density.Y);
        Assert.AreNotEqual(100, image.Width);
        Assert.AreNotEqual(100, image.Height);
      }
    }

    [TestMethod]
    public void Test_Resize()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Read(Files.MagickNETIconPNG);
        image.Resize(new MagickGeometry(64, 64));
        Assert.AreEqual(64, image.Width);
        Assert.AreEqual(64, image.Height);

        image.Read(Files.MagickNETIconPNG);
        image.Resize((Percentage)200);
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
        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.Resize(percentage);
        });
      }
    }

    [TestMethod]
    public void Test_Roll()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.Roll(40, 60);

        MagickColor blue = new MagickColor("#a8dff8");
        ColorAssert.AreEqual(blue, image, 66, 103);
        ColorAssert.AreEqual(blue, image, 120, 86);
        ColorAssert.AreEqual(blue, image, 0, 82);
      }
    }

    [TestMethod]
    public void Test_Rotate()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        Assert.AreEqual(640, image.Width);
        Assert.AreEqual(480, image.Height);

        image.Rotate(90);

        Assert.AreEqual(480, image.Width);
        Assert.AreEqual(640, image.Height);
      }
    }

    [TestMethod]
    public void Test_RotationalBlur()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.RotationalBlur(20);

#if Q8
        ColorAssert.AreEqual(new MagickColor("#fbfbfb2b"), image, 10, 10);
        ColorAssert.AreEqual(new MagickColor("#8b0303"), image, 13, 67);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#167516", "#167616")), image, 63, 67);
        ColorAssert.AreEqual(new MagickColor("#3131fc"), image, 125, 67);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#fbf7fbf7fbf72aab"), image, 10, 10);
        ColorAssert.AreEqual(new MagickColor("#8b2102990299"), image, 13, 67);
        ColorAssert.AreEqual(new MagickColor("#159275F21592"), image, 63, 67);
        ColorAssert.AreEqual(new MagickColor("#31853185fd47"), image, 125, 67);
#else
#error Not implemented!
#endif
      }

      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.RotationalBlur(20, Channels.RGB);

#if Q8
        ColorAssert.AreEqual(new MagickColor("#fbfbfb80"), image, 10, 10);
        ColorAssert.AreEqual(new MagickColor("#8b0303"), image, 13, 67);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#167516", "#167616")), image, 63, 67);
        ColorAssert.AreEqual(new MagickColor("#3131fc"), image, 125, 67);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(new MagickColor("#fbf7fbf7fbf78000"), image, 10, 10);
        ColorAssert.AreEqual(new MagickColor("#8b2102990299"), image, 13, 67);
        ColorAssert.AreEqual(new MagickColor("#159275f21592"), image, 63, 67);
        ColorAssert.AreEqual(new MagickColor("#31853185fd47"), image, 125, 67);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Sample()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Sample(400, 400);
        Assert.AreEqual(400, image.Width);
        Assert.AreEqual(300, image.Height);
      }
    }

    [TestMethod]
    public void Test_Scale()
    {
      using (MagickImage image = new MagickImage(Files.CirclePNG))
      {
        MagickColor color = MagickColor.FromRgba(255, 255, 255, 159);
        ColorAssert.AreEqual(color, image, image.Width / 2, image.Height / 2);

        image.Scale((Percentage)400);
        ColorAssert.AreEqual(color, image, image.Width / 2, image.Height / 2);
      }
    }

    [TestMethod]
    public void Test_Segment()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.Segment();

        ColorAssert.AreEqual(new MagickColor("#008300"), image, 77, 30);
        ColorAssert.AreEqual(new MagickColor("#f9f9f9"), image, 79, 30);
        ColorAssert.AreEqual(new MagickColor("#00c2fe"), image, 128, 62);
      }
    }

    [TestMethod]
    public void Test_SelectiveBlur()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.SelectiveBlur(5.0, 2.0, Quantum.Max / 2);

        using (MagickImage original = new MagickImage(Files.NoisePNG))
        {
          Assert.AreEqual(0.07777, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00002);
        }
      }
    }

    [TestMethod]
    public void Test_Separate()
    {
      using (MagickImage rose = new MagickImage(Files.Builtin.Rose))
      {
        int i = 0;
        foreach (MagickImage image in rose.Separate())
        {
          i++;
          image.Dispose();
        }

        Assert.AreEqual(3, i);

        i = 0;
        foreach (MagickImage image in rose.Separate(Channels.Red | Channels.Green))
        {
          i++;
          image.Dispose();
        }

        Assert.AreEqual(2, i);
      }
    }

    [TestMethod]
    public void Test_Separate_Composite()
    {
      using (MagickImage logo = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage blue = logo.Separate(Channels.Blue).First())
        {
          Test_Separate_Composite(blue, ColorSpace.Gray, 146);

          using (MagickImage green = logo.Separate(Channels.Green).First())
          {
            Test_Separate_Composite(green, ColorSpace.Gray, 62);

            blue.Composite(green, CompositeOperator.Modulate);

            Test_Separate_Composite(blue, ColorSpace.sRGB, 15);
          }
        }
      }
    }

    [TestMethod]
    public void Test_SepiaTone()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.SepiaTone();

#if Q8
        ColorAssert.AreEqual(new MagickColor("#472400"), image, 243, 45);
        ColorAssert.AreEqual(new MagickColor("#522e00"), image, 394, 394);
        ColorAssert.AreEqual(new MagickColor("#e4bb7c"), image, 477, 373);
#elif Q16
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#45be23e80000", "#475f24bf0000")), image, 243, 45);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#50852d680000", "#52672e770000")), image, 394, 394);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#e273b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#elif Q16HDRI
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#45be23e90001", "#475f24bf0000")), image, 243, 45);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#50862d690001", "#52672e770000")), image, 394, 394);
        ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#e274b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_SetAttenuate()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.SetAttenuate(5.6);
        Assert.AreEqual("5.6", image.GetArtifact("attenuate"));
      }
    }

    [TestMethod]
    public void Test_SetHighlightColor()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.SetHighlightColor(MagickColors.Fuchsia);
#if Q8
        Assert.AreEqual("#FF00FFFF", image.GetArtifact("highlight-color"));
#elif Q16 || Q16HDRI
        Assert.AreEqual("#FFFF0000FFFFFFFF", image.GetArtifact("highlight-color"));
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_SetLowlightColor()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.SetLowlightColor(MagickColors.Purple);
#if Q8
        Assert.AreEqual("#800080FF", image.GetArtifact("lowlight-color"));
#elif Q16 || Q16HDRI
        Assert.AreEqual("#808000008080FFFF", image.GetArtifact("lowlight-color"));
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_Shade()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.FontPointsize = 90;
        image.Read("label:Magick.NET");

        image.Shade();

        ColorAssert.AreEqual(new MagickColor("#7fff7fff7fff"), image, 64, 48);
        ColorAssert.AreEqual(MagickColors.Black, image, 118, 48);
        ColorAssert.AreEqual(new MagickColor("#7fff7fff7fff"), image, 148, 48);
      }

      using (MagickImage image = new MagickImage())
      {
        image.Settings.FontPointsize = 90;
        image.Read("label:Magick.NET");

        image.Shade(10, 20, false, Channels.Composite);

        ColorAssert.AreEqual(new MagickColor("#000000000000578e"), image, 64, 48);
        ColorAssert.AreEqual(new MagickColor("#0000000000000000"), image, 118, 48);
        ColorAssert.AreEqual(new MagickColor("#578e578e578e578e"), image, 148, 48);
      }
    }

    [TestMethod]
    public void Test_Shadow()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.BackgroundColor = MagickColors.Transparent;
        image.Settings.FontPointsize = 60;
        image.Read("label:Magick.NET");

        int width = image.Width;
        int height = image.Height;

        image.Shadow(2, 2, 5, new Percentage(50), MagickColors.Red);

        Assert.AreEqual(width + 20, image.Width);
        Assert.AreEqual(height + 20, image.Height);

        using (PixelCollection pixels = image.GetPixels())
        {
          Pixel pixel = pixels.GetPixel(90, 9);
#if Q8 || Q16
          Assert.AreEqual(0, pixel.ToColor().A);
#elif Q16HDRI
          Assert.AreEqual(OpenCLValue.Get(0.5, 0.0), pixel.ToColor().A);
#else
#error Not implemented!
#endif
          pixel = pixels.GetPixel(34, 55);

#if Q8
          Assert.AreEqual(72, pixel.ToColor().A);
#elif Q16 || Q16HDRI
          Assert.AreEqual(18096, (int)pixel.ToColor().A);
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_Sharpen()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.Sharpen(10, 20);
        image.Clamp();

        using (MagickImage original = new MagickImage(Files.NoisePNG))
        {
          Assert.AreEqual(0.06675, image.Compare(original, ErrorMetric.RootMeanSquared), 0.00001);
        }
      }
    }

    [TestMethod]
    public void Test_Shave()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Shave(20, 40);

        Assert.AreEqual(600, image.Width);
        Assert.AreEqual(400, image.Height);
      }
    }

    [TestMethod]
    public void Test_Shear()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.BackgroundColor = MagickColors.Firebrick;
        image.VirtualPixelMethod = VirtualPixelMethod.Background;
        image.Shear(20, 40);

#if Q8
        ColorAssert.AreEqual(MagickColors.Firebrick, image, 45, 6);
        ColorAssert.AreEqual(new MagickColor("#7e7e7efd"), image, 98, 86);
        ColorAssert.AreEqual(MagickColors.Firebrick, image, 158, 181);
#elif Q16 || Q16HDRI
        ColorAssert.AreEqual(MagickColors.Firebrick, image, 45, 6);
        ColorAssert.AreEqual(new MagickColor("#7e907e907e90fd57"), image, 98, 86);
        ColorAssert.AreEqual(MagickColors.Firebrick, image, 158, 181);
#else
#error Not implemented!
#endif
      }
    }

    [TestMethod]
    public void Test_SigmoidalContrast()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.SigmoidalContrast(true, 8.0);

        using (MagickImage original = new MagickImage(Files.NoisePNG))
        {
          Assert.AreEqual(0.07361, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00001);
        }
      }
    }

    [TestMethod]
    public void Test_Signature()
    {
      using (MagickImage image = new MagickImage())
      {
        Assert.AreEqual(0, image.Width);
        Assert.AreEqual(0, image.Height);
        Assert.AreEqual("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", image.Signature);
      }
    }

    [TestMethod]
    public void Test_SparseColors()
    {
      MagickReadSettings settings = new MagickReadSettings();
      settings.Width = 600;
      settings.Height = 60;

      using (MagickImage image = new MagickImage("xc:", settings))
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.SparseColor(Channels.Red, SparseColorMethod.Barycentric, null);
        });

        List<SparseColorArg> args = new List<SparseColorArg>();

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.SparseColor(Channels.Blue, SparseColorMethod.Barycentric, args);
        });

        using (PixelCollection pixels = image.GetPixels())
        {
          ColorAssert.AreEqual(pixels.GetPixel(0, 0).ToColor(), pixels.GetPixel(599, 59).ToColor());
        }

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          args.Add(new SparseColorArg(0, 0, null));
        });

        args.Add(new SparseColorArg(0, 0, MagickColors.SkyBlue));
        args.Add(new SparseColorArg(-600, 60, MagickColors.SkyBlue));
        args.Add(new SparseColorArg(600, 60, MagickColors.Black));

        image.SparseColor(SparseColorMethod.Barycentric, args);

        using (PixelCollection pixels = image.GetPixels())
        {
          ColorAssert.AreNotEqual(pixels.GetPixel(0, 0).ToColor(), pixels.GetPixel(599, 59).ToColor());
        }

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          image.SparseColor(Channels.Black, SparseColorMethod.Barycentric, args);
        });
      }
    }

    [TestMethod]
    public void Test_Sketch()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Resize(400, 400);

        image.Sketch();
        image.ColorType = ColorType.Bilevel;

        ColorAssert.AreEqual(MagickColors.White, image, 63, 100);
        ColorAssert.AreEqual(MagickColors.White, image, 150, 175);
      }
    }

    [TestMethod]
    public void Test_Solarize()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Solarize();

        ColorAssert.AreEqual(MagickColors.Black, image, 125, 125);
        ColorAssert.AreEqual(new MagickColor("#007f7f"), image, 122, 143);
        ColorAssert.AreEqual(new MagickColor("#2e6935"), image, 435, 240);
      }
    }

    [TestMethod]
    public void Test_Splice()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        image.BackgroundColor = MagickColors.Fuchsia;
        image.Splice(new MagickGeometry(105, 50, 10, 20));

        Assert.AreEqual(296, image.Width);
        Assert.AreEqual(87, image.Height);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 105, 50);
        ColorAssert.AreEqual(new MagickColor("#0000"), image, 115, 70);
      }
    }

    [TestMethod]
    public void Test_Spread()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        image.Spread(10);

        using (MagickImage original = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          Assert.AreEqual(0.121, original.Compare(image, ErrorMetric.RootMeanSquared), 0.002);
        }
      }
    }

    [TestMethod]
    public void Test_Statistic()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.Statistic(StatisticType.Minimum, 10, 1);

        ColorAssert.AreEqual(MagickColors.Black, image, 42, 119);
        ColorAssert.AreEqual(new MagickColor("#eeeeeeeeeeee"), image, 90, 120);
        ColorAssert.AreEqual(new MagickColor("#999999999999"), image, 90, 168);
      }
    }

    [TestMethod]
    public void Test_Stegano()
    {
      using (MagickImage message = new MagickImage("label:Magick.NET is the best!", 200, 20))
      {
        using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
        {
          image.Stegano(message);

          FileInfo tempFile = new FileInfo(Path.GetTempFileName() + ".png");

          try
          {
            image.Write(tempFile);

            MagickReadSettings settings = new MagickReadSettings();
            settings.Format = MagickFormat.Stegano;
            settings.Width = 200;
            settings.Height = 20;

            using (MagickImage hiddenMessage = new MagickImage(tempFile, settings))
            {
              Assert.AreEqual(0, message.Compare(hiddenMessage, ErrorMetric.RootMeanSquared), 0.001);
            }
          }
          finally
          {
            if (tempFile.Exists)
              tempFile.Delete();
          }
        }
      }
    }

    [TestMethod]
    public void Test_Stereo()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Flop();

        using (MagickImage rightImage = new MagickImage(Files.Builtin.Logo))
        {
          image.Stereo(rightImage);

          ColorAssert.AreEqual(new MagickColor("#2222ffffffff"), image, 250, 375);
          ColorAssert.AreEqual(new MagickColor("#ffff3e3e9292"), image, 380, 375);
        }
      }
    }

    [TestMethod]
    public void Test_Swirl()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Alpha(AlphaOption.Deactivate);

        ColorAssert.AreEqual(MagickColors.Red, image, 287, 74);
        ColorAssert.AreNotEqual(MagickColors.White, image, 363, 333);

        image.Swirl(60);

        ColorAssert.AreNotEqual(MagickColors.Red, image, 287, 74);
        ColorAssert.AreEqual(MagickColors.White, image, 363, 333);
      }
    }

    [TestMethod]
    public void Test_SubImageSearch()
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Add(new MagickImage(MagickColors.Green, 2, 2));
        images.Add(new MagickImage(MagickColors.Red, 2, 2));

        using (MagickImage combined = images.AppendHorizontally())
        {
          using (MagickSearchResult searchResult = combined.SubImageSearch(new MagickImage(MagickColors.Red, 0, 0), ErrorMetric.RootMeanSquared))
          {
            Assert.IsNotNull(searchResult);
            Assert.IsNotNull(searchResult.SimilarityImage);
            Assert.IsNotNull(searchResult.BestMatch);
            Assert.AreEqual(0.0, searchResult.SimilarityMetric);
            Assert.AreEqual(2, searchResult.BestMatch.X);
            Assert.AreEqual(0, searchResult.BestMatch.Y);
            Assert.AreEqual(1, searchResult.BestMatch.Width);
            Assert.AreEqual(1, searchResult.BestMatch.Height);
          }
        }
      }
    }

    [TestMethod]
    public void Test_Texture()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        using (MagickImage canvas = new MagickImage(MagickColors.Fuchsia, 300, 300))
        {
          canvas.Texture(image);

          ColorAssert.AreEqual(MagickColors.Fuchsia, canvas, 72, 68);
          ColorAssert.AreEqual(new MagickColor("#a8a8dfdff8f8"), canvas, 299, 48);
          ColorAssert.AreEqual(new MagickColor("#a8a8dfdff8f8"), canvas, 160, 299);
        }
      }
    }

    [TestMethod]
    public void Test_Tile()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage checkerboard = new MagickImage(Files.Patterns.Checkerboard))
        {
          image.Opaque(MagickColors.White, MagickColors.Transparent);
          image.Tile(checkerboard, CompositeOperator.DstOver);

          ColorAssert.AreEqual(new MagickColor("#66"), image, 578, 260);
        }
      }
    }

    [TestMethod]
    public void Test_Tint()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Settings.FillColor = MagickColors.Gold;
        image.Tint("1x2");
        image.Clamp();

        ColorAssert.AreEqual(new MagickColor("#dee500000000"), image, 400, 205);
        ColorAssert.AreEqual(MagickColors.Black, image, 400, 380);
      }
    }

    [TestMethod]
    public void Test_Threshold()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Threshold(new Percentage(80));
          image.CompressionMethod = CompressionMethod.Group4;
          image.Format = MagickFormat.Pdf;
          image.Write(memStream);
        }
      }
    }

    [TestMethod]
    public void Test_Thumbnail()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        image.Thumbnail(100, 100);
        Assert.AreEqual(100, image.Width);
        Assert.AreEqual(23, image.Height);
      }
    }

    [TestMethod]
    public void Test_ToByteArray()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        byte[] bytes = image.ToByteArray(new DdsWriteDefines()
        {
          Compression = DdsCompression.Dxt5
        });

        image.Read(bytes);
        Assert.AreEqual(CompressionMethod.DXT5, image.CompressionMethod);
        Assert.AreEqual(MagickFormat.Dds, image.Format);

        bytes = image.ToByteArray(MagickFormat.Jpg);

        image.Read(bytes);
        Assert.AreEqual(MagickFormat.Jpeg, image.Format);

        bytes = image.ToByteArray(new DdsWriteDefines()
        {
          Compression = DdsCompression.Dxt5
        });

        image.Read(bytes);
        Assert.AreEqual(CompressionMethod.DXT1, image.CompressionMethod);
        Assert.AreEqual(MagickFormat.Dds, image.Format);
      }
    }

    [TestMethod]
    public void Test_ToString()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Wizard))
      {
        Assert.AreEqual("Gif 480x640 8-bit sRGB 97.34kB", image.ToString());
      }

      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.AreEqual("Png 128x128 16-bit sRGB 22.93kB", image.ToString());
      }
    }

    [TestMethod]
    public void Test_TotalColors()
    {
      using (MagickImage image = new MagickImage())
      {
        Assert.AreEqual(0, image.TotalColors);

        image.Read(Files.Builtin.Logo);
        Assert.AreNotEqual(0, image.TotalColors);
      }
    }

    [TestMethod]
    public void Test_Transparent()
    {
      MagickColor red = new MagickColor("red");
      MagickColor transparentRed = new MagickColor("red");
      transparentRed.A = 0;

      using (MagickImage image = new MagickImage(Files.RedPNG))
      {
        ColorAssert.AreEqual(red, image, 0, 0);

        image.Transparent(red);

        ColorAssert.AreEqual(transparentRed, image, 0, 0);
        ColorAssert.AreNotEqual(transparentRed, image, image.Width - 1, 0);
      }

      using (MagickImage image = new MagickImage(Files.RedPNG))
      {
        ColorAssert.AreEqual(red, image, 0, 0);

        image.InverseTransparent(red);

        ColorAssert.AreNotEqual(transparentRed, image, 0, 0);
        ColorAssert.AreEqual(transparentRed, image, image.Width - 1, 0);
      }
    }

    [TestMethod]
    public void Test_TransparentChroma()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.TransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

        ColorAssert.AreEqual(new MagickColor("#3962396239620000"), image, 50, 50);
        ColorAssert.AreEqual(new MagickColor("#0000"), image, 32, 80);
        ColorAssert.AreEqual(new MagickColor("#f6def6def6deffff"), image, 132, 42);
        ColorAssert.AreEqual(new MagickColor("#0000808000000000"), image, 74, 79);
      }

      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.InverseTransparentChroma(MagickColors.Black, MagickColors.WhiteSmoke);

        ColorAssert.AreEqual(new MagickColor("#396239623962ffff"), image, 50, 50);
        ColorAssert.AreEqual(new MagickColor("#000f"), image, 32, 80);
        ColorAssert.AreEqual(new MagickColor("#f6def6def6de0000"), image, 132, 42);
        ColorAssert.AreEqual(new MagickColor("#000080800000ffff"), image, 74, 79);
      }
    }

    [TestMethod]
    public void Test_Transpose()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Transpose();

        Assert.AreEqual(480, image.Width);
        Assert.AreEqual(640, image.Height);

        ColorAssert.AreEqual(MagickColors.Red, image, 61, 292);
        ColorAssert.AreEqual(new MagickColor("#f5f5eeee3636"), image, 104, 377);
        ColorAssert.AreEqual(new MagickColor("#eded1f1f2424"), image, 442, 391);
      }
    }

    [TestMethod]
    public void Test_Transverse()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Transverse();

        Assert.AreEqual(480, image.Width);
        Assert.AreEqual(640, image.Height);

        ColorAssert.AreEqual(MagickColors.Red, image, 330, 508);
        ColorAssert.AreEqual(new MagickColor("#f5f5eeee3636"), image, 288, 474);
        ColorAssert.AreEqual(new MagickColor("#cdcd20202727"), image, 30, 123);
      }
    }

    [TestMethod]
    public void Test_Trim()
    {
      using (MagickImage image = new MagickImage("xc:fuchsia", 50, 50))
      {
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 49, 49);

        image.Extent(100, 60, Gravity.Center, MagickColors.Gold);

        Assert.AreEqual(100, image.Width);
        Assert.AreEqual(60, image.Height);
        ColorAssert.AreEqual(MagickColors.Gold, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 50, 30);

        image.Trim();

        Assert.AreEqual(50, image.Width);
        Assert.AreEqual(50, image.Height);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 49, 49);
      }
    }

    [TestMethod]
    public void Test_TransformColorSpace()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        Assert.AreEqual(ColorSpace.sRGB, image.ColorSpace);

        image.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.USWebCoatedSWOP);
        Assert.AreEqual(ColorSpace.sRGB, image.ColorSpace);

        image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
        Assert.AreEqual(ColorSpace.CMYK, image.ColorSpace);
      }
    }

    [TestMethod]
    public void Test_UniqueColors()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage uniqueColors = image.UniqueColors())
        {
          Assert.AreEqual(1, uniqueColors.Height);
          Assert.AreEqual(256, uniqueColors.Width);
        }
      }
    }

    [TestMethod]
    public void Test_UnsharpMask()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
        image.UnsharpMask(7.0, 3.0);

        using (MagickImage original = new MagickImage(Files.NoisePNG))
        {
#if Q8 || Q16
          Assert.AreEqual(0.06476, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00002);
#elif Q16HDRI
          Assert.AreEqual(0.10234, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00001);
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_Vignette()
    {
      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.BackgroundColor = MagickColors.Aqua;
        image.Vignette();

        ColorAssert.AreEqual(new MagickColor("#647cffffffff"), image, 292, 0);
        ColorAssert.AreEqual(new MagickColor("#91abffffffff"), image, 358, 479);
      }
    }

    [TestMethod]
    public void Test_VirtualPixelMethod()
    {
      using (MagickImage image = new MagickImage())
      {
        Assert.AreEqual(image.VirtualPixelMethod, VirtualPixelMethod.Undefined);
        image.VirtualPixelMethod = VirtualPixelMethod.Random;
        Assert.AreEqual(image.VirtualPixelMethod, VirtualPixelMethod.Random);
      }
    }

    [TestMethod]
    public void Test_Wave()
    {
      using (MagickImage image = new MagickImage(Files.TestPNG))
      {
        image.Wave();

        using (MagickImage original = new MagickImage(Files.TestPNG))
        {
#if Q8
          Assert.AreEqual(0.63104, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00001);
#elif Q16 || Q16HDRI
          Assert.AreEqual(0.63109, original.Compare(image, ErrorMetric.RootMeanSquared), 0.00001);
#else
#error Not implemented!
#endif
        }
      }
    }

    [TestMethod]
    public void Test_WaveletDenoise()
    {
      using (MagickImage image = new MagickImage(Files.NoisePNG))
      {
#if Q8
        MagickColor color = new MagickColor("#dd");
#elif Q16
        MagickColor color = new MagickColor(OpenCLValue.Get("#dea4dea4dea4", "#deb5deb5deb5"));
#elif Q16HDRI
        MagickColor color = new MagickColor(OpenCLValue.Get("#dea5dea5dea5", "#deb5deb5deb5"));
#else
#error Not implemented!
#endif

        ColorAssert.AreNotEqual(color, image, 130, 123);

        image.ColorType = ColorType.TrueColor;
        image.WaveletDenoise((Percentage)25);

        ColorAssert.AreEqual(color, image, 130, 123);
      }
    }

    [TestMethod]
    public void Test_Warning()
    {
      int count = 0;
      EventHandler<WarningEventArgs> warningDelegate = delegate (object sender, WarningEventArgs arguments)
      {
        Assert.IsNotNull(sender);
        Assert.IsNotNull(arguments);
        Assert.IsNotNull(arguments.Message);
        Assert.AreNotEqual("", arguments.Message);
        Assert.IsNotNull(arguments.Exception);

        count++;
      };

      using (MagickImage image = new MagickImage())
      {
        image.Warning += warningDelegate;
        image.Read(Files.EightBimTIF);

        Assert.AreNotEqual(0, count);

        int expectedCount = count;
        image.Warning -= warningDelegate;
        image.Read(Files.EightBimTIF);

        Assert.AreEqual(expectedCount, count);
      }
    }

    [TestMethod]
    public void Test_WhiteThreshold()
    {
      using (MagickImage image = new MagickImage(Files.MagickNETIconPNG))
      {
        image.WhiteThreshold(new Percentage(10));
        ColorAssert.AreEqual(MagickColors.White, image, 43, 74);
        ColorAssert.AreEqual(MagickColors.White, image, 60, 74);
      }
    }

    [TestMethod]
    public void Test_Write()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write((FileInfo)null);
        }
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write(new FileInfo("foo"), null);
        }
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write((string)null);
        }
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write("");
        }
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write("foo", null);
        }
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          image.Write((Stream)null);
        }
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        using (MagickImage image = new MagickImage())
        {
          using (MemoryStream memStream = new MemoryStream())
          {
            image.Write(memStream, null);
          }
        }
      });

      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        long fileSize = image.FileSize;

        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream);

          Assert.AreEqual(fileSize, memStream.Length);

          using (MagickImage result = new MagickImage(memStream))
          {
            Assert.AreEqual(image.Width, result.Width);
            Assert.AreEqual(image.Height, result.Height);
            Assert.AreEqual(MagickFormat.Png, result.Format);
          }
        }
      }

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        MagickFormat format = MagickFormat.Bmp;

        using (MemoryStream memStream = new MemoryStream())
        {
          image.Write(memStream, format);

          using (MagickImage result = new MagickImage(memStream))
          {
            Assert.AreEqual(image.Width, result.Width);
            Assert.AreEqual(image.Height, result.Height);
            Assert.AreEqual(format, result.Format);
          }
        }
      }

      string fileName = Path.GetTempFileName();
      try
      {
        using (MagickImage image = new MagickImage(Files.SnakewarePNG))
        {
          using (MemoryStream memStream = new MemoryStream())
          {
            image.Write(fileName);

            FileInfo file = new FileInfo(fileName);
            Assert.AreEqual(image.FileSize, file.Length);
          }
        }
      }
      finally
      {
        if (File.Exists(fileName))
          File.Delete(fileName);
      }

      fileName = Path.GetTempFileName();
      try
      {
        using (MagickImage image = new MagickImage(Files.Builtin.Logo))
        {
          using (MemoryStream memStream = new MemoryStream())
          {
            FileInfo file = new FileInfo(fileName);
            image.Write(file);

            Assert.AreEqual(image.FileSize, file.Length);
          }
        }
      }
      finally
      {
        if (File.Exists(fileName))
          File.Delete(fileName);
      }
    }
  }
}
