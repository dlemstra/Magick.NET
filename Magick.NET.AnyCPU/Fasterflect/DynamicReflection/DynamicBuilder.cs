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

#if DOT_NET_4
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Fasterflect
{
	internal sealed class DynamicBuilder : DynamicObject
	{
		private readonly Dictionary <string, object> members = new Dictionary <string, object>();  
		
		#region DynamicObject Overrides
		/// <summary>
		/// Assigns the given value to the specified member, overwriting any previous definition if one existed.
		/// </summary>     
		public override bool TrySetMember( SetMemberBinder binder, object value )
		{
	        members[ binder.Name ] = value;
		    return true;
		}

		/// <summary>
		/// Gets the value of the specified member.
		/// </summary>      
		public override bool TryGetMember( GetMemberBinder binder, out object result )
		{
		    if( members.ContainsKey( binder.Name ) )
		    {
		        result = members[ binder.Name ];
		        return true;
		    }
		    return base.TryGetMember( binder, out result );
		}

		/// <summary>
		/// Invokes the specified member (if it is a delegate).
		/// </summary>     
		public override bool TryInvokeMember( InvokeMemberBinder binder, object[] args, out object result )
		{
			object member;
			if( members.TryGetValue( binder.Name, out member ) )
		    {
		    	var method = member as Delegate;
				if( method != null )
				{
					result = method.DynamicInvoke( args );
			        return true;
				}
		    }
		    return base.TryInvokeMember( binder, args, out result );
		}

		/// <summary>
		/// Gets a list of all dynamically defined members.
		/// </summary>
		public override IEnumerable<string> GetDynamicMemberNames()
		{
		    return members.Keys;
		}
		#endregion
	}
}
#endif
