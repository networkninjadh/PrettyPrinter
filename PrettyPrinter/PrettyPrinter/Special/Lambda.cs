using System;
using System.Linq.Expressions;

namespace Tree
{
	class Lambda : Special
	{

		public Lambda() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
			Console.WriteLine ("(lambda ");

			Node secondNode = t.getCdr ().getCar ();
			if (secondNode.isPair ()) {
				secondNode.print (0, false);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
			Console.WriteLine ();
			Node thirdNode = t.getCdr ().getCdr ().getCar ();
			if (thirdNode.isPair ()) {
				thirdNode.print (n + 2, false);
			} else {
				throw new ArgumentOutOfRangeException ("Syntax Error");
			}
			Console.WriteLine ();
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
			Console.WriteLine (')');
		}
	}
}
