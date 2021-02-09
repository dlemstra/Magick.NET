// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;

namespace Magick.NET.Tests
{
    internal static class OpenCLValue
    {
        private static bool HasEnabledOpenCLDevices
        {
            get
            {
                if (OpenCL.IsEnabled == false)
                    return false;

                foreach (OpenCLDevice device in OpenCL.Devices)
                {
                    if (device.IsEnabled)
                        return true;
                }

                return false;
            }
        }

        public static void Assert(double expectedWith, double expectedWithout, double value, double delta)
        {
            if (HasEnabledOpenCLDevices)
                Xunit.Assert.InRange(value, expectedWith - delta, expectedWith + delta);
            else
                Xunit.Assert.InRange(value, expectedWithout - delta, expectedWithout + delta);
        }

        public static void Assert<T>(T expectedWith, T expectedWithout, T value)
            => Xunit.Assert.Equal(Get(expectedWith, expectedWithout), value);

        public static T Get<T>(T expectedWith, T expectedWithout)
        {
            if (HasEnabledOpenCLDevices)
                return expectedWith;
            else
                return expectedWithout;
        }
    }
}
