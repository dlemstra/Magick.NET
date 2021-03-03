// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that implements <see cref="IDefine"/>.
    /// </summary>
    public sealed class MagickDefine : IDefine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(string name, string? value)
        {
            Format = MagickFormat.Unknown;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(MagickFormat format, string name, string? value)
        {
            Format = format;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the format to set the define for.
        /// </summary>
        public MagickFormat Format { get; }

        /// <summary>
        /// Gets the name of the define.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the define.
        /// </summary>
        public string? Value { get; }
    }
}