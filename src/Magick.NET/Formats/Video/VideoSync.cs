// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies the video sync methods.
    /// </summary>
    public enum VideoSync
    {
        /// <summary>
        /// Chooses between <see cref="Cfr"/> and <see cref="Vfr"/> depending on muxer capabilities. This is the default method.
        /// </summary>
        Auto,

        /// <summary>
        /// Frames will be duplicated and dropped to achieve exactly the requested constant frame rate.
        /// </summary>
        Cfr,

        /// <summary>
        /// As passthrough but destroys all timestamps, making the muxer generate fresh timestamps based on frame-rate.
        /// </summary>
        Drop,

        /// <summary>
        /// Each frame is passed with its timestamp from the demuxer to the muxer.
        /// </summary>
        PassThrough,

        /// <summary>
        /// Frames are passed through with their timestamp or dropped so as to prevent 2 frames from having the same timestamp.
        /// </summary>
        Vfr,
    }
}
