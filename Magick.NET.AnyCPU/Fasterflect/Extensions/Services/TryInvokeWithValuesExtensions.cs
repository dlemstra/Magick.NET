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
using Fasterflect.Probing;

namespace Fasterflect
{
	/// <summary>
	/// A converter used to convert <paramref name="value"/> to <paramref name="parameterType"/>
	/// if it makes sense in the application.  Why implementation of converter can
	/// set new value for <paramref name="value"/>, it should not attempt to 
	/// modify child objects of <paramref name="value"/> because those changes will
	/// be permanent although if the method in question will not be selected as a match.
	/// </summary>
	/// <param name="parameterType">The type to be converted to.</param>
	/// <param name="target">The type or object whose method or constructor is being called.</param>
	/// <param name="value">The value to be converted.</param>
	/// <returns></returns>
	public delegate bool ParameterConverter(Type parameterType, object target, ref object value);

	/// <summary>
	/// Container class for TryCreateInstanceWithValues and TryCallMethodWithValues extensions.
	/// </summary>
	public static class TryInvokeWithValuesExtensions
	{
		/// <summary>
		/// Obtains the list of contructors for <paramref name="type"/> using the supplied parameter values
		/// and invokes the best match. This overload requires that the supplied <paramref name="parameterValues"/> 
		/// are all used in the order in which they are supplied. Parameter values can be null.
		/// 
		/// This method is very liberal and attempts to convert values that are not otherwise
		/// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
		/// You should carefully test any usage to ensure correct program behavior.
		/// </summary>
		/// <param name="type">The type of which an instance should be created.</param>
		/// <param name="parameterValues">The values to use when invoking the constructor.</param>
		/// <returns>The result of the invocation.</returns>
		public static object TryCreateInstanceWithValues(this Type type, params object[] parameterValues)
		{
			return TryCreateInstanceWithValues(type, null, Flags.InstanceAnyVisibility, parameterValues);
		}

		/// <summary>
		/// Obtains the list of contructors for <paramref name="type"/> using the supplied parameter values
		/// and invokes the best match. This overload requires that the supplied <paramref name="parameterValues"/> 
		/// are all used in the order in which they are supplied. Parameter values can be null.
		/// 
		/// This method is very liberal and attempts to convert values that are not otherwise
		/// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
		/// You should carefully test any usage to ensure correct program behavior.
		/// 
		/// If the default conversion rule doesn't do what you want, you can supply a custom converter.
		/// If it is null, default conversion rule is used.
		/// </summary>
		/// <param name="type">The type of which an instance should be created.</param>
		/// <param name="converter">The converter delegate used to perform user-defined conversion.</param>
		/// <param name="flags">Binding flags for look up constructors.</param>
		/// <param name="parameterValues">The values to use when invoking the constructor.</param>
		/// <returns>The result of the invocation.</returns>
		public static object TryCreateInstanceWithValues(this Type type, ParameterConverter converter, BindingFlags flags, params object[] parameterValues)
		{
			var ctors = type.Constructors();
			try
			{
				return TryCall(converter, ctors.Cast<MethodBase>(), type, parameterValues);
			}
			catch (MissingMemberException)
			{
				var values = parameterValues ?? new object[0];
				throw new MissingMemberException(string.Format("Unable to locate a matching constructor on type {0} for parameters: {1}",
																 type.Name, string.Join(", ", values.Select(v => v == null ? "null" : v.ToString()).ToArray())));
			}
		}

		/// <summary>
		/// Obtains the list of methods for <paramref name="obj"/> using the supplied parameter values
		/// and invokes the best match. This overload requires that the supplied <paramref name="parameterValues"/> 
		/// are all used in the order in which they are supplied. Parameter values can be null.
		/// 
		/// This method is very liberal and attempts to convert values that are not otherwise
		/// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
		/// You should carefully test any usage to ensure correct program behavior.
		/// </summary>
		/// <param name="obj">The object whose method is to be invoked.</param>
		/// <param name="methodName">The name of the method to be invoked.</param>
		/// <param name="parameterValues">The values to use when invoking the method.</param>
		/// <returns>The result of the invocation.</returns>
		public static object TryCallMethodWithValues(this object obj, string methodName, params object[] parameterValues)
		{
			return TryCallMethodWithValues(obj, null, methodName, 
				obj is Type ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility, parameterValues);
		}

		/// <summary>
		/// Obtains the list of methods for <paramref name="obj"/> using the supplied parameter values
		/// and invokes the best match. This overload requires that the supplied <paramref name="parameterValues"/> 
		/// are all used in the order in which they are supplied. Parameter values can be null.
		/// 
		/// This method is very liberal and attempts to convert values that are not otherwise
		/// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
		/// You should carefully test any usage to ensure correct program behavior.
		/// 
		/// If the default conversion rule doesn't do what you want, you can supply a custom converter.
		/// If it is null, default conversion rule is used.
		/// </summary>
		/// <param name="obj">The object whose method is to be invoked.</param>
		/// <param name="converter">The converter delegate used to perform user-defined conversion.</param>
		/// <param name="methodName">The name of the method to be invoked.</param>
		/// <param name="flags">Binding flags for look up methods.</param>
		/// <param name="parameterValues">The values to use when invoking the method.</param>
		/// <returns>The result of the invocation.</returns>
		public static object TryCallMethodWithValues(this object obj, ParameterConverter converter, string methodName, BindingFlags flags, params object[] parameterValues)
		{
			return TryCallMethodWithValues(obj, converter, methodName,Type.EmptyTypes, flags, parameterValues);
		}


