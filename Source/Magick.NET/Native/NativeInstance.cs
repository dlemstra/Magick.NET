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
  internal abstract class NativeInstance : ConstNativeInstance, INativeInstance, IDisposable
  {
    private class ZeroInstance : INativeInstance
    {
      public IntPtr Instance
      {
        get
        {
          return IntPtr.Zero;
        }
      }

      public void Dispose()
      {
      }
    }

    protected abstract void Dispose(IntPtr instance);

    protected void CheckException(IntPtr exception, IntPtr result)
    {
      MagickException magickException = MagickExceptionHelper.Create(exception);
      if (MagickExceptionHelper.IsError(magickException))
      {
        if (result != IntPtr.Zero)
          Dispose(result);
        throw magickException;
      }

      RaiseWarning(magickException);
    }

    public static INativeInstance Zero
    {
      get
      {
        return new ZeroInstance();
      }
    }

    public void Dispose()
    {
      Instance = IntPtr.Zero;
      GC.SuppressFinalize(this);
    }
  }
}
