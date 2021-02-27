// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;

namespace ImageMagick
{
    internal sealed class ColorProfileReader
    {
        private EndianReader _reader;

        public ColorSpace ColorSpace { get; private set; }

        public string Copyright { get; private set; }

        public string Description { get; private set; }

        public string Manufacturer { get; private set; }

        public string Model { get; private set; }

        public void Read(byte[] data)
        {
            if (data == null || data.Length < 20)
                return;

            _reader = new EndianReader(data);

            ReadColorSpace();
            ReadTagTable();
        }

        private static ColorSpace DetermineColorSpace(string colorSpace)
        {
            switch (colorSpace)
            {
                case "CMY":
                    return ColorSpace.CMY;
                case "CMYK":
                    return ColorSpace.CMYK;
                case "GRAY":
                    return ColorSpace.Gray;
                case "HSL":
                    return ColorSpace.HSL;
                case "HSV":
                    return ColorSpace.HSV;
                case "Lab":
                    return ColorSpace.Lab;
                case "Luv":
                    return ColorSpace.YUV;
                case "RGB":
                    return ColorSpace.sRGB;
                case "XYZ":
                    return ColorSpace.XYZ;
                case "YCbr":
                    return ColorSpace.YCbCr;
                default:
                    throw new NotSupportedException(colorSpace);
            }
        }

        private void ReadColorSpace()
        {
            _reader.Seek(16);

            var colorSpace = _reader.ReadString(4).TrimEnd();
            ColorSpace = DetermineColorSpace(colorSpace);
        }

        private void ReadTagTable()
        {
            if (!_reader.Seek(128))
                return;

            var count = _reader.ReadLong();
            for (var i = 0; i < count; i++)
            {
                var tag = _reader.ReadLong();
                switch (tag)
                {
                    case 0x63707274:
                        Copyright = ReadTag();
                        break;
                    case 0x64657363:
                        Description = ReadTag();
                        break;
                    case 0x646D6E64:
                        Manufacturer = ReadTag();
                        break;
                    case 0x646D6464:
                        Model = ReadTag();
                        break;
                    default:
                        _reader.Skip(8);
                        break;
                }
            }
        }

        private string ReadTag()
        {
            var offset = _reader.ReadLong();
            var length = _reader.ReadLong();

            if (offset == null || length == null)
                return null;

            var originalIndex = _reader.Index;

            if (!_reader.Seek(offset.Value))
                return null;

            var value = ReadTagValue(length.Value);

            _reader.Seek(originalIndex);

            return value;
        }

        private string ReadTagValue(uint length)
        {
            var type = _reader.ReadString(4);
            switch (type)
            {
                case "desc":
                    return ReadTextDescriptionTypeValue();
                case "text":
                    return ReadTextTypeValue(length);
                default:
                    return null;
            }
        }

        private string ReadTextDescriptionTypeValue()
        {
            if (!_reader.Skip(4))
                return null;

            var length = _reader.ReadLong();
            if (length == null)
                return null;

            return _reader.ReadString(length.Value);
        }

        private string ReadTextTypeValue(uint length)
        {
            if (!_reader.Skip(4))
                return null;

            return _reader.ReadString(length);
        }
    }
}
