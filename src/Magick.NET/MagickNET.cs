// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ImageMagick.Configuration;

namespace ImageMagick;

/// <summary>
/// Class that can be used to initialize Magick.NET.
/// </summary>
public partial class MagickNET : IMagickNET
{
    private static LogDelegate? _nativeLog;
    private static EventHandler<LogEventArgs>? _log;
    private static LogEventTypes _logEvents = LogEventTypes.None;

    /// <summary>
    /// Event that will be raised when something is logged by ImageMagick.
    /// </summary>
    public static event EventHandler<LogEventArgs> Log
    {
        add
        {
            if (_log is null)
            {
                _nativeLog = new LogDelegate(OnLog);
                NativeMagickNET.SetLogDelegate(_nativeLog);
                SetLogEvents();
            }

            _log += value;
        }

        remove
        {
            _log -= value;

            if (_log is null)
            {
                NativeMagickNET.SetLogDelegate(null);
                NativeMagickNET.SetLogEvents("None");
                _nativeLog = null;
            }
        }
    }

    /// <summary>
    /// Event that will be raised when something is logged by ImageMagick.
    /// </summary>
    event EventHandler<LogEventArgs> IMagickNET.Log
    {
        add => Log += value;
        remove => Log -= value;
    }

    /// <summary>
    /// Gets the ImageMagick delegate libraries.
    /// </summary>
    public static string Delegates
        => NativeMagickNET.Delegates_Get();

    /// <summary>
    /// Gets the ImageMagick features.
    /// </summary>
    public static string Features
        => NativeMagickNET.Features_Get();

    /// <summary>
    /// Gets the information about the supported formats.
    /// </summary>
    public static IReadOnlyCollection<IMagickFormatInfo> SupportedFormats
        => MagickFormatInfo.All;

