// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
	class Cond : Special
	{
		public Cond() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
				Console.WriteLine ("(cond");
			Node cond = t.getCdr ();
			if (cond.isPair ()) {
				cond.print (n + 2, true);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
		}
	}
}