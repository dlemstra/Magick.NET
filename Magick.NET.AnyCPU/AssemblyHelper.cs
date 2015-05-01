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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Reflection;

[assembly: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ImageMagick.AssemblyHelper.#Initialize()")]

namespace ImageMagick
{
	//==============================================================================================
	internal static class AssemblyHelper
	{
		//===========================================================================================
		private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.StartsWith("Magick.NET.Wrapper", StringComparison.Ordinal))
				return LoadAssembly();

			return null;
		}
		//===========================================================================================
		private static string CreateCacheDirectory()
		{
			AssemblyFileVersionAttribute version = (AssemblyFileVersionAttribute)typeof(AssemblyHelper).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];

			string path = Path.Combine(MagickAnyCPU.CacheDirectory, "Magick.NET." + version.Version);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}
		//===========================================================================================
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private static Assembly LoadAssembly()
		{
#if Q8
			string name = "Magick.NET-Q8.Wrapper-" + (Environment.Is64BitProcess ? "x64" : "x86");
#elif Q16
			string name = "Magick.NET-Q16.Wrapper-" + (Environment.Is64BitProcess ? "x64" : "x86");
#elif Q16HDRI
			string name = "Magick.NET-Q16.Wrapper-HDRI-" + (Environment.Is64BitProcess ? "x64" : "x86");
#else
#error Not implemented!
#endif
			string cacheDirectory = CreateCacheDirectory();
			string tempFile = Path.Combine(cacheDirectory, name + ".dll");

			WriteAssembly(tempFile);
			WriteXmlResources(cacheDirectory);

			Assembly assembly = LoadAssembly(tempFile);

			Type magickNET = assembly.GetType("ImageMagick.Wrapper.MagickNET");
			MethodInfo methodInfo = magickNET.GetMethod("SetEnv");
			methodInfo.Invoke(null, new object[] { "MAGICK_CONFIGURE_PATH", cacheDirectory });

			return assembly;
		}
		//===========================================================================================
		[SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods")]
		private static Assembly LoadAssembly(string tempFile)
		{
			return Assembly.LoadFile(tempFile);
		}
		//===========================================================================================
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		private static void WriteAssembly(string tempFile)
		{
			if (File.Exists(tempFile))
				return;

			string resourceName = "ImageMagick.Resources.Library.Magick.NET.Wrapper_" + (Environment.Is64BitProcess ? "x64" : "x86") + ".gz";

			using (Stream stream = typeof(AssemblyHelper).Assembly.GetManifestResourceStream(resourceName))
			{
				using (GZipStream compressedStream = new GZipStream(stream, CompressionMode.Decompress, false))
				{
					using (FileStream fileStream = File.Open(tempFile, FileMode.CreateNew))
					{
						compressedStream.CopyTo(fileStream);
					}
				}
			}
		}
		//===========================================================================================
		private static void WriteXmlResources(string cacheDirectory)
		{
			string[] xmlFiles = 
			{
				"coder.xml", "colors.xml", "configure.xml", "delegates.xml", "english.xml", "locale.xml",
				"log.xml", "magic.xml", "policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
			};

			foreach (string xmlFile in xmlFiles)
			{
				string outputFile = Path.Combine(cacheDirectory, xmlFile);
				if (File.Exists(outputFile))
					continue;

				string resourceName = "ImageMagick.Resources.Xml." + xmlFile;
				using (Stream stream = typeof(MagickImage).Assembly.GetManifestResourceStream(resourceName))
				{
					using (FileStream fileStream = File.Open(outputFile, FileMode.CreateNew))
					{
						stream.CopyTo(fileStream);
					}
				}
			}
		}
		//===========================================================================================
		public static void Initialize()
		{
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
		}
		//===========================================================================================
	}
	//==============================================================================================
}
