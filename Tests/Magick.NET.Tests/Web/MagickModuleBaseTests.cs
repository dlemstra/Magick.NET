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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Web
{
  [TestClass]
  public class MagickModuleBaseTests
  {
    [TestMethod]
    public void Test_Init()
    {
      TestHttpApplication context = new TestHttpApplication();
      Assert.IsFalse(context.BeginRequestHasEvent);

      TestMagickModule module = new TestMagickModule(true);
      module.Init(null);
      module.Init(context);
      module.Dispose();

      Assert.IsTrue(module.IsInitialized);
      Assert.IsTrue(context.BeginRequestHasEvent);
      Assert.IsTrue(context.PostAuthorizeRequestHasEvent);
      Assert.IsFalse(context.PostMapRequestHandlerHasEvent);

      context = new TestHttpApplication();

      module = new TestMagickModule(false);
      module.Init(context);

      Assert.IsTrue(module.IsInitialized);
      Assert.IsTrue(context.BeginRequestHasEvent);
      Assert.IsTrue(context.PostMapRequestHandlerHasEvent);
      Assert.IsFalse(context.PostAuthorizeRequestHasEvent);
    }
  }
}
