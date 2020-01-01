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
using System.IO;

namespace ImageMagick.Web
{
    internal sealed class StreamImageData : IImageData
    {
        private readonly IStreamUrlResolver _resolver;

        public StreamImageData(IStreamUrlResolver resolver, MagickFormatInfo formatInfo)
        {
            _resolver = resolver;
            FormatInfo = formatInfo;
        }

        public MagickFormatInfo FormatInfo { get; }

        public string ImageId => _resolver.ImageId;

        public bool IsValid => !string.IsNullOrEmpty(_resolver.ImageId);

        public DateTime ModifiedTimeUtc => _resolver.ModifiedTimeUtc;

        public Stream ReadImage()
        {
            return _resolver.OpenStream();
        }

        public MagickImage ReadImage(MagickReadSettings settings)
        {
            using (Stream stream = _resolver.OpenStream())
            {
                return new MagickImage(stream, settings);
            }
        }

        public void SaveImage(string fileName)
        {
            using (FileStream output = File.OpenWrite(fileName))
            {
                using (Stream input = _resolver.OpenStream())
                {
                    input.CopyTo(output);
                }
            }
        }
    }
}