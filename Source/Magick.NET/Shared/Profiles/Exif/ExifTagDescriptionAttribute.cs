//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;

namespace ImageMagick
{
    /// <summary>
    /// Class that provides a description for an ExifTag value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal sealed class ExifTagDescriptionAttribute : Attribute
    {
        private object _value;
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExifTagDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="value">The value of the exif tag.</param>
        /// <param name="description">The description for the value of the exif tag.</param>
        public ExifTagDescriptionAttribute(object value, string description)
        {
            _value = value;
            _description = description;
        }

        public static string GetDescription(ExifTag tag, object value)
        {
            ExifTagDescriptionAttribute[] attributes = TypeHelper.GetCustomAttributes<ExifTagDescriptionAttribute>(tag);

            if (attributes == null || attributes.Length == 0)
                return null;

            foreach (ExifTagDescriptionAttribute attribute in attributes)
            {
                if (Equals(attribute._value, value))
                    return attribute._description;
            }

            return null;
        }
    }
}
