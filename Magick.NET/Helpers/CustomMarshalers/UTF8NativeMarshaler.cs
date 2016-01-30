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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ImageMagick
{
  [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
  internal class UTF8NativeMarshaler : ICustomMarshaler
  {
    public void CleanUpManagedData(object managedObj)
    {
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
      MagickMemory.Relinquish(nativeData);
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
      throw new NotImplementedException();
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
      return UTF8MarshalerHelper.MarshalNativeToManaged(nativeData);
    }

    public int GetNativeDataSize()
    {
      return -1;
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cookie")]
    public static ICustomMarshaler GetInstance(string cookie)
    {
      return new UTF8NativeMarshaler();
    }
  }
}
