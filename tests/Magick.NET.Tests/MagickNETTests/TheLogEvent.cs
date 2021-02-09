// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheLogEvent
        {
            [Fact]
            public void ShouldPassOrderedTests()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    ShouldNotCallLogDelegeteWhenLogEventsAreNotSet();

                    ShouldCallLogDelegateWhenLogEventsAreSet();

                    ShouldLogTraceEventsWhenLogEventsIsSetToAll();

                    ShouldStopCallingLogDelegateWhenLogDelegateIsRemoved();
                });
            }

            private void ShouldNotCallLogDelegeteWhenLogEventsAreNotSet()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    int count = 0;
                    void LogDelegate(object sender, LogEventArgs arguments)
                    {
                        count++;
                    }

                    MagickNET.Log += LogDelegate;

                    image.Flip();

                    MagickNET.Log -= LogDelegate;

                    Assert.Equal(0, count);
                }
            }

            private void ShouldCallLogDelegateWhenLogEventsAreSet()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    int count = 0;
                    void LogDelegate(object sender, LogEventArgs arguments)
                    {
                        Assert.Null(sender);
                        Assert.NotNull(arguments);
                        Assert.NotEqual(LogEvents.None, arguments.EventType);
                        Assert.NotNull(arguments.Message);
                        Assert.NotEqual(0, arguments.Message.Length);

                        count++;
                    }

                    MagickNET.Log += LogDelegate;

                    MagickNET.SetLogEvents(LogEvents.Detailed);

                    image.Flip();

                    MagickNET.Log -= LogDelegate;

                    Assert.NotEqual(0, count);
                    count = 0;

                    image.Flip();
                    Assert.Equal(0, count);
                }
            }

            private void ShouldLogTraceEventsWhenLogEventsIsSetToAll()
            {
                int traceEvents = 0;
                void LogDelegate(object sender, LogEventArgs arguments)
                {
                    if (arguments.EventType == LogEvents.Trace)
                        traceEvents++;
                }

                MagickNET.SetLogEvents(LogEvents.All);

                MagickNET.Log += LogDelegate;

                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                }

                MagickNET.Log -= LogDelegate;

                Assert.NotEqual(0, traceEvents);
            }

            private void ShouldStopCallingLogDelegateWhenLogDelegateIsRemoved()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    int count = 0;
                    void LogDelegate(object sender, LogEventArgs arguments)
                    {
                        count++;
                    }

                    MagickNET.Log += LogDelegate;

                    MagickNET.SetLogEvents(LogEvents.Detailed);

                    MagickNET.Log -= LogDelegate;

                    image.Flip();
                    Assert.Equal(0, count);
                }
            }
        }
    }
}
