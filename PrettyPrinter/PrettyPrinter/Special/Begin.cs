// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
	public class Begin : Special 
	{
		public Begin() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0;i<n;i++){
				Console.WriteLine(' ');
			}
			Console.WriteLine ("(begin");
			if (t.getCdr ().isPair ()) {
				t.getCdr ().print (n + 2, p);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
			Console.WriteLine (")");
		}
	}
}
