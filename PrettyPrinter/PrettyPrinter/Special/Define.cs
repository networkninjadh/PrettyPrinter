// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
	class Define : Special
	{
		public Define() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
			Console.WriteLine ("(define ");
			Node name = t.getCdr ().getCar ();
			if (name.isSymbol ()) {
				name.print (0, false);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
			Console.WriteLine (" ");
			Node value = t.getCdr ().getCdr().getCar ();
			if (!value.isNull ()) {
				value.print (0, true);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
			Console.WriteLine (")");
		}
	}
}
