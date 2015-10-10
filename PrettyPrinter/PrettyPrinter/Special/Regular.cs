// Regular -- Parse tree node strategy for printing regular lines

using System;

namespace Tree
{
	public class Regular : Special
	{

		public Regular() {}

		public override void print(Node t, int n, bool p)
		{
			for (int i = 0; i < n; i++) {
				Console.WriteLine (' ');
			}
			if (!p) {
				Console.WriteLine ('(');
			}
			t.getCar ().print (0);
			Console.WriteLine (' ');
			t.getCdr ().print (0, true);
		}
	}
}
