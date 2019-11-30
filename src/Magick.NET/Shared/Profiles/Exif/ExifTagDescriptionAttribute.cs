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
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal sealed class ExifTagDescriptionAttribute : Attribute
    {
        private readonly object _value;
        private readonly string _description;

        public ExifTagDescriptionAttribute(object value, string description)
        {
            _value = value;
            _description = description;
        }

        public static string GetDescription(ExifTag tag, object value)
        {
            var tagValue = (ExifTagValue)(ushort)tag;
            var attributes = TypeHelper.GetCustomAttributes<ExifTagDescriptionAttribute>(tagValue);

            if (attributes == null || attributes.Length == 0)
                return null;

            foreach (var attribute in attributes)
            {
                if (Equals(attribute._value, value))
                    return attribute._description;
            }

            return null;
        }
    }
}
