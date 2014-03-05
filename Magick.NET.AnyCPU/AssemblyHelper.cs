//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Fasterflect;

namespace ImageMagick
{
	//==============================================================================================
	internal sealed class AssemblyHelper
	{
		//===========================================================================================
		private static readonly Assembly _Assembly = LoadAssembly();
		//===========================================================================================
		private static string GetTempFileName(string name)
		{
			AssemblyFileVersionAttribute version = (AssemblyFileVersionAttribute)typeof(AssemblyHelper).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];

			string path = Path.Combine(Path.GetTempPath(), "Magick.NET." + version.Version);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return Path.Combine(path, name + ".dll");
		}
		//===========================================================================================
		private static Assembly LoadAssembly()
		{
			string name = "Magick.NET-" + (Environment.Is64BitProcess ? "x64" : "x86");
#if Q8
			string resourceName = "ImageMagick.Resources.Q8.Magick.NET-" + (Environment.Is64BitProcess ? "x64" : "x86") + ".gzip";
#elif Q16
			string resourceName = "ImageMagick.Resources.Q16." + name + ".gzip";
#endif

			string tempFile = GetTempFileName(name);
			if (!File.Exists(tempFile))
				WriteAssembly(resourceName, tempFile);

			return Assembly.LoadFile(tempFile);
		}
		//===========================================================================================
		private static void WriteAssembly(string resourceName, string outputFile)
		{
			using (Stream stream = typeof(MagickImage).Assembly.GetManifestResourceStream(resourceName))
			{
				using (GZipStream compressedStream = new GZipStream(stream, CompressionMode.Decompress, false))
				{
					using (FileStream fileStream = File.Open(outputFile, FileMode.CreateNew))
					{
						compressedStream.CopyTo(fileStream);
					}
				}
			}
		}
		//===========================================================================================
		public static Type GetType(string name)
		{
			return _Assembly.GetType(name);
		}
		//===========================================================================================
		public static object CreateInstance(Type type)
		{
			try
			{
				return type.CreateInstance();
			}
			catch (Exception exception)
			{
				throw ExceptionHelper.Create(exception);
			}
		}
		//===========================================================================================
		public static object CreateInstance(Type type, Type[] parameterTypes, params object[] arguments)
		{
			try
			{
				return type.CreateInstance(parameterTypes, arguments);
			}
			catch (Exception exception)
			{
				throw ExceptionHelper.Create(exception);
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
