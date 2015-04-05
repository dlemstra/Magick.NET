//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
	//===============================================================================================
	public sealed partial class MagickScript
	{
		//============================================================================================
		IReadDefines CreateIReadDefines(XmlElement parent)
		{
			return CreateIDefines(parent) as IReadDefines;
		}
		//============================================================================================
		private IDefines CreateIDefines(XmlElement parent)
		{
			if (parent == null)
				return null;
			XmlElement element = (XmlElement)parent.FirstChild;
			if (element == null)
				return null;
			switch(element.Name[0])
			{
				case 'd':
				{
					return CreateDdsWriteDefines(element);
				}
				case 'j':
				{
					switch(element.Name[4])
					{
						case 'R':
						{
							return CreateJpegReadDefines(element);
						}
						case 'W':
						{
							return CreateJpegWriteDefines(element);
						}
					}
					break;
				}
				case 'p':
				{
					return CreatePdfReadDefines(element);
				}
				case 't':
				{
					switch(element.Name[4])
					{
						case 'R':
						{
							return CreateTiffReadDefines(element);
						}
						case 'W':
						{
							return CreateTiffWriteDefines(element);
						}
					}
					break;
				}
			}
			throw new NotImplementedException(element.Name);
		}
		//============================================================================================
		private IDefines CreateDdsWriteDefines(XmlElement element)
		{
			if (element == null)
				return null;
			DdsWriteDefines result = new DdsWriteDefines();
			result.ClusterFit = Variables.GetValue<Nullable<Boolean>>(element, "clusterFit");
			result.Compression = Variables.GetValue<Nullable<ImageMagick.Defines.DdsCompression>>(element, "compression");
			result.Mipmaps = Variables.GetValue<Nullable<Int32>>(element, "mipmaps");
			result.WeightByAlpha = Variables.GetValue<Nullable<Boolean>>(element, "weightByAlpha");
			return result;
		}
		//============================================================================================
		private IDefines CreateJpegReadDefines(XmlElement element)
		{
			if (element == null)
				return null;
			JpegReadDefines result = new JpegReadDefines();
			result.BlockSmoothing = Variables.GetValue<Nullable<Boolean>>(element, "blockSmoothing");
			result.Colors = Variables.GetValue<Nullable<Int32>>(element, "colors");
			result.DctMethod = Variables.GetValue<Nullable<ImageMagick.Defines.DctMethod>>(element, "dctMethod");
			result.FancyUpsampling = Variables.GetValue<Nullable<Boolean>>(element, "fancyUpsampling");
			result.Size = Variables.GetValue<MagickGeometry>(element, "size");
			result.SkipProfiles = Variables.GetValue<Nullable<ImageMagick.Defines.ProfileTypes>>(element, "skipProfiles");
			return result;
		}
		//============================================================================================
		private IDefines CreateJpegWriteDefines(XmlElement element)
		{
			if (element == null)
				return null;
			JpegWriteDefines result = new JpegWriteDefines();
			result.DctMethod = Variables.GetValue<Nullable<ImageMagick.Defines.DctMethod>>(element, "dctMethod");
			result.Extent = Variables.GetValue<Nullable<Int32>>(element, "extent");
			result.OptimizeCoding = Variables.GetValue<Nullable<Boolean>>(element, "optimizeCoding");
			result.Quality = Variables.GetValue<MagickGeometry>(element, "quality");
			result.QuantizationTables = Variables.GetValue<String>(element, "quantizationTables");
			result.SamplingFactors = CreateMagickGeometryCollection(element);
			return result;
		}
		//============================================================================================
		private IDefines CreatePdfReadDefines(XmlElement element)
		{
			if (element == null)
				return null;
			PdfReadDefines result = new PdfReadDefines();
			result.FitPage = Variables.GetValue<MagickGeometry>(element, "fitPage");
			result.UseCropBox = Variables.GetValue<Nullable<Boolean>>(element, "useCropBox");
			result.UseTrimBox = Variables.GetValue<Nullable<Boolean>>(element, "useTrimBox");
			return result;
		}
		//============================================================================================
		private IDefines CreateTiffReadDefines(XmlElement element)
		{
			if (element == null)
				return null;
			TiffReadDefines result = new TiffReadDefines();
			result.IgnoreExifPoperties = Variables.GetValue<Nullable<Boolean>>(element, "ignoreExifPoperties");
			return result;
		}
		//============================================================================================
		private IDefines CreateTiffWriteDefines(XmlElement element)
		{
			if (element == null)
				return null;
			TiffWriteDefines result = new TiffWriteDefines();
			result.Alpha = Variables.GetValue<Nullable<ImageMagick.Defines.TiffAlpha>>(element, "alpha");
			result.Endian = Variables.GetValue<Nullable<Endian>>(element, "endian");
			result.FillOrder = Variables.GetValue<Nullable<Endian>>(element, "fillOrder");
			result.RowsPerStrip = Variables.GetValue<Nullable<Int32>>(element, "rowsPerStrip");
			result.TileGeometry = Variables.GetValue<MagickGeometry>(element, "tileGeometry");
			return result;
		}
		//============================================================================================
	}
	//===============================================================================================
}
