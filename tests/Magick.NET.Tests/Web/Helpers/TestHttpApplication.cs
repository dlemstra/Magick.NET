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

#if !NETCORE

using System.Linq;
using System.Reflection;
using System.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestHttpApplication : HttpApplication
    {
        private object _beginRequestEvent;
        private object _postAuthorizeRequestEvent;
        private object _postMapRequestHandlerEvent;

        public TestHttpApplication()
        {
            var fields = typeof(HttpApplication).GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            _beginRequestEvent = GetValue(fields, "BeginRequest");
            _postAuthorizeRequestEvent = GetValue(fields, "PostAuthorizeRequest");
            _postMapRequestHandlerEvent = GetValue(fields, "PostMapRequest");
        }

        public bool BeginRequestHasEvent => Events[_beginRequestEvent] != null;

        public bool PostAuthorizeRequestHasEvent => Events[_postAuthorizeRequestEvent] != null;

        public bool PostMapRequestHandlerHasEvent => Events[_postMapRequestHandlerEvent] != null;

        private object GetValue(FieldInfo[] fields, string name)
        {
            return fields.First(f => f.Name.Contains(name)).GetValue(this);
        }
    }
}

#endif