		/// <summary>
		/// Obtains the list of methods for <paramref name="obj"/> using the supplied parameter values
		/// and invokes the best match. This overload requires that the supplied <paramref name="parameterValues"/> 
		/// are all used in the order in which they are supplied. Parameter values can be null.
		/// 
		/// This method is very liberal and attempts to convert values that are not otherwise
		/// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
		/// You should carefully test any usage to ensure correct program behavior.
		/// 
		/// If the default conversion rule doesn't do what you want, you can supply a custom converter.
		/// If it is null, default conversion rule is used.
		/// </summary>
		/// <param name="obj">The object whose method is to be invoked.</param>
		/// <param name="converter">The converter delegate used to perform user-defined conversion.</param>
		/// <param name="methodName">The name of the method to be invoked.</param>
		/// <param name="genericTypes">The type parameter types of the method if it's a generic method.</param>
		/// <param name="flags">Binding flags for look up methods.</param>
		/// <param name="parameterValues">The values to use when invoking the method.</param>
		/// <returns>The result of the invocation.</returns>
		public static object TryCallMethodWithValues(this object obj, ParameterConverter converter, string methodName,
			Type[] genericTypes, BindingFlags flags, params object[] parameterValues)
		{
			var type = obj is Type ? (Type)obj : obj.GetType();
			var methods = type.Methods(genericTypes, null, flags, methodName)
							  .Select(m => m.IsGenericMethodDefinition ? m.MakeGeneric(genericTypes) : m);
			try
			{
				return TryCall(converter, methods.Cast<MethodBase>(), obj, parameterValues);
			}
			catch (MissingMemberException)
			{
				var values = parameterValues ?? new object[0];
				throw new MissingMethodException(string.Format("Unable to locate a matching method {0} on type {1} for parameters: {2}",
																 methodName, type.Name,
																 string.Join(", ", values.Select(v => v == null ? "null" : v.ToString()).ToArray())));
			}
		}

		/// <summary>
		/// Implementation details:
		/// 
		/// Matching process is done on a shallow copy of parametersValues so that 
		/// the converter could "modify" elements at will.  
		/// 
		/// There will be a problem if the converter modifies a child array and the 
		/// method ends up not being matched (because of another parameter).  
		/// 
		/// The standard Fasterflect converter doesn't modify child array so it's safe.
		/// This is only problematic when a custom converter is provided.
		///   
		/// TODO How to fix it? a deep clone?
		/// </summary>
		public static object TryCall(ParameterConverter converter, IEnumerable<MethodBase> methodBases, 
			object obj, object[] parameterValues)
		{
			converter = converter ?? new ParameterConverter(StandardConvert);
			if (parameterValues == null)
			{
				parameterValues = new object[0];
			}
			foreach (var mb in GetCandidates(parameterValues, methodBases))
			{
				var convertedArgs = new List<object>();
				var parameters = mb.GetParameters();
				bool isMatch = true;
				for (int paramIndex = 0; paramIndex < parameters.Length; paramIndex++)
				{
					var parameter = parameters[paramIndex];
					if (paramIndex == parameters.Length - 1 && IsParams(parameter))
					{
						object paramArg;
						if (parameters.Length - 1 == parameterValues.Length)
						{
							paramArg = parameter.ParameterType.CreateInstance(0);
						}
						else
						{
							paramArg = parameter.ParameterType.CreateInstance(parameterValues.Length - parameters.Length + 1);
							var elementType = parameter.ParameterType.GetElementType();
							for (int argIndex = paramIndex; argIndex < parameterValues.Length; argIndex++)
							{
								var value = parameterValues[argIndex];
								if (!converter(elementType, obj, ref value))
								{
									isMatch = false;
									goto end_of_loop;
								}
								((Array)paramArg).SetValue( value, argIndex - paramIndex );
							}
						}
						convertedArgs.Add(paramArg);
					}
					else
					{
						var value = parameterValues[paramIndex];
						if (!converter(parameter.ParameterType, obj, ref value))
						{
							isMatch = false;
							goto end_of_loop;
						}
						convertedArgs.Add(value);
					}
				}

			end_of_loop:
				if (isMatch)
				{
					parameterValues = convertedArgs.Count == 0 ? null : convertedArgs.ToArray();
					return mb is ConstructorInfo
							   ? ((ConstructorInfo) mb).Invoke(parameterValues)
							   : mb.Invoke(obj is Type ? null : obj, parameterValues);
				}
			} // foreach loop
			throw new MissingMemberException();
		}

		private static IEnumerable<MethodBase> GetCandidates(object[] parameterValues, IEnumerable<MethodBase> methodBases)
		{
			return (from methodBase in methodBases
					let parameters = methodBase.GetParameters()
					where parameters.Length == parameterValues.Length ||
						  (parameters.Length > 0 && 
						   IsParams(parameters[parameters.Length - 1]) &&
						   parameterValues.Length >= (parameters.Length - 1))
					orderby parameters.Count()
					select methodBase).ToList();
		}

		private static bool StandardConvert(Type targetType, object owner, ref object value)
		{
			if( value == null )
				return !typeof(ValueType).IsAssignableFrom( targetType );
			try
			{
				return (value = TypeConverter.Get( targetType, value )) != null;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static bool IsParams(ParameterInfo param)
		{
			return param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
		}
	}
}
