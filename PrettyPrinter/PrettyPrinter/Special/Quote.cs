﻿// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
	public class Quote : Special
	{
		public Quote() {}

		public override void print(Node t, int n, bool p)
		{
			Console.WriteLine ("'");
			if (t.getCdr () is Nil) {
				Console.WriteLine ("");
			} else {
				t.getCdr ().print (0, false);
			}
		}
	}
}