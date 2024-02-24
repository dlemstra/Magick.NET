// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Configuration;

/// <summary>
/// Interface that represents the configuration files.
/// </summary>
public interface IConfigurationFiles
{
    /// <summary>
    /// Gets all the configuration files.
    /// </summary>
    IEnumerable<IConfigurationFile> All { get; }

    /// <summary>
    /// Gets the default configuration.
    /// </summary>
    IConfigurationFiles Default { get; }

    /// <summary>
    /// Gets the colors configuration.
    /// </summary>
    IConfigurationFile Colors { get; }

    /// <summary>
    /// Gets the configure configuration.
    /// </summary>
    IConfigurationFile Configure { get; }

    /// <summary>
    /// Gets the delegates configuration.
    /// </summary>
    IConfigurationFile Delegates { get; }

    /// <summary>
    /// Gets the english configuration.
    /// </summary>
    IConfigurationFile English { get; }

    /// <summary>
    /// Gets the locale configuration.
    /// </summary>
    IConfigurationFile Locale { get; }

    /// <summary>
    /// Gets the log configuration.
    /// </summary>
    IConfigurationFile Log { get; }

    /// <summary>
    /// Gets the policy configuration.
    /// </summary>
    IConfigurationFile Policy { get; }

    /// <summary>
    /// Gets the thresholds configuration.
    /// </summary>
    IConfigurationFile Thresholds { get; }

    /// <summary>
    /// Gets the type configuration.
    /// </summary>
    IConfigurationFile Type { get; }

    /// <summary>
    /// Gets the type-ghostscript configuration.
    /// </summary>
    IConfigurationFile TypeGhostscript { get; }

    /// <summary>
    /// Creates a new configuration with Inkscape enabled.
    /// </summary>
    /// <returns>A new configuration with Inkscape enabled.</returns>
    public IConfigurationFiles WithInkscapeEnabled();
}
