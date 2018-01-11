﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    internal partial class StringInfo
    {
        public byte[] Datum
        {
            get;
            private set;
        }

        public static StringInfo CreateInstance(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            NativeStringInfo native = new NativeStringInfo(instance);

            StringInfo result = new StringInfo();
            result.Datum = ByteConverter.ToArray(native.Datum, native.Length);

            return result;
        }
    }
}