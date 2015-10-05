// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
	public class Ident : Node
	{
		private string name;

		public Ident(string n)
		{
			name = n;
		}
		public override bool isSymbol()
		{
			return true;
		}
		public override void print(int n)
		{
			// There has to be a more efficeint way to print n spaces 
			for (int i = 0; i < n; i++)
				Console.Write (" ");
			Console.Write (" ");
			Console.WriteLine (name);
		}
	}
}
