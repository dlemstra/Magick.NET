//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
#pragma once

#include "MagickFormatInfo.h"

using namespace System::Reflection;
using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to initialize Magick.NET.
	///</summary>
	public ref class MagickNET abstract sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly array<String^>^ _ImageMagickFiles = gcnew array<String^>
		{
			"CORE_RL_bzlib_.dll", "CORE_RL_jbig_.dll", "CORE_RL_jp2_.dll", "CORE_RL_jpeg_.dll",
			"CORE_RL_lcms_.dll", "CORE_RL_libxml_.dll", "CORE_RL_magick_.dll", "CORE_RL_Magick++_.dll",
			"CORE_RL_png_.dll", "CORE_RL_tiff_.dll", "CORE_RL_ttf_.dll", "CORE_RL_wand_.dll",
			"CORE_RL_zlib_.dll", "IM_MOD_RL_aai_.dll", "IM_MOD_RL_art_.dll", "IM_MOD_RL_avs_.dll",
			"IM_MOD_RL_bgr_.dll", "IM_MOD_RL_bmp_.dll", "IM_MOD_RL_braille_.dll", "IM_MOD_RL_cals_.dll",
			"IM_MOD_RL_caption_.dll", "IM_MOD_RL_cin_.dll","IM_MOD_RL_cip_.dll", "IM_MOD_RL_clip_.dll",
			"IM_MOD_RL_clipboard_.dll", "IM_MOD_RL_cmyk_.dll", "IM_MOD_RL_cut_.dll", "IM_MOD_RL_dcm_.dll",
			"IM_MOD_RL_dds_.dll", "IM_MOD_RL_debug_.dll", "IM_MOD_RL_dib_.dll", "IM_MOD_RL_djvu_.dll",
			"IM_MOD_RL_dng_.dll", "IM_MOD_RL_dot_.dll", "IM_MOD_RL_dps_.dll", "IM_MOD_RL_dpx_.dll",
			"IM_MOD_RL_emf_.dll", "IM_MOD_RL_ept_.dll", "IM_MOD_RL_exr_.dll", "IM_MOD_RL_fax_.dll",
			"IM_MOD_RL_fd_.dll", "IM_MOD_RL_fits_.dll", "IM_MOD_RL_fpx_.dll", "IM_MOD_RL_gif_.dll",
			"IM_MOD_RL_gradient_.dll", "IM_MOD_RL_gray_.dll", "IM_MOD_RL_hald_.dll", "IM_MOD_RL_hdr_.dll",
			"IM_MOD_RL_histogram_.dll", "IM_MOD_RL_hrz_.dll", "IM_MOD_RL_html_.dll", "IM_MOD_RL_icon_.dll",
			"IM_MOD_RL_info_.dll", "IM_MOD_RL_inline_.dll", "IM_MOD_RL_ipl_.dll", "IM_MOD_RL_jbig_.dll",
			"IM_MOD_RL_jnx_.dll", "IM_MOD_RL_jp2_.dll", "IM_MOD_RL_jpeg_.dll", "IM_MOD_RL_label_.dll",
			"IM_MOD_RL_mac_.dll", "IM_MOD_RL_magick_.dll", "IM_MOD_RL_map_.dll", "IM_MOD_RL_mask_.dll",
			"IM_MOD_RL_mat_.dll", "IM_MOD_RL_matte_.dll", "IM_MOD_RL_meta_.dll", "IM_MOD_RL_miff_.dll",
			"IM_MOD_RL_mono_.dll", "IM_MOD_RL_mpc_.dll", "IM_MOD_RL_mpeg_.dll", "IM_MOD_RL_mpr_.dll",
			"IM_MOD_RL_msl_.dll", "IM_MOD_RL_mtv_.dll", "IM_MOD_RL_mvg_.dll", "IM_MOD_RL_null_.dll",
			"IM_MOD_RL_otb_.dll", "IM_MOD_RL_palm_.dll", "IM_MOD_RL_pango_.dll", "IM_MOD_RL_pattern_.dll",
			"IM_MOD_RL_pcd_.dll", "IM_MOD_RL_pcl_.dll", "IM_MOD_RL_pcx_.dll", "IM_MOD_RL_pdb_.dll",
			"IM_MOD_RL_pdf_.dll", "IM_MOD_RL_pes_.dll", "IM_MOD_RL_pict_.dll", "IM_MOD_RL_pix_.dll",
			"IM_MOD_RL_plasma_.dll", "IM_MOD_RL_png_.dll", "IM_MOD_RL_pnm_.dll", "IM_MOD_RL_preview_.dll",
			"IM_MOD_RL_ps_.dll", "IM_MOD_RL_ps2_.dll", "IM_MOD_RL_ps3_.dll", "IM_MOD_RL_psd_.dll",
			"IM_MOD_RL_pwp_.dll", "IM_MOD_RL_raw_.dll", "IM_MOD_RL_rgb_.dll", "IM_MOD_RL_rla_.dll",
			"IM_MOD_RL_rle_.dll", "IM_MOD_RL_scr_.dll", "IM_MOD_RL_sct_.dll", "IM_MOD_RL_sfw_.dll",
			"IM_MOD_RL_sgi_.dll", "IM_MOD_RL_stegano_.dll", "IM_MOD_RL_sun_.dll", "IM_MOD_RL_svg_.dll",
			"IM_MOD_RL_tga_.dll", "IM_MOD_RL_thumbnail_.dll", "IM_MOD_RL_tiff_.dll", "IM_MOD_RL_tile_.dll",
			"IM_MOD_RL_tim_.dll", "IM_MOD_RL_ttf_.dll", "IM_MOD_RL_txt_.dll", "IM_MOD_RL_uil_.dll",
			"IM_MOD_RL_url_.dll", "IM_MOD_RL_uyvy_.dll", "IM_MOD_RL_vicar_.dll", "IM_MOD_RL_vid_.dll",
			"IM_MOD_RL_viff_.dll", "IM_MOD_RL_wbmp_.dll", "IM_MOD_RL_webp_.dll", "IM_MOD_RL_wmf_.dll",
			"IM_MOD_RL_wpg_.dll", "IM_MOD_RL_x_.dll", "IM_MOD_RL_xbm_.dll", "IM_MOD_RL_xc_.dll",
			"IM_MOD_RL_xcf_.dll", "IM_MOD_RL_xpm_.dll", "IM_MOD_RL_xps_.dll", "IM_MOD_RL_xtrn_.dll",
			"IM_MOD_RL_xwd_.dll", "IM_MOD_RL_ycbcr_.dll", "IM_MOD_RL_yuv_.dll",
			"coder.xml", "colors.xml", "configure.xml", "delegates.xml",
			"english.xml", "locale.xml", "log.xml", "magic.xml",
			"policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
		};
		//===========================================================================================
		static void CheckImageMagickFiles(String^ path);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Returns the format information of the specified format.
		///</summary>
		static MagickFormatInfo^ GetFormatInformation(MagickFormat format);
		///==========================================================================================
		///<summary>
		/// Adds the sub directory ImageMagick of the current execution path to the environment path.
		/// You should place the supplied ImageMagick dlls in that directory.
		///</summary>
		static void Initialize();
		///==========================================================================================
		///<summary>
		/// Adds the specified path to the environment path. You should place the supplied ImageMagick
		/// dlls in that directory.
		///</summary>
		static void Initialize(String^ path);
		///==========================================================================================
		///<summary>
		/// Pixel cache threshold in megabytes. Once this memory threshold is exceeded, all subsequent
		/// pixels cache operations are to/from disk. This setting is shared by all MagickImage objects.
		///</summary>
		static void SetCacheThreshold(int threshold);
		///==========================================================================================
		///<summary>
		/// Returns information about the supported formats.
		///</summary>
		static property IEnumerable<MagickFormatInfo^>^ SupportedFormats
		{
			IEnumerable<MagickFormatInfo^>^ get()
			{
				return MagickFormatInfo::All;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the version of Magick.NET.
		///</summary>
		static property String^ Version
		{
			String^ get()
			{
				Object^ title = (MagickNET::typeid)->Assembly->GetCustomAttributes(AssemblyTitleAttribute::typeid, false)[0];
				Object^ version = (MagickNET::typeid)->Assembly->GetCustomAttributes(AssemblyFileVersionAttribute::typeid, false)[0];
				return ((AssemblyTitleAttribute^)title)->Title + " " + ((AssemblyFileVersionAttribute^)version)->Version;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}