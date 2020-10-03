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