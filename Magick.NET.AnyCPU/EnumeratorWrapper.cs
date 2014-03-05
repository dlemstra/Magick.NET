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
using System.Collections.Generic;
using Fasterflect;

namespace ImageMagick
{
	//==============================================================================================
	internal sealed class EnumeratorWrapper<T> : IEnumerable<T>
	{
		//===========================================================================================
		private object _Items;
		//===========================================================================================
		public EnumeratorWrapper(object items)
		{
			_Items = items;
		}
		//===========================================================================================
		public IEnumerator<T> GetEnumerator()
		{
			IEnumerator enumerator = (IEnumerator)_Items;
			List<T> list = new List<T>();
			while (enumerator.MoveNext())
			{
				T item = (T)typeof(T).CreateInstance(new Type[] { typeof(object) }, enumerator.Current);
				list.Add(item);
			}
			return list.GetEnumerator();
		}
		//===========================================================================================
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
