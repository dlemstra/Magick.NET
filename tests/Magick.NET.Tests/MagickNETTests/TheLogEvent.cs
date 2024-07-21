// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheLogEvent
    {
        [Fact]
        public void ShouldPassOrderedTests()
        {
            ShouldNotCallLogDelegateWhenLogEventsAreNotSet();

            ShouldCallLogDelegateWhenLogEventsAreSet();

            ShouldLogTraceEventsWhenLogEventsIsSetToAll();

            ShouldStopCallingLogDelegateWhenLogDelegateIsRemoved();
        }

        private void ShouldNotCallLogDelegateWhenLogEventsAreNotSet()
        {
            var count = 0;
            void LogDelegate(object sender, LogEventArgs arguments)
            {
                count++;
            }

            MagickNET.Log += LogDelegate;

            using var image = new MagickImage(Files.SnakewarePNG);
            image.Flip();

            MagickNET.Log -= LogDelegate;

            Assert.Equal(0, count);
        }

        private void ShouldCallLogDelegateWhenLogEventsAreSet()
        {
            var count = 0;
            void LogDelegate(object sender, LogEventArgs arguments)
            {
                Assert.Null(sender);
                Assert.NotNull(arguments);
                Assert.NotEqual(LogEventTypes.None, arguments.EventType);
                Assert.NotNull(arguments.Message);
                Assert.NotEqual(0, arguments.Message.Length);

                count++;
            }

            MagickNET.Log += LogDelegate;

            MagickNET.SetLogEvents(LogEventTypes.Detailed);

            using var image = new MagickImage(Files.SnakewarePNG);
            image.Flip();

            MagickNET.Log -= LogDelegate;

            Assert.NotEqual(0, count);
            count = 0;

            image.Flip();

            Assert.Equal(0, count);
        }

        private void ShouldLogTraceEventsWhenLogEventsIsSetToAll()
        {
            var traceEvents = 0;
            void LogDelegate(object sender, LogEventArgs arguments)
            {
                if (arguments.EventType == LogEventTypes.Trace)
                    traceEvents++;
            }

            MagickNET.SetLogEvents(LogEventTypes.All);

            MagickNET.Log += LogDelegate;

            using var image = new MagickImage(Files.SnakewarePNG);

            MagickNET.Log -= LogDelegate;

            Assert.NotEqual(0, traceEvents);
        }

        private void ShouldStopCallingLogDelegateWhenLogDelegateIsRemoved()
        {
            var count = 0;
            void LogDelegate(object sender, LogEventArgs arguments)
            {
                count++;
            }

            MagickNET.Log += LogDelegate;

            MagickNET.SetLogEvents(LogEventTypes.Detailed);

            MagickNET.Log -= LogDelegate;

            using var image = new MagickImage(Files.SnakewarePNG);
            image.Flip();

            Assert.Equal(0, count);
        }
    }
}
