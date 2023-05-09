// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Configuration;

/// <summary>
/// Encapsulates the configuration files of ImageMagick.
/// </summary>
public sealed class ConfigurationFiles : IConfigurationFiles
{
    private ConfigurationFiles()
    {
        Colors = new ConfigurationFile("colors.xml");
        Configure = new ConfigurationFile("configure.xml");
        Delegates = new ConfigurationFile("delegates.xml");
        English = new ConfigurationFile("english.xml");
        Locale = new ConfigurationFile("locale.xml");
        Log = new ConfigurationFile("log.xml");
        Policy = new ConfigurationFile("policy.xml");
        Thresholds = new ConfigurationFile("thresholds.xml");
        Type = new ConfigurationFile("type.xml");
        TypeGhostscript = new ConfigurationFile("type-ghostscript.xml");
    }

    /// <summary>
    /// Gets the default configuration.
    /// </summary>
    public static IConfigurationFiles Default
        => new ConfigurationFiles();

    /// <summary>
    /// Gets all the configuration files.
    /// </summary>
    IEnumerable<IConfigurationFile> IConfigurationFiles.All
    {
        get
        {
            yield return Colors;
            yield return Configure;
            yield return Delegates;
            yield return English;
            yield return Locale;
            yield return Log;
            yield return Policy;
            yield return Thresholds;
            yield return Type;
            yield return TypeGhostscript;
        }
    }

    /// <summary>
    /// Gets the default configuration.
    /// </summary>
    IConfigurationFiles IConfigurationFiles.Default
        => Default;

    /// <summary>
    /// Gets the colors configuration.
    /// </summary>
    public IConfigurationFile Colors { get; }

    /// <summary>
    /// Gets the configure configuration.
    /// </summary>
    public IConfigurationFile Configure { get; }

    /// <summary>
    /// Gets the delegates configuration.
    /// </summary>
    public IConfigurationFile Delegates { get; }

    /// <summary>
    /// Gets the english configuration.
    /// </summary>
    public IConfigurationFile English { get; }

    /// <summary>
    /// Gets the locale configuration.
    /// </summary>
    public IConfigurationFile Locale { get; }

    /// <summary>
    /// Gets the log configuration.
    /// </summary>
    public IConfigurationFile Log { get; }

    /// <summary>
    /// Gets the policy configuration.
    /// </summary>
    public IConfigurationFile Policy { get; }

    /// <summary>
    /// Gets the thresholds configuration.
    /// </summary>
    public IConfigurationFile Thresholds { get; }

    /// <summary>
    /// Gets the type configuration.
    /// </summary>
    public IConfigurationFile Type { get; }

    /// <summary>
    /// Gets the type-ghostscript configuration.
    /// </summary>
    public IConfigurationFile TypeGhostscript { get; }
}
