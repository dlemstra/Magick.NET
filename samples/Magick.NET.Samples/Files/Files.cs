﻿// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Samples
{
  public static class SampleFiles
  {
    //private const string _RootDirectory = @"..\..\Samples\Magick.NET.Samples\";
    //private static string _FilesDirectory = _RootDirectory + @"Files\";

    private static string _RootDirectory
    {
        get
        {
            string curDir = Directory.GetCurrentDirectory();
            return curDir + @"\..\..\..\..\";
        }
    }
    private static string _FilesDirectory
    {
        get
        {
            return _RootDirectory + @"Files\";
        }
    }

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
