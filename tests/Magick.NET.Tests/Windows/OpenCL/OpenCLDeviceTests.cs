// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using System.Collections.Generic;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class OpenCLDeviceTests
    {
        [Fact]
        public void Test_BenchmarkScore()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotEqual(0.0, device.BenchmarkScore);
            }
        }

        [Fact]
        public void Test_DeviceType()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotEqual(OpenCLDeviceType.Undefined, device.DeviceType);
            }
        }

        [Fact]
        public void Test_IsEnabled()
        {
            foreach (var device in OpenCL.Devices)
            {
                bool isEnabled = device.IsEnabled;

                device.IsEnabled = !isEnabled;
                Assert.NotEqual(isEnabled, device.IsEnabled);

                device.IsEnabled = isEnabled;
                Assert.Equal(isEnabled, device.IsEnabled);
            }
        }

        [Fact]
        public void Test_KernelProfileRecords()
        {
            var device = GetEnabledDevice();
            if (device == null)
                return;

            device.ProfileKernels = true;

            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Resize(500, 500);
                image.Resize(100, 100);
            }

            device.ProfileKernels = false;

            var records = new List<OpenCLKernelProfileRecord>(device.KernelProfileRecords);
            Assert.False(records.Count < 2);

            foreach (var record in records)
            {
                Assert.NotNull(record.Name);
                Assert.False(record.Count < 0);
                Assert.False(record.MaximumDuration < 0);
                Assert.False(record.MinimumDuration < 0);
                Assert.False(record.TotalDuration < 0);

                Assert.Equal(record.AverageDuration, record.TotalDuration / record.Count);
            }
        }

        [Fact]
        public void Test_Name()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotNull(device.Name);
            }
        }

        [Fact]
        public void Test_Version()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotNull(device.Version);
            }
        }

        private OpenCLDevice GetEnabledDevice()
        {
            foreach (var device in OpenCL.Devices)
            {
                if (device.IsEnabled)
                    return device;
            }

            return null;
        }
    }
}

#endif