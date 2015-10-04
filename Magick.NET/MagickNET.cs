//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
  public static class MagickNET
  {
    private static readonly string[] _ImageMagickFiles = new string[]
    {
      "coder.xml", "colors.xml", "configure.xml", "delegates.xml", "english.xml", "locale.xml",
      "log.xml", "magic.xml", "policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
    };

    private static EventHandler<LogEventArgs> _Log;
    private static LogEvents _LogEvents = LogEvents.None;

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

    private static void OnLog(LogEvents type, string text)
    {
      if (_Log == null)
        return;

      _Log(null, new LogEventArgs(type, text));
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

      Wrapper.MagickNET.SetLogEvents(eventFlags);
    }

    ///<summary>
    /// Returns the format information of the specified format based on the extension of the
    /// file.
    ///</summary>
    ///<param name="file">The file to get the format for.</param>
    public static MagickFormatInfo GetFormatInformation(FileInfo file)
    {
      Throw.IfNull("file", file);

      MagickFormat? format = null;
      if (file.Extension != null && file.Extension.Length > 1)
        format = (MagickFormat?)EnumHelper.Parse(typeof(MagickFormat), file.Extension.Substring(1));

      if (format == null)
        return null;

      return GetFormatInformation(format.Value);
    }

    ///<summary>
    /// Returns the format information of the specified format.
    ///</summary>
    ///<param name="format">The image format.</param>
    public static MagickFormatInfo GetFormatInformation(MagickFormat format)
    {
      return MagickFormatInfo.Create(Wrapper.MagickNET.GetFormatInformation(format));
    }

    ///<summary>
    /// Returns the format information of the specified format based on the extension of the
    /// file. If that fails the format will be determined by 'pinging' the file.
    ///</summary>
    ///<param name="fileName">The name of the file to get the format for.</param>
    public static MagickFormatInfo GetFormatInformation(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      return GetFormatInformation(new FileInfo(filePath));
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

      Wrapper.MagickNET.SetEnv("MAGICK_CONFIGURE_PATH", path);
    }

    ///<summary>
    /// Sets the directory that will be used when ImageMagick does not have enough memory for the
    /// pixel cache.
    ///</summary>
    ///<param name="path">The path where temp files will be written.</param>
    public static void SetTempDirectory(string path)
    {
      Wrapper.MagickNET.SetEnv("MAGICK_TEMPORARY_PATH", CheckDirectory(path));
    }
#if (WIN64)
    ///<summary>
    /// Sets the directory that contains the Ghostscript file gsdll64.dll.
    ///</summary>
    ///<param name="path">The path of the Ghostscript directory.</param>
#else
    ///<summary>
    /// Sets the directory that contains the Ghostscript file gsdll32.dll.
    ///</summary>
    ///<param name="path">The path of the Ghostscript directory.</param>
#endif
    public static void SetGhostscriptDirectory(string path)
    {
      Wrapper.MagickNET.SetEnv("MAGICK_GHOSTSCRIPT_PATH", CheckDirectory(path));
    }

    ///<summary>
    /// Sets the directory that contains the Ghostscript font files.
    ///</summary>
    ///<param name="path">The path of the Ghostscript font directory.</param>
    public static void SetGhostscriptFontDirectory(string path)
    {
      Wrapper.MagickNET.SetEnv("MAGICK_GHOSTSCRIPT_FONT_PATH", CheckDirectory(path));
    }

    ///<summary>
    /// Sets the directory that will be used by ImageMagick to store OpenCL cache files.
    ///</summary>
    ///<param name="path">The path of the OpenCL cache directory.</param>
    public static void SetOpenCLCacheDirectory(string path)
    {
      Wrapper.MagickNET.SetEnv("MAGICK_OPENCL_CACHE_DIR", CheckDirectory(path));
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
    /// Event that will be raised when something is logged by ImageMagick.
    ///</summary>
    public static event EventHandler<LogEventArgs> Log
    {
      add
      {
        if (_Log == null)
        {
          Wrapper.MagickNET.SetLogDelegate(OnLog);
          SetLogEvents();
        }

        _Log += value;
      }
      remove
      {
        _Log -= value;

        if (_Log == null)
        {
          Wrapper.MagickNET.SetLogDelegate(null);
          Wrapper.MagickNET.SetLogEvents("None");
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
        return Wrapper.MagickNET.Features;
      }
    }

    ///<summary>
    /// Returns information about the supported formats.
    ///</summary>
    public static IEnumerable<MagickFormatInfo> SupportedFormats
    {
      get
      {
        foreach (Wrapper.MagickFormatInfo formatInfo in Wrapper.MagickNET.SupportedFormats)
        {
          yield return MagickFormatInfo.Create(formatInfo);
        }
      }
    }

    ///<summary>
    /// Gets or sets the use of OpenCL.
    ///</summary>
    public static bool UseOpenCL
    {
      get
      {
        return Wrapper.MagickNET.UseOpenCL;
      }
      set
      {
        Wrapper.MagickNET.UseOpenCL = value;
      }
    }

    ///<summary>
    /// Returns the version of Magick.NET.
    ///</summary>
    public static string Version
    {
      get
      {
        object title = typeof(MagickNET).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
        object version = typeof(MagickNET).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
        return ((AssemblyTitleAttribute)title).Title + " " + ((AssemblyFileVersionAttribute)version).Version;
      }
    }
  }
}
