// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET
{
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
                var type = exception.GetType();
                if (type != typeof(TException))
                    Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);

                return exception;
            }
        }

        public static TException Throws<TException>(Action action, string messagePart)
           where TException : Exception
        {
            var exception = Throws<TException>(action);
            AssertMessagePart(exception, messagePart);

            return exception;
        }

        public static TException Throws<TException>(string paramName, Action action)
           where TException : ArgumentException
        {
            var exception = Throws<TException>(action);
            Assert.Equal(paramName, exception.ParamName);

            return exception;
        }

        public static TException Throws<TException>(string paramName, Action action, string messagePart)
           where TException : ArgumentException
        {
            var exception = Throws<TException>(paramName, action);
            AssertMessagePart(exception, messagePart);

            return exception;
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
            var containsMessage = exception.Message.Contains(messagePart);
            Assert.True(containsMessage, "Message does not contain: " + messagePart + "." + Environment.NewLine + Environment.NewLine + exception.Message);
        }
    }
}
