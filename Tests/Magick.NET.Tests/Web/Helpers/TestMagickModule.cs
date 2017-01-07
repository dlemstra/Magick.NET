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

using ImageMagick.Web;
using System;
using System.Web;

namespace Magick.NET.Tests.Web
{
  [ExcludeFromCodeCoverage]
  public class TestMagickModule : MagickModuleBase
  {
    private readonly bool _UsingIntegratedPipeline;

    public TestMagickModule(bool usingIntegratedPipeline)
    {
      _UsingIntegratedPipeline = usingIntegratedPipeline;
    }

    protected override bool UsingIntegratedPipeline => _UsingIntegratedPipeline;

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

    public bool IsInitialized { get; private set; }
  }
}
