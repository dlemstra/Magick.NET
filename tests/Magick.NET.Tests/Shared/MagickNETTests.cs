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
            var expected = "Cipher ";
#if WINDOWS_BUILD
            expected += "DPC ";
#endif
#if Q16HDRI
            expected += "HDRI ";
#endif
#if WINDOWS_BUILD
            expected += "OpenCL ";
#endif
#if OPENMP
            expected += "OpenMP(2.0) ";
#endif
#if DEBUG_TEST
            expected = "Debug " + expected;
#endif

            Assert.AreEqual(expected, MagickNET.Features);
        }

        [TestMethod]
        public void FontFamilies_ContainsArial()
        {
            var fontFamilies = MagickNET.FontFamilies.ToArray();
            var fontFamily = fontFamilies.FirstOrDefault(f => f == "Arial");
            Assert.IsNotNull(fontFamily, $"Unable to find Arial in font families: {string.Join(",", fontFamilies)}");
        }

        [TestMethod]
        public void FontFamilies_ContainsNoDuplicates()
        {
            var fontFamilies = MagickNET.FontFamilies.ToArray();
            Assert.AreEqual(fontFamilies.Count(), fontFamilies.Distinct().Count());
        }

        [TestMethod]
        public void FontNames_ContainsArial()
        {
            var fontNames = MagickNET.FontNames.ToArray();
            var fontName = fontNames.FirstOrDefault(f => f == "Arial");
            Assert.IsNotNull(fontName, $"Unable to find Arial in font families: {string.Join(",", fontNames)}");
        }

        [TestMethod]
        public void Initialize_PathIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.Initialize((string)null);
            });
        }

        [TestMethod]
        public void Initialize_PathIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>("path", () =>
            {
                MagickNET.Initialize("Invalid");
            });
        }

        [TestMethod]
        public void Initialize_XmlFileIsMissing_ThrowsException()
        {
            string path = Files.Root + @"../../src/Magick.Native/resources/Release";
#if Q8
            path += "Q8";
#elif Q16
            path += "Q16";
#elif Q16HDRI
            path += "Q16-HDRI";
#else
#error Not implemented!
#endif

            foreach (string fileName in Directory.GetFiles(path, "*.xml"))
            {
                string tempFile = fileName + ".tmp";

                Cleanup.DeleteFile(tempFile);

                File.Move(fileName, tempFile);

                try
                {
                    ExceptionAssert.Throws<ArgumentException>("path", () =>
                    {
                        MagickNET.Initialize(path);
                    }, "Unable to find file: " + Path.GetFullPath(fileName));
                }
                finally
                {
                    File.Move(tempFile, fileName);
                }
            }
        }

        [TestMethod]
        public void Initialize_ConfigurationFilesIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>("configFiles", () =>
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
            ExceptionAssert.Throws<ArgumentNullException>("configFiles", () =>
            {
                MagickNET.Initialize(null, Path.GetTempPath());
            });
        }

        [TestMethod]
        public void Initialize_WithPathAndPathIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.Initialize(ConfigurationFiles.Default, null);
            });
        }

        [TestMethod]
        public void Initialize_WithPathAndPathIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>("path", () =>
            {
                MagickNET.Initialize(ConfigurationFiles.Default, "invalid");
            }, "Unable to find directory");
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
                using (FileStream fs = tempFile.OpenWrite())
                {
                    byte[] bytes = new byte[4] { 255, 255, 255, 255 };
                    fs.Write(bytes, 0, bytes.Length);
                }

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
            ExceptionAssert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.SetTempDirectory(null);
            });
        }

        [TestMethod]
        public void SetTempDirectory_PathIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>("path", () =>
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
            Assert.IsTrue(File.Exists(Path.Combine(path, "colors.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "configure.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "delegates.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "english.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "locale.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "log.xml")));
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

                MagickNET.SetLogEvents(LogEvents.Detailed);

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

                MagickNET.SetLogEvents(LogEvents.Detailed);

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
            MagickNET.SetRandomSeed(42);

            using (IMagickImage first = new MagickImage("plasma:red", 10, 10))
            {
                using (IMagickImage second = new MagickImage("plasma:red", 10, 10))
                {
                    Assert.AreEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                }
            }

            MagickNET.ResetRandomSeed();
        }
    }
}
