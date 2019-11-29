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

using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Exif value that contains a <see cref="ushort"/>.
    /// </summary>
    public sealed class ExifShort : ExifValue<ushort>
    {
        internal ExifShort(ExifTagValue tag)
            : base(tag)
        {
        }

        /// <summary>
        /// Gets the data type of the exif value.
        /// </summary>
        public override ExifDataType DataType => ExifDataType.Short;

        /// <summary>
        /// Gets a string that represents the current value.
        /// </summary>
        protected override string StringValue => Value.ToString(CultureInfo.InvariantCulture);

        internal static ExifShort Create(ExifTagValue tag, ushort value) => new ExifShort(tag) { Value = value };

        /// <summary>
        /// Tries to set the value and returns a value indicating whether the value could be set.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A value indicating whether the value could be set.</returns>
        protected override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = (ushort)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
