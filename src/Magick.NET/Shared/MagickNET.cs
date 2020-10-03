// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ImageMagick.Configuration;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to initialize Magick.NET.
    /// </summary>
    public static partial class MagickNET
    {
        private static LogDelegate _nativeLog;
        private static EventHandler<LogEventArgs> _log;
        private static LogEvents _logEvents = LogEvents.None;

        /// <summary>
        /// Event that will be raised when something is logged by ImageMagick.
        /// </summary>
        public static event EventHandler<LogEventArgs> Log
        {
            add
            {
                if (_log == null)
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

                if (_log == null)
                {
                    NativeMagickNET.SetLogDelegate(null);
                    NativeMagickNET.SetLogEvents("None");
                    _nativeLog = null;
                }
            }
        }

        /// <summary>
        /// Gets the ImageMagick delegate libraries.
        /// </summary>
        public static string Delegates
            => NativeMagickNET.Delegates;

        /// <summary>
        /// Gets the ImageMagick features.
        /// </summary>
        public static string Features
            => NativeMagickNET.Features;

        /// <summary>
        /// Gets the information about the supported formats.
        /// </summary>
        public static IEnumerable<MagickFormatInfo> SupportedFormats
            => MagickFormatInfo.All;

        /// <summary>
        /// Gets the font families that are known by ImageMagick.
        /// </summary>
        public static IEnumerable<string> FontFamilies
        {
            get
            {
                var result = new List<string>();

                var list = IntPtr.Zero;
                var length = (UIntPtr)0;

                try
                {
                    list = NativeMagickNET.GetFonts(out length);

                    for (int i = 0; i < (int)length; i++)
                    {
                        var fontFamily = NativeMagickNET.GetFontFamily(list, i);
                        if (!string.IsNullOrEmpty(fontFamily) && !result.Contains(fontFamily))
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
        public static IEnumerable<string> FontNames
        {
            get
            {
                var result = new List<string>();

                var list = IntPtr.Zero;
                var length = (UIntPtr)0;

                try
                {
                    list = NativeMagickNET.GetFonts(out length);

                    for (int i = 0; i < (int)length; i++)
                    {
                        var fontName = NativeMagickNET.GetFontName(list, i);
                        if (!string.IsNullOrEmpty(fontName))
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
        /// Returns the format information of the specified format based on the extension of the file.
        /// </summary>
        /// <param name="file">The file to get the format for.</param>
        /// <returns>The format information.</returns>
        public static IMagickFormatInfo GetFormatInformation(FileInfo file)
            => MagickFormatInfo.Create(file);

        /// <summary>
        /// Returns the format information of the specified format.
        /// </summary>
        /// <param name="format">The image format.</param>
        /// <returns>The format information.</returns>
        public static IMagickFormatInfo GetFormatInformation(MagickFormat format)
            => MagickFormatInfo.Create(format);

        /// <summary>
        /// Returns the format information of the specified format based on the extension of the
        /// file. If that fails the format will be determined by 'pinging' the file.
        /// </summary>
        /// <param name="fileName">The name of the file to get the format for.</param>
        /// <returns>The format information.</returns>
        public static IMagickFormatInfo GetFormatInformation(string fileName)
            => MagickFormatInfo.Create(fileName);

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
        public static string Initialize(ConfigurationFiles configFiles)
        {
            Throw.IfNull(nameof(configFiles), configFiles);

            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(path);

            InitializePrivate(configFiles, path);

            return path;
        }

        /// <summary>
        /// Initializes ImageMagick with the specified configuration files in the specified the path.
        /// </summary>
        /// <param name="configFiles">The configuration files ot initialize ImageMagick with.</param>
        /// <param name="path">The directory to save the configuration files in.</param>
        public static void Initialize(ConfigurationFiles configFiles, string path)
        {
            Throw.IfNull(nameof(configFiles), configFiles);

            var newPath = FileHelper.GetFullPath(path);

            InitializePrivate(configFiles, newPath);
        }

        /// <summary>
        /// Resets the pseudo-random number generator secret key.
        /// </summary>
        public static void ResetRandomSeed()
            => NativeMagickNET.SetRandomSeed(-1);

        /// <summary>
        /// Sets the directory that contains the Ghostscript file gsdll32.dll / gsdll64.dll.
        /// </summary>
        /// <param name="path">The path of the Ghostscript directory.</param>
        public static void SetGhostscriptDirectory(string path)
            => Environment.SetEnv("MAGICK_GHOSTSCRIPT_PATH", FileHelper.GetFullPath(path));

        /// <summary>
        /// Sets the directory that contains the Ghostscript font files.
        /// </summary>
        /// <param name="path">The path of the Ghostscript font directory.</param>
        public static void SetGhostscriptFontDirectory(string path)
            => Environment.SetEnv("MAGICK_GHOSTSCRIPT_FONT_PATH", FileHelper.GetFullPath(path));

        /// <summary>
        /// Set the events that will be written to the log. The log will be written to the Log event
        /// and the debug window in VisualStudio. To change the log settings you must use a custom
        /// log.xml file.
        /// </summary>
        /// <param name="events">The events that will be logged.</param>
        public static void SetLogEvents(LogEvents events)
        {
            _logEvents = events;

            if (_log != null)
                SetLogEvents();
        }

        /// <summary>
        /// Sets the directory that contains the Native library. This currently only works on Windows.
        /// </summary>
        /// <param name="path">The path of the directory that contains the native library.</param>
        public static void SetNativeLibraryDirectory(string path)
        {
#if NETSTANDARD
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
               NativeWindowsMethods.SetDllDirectory(FileHelper.GetFullPath(path));
#else
            NativeWindowsMethods.SetDllDirectory(FileHelper.GetFullPath(path));
#endif
        }

        /// <summary>
        /// Sets the directory that will be used when ImageMagick does not have enough memory for the
        /// pixel cache.
        /// </summary>
        /// <param name="path">The path where temp files will be written.</param>
        public static void SetTempDirectory(string path)
            => Environment.SetEnv("MAGICK_TEMPORARY_PATH", FileHelper.GetFullPath(path));

        /// <summary>
        /// Sets the pseudo-random number generator secret key.
        /// </summary>
        /// <param name="seed">The secret key.</param>
        public static void SetRandomSeed(int seed)
            => NativeMagickNET.SetRandomSeed(seed);

        private static void CheckImageMagickFiles(string path)
        {
            foreach (IConfigurationFile configurationFile in ConfigurationFiles.Default.Files)
            {
                var fileName = Path.Combine(path, configurationFile.FileName);
                Throw.IfFalse(nameof(path), File.Exists(fileName), $"Unable to find file: {fileName}");
            }
        }

        private static void InitializePrivate(ConfigurationFiles configFiles, string newPath)
        {
            configFiles.WriteInDirectory(newPath);

            Environment.SetEnv("MAGICK_CONFIGURE_PATH", newPath);
        }

        private static void OnLog(UIntPtr type, IntPtr text)
        {
            if (_log == null)
                return;

            var managedText = UTF8Marshaler.NativeToManaged(text);
            _log(null, new LogEventArgs((LogEvents)type, managedText));
        }

        private static void SetLogEvents()
        {
            string eventFlags;

            if (_logEvents == LogEvents.All)
                eventFlags = "All,Trace";
            else if (_logEvents == LogEvents.Detailed)
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
}
