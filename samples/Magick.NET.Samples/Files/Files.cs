// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace Magick.NET.Samples
{
  public static class SampleFiles
  {
    private const string _RootDirectory = @"..\..\Samples\Magick.NET\";
    private const string _FilesDirectory = _RootDirectory + @"Files\";

    public static string CorruptImageJpg
    {
      get
      {
        return _FilesDirectory + "CorruptImage.jpg";
      }
    }

    public static string FileWithWarningJpg
    {
      get
      {
        return _FilesDirectory + "FileWithWarning.jpg";
      }
    }

    public static string FujiFilmFinePixS1ProJpg
    {
      get
      {
        return _FilesDirectory + "FujiFilmFinePixS1Pro.jpg";
      }
    }

    public static string InvalidFileJpg
    {
      get
      {
        return _FilesDirectory + "InvalidFile.jpg";
      }
    }

    public static string OutputDirectory
    {
      get
      {
        return _RootDirectory + @"Output\";
      }
    }

    public static string SnakewareEps
    {
      get
      {
        return _FilesDirectory + "Snakeware.eps";
      }
    }

    public static string SnakewareGif
    {
      get
      {
        return _FilesDirectory + "Snakeware.gif";
      }
    }

    public static string SnakewareJpg
    {
      get
      {
        return _FilesDirectory + "Snakeware.jpg";
      }
    }

    public static string SnakewarePdf
    {
      get
      {
        return _FilesDirectory + "Snakeware.pdf";
      }
    }

    public static string SnakewarePng
    {
      get
      {
        return _FilesDirectory + "Snakeware.png";
      }
    }

    public static string StillLifeCR2
    {
      get
      {
        return _FilesDirectory + "StillLife.cr2";
      }
    }

    public static string YourProfileIcc
    {
      get
      {
        return _FilesDirectory + "YourProfile.icc";
      }
    }

    public static string SampleBackground
    {
      get 
      { 
        return _FilesDirectory + "2FD-Background.jpg"; }
      }
    }
}
