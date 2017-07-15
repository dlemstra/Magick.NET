// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCOREAPP1_1

using System;
using System.Reflection;
using System.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class HttpCachePolicyExtensions
    {
        public static HttpCacheability GetCacheability(this HttpCachePolicy self)
        {
            var type = self.GetType();
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var cacheability = type.GetField("_cacheability", flags);
            return (HttpCacheability)cacheability.GetValue(self);
        }

        public static DateTime GetLastModified(this HttpCachePolicy self)
        {
            var type = self.GetType();
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var cacheability = type.GetField("_utcLastModified", flags);
            return (DateTime)cacheability.GetValue(self);
        }
    }
}

#endif