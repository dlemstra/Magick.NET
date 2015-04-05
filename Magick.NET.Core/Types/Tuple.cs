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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
#if NET20
	///=============================================================================================
	/// <summary>
	/// Tuple for .NET 2.0
	/// </summary>
	public class Tuple<T1, T2>
	{
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the Tuple class.
		/// </summary>
		/// <param name="item1">Item 1</param>
		/// <param name="item2">Item 2</param>
		public Tuple(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}
		///==========================================================================================
		/// <summary>
		/// Item 1
		/// </summary>
		public T1 Item1
		{
			get;
			set;
		}
		///==========================================================================================
		/// <summary>
		/// Item 2
		/// </summary>
		public T2 Item2
		{
			get;
			set;
		}
		//===========================================================================================
	}
	///=============================================================================================
	/// <summary>
	/// Tuple for .NET 2.0
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public class Tuple<T1, T2, T3> : Tuple<T1, T2>
	{
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the Tuple class.
		/// </summary>
		/// <param name="item1">Item 1</param>
		/// <param name="item2">Item 2</param>
		/// <param name="item3">Item 3</param>
		public Tuple(T1 item1, T2 item2, T3 item3)
			: base(item1, item2)
		{
			Item3 = item3;
		}
		///==========================================================================================
		/// <summary>
		/// Item 3
		/// </summary>
		public T3 Item3
		{
			get;
			set;
		}
		//===========================================================================================
	}
#endif
}
