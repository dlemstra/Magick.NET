//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.Collections.Generic;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class OpenCLDeviceTests
    {
        private OpenCLDevice GetEnabledDevice()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                if (device.IsEnabled)
                    return device;
            }

            return null;
        }

        [TestMethod]
        public void Test_BenchmarkScore()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                Assert.AreNotEqual(device.BenchmarkScore, 0.0);
            }
        }

        [TestMethod]
        public void Test_DeviceType()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                Assert.AreNotEqual(OpenCLDeviceType.Undefined, device.DeviceType);
            }
        }

        [TestMethod]
        public void Test_IsEnabled()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                bool isEnabled = device.IsEnabled;

                device.IsEnabled = !isEnabled;
                Assert.AreNotEqual(isEnabled, device.IsEnabled);

                device.IsEnabled = isEnabled;
                Assert.AreEqual(isEnabled, device.IsEnabled);
            }
        }

        [TestMethod]
        public void Test_KernelProfileRecords()
        {
            OpenCLDevice device = GetEnabledDevice();
            if (device == null)
                Assert.Inconclusive("No OpenCL devices detected.");

            device.ProfileKernels = true;

            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                image.Resize(500, 500);
                image.Resize(100, 100);
            }

            device.ProfileKernels = false;

            List<OpenCLKernelProfileRecord> records = new List<OpenCLKernelProfileRecord>(device.KernelProfileRecords);
            Assert.IsFalse(records.Count < 2);

            foreach (OpenCLKernelProfileRecord record in records)
            {
                Assert.IsNotNull(record.Name);
                Assert.IsFalse(record.Count < 0);
                Assert.IsFalse(record.MaximumDuration < 0);
                Assert.IsFalse(record.MinimumDuration < 0);
                Assert.IsFalse(record.TotalDuration < 0);

                Assert.AreEqual(record.AverageDuration, record.TotalDuration / record.Count);
            }
        }

        [TestMethod]
        public void Test_Name()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                Assert.IsNotNull(device.Name);
            }
        }

        [TestMethod]
        public void Test_Version()
        {
            foreach (OpenCLDevice device in OpenCL.Devices)
            {
                Assert.IsNotNull(device.Version);
            }
        }
    }
}
