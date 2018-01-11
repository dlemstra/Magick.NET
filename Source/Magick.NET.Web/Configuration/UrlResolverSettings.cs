﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace ImageMagick.Web
{
    /// <summary>
    /// Class that contains the settings for an url resolver.
    /// </summary>
    public class UrlResolverSettings : ConfigurationElement
    {
        private IUrlResolverConstructor _constructor;

        private delegate IUrlResolver IUrlResolverConstructor();

        [ConfigurationProperty("type", IsRequired = true)]
        internal string TypeName
        {
            get
            {
                return (string)this["type"];
            }
        }

        internal IUrlResolver CreateInstance()
        {
            return _constructor();
        }

        /// <summary>
        /// Called after deserialization.
        /// </summary>
        protected override void PostDeserialize()
        {
            base.PostDeserialize();

            Type type = Type.GetType(TypeName, true, true);
            CheckType(type);
            CreateConstructor(type);
        }

        private void CreateConstructor(Type type)
        {
            ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            NewExpression newExp = Expression.New(ctor);
            _constructor = (IUrlResolverConstructor)Expression.Lambda(typeof(IUrlResolverConstructor), newExp).Compile();
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IFileUrlResolver", Justification = "This is the correct spelling.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IStreamUrlResolver", Justification = "This is the correct spelling.")]
        private void CheckType(Type type)
        {
            if (typeof(IFileUrlResolver).IsAssignableFrom(type))
                return;

            if (typeof(IStreamUrlResolver).IsAssignableFrom(type))
                return;

            throw new ConfigurationErrorsException("The type '" + TypeName + "' should implement one of the following interfaces: " + nameof(IFileUrlResolver) + "," + nameof(IStreamUrlResolver));
        }
    }
}
