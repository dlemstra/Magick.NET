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
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Magick.NET.Tests
{
  [ExcludeFromCodeCoverage]
  internal static class ExceptionAssert
  {
    private static void Fail(string message, params object[] arguments)
    {
      if (arguments != null && arguments.Length > 0)
        Assert.Fail(string.Format(CultureInfo.InvariantCulture, message, arguments));
      else
        Assert.Fail(message);
    }

    public static void Throws<TException>(Action action)
       where TException : Exception
    {
      Throws<TException>(action, "Exception of type {0} was not thrown.", typeof(TException).Name);
    }

    public static void Throws<TException>(Action action, string message, params object[] arguments)
       where TException : Exception
    {
      try
      {
        action();
        Fail(message, arguments);
      }
      catch (TException exception)
      {
        Type type = exception.GetType();
        if (type != typeof(TException))
          Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
