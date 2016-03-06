//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;
using System.Reflection;

namespace FileGenerator.MagickScript
{
  [Serializable]
  internal static class AppDomainHelper
  {
    private static AppDomain CreateDomain()
    {
      return AppDomain.CreateDomain("AppDomainHelper", null, new AppDomainSetup()
      {
        ApplicationName = "AppDomainHelper"
      });
    }

    private static ApplicationProxy CreateProxy(AppDomain domain)
    {
      Type activator = typeof(ApplicationProxy);
      ApplicationProxy proxy = domain.CreateInstanceAndUnwrap(
            Assembly.GetAssembly(activator).FullName,
            activator.ToString()) as ApplicationProxy;
      return proxy;
    }

    private static void GenerateCode()
    {
      AppDomain domain = CreateDomain();
      ApplicationProxy proxy = CreateProxy(domain);

      proxy.GenerateCode();

      AppDomain.Unload(domain);
    }

    private static void GenerateXsd(QuantumDepth depth)
    {
      AppDomain domain = CreateDomain();
      ApplicationProxy proxy = CreateProxy(domain);

      proxy.GenerateXsd(depth);

      AppDomain.Unload(domain);
    }

    public static void Execute()
    {
      GenerateXsd(QuantumDepth.Q8);
      GenerateXsd(QuantumDepth.Q16);
      GenerateXsd(QuantumDepth.Q16HDRI);
      GenerateCode();
    }
  }
}
