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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ImageMagick.Drawables.Paths;

namespace ImageMagick.Drawables
{
	///=============================================================================================
	///<summary>
	/// Base class for classes that need Coordinates.
	///</summary>
	public abstract class DrawableCoordinates<TCoordinateType>
	{
		//===========================================================================================
		private List<TCoordinateType> BaseCoordinates;
		//===========================================================================================
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private void CheckCoordinates()
		{
			if (BaseCoordinates.Count == 0)
				throw new ArgumentException("Value cannot be empty", "coordinates");

			foreach (TCoordinateType coordinate in BaseCoordinates)
			{
				if (coordinate == null)
					throw new ArgumentNullException("Value should not contain null values", "coordinates");
			}
		}
		///==========================================================================================
		/// <summary>
		/// Cast IEnumerable with interface, necessary for .NET 2.0 build.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="coordinates"></param>
		/// <returns></returns>
		protected static IEnumerable<IPathArc> CastPathArc<T>(IEnumerable<T> coordinates)
			where T : IPathArc
		{
#if NET20
			foreach (T coordinate in coordinates)
				yield return (IPathArc)coordinate;
#else
			return (IEnumerable<IPathArc>)coordinates;
#endif
		}
		///==========================================================================================
		/// <summary>
		/// Cast IEnumerable with interface, necessary for .NET 2.0 build.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="coordinates"></param>
		/// <returns></returns>
		protected static IEnumerable<IPathCurveto> CastPathCurveto<T>(IEnumerable<T> coordinates)
			where T : IPathCurveto
		{
#if NET20
			foreach (T coordinate in coordinates)
				yield return (IPathCurveto)coordinate;
#else
			return (IEnumerable<IPathCurveto>)coordinates;
#endif
		}
		///==========================================================================================
		/// <summary>
		/// Cast IEnumerable with interface, necessary for .NET 2.0 build.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="coordinates"></param>
		/// <returns></returns>
		protected static IEnumerable<IPathQuadraticCurveto> CastPathQuadraticCurveto<T>(IEnumerable<T> coordinates)
			where T : IPathQuadraticCurveto
		{
#if NET20
			foreach (T coordinate in coordinates)
				yield return (IPathQuadraticCurveto)coordinate;
#else
			return (IEnumerable<IPathQuadraticCurveto>)coordinates;
#endif
		}
		///==========================================================================================
		/// <summary>
		/// Checks if the coordinate list contains at least the specified minimum count.
		/// </summary>
		/// <param name="minCount"></param>
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		protected void CheckCoordinateCount(int minCount)
		{
			if (BaseCoordinates.Count < minCount)
				throw new ArgumentException("Value should contain at least " + minCount + " coordinates.", "coordinates");
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the DrawableCoordinates class.
		///</summary>
		///<param name="coordinates">The coordinates to use.</param>
		protected DrawableCoordinates(IEnumerable<TCoordinateType> coordinates)
		{
			Throw.IfNull("coordinates", coordinates);

			BaseCoordinates = new List<TCoordinateType>(coordinates);
			CheckCoordinates();
		}
		///==========================================================================================
		/// <summary>
		/// The coordinates.
		/// </summary>
		public IEnumerable<TCoordinateType> Coordinates
		{
			get
			{
				return BaseCoordinates;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}