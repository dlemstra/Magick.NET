// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.IO;
using System.Xml;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickScriptTests
    {
        [TestClass]
        public class TheExecuteMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenReadEventNotSet()
            {
                var script = new MagickScript(Files.Scripts.Events);

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    script.Execute();
                }, "The Read event should be bound when the fileName attribute is not set.");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenReadEventDoesNotSetImage()
            {
                var script = new MagickScript(Files.Scripts.Events);
                script.Read += ReadNothing;

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    script.Execute();
                }, "The Image property should not be null after the Read event has been raised.");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenReadEventIsSetAndUnset()
            {
                var script = new MagickScript(Files.Scripts.Events);
                script.Read += EventsScriptRead;
                script.Read -= EventsScriptRead;

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    script.Execute();
                }, "The Read event should be bound when the fileName attribute is not set.");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWriteEventIsNotSet()
            {
                var script = new MagickScript(Files.Scripts.Events);
                script.Read += EventsScriptRead;

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    script.Execute();
                }, "The Write event should be bound when the fileName attribute is not set.");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWriteEventIsSetAndUnset()
            {
                var script = new MagickScript(Files.Scripts.Events);
                script.Read += EventsScriptRead;
                script.Write += EventsScriptWrite;
                script.Write -= EventsScriptWrite;

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    script.Execute();
                }, "The Write event should be bound when the fileName attribute is not set.");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenVariableNotSet()
            {
                var script = new MagickScript(Files.Scripts.Variables);

                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("attribute", () =>
                    {
                        script.Execute(image);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenVariableHasInvalidValue()
            {
                var script = new MagickScript(Files.Scripts.Variables);
                script.Variables["width"] = "test";

                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    ExceptionAssert.Throws<FormatException>(() =>
                    {
                        script.Execute(image);
                    });
                }
            }

            [TestMethod]
            public void ShouldExecuteTheCollectionScript()
            {
                var script = new MagickScript(Files.Scripts.Collection);
                script.Read += CollectionScriptRead;

                IMagickImage image = script.Execute();

                Assert.IsNotNull(image);
                Assert.AreEqual(MagickFormat.Png, image.Format);
                Assert.AreEqual(128, image.Width);
                Assert.AreEqual(128, image.Height);
            }

            [TestMethod]
            public void ShouldExecuteTheDefinesScript()
            {
                var script = new MagickScript(Files.Scripts.Defines);
                script.Read += DefinesScriptRead;

                IMagickImage image = script.Execute();

                Assert.IsNotNull(image);
                Assert.AreEqual(827, image.Width);
                Assert.AreEqual(700, image.Height);
            }

            [TestMethod]
            public void ShouldExecuteTheDistortScript()
            {
                var script = new MagickScript(Files.Scripts.Distort);
                IMagickImage image = script.Execute();

                Assert.IsNotNull(image);
                Assert.AreEqual(500, image.Width);
                Assert.AreEqual(500, image.Height);
            }

            [TestMethod]
            public void ShouldExecuteTheDrawScript()
            {
                XmlDocument doc = new XmlDocument();
                using (FileStream stream = File.OpenRead(Files.Scripts.Draw))
                {
                    doc.Load(stream);
                }

                var script = new MagickScript(doc.CreateNavigator());

                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    script.Execute(image);
                }
            }

            [TestMethod]
            public void ShouldExecuteTheEventsScript()
            {
                var script = new MagickScript(Files.Scripts.Events);
                script.Read += EventsScriptRead;
                script.Write += EventsScriptWrite;

                script.Execute();
            }

            [TestMethod]
            public void ShouldExecuteTheImageProfileScript()
            {
                var script = new MagickScript(Files.Scripts.ImageProfile);

                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    var colorProfile = image.GetColorProfile();
                    Assert.IsNull(colorProfile);

                    script.Execute(image);

                    colorProfile = image.GetColorProfile();

                    Assert.IsNotNull(colorProfile);
                    Assert.AreEqual(colorProfile.ToByteArray().Length, ColorProfile.SRGB.ToByteArray().Length);
                }
            }

            [TestMethod]
            public void ShouldExecutePixelReadSettingsScript()
            {
                var script = new MagickScript(Files.Scripts.PixelReadSettings);
                script.Read += PixelReadSettingsRead;

                using (IMagickImage image = script.Execute())
                {
                    Assert.AreEqual(1, image.Width);
                    Assert.AreEqual(2, image.Height);
                    Assert.AreEqual(1, image.ChannelCount);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Black, image, 0, 1);
                }
            }

            [TestMethod]
            public void ShouldExecuteTheResizeScript()
            {
                var script = new MagickScript(Files.Scripts.Resize);

                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    script.Execute(image);
                    TestResizeResult(image);

                    script.Read += ResizeScriptRead;
                    using (IMagickImage result = script.Execute())
                    {
                        TestResizeResult(result);
                    }
                }
            }

            [TestMethod]
            public void ShouldExecuteTheVariablesScript()
            {
                var script = new MagickScript(Files.Scripts.Variables);
                script.Variables.Set("width", 120);
                script.Variables.Set("height", 150);
                script.Variables["color"] = MagickColors.Yellow;
                script.Variables["fillColor"] = MagickColors.Red;

                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    script.Execute(image);

                    Assert.AreEqual(120, image.Width);
                    Assert.AreEqual(120, image.Height);
                    ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);

                    ColorAssert.AreEqual(MagickColors.Yellow, image.Settings.StrokeColor);
                    ColorAssert.AreEqual(MagickColors.Red, image.Settings.FillColor);
                }
            }

            private static void CollectionScriptRead(object sender, ScriptReadEventArgs arguments)
            {
                switch (arguments.Id)
                {
                    case "icon":
                        arguments.Image = new MagickImage(Files.MagickNETIconPNG, arguments.ReadSettings);
                        break;
                    case "snakeware":
                        arguments.Image = new MagickImage(Files.SnakewarePNG, arguments.ReadSettings);
                        break;
                    default:
                        throw new NotImplementedException(arguments.Id);
                }
            }

            private static void DefinesScriptRead(object sender, ScriptReadEventArgs arguments)
            {
                arguments.Image = new MagickImage(Files.InvitationTIF, arguments.ReadSettings);
                Assert.IsNull(arguments.Image.GetAttribute("exif:PixelXDimension"));
            }

            private static void ReadNothing(object sender, ScriptReadEventArgs arguments)
            {
            }

            private static void EventsScriptRead(object sender, ScriptReadEventArgs arguments)
            {
                Assert.AreEqual("read.id", arguments.Id);
                arguments.Image = new MagickImage(Files.SnakewarePNG, arguments.ReadSettings);
            }

            private static void EventsScriptWrite(object sender, ScriptWriteEventArgs arguments)
            {
                Assert.AreEqual("write.id", arguments.Id);
                Assert.AreEqual(100, arguments.Image.Density.X);
                Assert.AreEqual(100, arguments.Image.Density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerCentimeter, arguments.Image.Density.Units);
            }

            private static void PixelReadSettingsRead(object sender, ScriptReadEventArgs arguments)
            {
                var bytes = new byte[] { 255, 0 };
                arguments.Image = new MagickImage(bytes, arguments.PixelReadSettings);
            }

            private static void TestResizeResult(IMagickImage result)
            {
                Assert.AreEqual("Magick.NET.Resize", result.Comment);
                Assert.AreEqual(62, result.Width);
                Assert.AreEqual(59, result.Height);
            }

            private static void ResizeScriptRead(object sender, ScriptReadEventArgs arguments)
            {
                arguments.Image = new MagickImage(Files.ImageMagickJPG, arguments.ReadSettings);
                Assert.AreEqual("64x64", arguments.Image.Settings.GetDefine(MagickFormat.Jpeg, "size"));
            }
        }
    }
}
