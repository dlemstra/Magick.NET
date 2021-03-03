// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Formats
{
    /// <summary>
    /// The additional info of a <see cref="MagickFormat.Psd"/> image.
    /// </summary>
    public sealed class PsdAdditionalInfo
    {
        private PsdAdditionalInfo(string layerName)
        {
            LayerName = layerName;
        }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        public string LayerName { get; }

        /// <summary>
        /// Creates additional info from a <see cref="MagickFormat.Psd"/> image.
        /// </summary>
        /// <param name="image">The image to create the additonal info from.</param>
        /// <returns>The additional info from a <see cref="MagickFormat.Psd"/> image.</returns>
        public static PsdAdditionalInfo? FromImage(IMagickImage<QuantumType> image)
        {
            Throw.IfNull(nameof(image), image);

            var profile = image.GetProfile("psd:additional-info");
            if (profile == null)
                return null;

            var bytes = profile.ToByteArray();
            if (bytes == null)
                return null;

            return ParseAdditionalInfo(bytes);
        }

        private static PsdAdditionalInfo? ParseAdditionalInfo(byte[] bytes)
        {
            var offset = 0;

            while (offset < bytes.Length - 12)
            {
                offset += 4;
                var key = Encoding.ASCII.GetString(bytes, offset, 4);
                offset += 4;

                int size = bytes[offset++] << 24;
                size |= bytes[offset++] << 16;
                size |= bytes[offset++] << 8;
                size |= bytes[offset++];

                if (offset + size > bytes.Length)
                    break;

                if ("luni".Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    int count = bytes[offset++] << 24;
                    count |= bytes[offset++] << 16;
                    count |= bytes[offset++] << 8;
                    count |= bytes[offset++];
                    count *= 2;

                    if (count > size - 4)
                        break;

                    SwapBytes(bytes, offset, offset + count);

                    var layerName = Encoding.Unicode.GetString(bytes, offset, count);
                    return new PsdAdditionalInfo(layerName);
                }

                offset += size;
            }

            return null;
        }

        private static void SwapBytes(byte[] bytes, int start, int end)
        {
            for (var i = start + 1; i < end; i += 2)
            {
                var value = bytes[i - 1];
                bytes[i - 1] = bytes[i];
                bytes[i] = value;
            }
        }
    }
}