// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using System.Collections.Generic;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheKernelProfileRecordsProperty
{
    [Fact]
    public void ShouldReturnTheCorrectInformation()
    {
        var device = GetEnabledDevice();
        if (device is null)
            return;

        device.ProfileKernels = true;

        using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
        image.Resize(500, 500);
        image.Resize(100, 100);

        device.ProfileKernels = false;

        var records = new List<IOpenCLKernelProfileRecord>(device.KernelProfileRecords);
        Assert.False(records.Count < 2);

        foreach (var record in records)
        {
            Assert.NotNull(record.Name);
            Assert.False(record.Count < 0);
            Assert.False(record.MaximumDuration < 0);
            Assert.False(record.MinimumDuration < 0);
            Assert.False(record.TotalDuration < 0);
        }
    }

    private IOpenCLDevice GetEnabledDevice()
    {
        foreach (var device in OpenCL.Devices)
        {
            if (device.IsEnabled)
                return device;
        }

        return null;
    }
}

#endif
