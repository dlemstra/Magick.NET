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
using System.Reflection;

namespace Fasterflect.Probing
{
	internal class ConstructorMap : MethodMap
	{
		private ConstructorInvoker invoker;

		public ConstructorMap( ConstructorInfo constructor, string[] paramNames, Type[] parameterTypes, 
			                   object[] sampleParamValues, bool mustUseAllParameters )
            : base(constructor, paramNames, parameterTypes, sampleParamValues, mustUseAllParameters)
		{
		}

		#region UpdateMembers Private Helper Method
		private void UpdateMembers(object target, object[] row)
		{
			for( int i = 0; i < row.Length; i++ )
			{
				if( parameterReflectionMask[ i ] )
				{
					MemberInfo member = members[ i ];
					if( member != null )
					{
						object value = parameterTypeConvertMask[ i ] ? TypeConverter.Get( member.Type(), row[ i ] ) : row[ i ];
						member.Set( target, value );
					}
				}
			}
		}
		#endregion

		public override object Invoke( object[] row )
		{
			object[] methodParameters = isPerfectMatch ? row : PrepareParameters(row);
			object result = invoker.Invoke(methodParameters);
			if (!isPerfectMatch && AnySet(parameterReflectionMask))
				UpdateMembers(result, row);
			return result;
		}

		internal override void InitializeInvoker()
		{
			invoker = type.DelegateForCreateInstance( GetParamTypes() );
		}
	}
}