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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fasterflect
{
	/// <summary>
	/// Container class for event/delegate extensions.
	/// </summary>
    public static class DynamicHandler
    {
        /// <summary>
        /// Invokes a static delegate using supplied parameters.
        /// </summary>
        /// <param name="targetType">The type where the delegate belongs to.</param>
        /// <param name="delegateName">The field name of the delegate.</param>
        /// <param name="parameters">The parameters used to invoke the delegate.</param>
        /// <returns>The return value of the invocation.</returns>
        public static object InvokeDelegate(this Type targetType, string delegateName, params object[] parameters)
        {
            return ((Delegate)targetType.GetFieldValue(delegateName)).DynamicInvoke(parameters);
        }

        /// <summary>
        /// Invokes an instance delegate using supplied parameters.
        /// </summary>
        /// <param name="target">The object where the delegate belongs to.</param>
        /// <param name="delegateName">The field name of the delegate.</param>
        /// <param name="parameters">The parameters used to invoke the delegate.</param>
        /// <returns>The return value of the invocation.</returns>
        public static object InvokeDelegate(this object target, string delegateName, params object[] parameters)
        {
            return ((Delegate)target.GetFieldValue(delegateName)).DynamicInvoke(parameters);
        }

        /// <summary>
        /// Adds a dynamic handler for a static delegate.
        /// </summary>
        /// <param name="targetType">The type where the delegate belongs to.</param>
        /// <param name="fieldName">The field name of the delegate.</param>
        /// <param name="func">The function which will be invoked whenever the delegate is invoked.</param>
        /// <returns>The return value of the invocation.</returns>
        public static Type AddHandler(this Type targetType, string fieldName,
            Func<object[], object> func)
        {
            return InternalAddHandler(targetType, fieldName, func, null, false);
        }

        /// <summary>
        /// Adds a dynamic handler for an instance delegate.
        /// </summary>
        /// <param name="target">The object where the delegate belongs to.</param>
        /// <param name="fieldName">The field name of the delegate.</param>
        /// <param name="func">The function which will be invoked whenever the delegate is invoked.</param>
        /// <returns>The return value of the invocation.</returns>
        public static Type AddHandler(this object target, string fieldName,
            Func<object[], object> func)
        {
            return InternalAddHandler(target.GetType(), fieldName, func, target, false);
        }

        /// <summary>
        /// Assigns a dynamic handler for a static delegate or event.
        /// </summary>
        /// <param name="targetType">The type where the delegate or event belongs to.</param>
        /// <param name="fieldName">The field name of the delegate or event.</param>
        /// <param name="func">The function which will be invoked whenever the delegate or event is fired.</param>
        /// <returns>The return value of the invocation.</returns>
        public static Type AssignHandler(this Type targetType, string fieldName,
            Func<object[], object> func)
        {
            return InternalAddHandler(targetType, fieldName, func, null, true);
        }

        /// <summary>
        /// Assigns a dynamic handler for a static delegate or event.
        /// </summary>
        /// <param name="target">The object where the delegate or event belongs to.</param>
        /// <param name="fieldName">The field name of the delegate or event.</param>
        /// <param name="func">The function which will be invoked whenever the delegate or event is fired.</param>
        /// <returns>The return value of the invocation.</returns>
        public static Type AssignHandler(this object target, string fieldName,
            Func<object[], object> func)
        {
            return InternalAddHandler(target.GetType(), fieldName, func, target, true);
        }

        private static Type InternalAddHandler(Type targetType, string fieldName,
            Func<object[], object> func, object target, bool assignHandler)
        {
            Type delegateType;
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                               (target == null ? BindingFlags.Static : BindingFlags.Instance);
            var eventInfo = targetType.GetEvent(fieldName, bindingFlags);
            if (eventInfo != null && assignHandler)
                throw new ArgumentException("Event can be assigned.  Use AddHandler() overloads instead.");

            if (eventInfo != null)
            {
                delegateType = eventInfo.EventHandlerType;
                var dynamicHandler = BuildDynamicHandler(delegateType, func);
                eventInfo.GetAddMethod(true).Invoke(target, new Object[] { dynamicHandler });
            }
            else
            {
                var fieldInfo = targetType.Field(fieldName,
                                                    target == null
                                                        ? Flags.StaticAnyVisibility
                                                        : Flags.InstanceAnyVisibility);
                delegateType = fieldInfo.FieldType;
                var dynamicHandler = BuildDynamicHandler(delegateType, func);
                var field = assignHandler ? null : target == null
                                ? (Delegate)fieldInfo.Get()
                                : (Delegate)fieldInfo.Get(target);
                field = field == null
                            ? dynamicHandler
                            : Delegate.Combine(field, dynamicHandler);
                (target ?? targetType).SetFieldValue(fieldName, field);
            }
            return delegateType;
        }

        /// <summary>
        /// Dynamically generates code for a method whose can be used to handle a delegate of type 
        /// <paramref name="delegateType"/>.  The generated method will forward the call to the
        /// supplied <paramref name="func"/>.
        /// </summary>
        /// <param name="delegateType">The delegate type whose dynamic handler is to be built.</param>
        /// <param name="func">The function which will be forwarded the call whenever the generated
        /// handler is invoked.</param>
        /// <returns></returns>
        public static Delegate BuildDynamicHandler(this Type delegateType, Func<object[], object> func)
        {
            var invokeMethod = delegateType.GetMethod("Invoke");
            var parameters = invokeMethod.GetParameters().Select(parm =>
                Expression.Parameter(parm.ParameterType, parm.Name)).ToArray();
            var instance = func.Target == null ? null : Expression.Constant(func.Target);
            var convertedParameters = parameters.Select(parm => Expression.Convert(parm, typeof(object))).Cast<Expression>().ToArray();
            var call = Expression.Call(instance, func.Method, Expression.NewArrayInit(typeof(object), convertedParameters));
            var body = invokeMethod.ReturnType == typeof(void)
                ? (Expression)call
                : Expression.Convert(call, invokeMethod.ReturnType);
            var expr = Expression.Lambda(delegateType, body, parameters);
            return expr.Compile();
        }
    }
}
