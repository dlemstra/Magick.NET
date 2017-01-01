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

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    public static TException Throws<TException>(Action action)
       where TException : Exception
    {
      return Throws<TException>(action, "Exception of type {0} was not thrown.", typeof(TException).Name);
    }

    public static TException Throws<TException>(Action action, string message, params object[] arguments)
       where TException : Exception
    {
      try
      {
        action();
        Fail(message, arguments);
        return null;
      }
      catch (TException exception)
      {
        Type type = exception.GetType();
        if (type != typeof(TException))
          Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);

        return exception;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public static void ThrowsArgumentException(Action action, string paramName, string messagePart)
    {
      ArgumentException exception = Throws<ArgumentException>(action);
      Assert.AreEqual(paramName, exception.ParamName);
      bool containsMessage = exception.Message.Contains(messagePart);
      Assert.IsTrue(containsMessage, "Message does not contain: " + messagePart);
    }
  }
}
