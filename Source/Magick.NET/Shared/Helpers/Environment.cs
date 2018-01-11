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

namespace ImageMagick
{
    internal static partial class Environment
    {
        private static readonly object _lock = new object();
        private static bool _initialized;

        public static void Initialize()
        {
            lock (_lock)
            {
                if (_initialized)
                    return;

                NativeEnvironment.Initialize();
                _initialized = true;
            }
        }

        public static void SetEnv(string name, string value)
        {
            NativeEnvironment.SetEnv(name, value);
        }
    }
}
