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
  internal class UTF8Marshaler : ICustomMarshaler
  {
    public void CleanUpManagedData(object managedObj)
    {
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
      Marshal.FreeHGlobal(nativeData);
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
      return UTF8MarshalerHelper.MarshalManagedToNative(managedObj);
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
      throw new NotImplementedException();
    }

    public int GetNativeDataSize()
    {
      return -1;
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cookie")]
    public static ICustomMarshaler GetInstance(string cookie)
    {
      return new UTF8Marshaler();
    }
  }
}
