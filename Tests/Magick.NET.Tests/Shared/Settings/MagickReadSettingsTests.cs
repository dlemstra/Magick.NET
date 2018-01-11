﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MagickReadSettingsTests
    {
        [TestMethod]
        public void Test_Constructor()
        {
            ExceptionAssert.ThrowsArgumentNullException("defines", () =>
            {
                MagickReadSettings settings = new MagickReadSettings((IReadDefines)null);
            });
        }

        [TestMethod]
        public void Test_Collection_Read()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Density = new Density(150);

                collection.Read(Files.RoseSparkleGIF, settings);

                Assert.AreEqual(150, collection[0].Density.X);

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

        [TestMethod]
        public void Extract_DefaultValueIsNull()
        {
            MagickReadSettings settings = new MagickReadSettings();
            Assert.IsNull(settings.ExtractArea);
        }

        [TestMethod]
        public void Extract_SetToSpecificAreaOfImage_OnlyAreaIsRead()
        {
            MagickReadSettings readSettings = new MagickReadSettings()
            {
                ExtractArea = new MagickGeometry(10, 10, 20, 30),
            };

            using (IMagickImage image = new MagickImage(Files.Coders.GrimJp2, readSettings))
            {
                Assert.AreEqual(20, image.Width);
                Assert.AreEqual(30, image.Height);
            }
        }

        [TestMethod]
        public void Test_Image_Exceptions()
        {
            ExceptionAssert.ThrowsArgumentException("readSettings", () =>
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    FrameCount = 2,
                };
                new MagickImage(Files.RoseSparkleGIF, settings);
            });
        }

        [TestMethod]
        public void Test_Image_Read_Density()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Density = new Density(300);

                image.Read(Files.SnakewarePNG, settings);

                Assert.AreEqual(300, image.Density.X);

                settings = null;
                image.Read(Files.ImageMagickJPG, settings);
            }
        }

        [TestMethod]
        public void Test_Image_Read_FrameIndex()
        {
            using (IMagickImage image = new MagickImage(Files.RoseSparkleGIF))
            {
                IMagickImage imageA = new MagickImage();
                IMagickImage imageB = new MagickImage();

                MagickReadSettings settings = new MagickReadSettings();

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.AreEqual(image, imageA);

                settings = new MagickReadSettings();
                settings.FrameIndex = 1;

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.AreNotEqual(image, imageA);

                imageB.Read(Files.RoseSparkleGIF + "[1]");
                Assert.AreEqual(imageA, imageB);

                settings = new MagickReadSettings();
                settings.FrameIndex = 2;

                imageA.Read(Files.RoseSparkleGIF, settings);
                Assert.AreNotEqual(image, imageA);

                imageB.Read(Files.RoseSparkleGIF + "[2]");
                Assert.AreEqual(imageA, imageB);

                ExceptionAssert.Throws<MagickOptionErrorException>(() =>
                {
                    settings = new MagickReadSettings();
                    settings.FrameIndex = 3;

                    imageA.Read(Files.RoseSparkleGIF, settings);
                });

                imageA.Dispose();
                imageB.Dispose();
            }
        }

        [TestMethod]
        public void Test_Image_Read_Dimensions()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Width = 10;

                image.Read("xc:fuchsia", settings);

                Assert.AreEqual(10, image.Width);
                Assert.AreEqual(1, image.Height);

                settings.Width = null;
                settings.Height = 20;

                image.Read("xc:fuchsia", settings);

                Assert.AreEqual(1, image.Width);
                Assert.AreEqual(20, image.Height);

                settings.Width = 30;
                settings.Height = 40;

                image.Read("xc:fuchsia", settings);

                Assert.AreEqual(30, image.Width);
                Assert.AreEqual(40, image.Height);
            }
        }

        [TestMethod]
        public void Test_Image_Read_Scenes()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.RoseSparkleGIF);

                Type type = image.Settings.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                int numberScenes = (int)type.GetProperty("NumberScenes", flags).GetValue(image.Settings, null);
                int scene = (int)type.GetProperty("Scene", flags).GetValue(image.Settings, null);
                string scenes = (string)type.GetProperty("Scenes", flags).GetValue(image.Settings, null);

                Assert.AreEqual(1, numberScenes);
                Assert.AreEqual(0, scene);
                Assert.AreEqual("0-1", scenes);
            }
        }
    }
}
