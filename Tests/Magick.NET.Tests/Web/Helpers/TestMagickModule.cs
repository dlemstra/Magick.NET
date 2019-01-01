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

using System.Web;
using ImageMagick.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestMagickModule : MagickModuleBase
    {
        public TestMagickModule(bool usingIntegratedPipeline)
        {
            UsingIntegratedPipeline = usingIntegratedPipeline;
        }

        public bool IsInitialized { get; private set; }

        protected override bool UsingIntegratedPipeline { get; }

        internal override void Initialize()
        {
            IsInitialized = true;
        }

        internal override void OnBeginRequest(HttpContextBase context)
        {
        }

        internal override void OnPostAuthorizeRequest(HttpContextBase context)
        {
        }

        internal override void OnPostMapRequestHandler(HttpContextBase context)
        {
        }
    }
}

#endif