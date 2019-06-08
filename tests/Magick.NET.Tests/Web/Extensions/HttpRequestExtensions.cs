// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCORE

using System.Collections;
using System.Reflection;
using System.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class HttpRequestExtensions
    {
        public static void SetHeaders(this HttpRequest self, params string[] headerValues)
        {
            var headers = self.Headers;
            var type = headers.GetType();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var isReadOnly = type.GetProperty("IsReadOnly", flags | BindingFlags.FlattenHierarchy);
            isReadOnly.SetValue(headers, false, null);

            type.InvokeMember("InvalidateCachedArrays", flags | BindingFlags.InvokeMethod, null, headers, null);

            type.InvokeMember("BaseClear", flags | BindingFlags.InvokeMethod, null, headers, null);

            for (int i = 0; i < headerValues.Length - 1; i += 2)
            {
                var value = new object[] { headerValues[i], new ArrayList { headerValues[i + 1] } };
                type.InvokeMember("BaseAdd", flags | BindingFlags.InvokeMethod, null, headers, value);
            }

            isReadOnly.SetValue(headers, true, null);
        }
    }
}

#endif