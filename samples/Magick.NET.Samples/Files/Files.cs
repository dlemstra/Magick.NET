// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace Magick.NET.Samples;

public static class SampleFiles
{
    private const string _RootDirectory = @"..\..\Samples\Magick.NET\";
    private const string _FilesDirectory = _RootDirectory + @"Files\";

    public static string CorruptImageJpg
        => _FilesDirectory + "CorruptImage.jpg";

    public static string FileWithWarningJpg
        => _FilesDirectory + "FileWithWarning.jpg";

    public static string FujiFilmFinePixS1ProJpg
        => _FilesDirectory + "FujiFilmFinePixS1Pro.jpg";

    public static string InvalidFileJpg
        => _FilesDirectory + "InvalidFile.jpg";

    public static string OutputDirectory
        => _RootDirectory + @"Output\";

    public static string SnakewareEps
        => _FilesDirectory + "Snakeware.eps";

    public static string SnakewareGif
        => _FilesDirectory + "Snakeware.gif";

    public static string SnakewareJpg
        => _FilesDirectory + "Snakeware.jpg";

    public static string SnakewarePdf
        => _FilesDirectory + "Snakeware.pdf";

    public static string SnakewarePng
        => _FilesDirectory + "Snakeware.png";

    public static string StillLifeCR2
        => _FilesDirectory + "StillLife.cr2";

    public static string YourProfileIcc
        => _FilesDirectory + "YourProfile.icc";

    public static string SampleBackground
        => _FilesDirectory + "2FD-Background.jpg";
}