    /// <summary>
    /// Gets the font families that are known by ImageMagick.
    /// </summary>
    public static IReadOnlyList<string> FontFamilies
    {
        get
        {
            var result = new List<string>();

            var list = IntPtr.Zero;

            try
            {
                list = NativeMagickNET.GetFonts(out var length);
                result.Capacity = (int)length;

                for (var i = 0U; i < (int)length; i++)
                {
                    var fontFamily = NativeMagickNET.GetFontFamily(list, i);
                    if (fontFamily is not null && fontFamily.Length > 0 && !result.Contains(fontFamily))
                        result.Add(fontFamily);
                }
            }
            finally
            {
                if (list != IntPtr.Zero)
                    NativeMagickNET.DisposeFonts(list);
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the font names that are known by ImageMagick.
    /// </summary>
    public static IReadOnlyList<string> FontNames
    {
        get
        {
            var result = new List<string>();

            var list = IntPtr.Zero;

            try
            {
                list = NativeMagickNET.GetFonts(out var length);
                result.Capacity = (int)length;

                for (var i = 0U; i < (int)length; i++)
                {
                    var fontName = NativeMagickNET.GetFontName(list, i);
                    if (fontName is not null && fontName.Length > 0)
                        result.Add(fontName);
                }
            }
            finally
            {
                if (list != IntPtr.Zero)
                    NativeMagickNET.DisposeFonts(list);
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the version of ImageMagick.
    /// </summary>
    public static string ImageMagickVersion
        => NativeMagickNET.ImageMagickVersion_Get();

    /// <summary>
    /// Gets the version of Magick.NET.
    /// </summary>
    public static string Version
    {
        get
        {
            var title = TypeHelper.GetCustomAttribute<AssemblyTitleAttribute>(typeof(MagickNET));
            var version = TypeHelper.GetCustomAttribute<AssemblyFileVersionAttribute>(typeof(MagickNET));
            return title.Title + " " + version.Version;
        }
    }

    /// <summary>
    /// Gets the ImageMagick delegate libraries.
    /// </summary>
    string IMagickNET.Delegates
        => Delegates;

    /// <summary>
    /// Gets the ImageMagick features.
    /// </summary>
    string IMagickNET.Features
        => Features;

    /// <summary>
    /// Gets the font families that are known by ImageMagick.
    /// </summary>
    IReadOnlyList<string> IMagickNET.FontFamilies
        => FontFamilies;

    /// <summary>
    /// Gets the font names that are known by ImageMagick.
    /// </summary>
    IReadOnlyList<string> IMagickNET.FontNames
        => FontNames;

    /// <summary>
    /// Gets the version of ImageMagick.
    /// </summary>
    string IMagickNET.ImageMagickVersion
        => ImageMagickVersion;

    /// <summary>
    /// Gets the information about the supported formats.
    /// </summary>
    IReadOnlyCollection<IMagickFormatInfo> IMagickNET.SupportedFormats
        => SupportedFormats;

    /// <summary>
    /// Gets the version of Magick.NET.
    /// </summary>
    string IMagickNET.Version
        => Version;

    internal static string TemporaryDirectory { get; private set; } = Path.GetTempPath();

    /// <summary>
    /// Gets the environment variable with the specified name.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <returns>The environment variable with the specified name.</returns>
    public static string? GetEnvironmentVariable(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        return Environment.GetEnv(name);
    }

    /// <summary>
    /// Initializes ImageMagick.
    /// </summary>
    public static void Initialize()
        => Environment.Initialize();

    /// <summary>
    /// Initializes ImageMagick with the xml files that are located in the specified path.
    /// </summary>
    /// <param name="path">The path that contains the ImageMagick xml files.</param>
    public static void Initialize(string path)
    {
        var newPath = FileHelper.GetFullPath(path);

        CheckImageMagickFiles(newPath);

        Environment.SetEnv("MAGICK_CONFIGURE_PATH", path);
    }

    /// <summary>
    /// Initializes ImageMagick with the specified configuration files and returns the path to the
    /// temporary directory where the xml files were saved.
    /// </summary>
    /// <param name="configFiles">The configuration files ot initialize ImageMagick with.</param>
    /// <returns>The path of the folder that was created and contains the configuration files.</returns>
    public static string Initialize(IConfigurationFiles configFiles)
    {
        Throw.IfNull(nameof(configFiles), configFiles);

        var path = Path.Combine(TemporaryDirectory, Guid.NewGuid().ToString());
        Directory.CreateDirectory(path);

        InitializeConfiguration(configFiles, path);

        return path;
    }

    /// <summary>
    /// Initializes ImageMagick with the specified configuration files in the specified the path.
    /// </summary>
    /// <param name="configFiles">The configuration files ot initialize ImageMagick with.</param>
    /// <param name="path">The directory to save the configuration files in.</param>
    public static void Initialize(IConfigurationFiles configFiles, string path)
    {
        Throw.IfNull(nameof(configFiles), configFiles);

        var newPath = FileHelper.GetFullPath(path);

        InitializeConfiguration(configFiles, newPath);
    }

    /// <summary>
    /// Resets the pseudo-random number generator secret key.
    /// </summary>
    public static void ResetRandomSeed()
        => NativeMagickNET.SetRandomSeed(ulong.MaxValue);

    /// <summary>
    /// Set the path to the default font file.
    /// </summary>
    /// <param name="file">The file to use at the default font file.</param>
    public static void SetDefaultFontFile(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        SetDefaultFontFile(file.FullName);
    }

    /// <summary>
    /// Set the path to the default font file.
    /// </summary>
    /// <param name="fileName">The file name to use at the default font file.</param>
    public static void SetDefaultFontFile(string fileName)
    {
        Throw.IfNullOrEmpty(nameof(fileName), fileName);

        NativeMagickNET.SetDefaultFontFile(fileName);
    }

    /// <summary>
    /// Set the environment variable with the specified name to the specified value.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="value">The value of the environment variable.</param>
    public static void SetEnvironmentVariable(string name, string value)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Environment.SetEnv(name, value);
    }

    /// <summary>
    /// Sets the directory that contains the FontConfig configuration files.
    /// </summary>
    /// <param name="path">The path of the FontConfig directory.</param>
    public static void SetFontConfigDirectory(string path)
        => Environment.SetEnv("FONTCONFIG_PATH", FileHelper.GetFullPath(path));

    /// <summary>
    /// Sets the directory that contains the Ghostscript file gsdll32.dll / gsdll64.dll.
    /// This method is only supported on Windows.
    /// </summary>
    /// <param name="path">The path of the Ghostscript directory.</param>
    public static void SetGhostscriptDirectory(string path)
        => Environment.SetEnv("MAGICK_GHOSTSCRIPT_PATH", FileHelper.GetFullPath(path));

    /// <summary>
    /// Sets the directory that contains the Ghostscript font files.
    /// This method is only supported on Windows.
    /// </summary>
    /// <param name="path">The path of the Ghostscript font directory.</param>
    public static void SetGhostscriptFontDirectory(string path)
        => Environment.SetEnv("MAGICK_GHOSTSCRIPT_FONT_PATH", FileHelper.GetFullPath(path));

    /// <summary>
    /// Set the events that will be written to the log. The log will be written to the Log event
    /// and the debug window in VisualStudio. To change the log settings you a custom log.xml file
    /// should be used.
    /// </summary>
    /// <param name="events">The events that should be logged.</param>
    public static void SetLogEvents(LogEventTypes events)
    {
        _logEvents = events;

        if (_log is not null)
            SetLogEvents();
    }

    /// <summary>
    /// Sets the directory that contains the Native library. This currently only works on Windows.
    /// </summary>
    /// <param name="path">The path of the directory that contains the native library.</param>
    public static void SetNativeLibraryDirectory(string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            NativeWindowsMethods.SetDllDirectory(FileHelper.GetFullPath(path));
    }

    /// <summary>
    /// Sets the directory that will be used when ImageMagick does not have enough memory for the
    /// pixel cache.
    /// </summary>
    /// <param name="path">The path where temp files will be written.</param>
    public static void SetTempDirectory(string path)
    {
        TemporaryDirectory = FileHelper.GetFullPath(path);
        Environment.SetEnv("MAGICK_TEMPORARY_PATH", TemporaryDirectory);
    }

    /// <summary>
    /// Sets the pseudo-random number generator secret key.
    /// </summary>
    /// <param name="seed">The secret key.</param>
    public static void SetRandomSeed(ulong seed)
        => NativeMagickNET.SetRandomSeed(seed);

    /// <summary>
    /// Gets the environment variable with the specified name.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <returns>The environment variable with the specified name.</returns>
    string? IMagickNET.GetEnvironmentVariable(string name)
        => GetEnvironmentVariable(name);

    /// <summary>
    /// Initializes ImageMagick.
    /// </summary>
    void IMagickNET.Initialize()
        => Initialize();

    /// <summary>
    /// Initializes ImageMagick with the xml files that are located in the specified path.
    /// </summary>
    /// <param name="path">The path that contains the ImageMagick xml files.</param>
    void IMagickNET.Initialize(string path)
        => Initialize(path);

    /// <summary>
    /// Initializes ImageMagick with the specified configuration files and returns the path to the
    /// temporary directory where the xml files were saved.
    /// </summary>
    /// <param name="configFiles">The configuration files ot initialize ImageMagick with.</param>
    /// <returns>The path of the folder that was created and contains the configuration files.</returns>
    string IMagickNET.Initialize(IConfigurationFiles configFiles)
        => Initialize(configFiles);

    /// <summary>
    /// Initializes ImageMagick with the specified configuration files in the specified the path.
    /// </summary>
    /// <param name="configFiles">The configuration files ot initialize ImageMagick with.</param>
    /// <param name="path">The directory to save the configuration files in.</param>
    void IMagickNET.Initialize(IConfigurationFiles configFiles, string path)
        => Initialize(configFiles, path);

    /// <summary>
    /// Resets the pseudo-random number generator secret key.
    /// </summary>
    void IMagickNET.ResetRandomSeed()
        => ResetRandomSeed();

    /// <summary>
    /// Set the path to the default font file.
    /// </summary>
    /// <param name="file">The file to use at the default font file.</param>
    void IMagickNET.SetDefaultFontFile(FileInfo file)
        => SetDefaultFontFile(file);

    /// <summary>
    /// Set the path to the default font file.
    /// </summary>
    /// <param name="fileName">The file name to use at the default font file.</param>
    void IMagickNET.SetDefaultFontFile(string fileName)
        => SetDefaultFontFile(fileName);

    /// <summary>
    /// Set the environment variable with the specified name to the specified value.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="value">The value of the environment variable.</param>
    void IMagickNET.SetEnvironmentVariable(string name, string value)
        => SetEnvironmentVariable(name, value);

    /// <summary>
    /// Sets the directory that contains the FontConfig configuration files.
    /// </summary>
    /// <param name="path">The path of the FontConfig directory.</param>
    void IMagickNET.SetFontConfigDirectory(string path)
        => SetFontConfigDirectory(path);

    /// <summary>
    /// Sets the directory that contains the Ghostscript file gsdll32.dll / gsdll64.dll.
    /// This method is only supported on Windows.
    /// </summary>
    /// <param name="path">The path of the Ghostscript directory.</param>
    void IMagickNET.SetGhostscriptDirectory(string path)
        => SetGhostscriptDirectory(path);

    /// <summary>
    /// Sets the directory that contains the Ghostscript font files.
    /// This method is only supported on Windows.
    /// </summary>
    /// <param name="path">The path of the Ghostscript font directory.</param>
    void IMagickNET.SetGhostscriptFontDirectory(string path)
        => SetGhostscriptDirectory(path);

    /// <summary>
    /// Set the events that will be written to the log. The log will be written to the Log event
    /// and the debug window in VisualStudio. To change the log settings you must use a custom
    /// log.xml file.
    /// </summary>
    /// <param name="events">The events that will be logged.</param>
    void IMagickNET.SetLogEvents(LogEventTypes events)
        => SetLogEvents(events);

    /// <summary>
    /// Sets the directory that contains the Native library. This currently only works on Windows.
    /// </summary>
    /// <param name="path">The path of the directory that contains the native library.</param>
    void IMagickNET.SetNativeLibraryDirectory(string path)
        => SetNativeLibraryDirectory(path);

    /// <summary>
    /// Sets the directory that will be used when ImageMagick does not have enough memory for the
    /// pixel cache.
    /// </summary>
    /// <param name="path">The path where temp files will be written.</param>
    void IMagickNET.SetTempDirectory(string path)
        => SetTempDirectory(path);

    /// <summary>
    /// Sets the pseudo-random number generator secret key.
    /// </summary>
    /// <param name="seed">The secret key.</param>
    void IMagickNET.SetRandomSeed(ulong seed)
        => SetRandomSeed(seed);

    private static void CheckImageMagickFiles(string path)
    {
        foreach (var configurationFile in ConfigurationFiles.Default.All)
        {
            var fileName = Path.Combine(path, configurationFile.FileName);
            Throw.IfFalse(nameof(path), File.Exists(fileName), "Unable to find file: {0}", fileName);
        }
    }

    private static void InitializeConfiguration(IConfigurationFiles configFiles, string path)
    {
        foreach (var configFile in configFiles.All)
        {
            var outputFile = Path.Combine(path, configFile.FileName);
            if (File.Exists(outputFile))
                continue;

            using var fileStream = File.Open(outputFile, FileMode.CreateNew);
            var data = Encoding.UTF8.GetBytes(configFile.Data);
            fileStream.Write(data, 0, data.Length);
        }

        Environment.SetEnv("MAGICK_CONFIGURE_PATH", path);
    }

    private static void OnLog(UIntPtr type, IntPtr text)
    {
        if (_log is null)
            return;

        var instance = UTF8Marshaler.CreateInstance(text);
        _log(null, new LogEventArgs((LogEventTypes)type, instance));
    }

    private static void SetLogEvents()
    {
        string eventFlags;

        if (_logEvents == LogEventTypes.All)
            eventFlags = "All,Trace";
        else if (_logEvents == LogEventTypes.Detailed)
            eventFlags = "All";
        else
            eventFlags = EnumHelper.ConvertFlags(_logEvents);

        NativeMagickNET.SetLogEvents(eventFlags);
    }

    private static class NativeWindowsMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetDllDirectory(string lpPathName);
    }
}
