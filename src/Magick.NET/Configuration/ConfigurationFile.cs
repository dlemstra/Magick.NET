// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick.Configuration;

internal sealed class ConfigurationFile : IConfigurationFile
{
    public ConfigurationFile(string fileName)
    {
        FileName = fileName;
        Data = LoadData();
    }

    public ConfigurationFile(IConfigurationFile configurationFile)
    {
        FileName = configurationFile.FileName;
        Data = configurationFile.Data;
    }

    public string FileName { get; }

    public string Data { get; set; }

    private string LoadData()
    {
        using var stream = TypeHelper.GetManifestResourceStream(typeof(ConfigurationFile), "ImageMagick.Resources.Xml", FileName);
        using var reader = new StreamReader(stream);
        var data = reader.ReadToEnd();

        data = UpdateDelegatesXml(data);

        return data;
    }

    private string UpdateDelegatesXml(string data)
    {
        if (Runtime.IsWindows || FileName != "delegates.xml")
            return data;

        data = data.Replace("@PSDelegate@", "gs");
        data = data.Replace("ffmpeg.exe", "ffmpeg");

        return data;
    }
}
