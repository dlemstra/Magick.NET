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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ExceptionHelperTests
    {
        private unsafe void Test_CreateException(int severity, Type expectedType)
        {
            ExceptionInfo info = new ExceptionInfo();
            info.severity = severity;

            GCHandle handle = GCHandle.Alloc(info, GCHandleType.Pinned);
            try
            {
                MagickException exception = MagickExceptionHelper.CreateException(handle.AddrOfPinnedObject());
                Assert.AreEqual(expectedType, exception.GetType());
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod]
        public unsafe void Test_CreateException()
        {
            Test_CreateException(0, typeof(MagickWarningException));

            Test_CreateException(300, typeof(MagickResourceLimitWarningException));
            Test_CreateException(305, typeof(MagickTypeWarningException));
            Test_CreateException(310, typeof(MagickOptionWarningException));
            Test_CreateException(315, typeof(MagickDelegateWarningException));
            Test_CreateException(320, typeof(MagickMissingDelegateWarningException));
            Test_CreateException(325, typeof(MagickCorruptImageWarningException));
            Test_CreateException(330, typeof(MagickFileOpenWarningException));
            Test_CreateException(335, typeof(MagickBlobWarningException));
            Test_CreateException(340, typeof(MagickStreamWarningException));
            Test_CreateException(345, typeof(MagickCacheWarningException));
            Test_CreateException(350, typeof(MagickCoderWarningException));
            Test_CreateException(352, typeof(MagickWarningException));
            Test_CreateException(355, typeof(MagickModuleWarningException));
            Test_CreateException(360, typeof(MagickDrawWarningException));
            Test_CreateException(365, typeof(MagickImageWarningException));
            Test_CreateException(370, typeof(MagickWarningException));
            Test_CreateException(375, typeof(MagickWarningException));
            Test_CreateException(380, typeof(MagickWarningException));
            Test_CreateException(385, typeof(MagickWarningException));
            Test_CreateException(390, typeof(MagickRegistryWarningException));
            Test_CreateException(395, typeof(MagickConfigureWarningException));
            Test_CreateException(399, typeof(MagickPolicyWarningException));

            Test_CreateException(386, typeof(MagickWarningException));
            Test_CreateException(100, typeof(MagickWarningException));

            Test_CreateException(400, typeof(MagickResourceLimitErrorException));
            Test_CreateException(405, typeof(MagickTypeErrorException));
            Test_CreateException(410, typeof(MagickOptionErrorException));
            Test_CreateException(415, typeof(MagickDelegateErrorException));
            Test_CreateException(420, typeof(MagickMissingDelegateErrorException));
            Test_CreateException(425, typeof(MagickCorruptImageErrorException));
            Test_CreateException(430, typeof(MagickFileOpenErrorException));
            Test_CreateException(435, typeof(MagickBlobErrorException));
            Test_CreateException(440, typeof(MagickStreamErrorException));
            Test_CreateException(445, typeof(MagickCacheErrorException));
            Test_CreateException(450, typeof(MagickCoderErrorException));
            Test_CreateException(452, typeof(MagickErrorException));
            Test_CreateException(455, typeof(MagickModuleErrorException));
            Test_CreateException(460, typeof(MagickDrawErrorException));
            Test_CreateException(465, typeof(MagickImageErrorException));
            Test_CreateException(470, typeof(MagickErrorException));
            Test_CreateException(475, typeof(MagickErrorException));
            Test_CreateException(480, typeof(MagickErrorException));
            Test_CreateException(485, typeof(MagickErrorException));
            Test_CreateException(490, typeof(MagickRegistryErrorException));
            Test_CreateException(495, typeof(MagickConfigureErrorException));
            Test_CreateException(499, typeof(MagickPolicyErrorException));

            Test_CreateException(486, typeof(MagickErrorException));
            Test_CreateException(700, typeof(MagickErrorException));

            /* These are just here to test all the if branches that are created */
            Test_CreateException(306, typeof(MagickWarningException));
            Test_CreateException(324, typeof(MagickWarningException));
            Test_CreateException(326, typeof(MagickWarningException));
            Test_CreateException(344, typeof(MagickWarningException));
            Test_CreateException(364, typeof(MagickWarningException));
            Test_CreateException(404, typeof(MagickErrorException));
            Test_CreateException(406, typeof(MagickErrorException));
            Test_CreateException(424, typeof(MagickErrorException));
            Test_CreateException(426, typeof(MagickErrorException));
            Test_CreateException(444, typeof(MagickErrorException));
            Test_CreateException(464, typeof(MagickErrorException));
        }

        [TestMethod]
        public unsafe void Test_Description()
        {
            fixed (byte* description = Encoding.UTF8.GetBytes("description"))
            {
                fixed (byte* reason = Encoding.UTF8.GetBytes("reason"))
                {
                    ExceptionInfo info = new ExceptionInfo();
                    info.description = (char*)description;
                    info.reason = (char*)reason;

                    GCHandle handle = GCHandle.Alloc(info, GCHandleType.Pinned);
                    try
                    {
                        MagickException exception = MagickExceptionHelper.CreateException(handle.AddrOfPinnedObject());
                        Assert.AreEqual("reason (description)", exception.Message);
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
            }
        }
    }
}
