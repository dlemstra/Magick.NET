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

#if !NETCOREAPP1_1

using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestHttpContextBase : HttpContextBase
    {
        private Dictionary<object, object> _items;
        private HttpRequestBase _request;

        public TestHttpContextBase()
          : this("https://www.imagemagick.org")
        {
        }

        public TestHttpContextBase(string url)
        {
            _items = new Dictionary<object, object>();
            _request = new TestHttpRequest(url);
        }

        public override IHttpHandler Handler { get; set; }

        public override IDictionary Items => _items;

        public override HttpRequestBase Request => _request;

        public IHttpHandler RemapedHandler { get; private set; }

        public override void RemapHandler(IHttpHandler handler)
        {
            RemapedHandler = handler;
        }
    }
}

#endif