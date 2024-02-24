// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Configuration;

/// <summary>
/// Encapsulates the configuration files of ImageMagick.
/// </summary>
public sealed class ConfigurationFiles : IConfigurationFiles
{
    private ConfigurationFiles(
        IConfigurationFile colors,
        IConfigurationFile configure,
        IConfigurationFile delegates,
        IConfigurationFile english,
        IConfigurationFile locale,
        IConfigurationFile log,
        IConfigurationFile policy,
        IConfigurationFile thresholds,
        IConfigurationFile type,
        IConfigurationFile typeGhostscript)
    {
        Colors = colors;
        Configure = configure;
        Delegates = delegates;
        English = english;
        Locale = locale;
        Log = log;
        Policy = policy;
        Thresholds = thresholds;
        Type = type;
        TypeGhostscript = typeGhostscript;
    }

    /// <summary>
    /// Gets the default configuration.
    /// </summary>
    public static IConfigurationFiles Default
        => new ConfigurationFiles(
            colors: new ConfigurationFile("colors.xml"),
            configure: new ConfigurationFile("configure.xml"),
            delegates: new ConfigurationFile("delegates.xml"),
            english: new ConfigurationFile("english.xml"),
            locale: new ConfigurationFile("locale.xml"),
            log: new ConfigurationFile("log.xml"),
            policy: new ConfigurationFile("policy.xml"),
            thresholds: new ConfigurationFile("thresholds.xml"),
            type: new ConfigurationFile("type.xml"),
            typeGhostscript: new ConfigurationFile("type-ghostscript.xml"));

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

    /// <summary>
    /// Creates a new configuration with Inkscape enabled.
    /// </summary>
    /// <returns>A new configuration with Inkscape enabled.</returns>
    public IConfigurationFiles WithInkscapeEnabled()
    {
        var delegates = new ConfigurationFile(Delegates);
        delegates.Data = delegates.Data.Replace(@"<!--<delegate decode=""svg:decode"" stealth=""True"" command=""&quot;inkscape&quot; &quot;%s&quot; - -export-filename=&quot;%s&quot; - -export-dpi=&quot;%s&quot; - -export-background=&quot;%s&quot; - -export-background-opacity=&quot;%s&quot; &gt; &quot;%s&quot; 2&gt;&amp;1""/>-->", @"<delegate decode=""svg:decode"" stealth=""True"" command=""&quot;inkscape&quot; &quot;%s&quot; --export-filename=&quot;%s&quot; --export-dpi=&quot;%s&quot; --export-background=&quot;%s&quot; --export-background-opacity=&quot;%s&quot; &gt; &quot;%s&quot; 2&gt;&amp;1""/>");

        return new ConfigurationFiles(
            colors: new ConfigurationFile(Colors),
            configure: new ConfigurationFile(Configure),
            delegates: new ConfigurationFile(delegates),
            english: new ConfigurationFile(English),
            locale: new ConfigurationFile(Locale),
            log: new ConfigurationFile(Log),
            policy: new ConfigurationFile(Policy),
            thresholds: new ConfigurationFile(Thresholds),
            type: new ConfigurationFile(Type),
            typeGhostscript: new ConfigurationFile(TypeGhostscript));
    }
}
