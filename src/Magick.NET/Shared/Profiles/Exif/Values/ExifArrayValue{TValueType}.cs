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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    internal abstract class ExifArrayValue<TValueType> : ExifValue, IExifValue<TValueType[]>
    {
        public ExifArrayValue(ExifTag<TValueType[]> tag)
            : base(tag)
        {
        }

        public ExifArrayValue(ExifTagValue tag)
            : base(tag)
        {
        }

        public override bool IsArray => true;

        [SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "This value is typed.")]
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "The property needs to be an array.")]
        public TValueType[] Value { get; set; }

        public override object GetValue() => Value;

        public override bool SetValue(object value)
        {
            if (value == null)
            {
                Value = null;
                return true;
            }

            if (value is TValueType[] typeValueArray)
            {
                Value = typeValueArray;
                return true;
            }

            if (value is TValueType typeValue)
            {
                Value = new TValueType[] { typeValue };
                return true;
            }

            return false;
        }
    }
}