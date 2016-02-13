//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ImageMagick
{
  ///<summary>
  /// Class that can be used to initialize Magick.NET.
  ///</summary>
  public static partial class MagickNET
  {
    private static LogDelegate _NativeLog;
    private static EventHandler<LogEventArgs> _Log;
    private static LogEvents _LogEvents = LogEvents.None;

    private static readonly string[] _ImageMagickFiles = new string[]
    {
      "coder.xml", "colors.xml", "configure.xml", "delegates.xml", "english.xml", "locale.xml",
      "log.xml", "magic.xml", "policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
    };
    private static bool? _UseOpenCL;

    private static string CheckDirectory(string path)
    {
      Throw.IfNullOrEmpty("path", path);

      path = FileHelper.CheckForBaseDirectory(path);
      path = Path.GetFullPath(path);
      Throw.IfFalse("path", Directory.Exists(path), "Unable to find directory: {0}", path);
      return path;
    }

    private static void CheckImageMagickFiles(string path)
    {
      foreach (string imageMagickFile in _ImageMagickFiles)
      {
        string fileName = path + "\\" + imageMagickFile;
        Throw.IfFalse("path", File.Exists(fileName), "Unable to find file: {0}", fileName);
      }
    }

    private static void OnLog(UIntPtr type, IntPtr text)
    {
      if (_Log == null)
        return;

      string managedText = UTF8Marshaler.NativeToManaged(text);
      _Log(null, new LogEventArgs((LogEvents)type, managedText));
    }

    private static void SetLogEvents()
    {
      string eventFlags = null;

      if (EnumHelper.HasFlag(_LogEvents, LogEvents.All))
      {
        if (EnumHelper.HasFlag(_LogEvents, LogEvents.Trace))
          eventFlags = "All,Trace";
        else
          eventFlags = "All";
      }
      else
        eventFlags = EnumHelper.ConvertFlags(_LogEvents);

      NativeMagickNET.SetLogEvents(eventFlags);
    }

    ///<summary>
    /// Returns the format information of the specified format based on the extension of the
    /// file.
    ///</summary>
    ///<param name="file">The file to get the format for.</param>
    public static MagickFormatInfo GetFormatInformation(FileInfo file)
    {
      return MagickFormatInfo.Create(file);
    }

    ///<summary>
    /// Returns the format information of the specified format.
    ///</summary>
    ///<param name="format">The image format.</param>
    public static MagickFormatInfo GetFormatInformation(MagickFormat format)
    {
      return MagickFormatInfo.Create(format);
    }

    ///<summary>
    /// Returns the format information of the specified format based on the extension of the
    /// file. If that fails the format will be determined by 'pinging' the file.
    ///</summary>
    ///<param name="fileName">The name of the file to get the format for.</param>
    public static MagickFormatInfo GetFormatInformation(string fileName)
    {
      return MagickFormatInfo.Create(fileName);
    }

    ///<summary>
    /// Adds the specified path to the environment path. You should place the ImageMagick
    /// xml files in that directory.
    ///</summary>
    ///<param name="path">The path that contains the ImageMagick xml files.</param>
    public static void Initialize(string path)
    {
      string newPath = CheckDirectory(path);

      CheckImageMagickFiles(newPath);

      NativeMagickNET.SetEnv("MAGICK_CONFIGURE_PATH", path);
    }

    ///<summary>
    /// Set the events that will be written to the log. The log will be written to the Log event
    /// and the debug window in VisualStudio. To change the log settings you must use a custom
    /// log.xml file.
    ///</summary>
    ///<param name="events">The events that will be logged.</param>
    public static void SetLogEvents(LogEvents events)
    {
      _LogEvents = events;

      if (_Log != null)
        SetLogEvents();
    }

    ///<summary>
    /// Sets the directory that contains the Ghostscript file gsdll32.dll / gsdll64.dll.
    ///</summary>
    ///<param name="path">The path of the Ghostscript directory.</param>
    public static void SetGhostscriptDirectory(string path)
    {
      NativeMagickNET.SetEnv("MAGICK_GHOSTSCRIPT_PATH", CheckDirectory(path));
    }

    ///<summary>
    /// Sets the directory that contains the Ghostscript font files.
    ///</summary>
    ///<param name="path">The path of the Ghostscript font directory.</param>
    public static void SetGhostscriptFontDirectory(string path)
    {
      NativeMagickNET.SetEnv("MAGICK_GHOSTSCRIPT_FONT_PATH", CheckDirectory(path));
    }

    ///<summary>
    /// Sets the directory that will be used by ImageMagick to store OpenCL cache files.
    ///</summary>
    ///<param name="path">The path of the OpenCL cache directory.</param>
    public static void SetOpenCLCacheDirectory(string path)
    {
      NativeMagickNET.SetEnv("MAGICK_OPENCL_CACHE_DIR", CheckDirectory(path));
    }

    ///<summary>
    /// Sets the directory that will be used when ImageMagick does not have enough memory for the
    /// pixel cache.
    ///</summary>
    ///<param name="path">The path where temp files will be written.</param>
    public static void SetTempDirectory(string path)
    {
      NativeMagickNET.SetEnv("MAGICK_TEMPORARY_PATH", CheckDirectory(path));
    }

    /// <summary>
    /// Sets the pseudo-random number generator secret key.
    /// </summary>
    /// <param name="seed">The secret key.</param>
    public static void SetRandomSeed(int seed)
    {
      NativeMagickNET.SetRandomSeed(seed);
    }

    ///<summary>
    /// Event that will be raised when something is logged by ImageMagick.
    ///</summary>
    public static event EventHandler<LogEventArgs> Log
    {
      add
      {
        if (_Log == null)
        {
          _NativeLog = new LogDelegate(OnLog);
          NativeMagickNET.SetLogDelegate(_NativeLog);
          SetLogEvents();
        }

        _Log += value;
      }
      remove
      {
        _Log -= value;

        if (_Log == null)
        {
          NativeMagickNET.SetLogDelegate(null);
          NativeMagickNET.SetLogEvents("None");
          _NativeLog = null;
        }
      }
    }

    ///<summary>
    /// Returns the features reported by ImageMagick.
    ///</summary>
    public static string Features
    {
      get
      {
        return NativeMagickNET.Features;
      }
    }

    ///<summary>
    /// Returns information about the supported formats.
    ///</summary>
    public static IEnumerable<MagickFormatInfo> SupportedFormats
    {
      get
      {
        return MagickFormatInfo.All;
      }
    }

    ///<summary>
    /// Gets or sets the use of OpenCL.
    ///</summary>
    public static bool UseOpenCL
    {
      get
      {
        if (!_UseOpenCL.HasValue)
          _UseOpenCL = NativeMagickNET.SetUseOpenCL(true);

        return _UseOpenCL.Value;
      }
      set
      {
        _UseOpenCL = NativeMagickNET.SetUseOpenCL(value);
      }
    }

    ///<summary>
    /// Returns the version of Magick.NET.
    ///</summary>
    public static string Version
    {
      get
      {
        AssemblyTitleAttribute title = TypeHelper.GetCustomAttribute<AssemblyTitleAttribute>(typeof(MagickNET));
        AssemblyFileVersionAttribute version = TypeHelper.GetCustomAttribute<AssemblyFileVersionAttribute>(typeof(MagickNET));
        return title.Title + " " + version.Version;
      }
    }
  }
}
