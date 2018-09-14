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

#if !NETCORE

using System.Collections;
using System.Reflection;
using System.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class HttpResponseExtensions
    {
        public static string GetHeader(this HttpResponse self, string headerName)
        {
            var type = self.GetType();
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var customHeaders = type.GetField("_customHeaders", flags);
            var arrayList = customHeaders.GetValue(self) as ArrayList;
            if (arrayList.Count == 0)
                return null;

            var headerType = arrayList[0].GetType();
            var name = headerType.GetProperty("Name", flags);
            var value = headerType.GetProperty("Value", flags);

            foreach (var header in arrayList)
            {
                if (headerName.Equals(name.GetValue(header, null)))
                    return (string)value.GetValue(header, null);
            }

            return null;
        }
    }
}

#endif