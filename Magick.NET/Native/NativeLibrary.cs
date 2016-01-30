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

namespace ImageMagick
{
  internal static class NativeLibrary
  {
    private static volatile bool _Initialized;

#if Q8
    public const string X64Name = "Magick.NET-Q8-x64.Native.dll";
    public const string X86Name = "Magick.NET-Q8-x86.Native.dll";
#elif Q16
    public const string X64Name = "Magick.NET-Q16-x64.Native.dll";
    public const string X86Name = "Magick.NET-Q16-x86.Native.dll";
#elif Q16HDRI
    public const string X64Name = "Magick.NET-Q16-HDRI-x64.Native.dll";
    public const string X86Name = "Magick.NET-Q16-HDRI-x86.Native.dll";
#else
#error Not implemented!
#endif

    public static bool Is64Bit
    {
      get
      {
        return IntPtr.Size == 8;
      }
    }

    public static event EventHandler Initialize;

    public static void DoInitialize()
    {
      if (_Initialized)
        return;

      _Initialized = true;

      if (Initialize != null)
        Initialize(null, EventArgs.Empty);
    }
  }
}
