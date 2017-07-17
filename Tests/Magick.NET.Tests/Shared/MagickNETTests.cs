// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageMagick;
using ImageMagick.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class MagickNETTests
    {
        [TestMethod]
        public void Features_ContainsExpectedFeatures()
        {
#if Q8 || Q16
#if DEBUG_TEST
            Assert.AreEqual("Debug Cipher DPC OpenCL ", MagickNET.Features);
#else
            Assert.AreEqual("Cipher DPC OpenCL ", MagickNET.Features);
#endif
#elif Q16HDRI
#if DEBUG_TEST
            Assert.AreEqual("Debug Cipher DPC HDRI OpenCL ", MagickNET.Features);
#else
            Assert.AreEqual("Cipher DPC HDRI OpenCL ", MagickNET.Features);
#endif
#else
#error Not implemented!
#endif
        }

        [TestMethod]
        public void FontFamilies_ContainsArial()
        {
            string fontFamily = MagickNET.FontFamilies.FirstOrDefault(f => f == "Arial");
            Assert.IsNotNull(fontFamily);
        }

        [TestMethod]
        public void Initialize_PathIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                MagickNET.Initialize((string)null);
            });
        }

        [TestMethod]
        public void Initialize_PathIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                MagickNET.Initialize("Invalid");
            });
        }

        [TestMethod]
        public void Initialize_XmlFileIsMissing_ThrowsException()
        {
            string path = Files.Root + @"..\..\Source\Magick.NET.Native\Resources\xml";
            foreach (string fileName in Directory.GetFiles(path, "*.xml"))
            {
                string tempFile = fileName + ".tmp";

                Cleanup.DeleteFile(tempFile);

                File.Move(fileName, tempFile);

                ExceptionAssert.Throws<ArgumentException>(() =>
                {
                    MagickNET.Initialize(path);
                }, "MagickNET._ImageMagickFiles does not contain: " + Path.GetFileName(fileName));

                File.Move(tempFile, fileName);
            }
        }

        [TestMethod]
        public void Initialize_ConfigurationFilesIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                MagickNET.Initialize((ConfigurationFiles)null);
            });
        }

        [TestMethod]
        public void Initialize_WithConfigurationFiles_FolderContainsAllFiles()
        {
            string path = null;
            try
            {
                path = MagickNET.Initialize(ConfigurationFiles.Default);

                AssertFiles(path);
            }
            finally
            {
                Cleanup.DeleteDirectory(path);
            }
        }

        [TestMethod]
        public void Initialize_WithCustomPolicy_PolicyIsWrittenToDisk()
        {
            string policy = @"<test/>";

            string path = null;
            try
            {
                ConfigurationFiles configFiles = ConfigurationFiles.Default;
                configFiles.Policy.Data = policy;

                path = MagickNET.Initialize(configFiles);

                Assert.AreEqual(policy, File.ReadAllText(Path.Combine(path, "policy.xml")));
            }
            finally
            {
                Cleanup.DeleteDirectory(path);
            }
        }

        [TestMethod]
        public void Initialize_WithPathAndConfigurationFilesIsNull_ThrowsException()
        {
            ArgumentNullException exception = ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                MagickNET.Initialize(null, Path.GetTempPath());
            });

            Assert.AreEqual("configFiles", exception.ParamName);
        }

        [TestMethod]
        public void Initialize_WithPathAndPathIsNull_ThrowsException()
        {
            ArgumentNullException exception = ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                MagickNET.Initialize(ConfigurationFiles.Default, null);
            });

            Assert.AreEqual("path", exception.ParamName);
        }

        [TestMethod]
        public void Initialize_WithPathAndPathIsInvalid_ThrowsException()
        {
            ArgumentException exception = ExceptionAssert.Throws<ArgumentException>(() =>
            {
                MagickNET.Initialize(ConfigurationFiles.Default, "invalid");
            });

            Assert.AreEqual("path", exception.ParamName);
            Assert.IsTrue(exception.Message.Contains("Unable to find directory"));
        }

        [TestMethod]
        public void Initialize_WithPathAndConfigurationFiles_FolderContainsAllFiles()
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string path = directory.FullName;

                MagickNET.Initialize(ConfigurationFiles.Default, path);

                AssertFiles(path);
            }
        }

        [TestMethod]
        public void Initialize_CanBeCalledTwice()
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string path = directory.FullName;

                MagickNET.Initialize(ConfigurationFiles.Default, path);
                MagickNET.Initialize(ConfigurationFiles.Default, path);
            }
        }

        /// <summary>
        /// The policy is initialized with <see cref="TestInitializer.InitializeWithCustomPolicy(TestContext)"/> at the start of all tests.
        /// </summary>
        [TestMethod]
        public void InitializedWithCustomPolicy_ReadPalmFile_ThrowsException()
        {
            using (TemporaryFile tempFile = new TemporaryFile("test.palm"))
            {
                ExceptionAssert.Throws<MagickPolicyErrorException>(() =>
                {
                    using (MagickImage image = new MagickImage(tempFile))
                    {
                    }
                });
            }
        }

        [TestMethod]
        public void Log_OrderedTests()
        {
            Log_LogEventsAreNotSet_LogDelegateIsNotCalled();

            Log_LogEventsAreSet_LogDelegateIsCalled();

            Log_LogDelegateIsRemove_LogDelegateIsNoLongerCalled();
        }

        [TestMethod]
        public void MagickFormats_ContainsFormatInformationForAllFormats()
        {
            List<string> missingFormats = new List<string>();

            foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
            {
                if (format == MagickFormat.Unknown)
                    continue;

                MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
                if (formatInfo == null)
                    missingFormats.Add(format.ToString());
            }

            if (missingFormats.Count > 0)
                Assert.Fail("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
        }

        [TestMethod]
        public void SetRandomSeed_OrderedTests()
        {
            SetRandomSeed_NotSet_ImagesWithPlasmaAreNotEqual();

            SetRandomSeed_SetToFixedValue_ImagesWithPlasmaAreEqual();
        }

        [TestMethod]
        public void SupportedFormats_ContainsNoFormatInformationWithMagickFormatSetToUnknown()
        {
            foreach (MagickFormatInfo formatInfo in MagickNET.SupportedFormats)
            {
                Assert.AreNotEqual(MagickFormat.Unknown, formatInfo.Format, "Unknown format: " + formatInfo.Description + " (" + formatInfo.Module + ")");
            }
        }

        [TestMethod]
        public void Version_ContainsCorrectQuantum()
        {
#if Q8
            StringAssert.Contains(MagickNET.Version, "Q8");
#elif Q16
            StringAssert.Contains(MagickNET.Version, "Q16");
#elif Q16HDRI
            StringAssert.Contains(MagickNET.Version, "Q16-HDRI");
#else
#error Not implemented!
#endif
        }

        [TestMethod]
        public void SetTempDirectory_PathIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                MagickNET.SetTempDirectory(null);
            });
        }

        [TestMethod]
        public void SetTempDirectory_PathIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                MagickNET.SetTempDirectory("Invalid");
            });
        }

        [TestMethod]
        public void SetTempDirectory_PathIsCorrect_ThrowsNoException()
        {
            MagickNET.SetTempDirectory(Path.GetTempPath());
        }

        private static void AssertFiles(string path)
        {
            Assert.IsTrue(File.Exists(Path.Combine(path, "coder.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "colors.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "configure.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "delegates.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "english.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "locale.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "log.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "magic.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "policy.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "thresholds.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "type.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "type-ghostscript.xml")));
        }

        private void Log_LogEventsAreNotSet_LogDelegateIsNotCalled()
        {
            using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
            {
                int count = 0;
                EventHandler<LogEventArgs> logDelegate = (sender, arguments) =>
               {
                   count++;
               };

                MagickNET.Log += logDelegate;

                image.Flip();
                Assert.AreEqual(0, count);

                MagickNET.Log -= logDelegate;
            }
        }

        private void Log_LogEventsAreSet_LogDelegateIsCalled()
        {
            using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
            {
                int count = 0;
                EventHandler<LogEventArgs> logDelegate = (sender, arguments) =>
                {
                    Assert.IsNull(sender);
                    Assert.IsNotNull(arguments);
                    Assert.AreNotEqual(LogEvents.None, arguments.EventType);
                    Assert.IsNotNull(arguments.Message);
                    Assert.AreNotEqual(0, arguments.Message.Length);

                    count++;
                };

                MagickNET.Log += logDelegate;

                MagickNET.SetLogEvents(LogEvents.All);

                image.Flip();
                Assert.AreNotEqual(0, count);

                MagickNET.Log -= logDelegate;
                count = 0;

                image.Flip();
                Assert.AreEqual(0, count);
            }
        }

        private void Log_LogDelegateIsRemove_LogDelegateIsNoLongerCalled()
        {
            using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
            {
                int count = 0;
                EventHandler<LogEventArgs> logDelegate = (sender, arguments) =>
                {
                    count++;
                };

                MagickNET.Log += logDelegate;

                MagickNET.SetLogEvents(LogEvents.All);

                MagickNET.Log -= logDelegate;

                image.Flip();
                Assert.AreEqual(0, count);
            }
        }

        private void SetRandomSeed_NotSet_ImagesWithPlasmaAreNotEqual()
        {
            using (IMagickImage first = new MagickImage("plasma:red", 10, 10))
            {
                using (IMagickImage second = new MagickImage("plasma:red", 10, 10))
                {
                    Assert.AreNotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                }
            }
        }

        private void SetRandomSeed_SetToFixedValue_ImagesWithPlasmaAreEqual()
        {
            MagickNET.SetRandomSeed(1337);

            using (IMagickImage first = new MagickImage("plasma:red", 10, 10))
            {
                using (IMagickImage second = new MagickImage("plasma:red", 10, 10))
                {
                    Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                }
            }
        }
    }
}
