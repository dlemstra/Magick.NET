// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Reflection;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class MagickReadSettingsTests
    {
        [Fact]
        public void Test_Constructor()
        {
            Assert.Throws<ArgumentNullException>("defines", () =>
            {
                MagickReadSettings settings = new MagickReadSettings((IReadDefines)null);
            });
        }

        [Fact]
        public void Test_Collection_Read()
        {
            using (var collection = new MagickImageCollection())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Density = new Density(150);

                collection.Read(Files.RoseSparkleGIF, settings);

                Assert.Equal(150, collection[0].Density.X);

                settings = new MagickReadSettings();
                settings.FrameIndex = 1;

                collection.Read(Files.RoseSparkleGIF, settings);

                Assert.Single(collection);

                settings = new MagickReadSettings();
                settings.FrameIndex = 1;
                settings.FrameCount = 2;

                collection.Read(Files.RoseSparkleGIF, settings);

                Assert.Equal(2, collection.Count);

                settings = null;
                collection.Read(Files.RoseSparkleGIF, settings);
            }
        }

        [Fact]
        public void Extract_DefaultValueIsNull()
        {
            MagickReadSettings settings = new MagickReadSettings();
            Assert.Null(settings.ExtractArea);
        }

        [Fact]
        public void Extract_SetToSpecificAreaOfImage_OnlyAreaIsRead()
        {
            var readSettings = new MagickReadSettings
            {
                ExtractArea = new MagickGeometry(10, 10, 20, 30),
            };

            using (var image = new MagickImage(Files.Coders.GrimJP2, readSettings))
            {
                Assert.Equal(20, image.Width);
                Assert.Equal(30, image.Height);
            }
        }

        [Fact]
        public void Test_Image_Exceptions()
        {
            Assert.Throws<ArgumentException>("readSettings", () =>
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    FrameCount = 2,
                };
                new MagickImage(Files.RoseSparkleGIF, settings);
            });
        }

        [Fact]
        public void Test_Image_Read_Density()
        {
            using (var image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Density = new Density(300);

                image.Read(Files.SnakewarePNG, settings);

                Assert.Equal(300, image.Density.X);

                settings = null;
                image.Read(Files.ImageMagickJPG, settings);
            }
        }

        [Fact]
        public void Test_Image_Read_FrameIndex()
        {
            using (var image = new MagickImage(Files.RoseSparkleGIF))
            {
                var imageA = new MagickImage();
                var imageB = new MagickImage();

                MagickReadSettings settings = new MagickReadSettings();

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.Equal(image, imageA);

                settings = new MagickReadSettings();
                settings.FrameIndex = 1;

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.False(image.Equals(imageA));

                imageB.Read(Files.RoseSparkleGIF + "[1]");
                Assert.True(imageA.Equals(imageB));

                settings = new MagickReadSettings();
                settings.FrameIndex = 2;

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.False(image.Equals(imageA));

                imageB.Read(Files.RoseSparkleGIF + "[2]");
                Assert.True(imageA.Equals(imageB));

                Assert.Throws<MagickOptionErrorException>(() =>
                {
                    settings = new MagickReadSettings();
                    settings.FrameIndex = 3;

                    imageA.Read(Files.RoseSparkleGIF, settings);
                });

                imageA.Dispose();
                imageB.Dispose();
            }
        }

        [Fact]
        public void Test_Image_Read_Dimensions()
        {
            using (var image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Width = 10;

                image.Read("xc:fuchsia", settings);

                Assert.Equal(10, image.Width);
                Assert.Equal(1, image.Height);

                settings.Width = null;
                settings.Height = 20;

                image.Read("xc:fuchsia", settings);

                Assert.Equal(1, image.Width);
                Assert.Equal(20, image.Height);

                settings.Width = 30;
                settings.Height = 40;

                image.Read("xc:fuchsia", settings);

                Assert.Equal(30, image.Width);
                Assert.Equal(40, image.Height);
            }
        }

        [Fact]
        public void Test_Image_Read_Scenes()
        {
            using (var image = new MagickImage())
            {
                image.Read(Files.RoseSparkleGIF);

                Type type = image.Settings.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                int numberScenes = (int)type.GetProperty("NumberScenes", flags).GetValue(image.Settings, null);
                int scene = (int)type.GetProperty("Scene", flags).GetValue(image.Settings, null);
                string scenes = (string)type.GetProperty("Scenes", flags).GetValue(image.Settings, null);

                Assert.Equal(1, numberScenes);
                Assert.Equal(0, scene);
                Assert.Null(scenes);
            }
        }
    }
}
