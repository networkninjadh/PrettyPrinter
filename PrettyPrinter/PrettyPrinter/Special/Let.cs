// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
	class Let : Special
	{
		public Let(){}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine(' ');
			}
			Console.WriteLine("(let ");

			Node assignments = t.getCdr().getCar();
			if (assignments.isPair()) {
				assignments.print(0, false);
			} else {
				throw new ArgumentOutOfRangeException("SYNTAX ERROR");
			}

			Console.WriteLine();

			Node body = t.getCdr().getCdr();
			if (body.isPair()) {
				body.print(n + 2, true);
			} else {
				throw new ArgumentOutOfRangeException("SYNTAX ERROR");
			}

			Console.WriteLine();
			for (int i = 0; i < n; i++) {
				Console.WriteLine(' ');
			}
			Console.WriteLine("/LET");
		}
	}
}
