// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class ExceptionAssert
    {
        public static TException Throws<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
                Fail("Exception of type {0} was not thrown.", typeof(TException).Name);
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

        public static TException Throws<TException>(Action action, string messagePart)
           where TException : Exception
        {
            TException exception = Throws<TException>(action);
            AssertMessagePart(exception, messagePart);

            return exception;
        }

        public static void ThrowsArgumentNullException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentNullException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentOutOfRangeException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentOutOfRangeException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, Action action, string messagePart)
        {
            ArgumentException exception = Throws<ArgumentException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
            AssertMessagePart(exception, messagePart);
        }

        private static void Fail(string message, params object[] arguments)
        {
            if (arguments != null && arguments.Length > 0)
                Assert.Fail(string.Format(CultureInfo.InvariantCulture, message, arguments));
            else
                Assert.Fail(message);
        }

        private static void AssertMessagePart<TException>(TException exception, string messagePart)
           where TException : Exception
        {
            bool containsMessage = exception.Message.Contains(messagePart);
            Assert.IsTrue(containsMessage, "Message does not contain: " + messagePart + "." + Environment.NewLine + Environment.NewLine + exception.Message);
        }
    }
}
