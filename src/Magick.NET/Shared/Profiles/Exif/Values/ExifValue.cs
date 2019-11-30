// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    internal abstract class ExifValue : IExifValue, IEquatable<ExifTag>
    {
        public ExifValue(ExifTag tag) => Tag = tag;

        public ExifValue(ExifTagValue tag) => Tag = new UnkownExifTag(tag);

        public abstract ExifDataType DataType { get; }

        public abstract bool IsArray { get; }

        public ExifTag Tag { get; }

        public static bool operator ==(ExifValue left, ExifTag right) => Equals(left, right);

        public static bool operator !=(ExifValue left, ExifTag right) => !Equals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ExifTag tag)
                return Equals(tag);

            if (obj is ExifValue value)
                return Tag.Equals(value.Tag) && Equals(GetValue(), value.GetValue());

            return false;
        }

        public bool Equals(ExifTag other) => Tag.Equals(other);

        public override int GetHashCode()
        {
            var hashCode = Tag.GetHashCode();

            var value = GetValue();
            if (value == null)
                return hashCode;

            return hashCode ^= value.GetHashCode();
        }

        public abstract object GetValue();

        public abstract bool SetValue(object value);
    }
}