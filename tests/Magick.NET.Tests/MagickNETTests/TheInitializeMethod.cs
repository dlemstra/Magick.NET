// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheInitializeMethod : MagickNETTests
        {
            public class WithPath
            {
                [Fact]
                public void ShouldThrowExceptionWhenPathIsNull()
                {
                    Assert.Throws<ArgumentNullException>("path", () =>
                    {
                        MagickNET.Initialize((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenPathIsInvalid()
                {
                    Assert.Throws<ArgumentException>("path", () =>
                    {
                        MagickNET.Initialize("Invalid");
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenXmlFileIsMissing()
                {
                    string path = Files.Root + @"../../src/Magick.Native/resources/Release";
#if Q8
                    path += "Q8";
#elif Q16
                    path += "Q16";
#else
                    path += "Q16-HDRI";
#endif

                    foreach (string fileName in Directory.GetFiles(path, "*.xml"))
                    {
                        string tempFile = fileName + ".tmp";

                        Cleanup.DeleteFile(tempFile);

                        File.Move(fileName, tempFile);

                        try
                        {
                            var exception = Assert.Throws<ArgumentException>("path", () =>
                            {
                                MagickNET.Initialize(path);
                            });

                            Assert.Contains("Unable to find file: " + Path.GetFullPath(fileName), exception.Message);
                        }
                        finally
                        {
                            File.Move(tempFile, fileName);
                        }
                    }
                }

                /// <summary>
                /// The policy is initialized with <see cref="TestCollectionOrderer"/> at the start of all tests.
                /// </summary>
                [Fact]
                public void ShouldThrowExceptionWhenInitializedWithCustomPolicyThatDisablesReadingPalmFiles()
                {
                    using (var tempFile = new TemporaryFile("test.palm"))
                    {
                        using (var fs = tempFile.OpenWrite())
                        {
                            var bytes = new byte[4] { 0, 0, 0, 0 };
                            fs.Write(bytes, 0, bytes.Length);
                        }

                        Assert.Throws<MagickPolicyErrorException>(() =>
                        {
                            using (var image = new MagickImage(tempFile))
                            {
                            }
                        });
                    }
                }
            }

            public class WithConfigurationFiles
            {
                [Fact]
                public void ShouldThrowExceptionWhenConfigurationFilesIsNull()
                {
                    Assert.Throws<ArgumentNullException>("configFiles", () =>
                    {
                        MagickNET.Initialize((ConfigurationFiles)null);
                    });
                }

                [Fact]
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

                [Fact]
                public void ShouldWriteCustomPolicyToDisk()
                {
                    string policy = @"<test/>";

                    string path = null;
                    try
                    {
                        var configFiles = ConfigurationFiles.Default;
                        configFiles.Policy.Data = policy;

                        path = MagickNET.Initialize(configFiles);

                        Assert.Equal(policy, File.ReadAllText(Path.Combine(path, "policy.xml")));
                    }
                    finally
                    {
                        Cleanup.DeleteDirectory(path);
                    }
                }
            }

            public class WithConfigurationFilesAndPath
            {
                [Fact]
                public void ShouldThrowExceptionWhenConfigurationFilesIsNull()
                {
                    Assert.Throws<ArgumentNullException>("configFiles", () =>
                    {
                        MagickNET.Initialize(null, Path.GetTempPath());
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenPathIsNull()
                {
                    Assert.Throws<ArgumentNullException>("path", () =>
                    {
                        MagickNET.Initialize(ConfigurationFiles.Default, null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenPathIsInvalid()
                {
                    var exception = Assert.Throws<ArgumentException>("path", () =>
                    {
                        MagickNET.Initialize(ConfigurationFiles.Default, "invalid");
                    });

                    Assert.Contains("Unable to find directory", exception.Message);
                }

                [Fact]
                public void ShouldWriteAllFilesInTheReturnedPath()
                {
                    using (var directory = new TemporaryDirectory())
                    {
                        string path = directory.FullName;

                        MagickNET.Initialize(ConfigurationFiles.Default, path);

                        AssertConfigFiles(path);
                    }
                }

                [Fact]
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
