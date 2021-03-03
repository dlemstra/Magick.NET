// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
