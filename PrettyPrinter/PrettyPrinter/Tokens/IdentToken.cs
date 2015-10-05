// IdentToken -- Token object for representing identifiers.
using System.Security.Principal;
using System;

namespace Tokens
{
	public class IdentToken : Token
	{
		private string name;

		public IdentToken(string s)  : base(TokenType.IDENT)
		{
			name = s;
		}

		public override string getName ()
		{
			return name;
		}
	}
}
