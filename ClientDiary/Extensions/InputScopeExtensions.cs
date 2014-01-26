using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientDiary.Extensions
{
	public static class InputScopeExtensions
	{
		public static void SetScopeName(this InputScope scope, InputScopeNameValue value)
		{
			InputScopeName name = new InputScopeName();
			name.NameValue = value;
			scope.Names.Add(name);
		}
	}
}
