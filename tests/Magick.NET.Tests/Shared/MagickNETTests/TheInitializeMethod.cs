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
using System.IO;
using ImageMagick;
using ImageMagick.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheInitializeMethod : MagickNETTests
        {
            [TestClass]
            public class WithPath
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenPathIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("path", () =>
                    {
                        MagickNET.Initialize((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPathIsInvalid()
                {
                    ExceptionAssert.Throws<ArgumentException>("path", () =>
                    {
                        MagickNET.Initialize("Invalid");
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenXmlFileIsMissing()
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

                /// <summary>
                /// The policy is initialized with <see cref="TestInitializer.InitializeWithCustomPolicy(TestContext)"/> at the start of all tests.
                /// </summary>
                [TestMethod]
                public void ShouldThrowExceptionWhenInitializedWithCustomPolicyThatDisablesReadingPalmFiles()
                {
                    using (var tempFile = new TemporaryFile("test.palm"))
                    {
                        using (var fs = tempFile.OpenWrite())
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
            }

            [TestClass]
            public class WithConfigurationFiles
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenConfigurationFilesIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("configFiles", () =>
                    {
                        MagickNET.Initialize((ConfigurationFiles)null);
                    });
                }

                [TestMethod]
                public void ShouldWriteAllFilesInTheReturnedPath()
                {
                    string path = null;
                    try
                    {
                        path = MagickNET.Initialize(ConfigurationFiles.Default);

                        AssertConfigFiles(path);
                    }
                    finally
                    {
                        Cleanup.DeleteDirectory(path);
                    }
                }

                [TestMethod]
                public void ShouldWriteCustomPolicyToDisk()
                {
                    string policy = @"<test/>";

                    string path = null;
                    try
                    {
                        var configFiles = ConfigurationFiles.Default;
                        configFiles.Policy.Data = policy;

                        path = MagickNET.Initialize(configFiles);

                        Assert.AreEqual(policy, File.ReadAllText(Path.Combine(path, "policy.xml")));
                    }
                    finally
                    {
                        Cleanup.DeleteDirectory(path);
                    }
                }
            }

            [TestClass]
            public class WithConfigurationFilesAndPath
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenConfigurationFilesIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("configFiles", () =>
                    {
                        MagickNET.Initialize(null, Path.GetTempPath());
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPathIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("path", () =>
                    {
                        MagickNET.Initialize(ConfigurationFiles.Default, null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenPathIsInvalid()
                {
                    ExceptionAssert.Throws<ArgumentException>("path", () =>
                    {
                        MagickNET.Initialize(ConfigurationFiles.Default, "invalid");
                    }, "Unable to find directory");
                }

                [TestMethod]
                public void ShouldWriteAllFilesInTheReturnedPath()
                {
                    using (var directory = new TemporaryDirectory())
                    {
                        string path = directory.FullName;

                        MagickNET.Initialize(ConfigurationFiles.Default, path);

                        AssertConfigFiles(path);
                    }
                }

                [TestMethod]
                public void CanBeCalledTwice()
                {
                    using (var directory = new TemporaryDirectory())
                    {
                        string path = directory.FullName;

                        MagickNET.Initialize(ConfigurationFiles.Default, path);
                        MagickNET.Initialize(ConfigurationFiles.Default, path);
                    }
                }
            }
        }
    }
}
