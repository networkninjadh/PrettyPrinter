using System;

namespace Tree
{
	class If : Special
	{
		public If() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine(' ');
			}
			Console.WriteLine("(if ");

			Node predicate = t.getCdr().getCar();
			if (predicate.isPair()) {
				predicate.print(0, p);
			} else {
				throw new ArgumentOutOfRangeException ("SYNTAX ERROR");
			}

			Console.WriteLine();

			Node thenClause = t.getCdr().getCdr().getCar();
			if (!thenClause.isNull()) {
				thenClause.print(n + 2, p);
			} else {
				throw new ArgumentOutOfRangeException ("SYNTAX ERROR");
			}

			Console.WriteLine();

			Node elseClause = t.getCdr().getCdr().getCdr().getCar();
			if (!elseClause.isNull()) {
				elseClause.print(n + 2, p);
			} else {
				throw new ArgumentOutOfRangeException ("SYNTAX ERROR");
			}

			for (int i = 0; i < n; i++) {
				Console.WriteLine(' ');
			}
			Console.WriteLine(')');
		}
		}
	}

