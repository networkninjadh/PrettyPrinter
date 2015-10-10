// Set -- Parse tree node strategy for printing the special form Set

using System;

namespace Tree
{
	class Set : Special
	{
		public Set() {}

		public override void print(Node t, int n, bool p)
		{
			if (!p) 
			{
				if (t.getCar ().isPair ()) {
					t.getCar ().print (0, false);
				} else {
					t.getCar ().print (0, true);
				}
				Console.WriteLine (" ");
				Node value = t.getCdr ();
				if (value is Nil) {
					Console.WriteLine (")");
				} else {
					value.print (0, true);
				}
			}
		}
	}
}
