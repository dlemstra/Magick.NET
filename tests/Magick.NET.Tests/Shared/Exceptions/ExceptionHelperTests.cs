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
using System.Runtime.InteropServices;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ExceptionHelperTests
    {
        [Fact]
        public unsafe void Test_CreateException()
        {
            AssertCreateException(0, typeof(MagickWarningException));

            AssertCreateException(300, typeof(MagickResourceLimitWarningException));
            AssertCreateException(305, typeof(MagickTypeWarningException));
            AssertCreateException(310, typeof(MagickOptionWarningException));
            AssertCreateException(315, typeof(MagickDelegateWarningException));
            AssertCreateException(320, typeof(MagickMissingDelegateWarningException));
            AssertCreateException(325, typeof(MagickCorruptImageWarningException));
            AssertCreateException(330, typeof(MagickFileOpenWarningException));
            AssertCreateException(335, typeof(MagickBlobWarningException));
            AssertCreateException(340, typeof(MagickStreamWarningException));
            AssertCreateException(345, typeof(MagickCacheWarningException));
            AssertCreateException(350, typeof(MagickCoderWarningException));
            AssertCreateException(352, typeof(MagickWarningException));
            AssertCreateException(355, typeof(MagickModuleWarningException));
            AssertCreateException(360, typeof(MagickDrawWarningException));
            AssertCreateException(365, typeof(MagickImageWarningException));
            AssertCreateException(370, typeof(MagickWarningException));
            AssertCreateException(375, typeof(MagickWarningException));
            AssertCreateException(380, typeof(MagickWarningException));
            AssertCreateException(385, typeof(MagickWarningException));
            AssertCreateException(390, typeof(MagickRegistryWarningException));
            AssertCreateException(395, typeof(MagickConfigureWarningException));
            AssertCreateException(399, typeof(MagickPolicyWarningException));

            AssertCreateException(386, typeof(MagickWarningException));
            AssertCreateException(100, typeof(MagickWarningException));

            AssertCreateException(400, typeof(MagickResourceLimitErrorException));
            AssertCreateException(405, typeof(MagickTypeErrorException));
            AssertCreateException(410, typeof(MagickOptionErrorException));
            AssertCreateException(415, typeof(MagickDelegateErrorException));
            AssertCreateException(420, typeof(MagickMissingDelegateErrorException));
            AssertCreateException(425, typeof(MagickCorruptImageErrorException));
            AssertCreateException(430, typeof(MagickFileOpenErrorException));
            AssertCreateException(435, typeof(MagickBlobErrorException));
            AssertCreateException(440, typeof(MagickStreamErrorException));
            AssertCreateException(445, typeof(MagickCacheErrorException));
            AssertCreateException(450, typeof(MagickCoderErrorException));
            AssertCreateException(452, typeof(MagickErrorException));
            AssertCreateException(455, typeof(MagickModuleErrorException));
            AssertCreateException(460, typeof(MagickDrawErrorException));
            AssertCreateException(465, typeof(MagickImageErrorException));
            AssertCreateException(470, typeof(MagickErrorException));
            AssertCreateException(475, typeof(MagickErrorException));
            AssertCreateException(480, typeof(MagickErrorException));
            AssertCreateException(485, typeof(MagickErrorException));
            AssertCreateException(490, typeof(MagickRegistryErrorException));
            AssertCreateException(495, typeof(MagickConfigureErrorException));
            AssertCreateException(499, typeof(MagickPolicyErrorException));

            AssertCreateException(486, typeof(MagickErrorException));
            AssertCreateException(700, typeof(MagickErrorException));

            /* These are just here to test all the if branches that are created */
            AssertCreateException(306, typeof(MagickWarningException));
            AssertCreateException(324, typeof(MagickWarningException));
            AssertCreateException(326, typeof(MagickWarningException));
            AssertCreateException(344, typeof(MagickWarningException));
            AssertCreateException(364, typeof(MagickWarningException));
            AssertCreateException(404, typeof(MagickErrorException));
            AssertCreateException(406, typeof(MagickErrorException));
            AssertCreateException(424, typeof(MagickErrorException));
            AssertCreateException(426, typeof(MagickErrorException));
            AssertCreateException(444, typeof(MagickErrorException));
            AssertCreateException(464, typeof(MagickErrorException));
        }

        [Fact]
        public unsafe void Test_Description()
        {
            fixed (byte* description = Encoding.UTF8.GetBytes("description"))
            {
                fixed (byte* reason = Encoding.UTF8.GetBytes("reason"))
                {
                    ExceptionInfo info = new ExceptionInfo()
                    {
                        description = (char*)description,
                        reason = (char*)reason,
                    };
                    GCHandle handle = GCHandle.Alloc(info, GCHandleType.Pinned);
                    try
                    {
                        MagickException exception = MagickExceptionHelper.CreateException(handle.AddrOfPinnedObject());
                        Assert.Equal("reason (description)", exception.Message);
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
            }
        }

        private unsafe void AssertCreateException(int severity, Type expectedType)
        {
            ExceptionInfo info = new ExceptionInfo()
            {
                severity = severity,
            };

            GCHandle handle = GCHandle.Alloc(info, GCHandleType.Pinned);
            try
            {
                MagickException exception = MagickExceptionHelper.CreateException(handle.AddrOfPinnedObject());
                Assert.Equal(expectedType, exception.GetType());
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
