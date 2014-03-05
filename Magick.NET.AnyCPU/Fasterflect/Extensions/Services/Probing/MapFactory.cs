#region License

// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect.Caching;

namespace Fasterflect.Probing
{
	/// <summary>
	/// Helper class for producing invocation maps that describe how to create an instance of an object
	/// given a set of parameters. Maps are cached to speed up subsequent requests.
	/// </summary>
	internal static class MapFactory
	{
		/// <summary>
		/// This field contains a dictionary mapping from a particular constructor to all known parameter sets,
		/// each with an associated MethodMap responsible for creating instances of the type using the given
		/// constructor and parameter set.
		/// </summary>
		private static readonly Cache<int, MethodMap> mapCache = new Cache<int, MethodMap>();

		#region Map Construction
		public static MethodMap PrepareInvoke( Type type, string[] paramNames, Type[] parameterTypes,
		                                       object[] sampleParamValues )
		{
            SourceInfo sourceInfo = new SourceInfo(type, paramNames, parameterTypes);
			int hash = sourceInfo.GetHashCode();
			MethodMap map = mapCache.Get( hash );
			if( map == null )
			{
                map = DetermineBestConstructorMatch(type, paramNames, parameterTypes, sampleParamValues);
				mapCache.Insert( hash, map );
			}
			return map;
		}
		#endregion

		#region Map Construction Helpers
		internal static MethodMap DetermineBestConstructorMatch( Type type, string[] paramNames, Type[] parameterTypes,
		                                                        object[] sampleParamValues )
		{
			MethodMap map = DetermineBestMatch( type.GetConstructors(), false, paramNames, parameterTypes, sampleParamValues );
			if( map != null )
			{
				return map;
			}
			var sb = new StringBuilder();
			sb.AppendFormat( "No constructor found for type {0} using parameters:{1}", type.Name, Environment.NewLine );
            sb.AppendFormat("{0}{1}", string.Join(", ", Enumerable.Range(0, paramNames.Length).Select(i => string.Format("{0}:{1}", paramNames[i], parameterTypes[i])).ToArray()), Environment.NewLine);
			throw new MissingMethodException( sb.ToString() );
		}
		internal static MethodMap DetermineBestMethodMatch( IEnumerable<MethodBase> methods, bool mustUseAllParameters, string[] paramNames, 
															Type[] parameterTypes, object[] sampleParamValues )
		{
			MethodMap map = DetermineBestMatch( methods, mustUseAllParameters, paramNames, parameterTypes, sampleParamValues );
			if( map != null )
			{
				return map;
			}
			var sb = new StringBuilder();
			sb.AppendFormat( "No method found ({0} candidates examined) matching the parameters:{1}", methods.ToList().Count, Environment.NewLine );
			//sb.AppendFormat( "{0}{1}", Format( parameters, "=", ", " ), Environment.NewLine );
            sb.AppendFormat("{0}{1}", string.Join(", ", Enumerable.Range(0, paramNames.Length).Select(i => string.Format("{0}:{1}", paramNames[i], parameterTypes[i])).ToArray()), Environment.NewLine);
			throw new MissingMethodException( sb.ToString() );
		}

		private static MethodMap DetermineBestMatch( IEnumerable<MethodBase> methods, bool mustUseAllParameters,
													 string[] paramNames, Type[] parameterTypes, object[] sampleParamValues )
		{
			MethodMap bestMap = null;
			foreach( MethodBase method in methods )
			{
                MethodMap map = CreateMap( method, paramNames, parameterTypes, sampleParamValues, mustUseAllParameters );
				if( map != null && map.IsValid )
				{
					bool isBetter = bestMap == null;
					isBetter |= map.IsPerfectMatch;
					isBetter |= bestMap != null &&
					            (map.Cost < bestMap.Cost ||
					             (map.Cost == bestMap.Cost && map.RequiredParameterCount > bestMap.RequiredParameterCount));
					isBetter &= map.IsValid;
					if( isBetter )
					{
						bestMap = map;
					}
				}
			}
			if( bestMap != null )
			{
				bestMap.InitializeInvoker();
				return bestMap;
			}
			return null;
		}

		private static MethodMap CreateMap( MethodBase method, string[] paramNames, Type[] parameterTypes,
		                                    object[] sampleParamValues, bool mustUseAllParameters )
		{
			if( method.IsConstructor )
			{
                return new ConstructorMap( method as ConstructorInfo, paramNames, parameterTypes, sampleParamValues,
				                           mustUseAllParameters );
			}
            return new MethodMap( method, paramNames, parameterTypes, sampleParamValues, mustUseAllParameters );
		}
		#endregion
	}
}