// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Diagnostics;

namespace ImageMagick
{
    internal static class DebugThrow
    {
        [Conditional("DEBUG")]
        public static void IfNotEqual(string expected, string actual)
        {
            if (expected != actual)
                throw new InvalidOperationException("Value should be equal.");
        }

        [Conditional("DEBUG")]
        public static void IfNull(object value)
        {
            if (value == null)
                throw new InvalidOperationException("Value should not be null.");
        }

        [Conditional("DEBUG")]
        public static void IfNull(string paramName, object value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(paramName);
        }
    }
}