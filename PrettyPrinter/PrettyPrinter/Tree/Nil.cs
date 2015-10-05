// Nil -- Parse Tree node class for representing the empty list
using System;

namespace Tree
{
	public class Nil : Node
	{
		public Nil(){}

		public override void print(int n)
		{
			print (n, false);
		}

		public override bool isNull ()
		{
			return true;
		}
		public override void print(int n, bool p)
		{
			for (int i = 0; i < n; i++)
				Console.Write (" ");
			if (p)
				Console.WriteLine (")");
			else
				Console.WriteLine ("()");
		}
	}
}
