// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using System.Text;
using ImageMagick;
using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheNativeResources
    {
        [Fact]
        public void ShouldBeEmbeddedInTheNativeLibrary()
        {
            if (!Runtime.IsWindows)
                Assert.Skip("The embedded resources are only available on Windows.");

#if PLATFORM_x64 || PLATFORM_AnyCPU
            var module = GetModuleHandle(ImageMagick.NativeLibrary.X64Name);
#elif PLATFORM_arm64
            var module = GetModuleHandle(ImageMagick.NativeLibrary.Arm64Name);
#else
            var module = GetModuleHandle(ImageMagick.NativeLibrary.X86Name);
#endif
            Assert.NotEqual(IntPtr.Zero, module);

            foreach (var configurationFile in ConfigurationFiles.Default.All)
            {
                var resource = FindResource(module, configurationFile.FileName, "IMAGEMAGICK");
                Assert.NotEqual(IntPtr.Zero, resource);

                var resourceData = LoadResource(module, resource);
                Assert.NotEqual(IntPtr.Zero, resourceData);

                var resourcePointer = LockResource(resourceData);
                Assert.NotEqual(IntPtr.Zero, resourcePointer);

                var size = SizeofResource(module, resource);
                Assert.NotEqual(0U, size);

                var bytes = new byte[size];
                Marshal.Copy(resourcePointer, bytes, 0, (int)size);

                var data = Encoding.UTF8.GetString(bytes);
                Assert.Equal(configurationFile.Data, data);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LockResource(IntPtr hResData);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);
    }
}
