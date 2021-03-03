// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Configuration
{
    /// <summary>
    /// Interface that represents a configuration file.
    /// </summary>
    public interface IConfigurationFile
    {
        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets or sets the data of the configuration file.
        /// </summary>
        string Data { get; set; }
    }
}