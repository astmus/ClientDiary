using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientDiary.Extensions;

namespace ClientDiary
{
	static class Utils
	{
		public static void InitTextBox(ref PhoneTextBox box, string hint, InputScopeNameValue scopeName = InputScopeNameValue.Text)
		{
			InputScope scope = new InputScope();
			scope.SetScopeName(scopeName);
			
			box = new PhoneTextBox()
			{
				Hint = hint,
				InputScope = scope
			};
		}
	}
}
