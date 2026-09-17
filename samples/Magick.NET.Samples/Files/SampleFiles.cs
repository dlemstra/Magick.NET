// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace Magick.NET.Samples;

public static class SampleFiles
{
    private const string _rootDirectory = @"..\..\Samples\Magick.NET\";
    private const string _filesDirectory = _rootDirectory + @"Files\";

    public static string CorruptImageJpg
        => _filesDirectory + "CorruptImage.jpg";

    public static string FileWithWarningJpg
        => _filesDirectory + "FileWithWarning.jpg";

    public static string FujiFilmFinePixS1ProJpg
        => _filesDirectory + "FujiFilmFinePixS1Pro.jpg";

    public static string InvalidFileJpg
        => _filesDirectory + "InvalidFile.jpg";

    public static string OutputDirectory
        => _rootDirectory + @"Output\";

    public static string SnakewareEps
        => _filesDirectory + "Snakeware.eps";

    public static string SnakewareGif
        => _filesDirectory + "Snakeware.gif";

    public static string SnakewareJpg
        => _filesDirectory + "Snakeware.jpg";

    public static string SnakewarePdf
        => _filesDirectory + "Snakeware.pdf";

    public static string SnakewarePng
        => _filesDirectory + "Snakeware.png";

    public static string StillLifeCR2
        => _filesDirectory + "StillLife.cr2";

    public static string YourProfileIcc
        => _filesDirectory + "YourProfile.icc";

    public static string SampleBackground
        => _filesDirectory + "2FD-Background.jpg";
}
