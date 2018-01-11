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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Specifies a kind of color space.
    /// </summary>
    public enum ColorSpace
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// CMY
        /// </summary>
        CMY,

        /// <summary>
        /// CMYK
        /// </summary>
        CMYK,

        /// <summary>
        /// Gray
        /// </summary>
        Gray,

        /// <summary>
        /// HCL
        /// </summary>
        HCL,

        /// <summary>
        /// HCLp
        /// </summary>
        HCLp,

        /// <summary>
        /// HSB
        /// </summary>
        HSB,

        /// <summary>
        /// HSI
        /// </summary>
        HSI,

        /// <summary>
        /// HSL
        /// </summary>
        HSL,

        /// <summary>
        /// HSV
        /// </summary>
        HSV,

        /// <summary>
        /// HWB
        /// </summary>
        HWB,

        /// <summary>
        /// Lab
        /// </summary>
        Lab,

        /// <summary>
        /// LCH
        /// </summary>
        LCH,

        /// <summary>
        /// LCHab
        /// </summary>
        LCHab,

        /// <summary>
        /// LCHuv
        /// </summary>
        LCHuv,

        /// <summary>
        /// Log
        /// </summary>
        Log,

        /// <summary>
        /// LMS
        /// </summary>
        LMS,

        /// <summary>
        /// Luv
        /// </summary>
        Luv,

        /// <summary>
        /// OHTA
        /// </summary>
        OHTA,

        /// <summary>
        /// Rec601YCbCr
        /// </summary>
        Rec601YCbCr,

        /// <summary>
        /// Rec709YCbCr
        /// </summary>
        Rec709YCbCr,

        /// <summary>
        /// RGB
        /// </summary>
        RGB,

        /// <summary>
        /// scRGB
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element must begin with upper-case letter", Justification = "Special case that starts lowercase.")]
        scRGB,

        /// <summary>
        /// sRGB
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element must begin with upper-case letter", Justification = "Special case that starts lowercase.")]
        sRGB,

        /// <summary>
        /// Transparent
        /// </summary>
        Transparent,

        /// <summary>
        /// XyY
        /// </summary>
        XyY,

        /// <summary>
        /// XYZ
        /// </summary>
        XYZ,

        /// <summary>
        /// YCbCr
        /// </summary>
        YCbCr,

        /// <summary>
        /// YCC
        /// </summary>
        YCC,

        /// <summary>
        /// YDbDr
        /// </summary>
        YDbDr,

        /// <summary>
        /// YIQ
        /// </summary>
        YIQ,

        /// <summary>
        /// YPbPr
        /// </summary>
        YPbPr,

        /// <summary>
        /// YUV
        /// </summary>
        YUV,

        /// <summary>
        /// LinearGRAY
        /// </summary>
        LinearGRAY,
    }
}